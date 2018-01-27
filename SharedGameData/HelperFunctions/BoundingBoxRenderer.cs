///////////////////////////////////////////////////////////////////////////////////////
//
//  Sample code from:
//  http://www.xnawiki.com/index.php/Rendering_Bounding_Boxes
//
///////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SharedGameData.HelperFunctions
{
    /// <summary>
    /// Provides a set of methods for the rendering BoundingBoxes.
    /// </summary>
    public static class BoundingBoxRenderer
    {
        #region Fields

        static VertexPositionColor[] verts = new VertexPositionColor[8];
        static int[] indices = new int[]
        {
            0, 1,
            1, 2,
            2, 3,
            3, 0,
            0, 4,
            1, 5,
            2, 6,
            3, 7,
            4, 5,
            5, 6,
            6, 7,
            7, 4,
        };

        static BasicEffect effect;
        static VertexBuffer vertex_Buffer;

        #endregion

        /// <summary>
        /// Renders the bounding box for debugging purposes.
        /// </summary>
        /// <param name="box">The box to render.</param>
        /// <param name="graphicsDevice">The graphics device to use when rendering.</param>
        /// <param name="view">The current view matrix.</param>
        /// <param name="projection">The current projection matrix.</param>
        /// <param name="color">The color to use for drawing the lines of the box.</param>
        public static void Render(
            BoundingBox box,
            GraphicsDevice graphicsDevice,
            Matrix view,
            Matrix projection,
            Color color)
        {
            if (effect == null)
            {
                effect = new BasicEffect(graphicsDevice);
                effect.VertexColorEnabled = true;
                effect.LightingEnabled = false;
            }

            Vector3[] corners = box.GetCorners();

            for (int i = 0; i < 8; i++)
            {
                verts[i].Position = corners[i];
                verts[i].Color = color;
            }

            if(vertex_Buffer == null)
                vertex_Buffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionColor), verts.Length, BufferUsage.WriteOnly);
            vertex_Buffer.SetData<VertexPositionColor>(verts);
            graphicsDevice.SetVertexBuffer(vertex_Buffer);

            effect.View = view;
            effect.Projection = projection;

            effect.CurrentTechnique.Passes[0].Apply();

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                graphicsDevice.DrawUserIndexedPrimitives(
                    PrimitiveType.LineList,
                    verts,
                    0,
                    8,
                    indices,
                    0,
                    indices.Length / 2);
            }
        }
    }
}
