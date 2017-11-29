using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SharedGameData
{
    public class MeshTag
    {
        public Vector3 Color;
        public Texture2D Texture;
        public float SpecularPower;
        public BasicEffect cachedBasicEffect;
        public MeshTag(Vector3 Color, Texture2D Texture, float SpecularPower, BasicEffect basicEff)
        {
            this.Color = Color;
            this.Texture = Texture;
            this.SpecularPower = SpecularPower;
            this.cachedBasicEffect = basicEff;
        }
    }
}
