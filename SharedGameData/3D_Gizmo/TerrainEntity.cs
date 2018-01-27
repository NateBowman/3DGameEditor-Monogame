#region Usings

using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharedGameData;

#endregion

public class TerrainEntity : SceneEntity {
    public TerrainEntity(Texture2D texture, string textureName) {
        this.texture = texture;
        ModelName = textureName;

        if (Name == "") {
            Name = $"{ModelName}:{new Random().Next(1000, 9999)}";
        }
    }

    public TerrainEntity() { }

    [XmlIgnore, Browsable(false)]
    public Vector3 center { get; set; }

    [XmlIgnore, Browsable(false)]
    public int Height { get; set; }

    [XmlIgnore, Browsable(false)]
    public float[,] heightData { get; set; }

    [XmlIgnore, Browsable(false)]
    public int[] indices { get; set; }

    [ReadOnly(true)]
    public override ShaderType Shader => ShaderType.Terrain;

    [XmlIgnore, Browsable(false)]
    public VertexPositionColorTexture[] vertices { get; set; }

    [XmlIgnore, ReadOnly(true)]
    public int Width { get; set; }

    public override void Draw(GraphicsDevice gd = null) {
        //gd.Clear(Color.Black);
        if (gd == null) {
            return;
        }

        //effect.CurrentTechnique = effect.Techniques["ColoredNoShading"];
        effect.Parameters["View"].SetValue(Camera.viewMatrix);
        effect.Parameters["Projection"].SetValue(Camera.projMatrix);
        effect.Parameters["World"].SetValue(_world);

        if (EffectParameterNames.Contains("ViewVector")) {
            effect.Parameters["ViewVector"].SetValue(Camera.GetViewVector());
        }
        if (EffectParameterNames.Contains("ModelTexture")) {
            effect.Parameters["ModelTexture"].SetValue(texture);
        }

        //Additional parameters example:

        if (EffectParameterNames.Contains("AmbientColor")) {
            effect.Parameters["AmbientColor"].SetValue(AmbientColor);
        }
        if (EffectParameterNames.Contains("AmbientIntensity")) {
            effect.Parameters["AmbientIntensity"].SetValue(AmbientIntensity);
        }
        if (EffectParameterNames.Contains("DiffuseLightDirection")) {
            effect.Parameters["DiffuseLightDirection"].SetValue(-DiffuseLightDirection);
        }
        if (EffectParameterNames.Contains("DiffuseColor")) {
            effect.Parameters["DiffuseColor"].SetValue(DiffuseColor);
        }
        if (EffectParameterNames.Contains("DiffuseIntensity")) {
            effect.Parameters["DiffuseIntensity"].SetValue(DiffuseIntensity);
        }

        foreach (var pass in effect.CurrentTechnique.Passes) {
            pass.Apply();

            gd.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length, indices, 0, indices.Length / 3, VertexPositionColorTexture.VertexDeclaration);
        }

        RenderBB(gd);
    }

    public override void LoadAsset(ContentManager cm) {
        SetUpContentMan(cm);
        //SetUpTagsAndTexture();

        SetUpEffects();

        if (texture == null) {
            texture = Content.Load<Texture2D>(Path.Combine("HeightMaps", ModelName));
        }

        LoadHeightData(texture);

        SetUpVertices();
        SetUpIndices();

        RebuildBB();
    }

    public override void Update() {
#if USE_QUATERNION

      world = Matrix.CreateScale(scale) * Matrix.CreateFromQuaternion(orientation) * Matrix.CreateTranslation(position);
#elif USE_ROTATIONMATRIX
            world = Matrix.CreateScale(scale) * rotation * Matrix.CreateTranslation(position);
#else
        _world = Matrix.CreateScale(_scale) * Matrix.CreateWorld(_position - center, _forward, _up);
#endif
    }

    protected override void RebuildBB() {
        if (vertices == null) {
            return;
        }

        var min = new Vector3(float.MaxValue);
        var max = new Vector3(float.MinValue);

        foreach (var vertex in vertices) {
            min = Vector3.Min(min, vertex.Position);
            max = Vector3.Max(max, vertex.Position);
        }

        center = min + ((max - min) / 2f);

        _world = Matrix.CreateScale(_scale) * Matrix.CreateWorld(_position - center, _forward, _up);

        min = Vector3.Transform(min, _world);
        max = Vector3.Transform(max, _world);

        bounds = new BoundingBox(min, max);
        //bounds = VertexElementExtractor.UpdateBoundingBoxFromVertexList(EntityModel, _world, vertices);
    }

    /// <summary>
    ///     heightmap from alpha chan
    /// </summary>
    /// <param name="heightMap"></param>
    private void LoadHeightData(Texture2D heightMap) {
        Width = heightMap.Width;
        Height = heightMap.Height;

        var heightMapColors = new Color[Width * Height];
        heightMap.GetData(heightMapColors);

        heightData = new float[Width, Height];
        for (var x = 0; x < Width; x++)
        for (var y = 0; y < Height; y++) {
            heightData[x, y] = heightMapColors[x + (y * Width)].A / 5.0f;
        }
    }

    private void SetUpIndices() {
        indices = new int[(Width - 1) * (Height - 1) * 6];
        var counter = 0;
        for (var y = 0; y < (Height - 1); y++) {
            for (var x = 0; x < (Width - 1); x++) {
                var lowerLeft = x + (y * Width);
                var lowerRight = (x + 1) + (y * Width);
                var topLeft = x + ((y + 1) * Width);
                var topRight = (x + 1) + ((y + 1) * Width);

                indices[counter++] = topLeft;
                indices[counter++] = lowerRight;
                indices[counter++] = lowerLeft;

                indices[counter++] = topLeft;
                indices[counter++] = topRight;
                indices[counter++] = lowerRight;
            }
        }
    }

    private void SetUpVertices() {
        vertices = new VertexPositionColorTexture[Width * Height];
        for (var x = 0; x < Width; x++) {
            for (var y = 0; y < Height; y++) {
                vertices[x + (y * Width)].Position = new Vector3(x, heightData[x, y], -y);
                vertices[x + (y * Width)].Color = Color.White;
                vertices[x + (y * Width)].TextureCoordinate = new Vector2(x / (float) Width, y / (float) Height);
            }
        }
    }
}