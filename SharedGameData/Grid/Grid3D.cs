using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedGameData.Grid
{
    class Grid3D
    {
        public Vector3 Position;
        public int Width;
        public int Height;
        public int Rows;
        public int Columns;
        GraphicsDevice graphicsDevice;
        Color Colour;


        VertexPositionColor[] vertices;
        BasicEffect effect;
        public static BoundingBox BB;

        public Grid3D(Vector3 position, int width, int height, int rows, int columns, Color colour, GraphicsDevice graphicsDevice)
        {
            Position = position;
            Width = width;
            Height = height;
            Rows = rows;
            Columns = columns;
            this.graphicsDevice = graphicsDevice;
            this.Colour = colour;

            effect = new BasicEffect(graphicsDevice);
            effect.VertexColorEnabled = true;


            List<VertexPositionColor> tempVertices = new List<VertexPositionColor>();
            int xDiff = Width / Columns;
            int yDiff = Height / rows;

            float xBase = Position.X - Width / 2f;
            float zBase = Position.Z - Height / 2f;
            float yBase = Position.Y;

            for (int y = 0; y <= Rows; y++)
            {
                tempVertices.Add(new VertexPositionColor(
                    new Vector3(xBase + y * xDiff, yBase, zBase), Colour));

                tempVertices.Add(new VertexPositionColor(
                    new Vector3(xBase + y * xDiff, yBase, zBase + Height), Colour));

                for (int x = 0; x <= Columns; x++)
                {
                                    tempVertices.Add(new VertexPositionColor(
                    new Vector3(xBase, yBase, zBase + x * xDiff), Colour));

                tempVertices.Add(new VertexPositionColor(
                    new Vector3(xBase + Width, yBase, zBase + x * xDiff), Colour));

                }

            }
            vertices = tempVertices.ToArray();

            var max = new Vector3(float.MinValue);
            var min = new Vector3(float.MaxValue);

            for (int i = 0; i < tempVertices.Count; i++)
            {
                min = Vector3.Min(min, tempVertices[i].Position);
                max = Vector3.Max(max, tempVertices[i].Position);
            }
            BB = new BoundingBox(min, max);

        }

        public void Draw()
        {
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                effect.View = Camera.viewMatrix;
                effect.Projection = Camera.projMatrix;

                graphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, vertices, 0, vertices.Length / 2);
            }
        }
    }
}
