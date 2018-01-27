namespace SharedGameData {
    #region Usings

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    public class MeshTag {
        public BasicEffect cachedBasicEffect;
        public Vector3 Color;
        public float SpecularPower;
        public Texture2D Texture;

        public MeshTag(Vector3 Color, Texture2D Texture, float SpecularPower, BasicEffect basicEff) {
            this.Color = Color;
            this.Texture = Texture;
            this.SpecularPower = SpecularPower;
            cachedBasicEffect = basicEff;
        }
    }
}