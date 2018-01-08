// -------------------------------------------------------------
// -- XNA 3D Gizmo (Component)
// -------------------------------------------------------------
// -- open-source gizmo component for any 3D level editor.
// -- contains any feature you may be looking for in a transformation gizmo.
// -- 
// -- for additional information and instructions visit codeplex.
// --
// -- codeplex url: http://xnagizmo.codeplex.com/
// --
// -----------------Please Do Not Remove ----------------------
// -- Work by Tom Looman, licensed under Ms-PL
// -- My Blog: http://coreenginedev.blogspot.com
// -- My Portfolio: http://tomlooman.com
// -- You may find additional XNA resources and information on these sites.
// ------------------------------------------------------------
// -- uncomment the define statements below to choose your desired rotation method.

//#define USE_QUATERNION
//#define USE_ROTATIONMATRIX

#region Using Statements

//using SharedGameData.HelperFunctions;

#endregion

namespace SharedGameData {
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;
    using Annotations;
    using HelperFunctions;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using XNAGizmo;

    #endregion

    public enum ShaderType {
        BasicEffect,
        Ambient,
        Diffuse,
        Specular,
        Textured,
        Terrain
    }

    [Serializable]
    public class SceneEntity : ITransformable, INotifyPropertyChanged {
        #region Fields & Properties

        protected Texture2D texture;

        protected Vector3 _position = Vector3.Zero;

        [NonSerialized]
        private List<float[]> verts;

        protected virtual void RebuildBB() {
            if (EntityModel == null) {
                return;
            }

            if (verts == null) {
                verts = VertexElementExtractor.GetVertexData(EntityModel);
            }

            _world = Matrix.CreateScale(_scale) * Matrix.CreateWorld(_position, _forward, _up);
            bounds = VertexElementExtractor.UpdateBoundingBoxFromVertexList(EntityModel, _world, verts);
        }

        [ReadOnly(true)]
        public string ModelName { get; set; }


        public Vector3 Position {
            get => _position;
            set {
                _position = value;
                OnPropertyChanged();
                RebuildBB();
            }
        }

        protected Vector3 _scale = Vector3.One;

        public Vector3 Scale {
            get => _scale;
            set {
                _scale = value;
                OnPropertyChanged();
                RebuildBB();
            }
        }

#if USE_QUATERNION
    private Quaternion orientation = Quaternion.Identity;
    public Quaternion Orientation
    {
      get { return orientation; }
      set
      {
        orientation = value;
        orientation.Normalize();
      }
    }
#elif USE_ROTATIONMATRIX
        private Matrix rotation = Matrix.Identity;
        public Matrix Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
#endif

        protected Vector3 _forward = Vector3.Forward;
        protected Vector3 _up = Vector3.Up;

        [ReadOnly(true)]
        public Vector3 Forward {
            get => _forward;
            set {
                _forward = value;
                _forward.Normalize();
            }
        }

        [ReadOnly(true)]
        public Vector3 Up {
            get => _up;
            set {
                _up = value;
                _up.Normalize();
            }
        }

        private Model _model;
        protected Matrix _world;

        private const float LENGTH = 5f;
        protected BoundingBox bounds;

        [ReadOnly(true)]
        public BoundingBox BoundingBox => bounds;

        private ShaderType shader = ShaderType.BasicEffect;

        [Category("Shaders"), Description("Selected Shader")]
        public virtual ShaderType Shader {
            get => shader;
            set {
                if (value == ShaderType.Terrain) {
                    return;
                }

                shader = value;
                LoadAsset(Content);
            }
        }

        private bool _generatedTags;

        #region Shader Related Variables

        //custom shader
        protected Effect effect;

        //holds the string names of all parameters (variables) in a shader
        public List<string> EffectParameterNames;

        private Vector4 ambientColor = new Color(1, 1, 1, 1).ToVector4();

        [Category("Shaders"), Description("Ambient colour")]
        public Vector4 AmbientColor { get => ambientColor; set => ambientColor = value; }

        private float ambientIntensity = 0.1f;

        [Category("Shaders"), Description("Ambient intensity")]
        public float AmbientIntensity { get => ambientIntensity; set => ambientIntensity = value; }

        private Vector3 diffuseLightDirection = new Vector3(1, 0, 0);

        [Category("Shaders"), Description("Diffuse light dir")]
        public Vector3 DiffuseLightDirection { get => diffuseLightDirection; set => diffuseLightDirection = value; }

        private Vector4 diffuseColor = new Vector4(1, 1, 1, 1);

        [Category("Shaders"), Description("Diffuse Light colour")]
        public Vector4 DiffuseColor { get => diffuseColor; set => diffuseColor = value; }

        private float diffuseIntensity = 1.0f;
        private string _name;

        [Category("Shaders"), Description("Diffuse intensity")]
        public float DiffuseIntensity { get => diffuseIntensity; set => diffuseIntensity = value; }

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ParentId { get; set; } = null;

        [XmlIgnore]
        public Model EntityModel { get => _model; set => _model = value; }

        #endregion

        public static ContentManager Content;

        #endregion

        public SceneEntity() { }

        public SceneEntity(Model model, string modelName) {
            EntityModel = model;
            ModelName = modelName;
            Name = $"{modelName}:{new Random().Next(1000, 9999)}";
            // scale the object to something resonable
            model.Root.Transform = Matrix.Identity * Matrix.CreateScale(0.1f);
        }

        public string Name {
            get { return _name; }
            set {
                if (value == _name)
                    return;

                _name = value;
                OnPropertyChanged();
            }
        }

        protected void SetUpContentMan(ContentManager cm) {
            if (Content == null)
            {
                Content = cm;
            }
        }

        protected void SetUpTagsAndTexture() {
            if (!_generatedTags)
            {
                EntityModel = Content.Load<Model>(Path.Combine("Models", ModelName));
                generateTags();
                _generatedTags = true;
            }
            //find textureName from meshtag, this is automatically populated with information from the model itself with the generateTags method
            texture = ((MeshTag)(EntityModel.Meshes[0].MeshParts[0].Tag)).Texture;
        }

        protected void SetUpEffects() {
            EffectParameterNames = new List<string>();
            var effectName = Shader.ToString();

            if (Shader != ShaderType.BasicEffect)
            {
                effect = Content.Load<Effect>(Path.Combine("Shaders", effectName));

                //extract a list of all the parameters in the shader, we can then use this list to test if the parameter exists before sending it to the shader
                var eParamColl = effect.Parameters;

                foreach (var t in eParamColl) {
                    EffectParameterNames.Add(t.Name);
                }
            }
        }

        public virtual void LoadAsset(ContentManager cm) {
            SetUpContentMan(cm);

            SetUpTagsAndTexture();

            SetUpEffects();

            RebuildBB();
        }

        private void generateTags() {
            foreach (var mesh in EntityModel.Meshes) {
                foreach (var part in mesh.MeshParts) {
                    if (part.Effect is BasicEffect) {
                        var effect = (BasicEffect) part.Effect;
                        var tag = new MeshTag(effect.DiffuseColor, effect.Texture, effect.SpecularPower, (BasicEffect) part.Effect);
                        part.Tag = tag;
                    }
                }
            }
        }

        public virtual void Update() {
#if USE_QUATERNION

      world = Matrix.CreateScale(scale) * Matrix.CreateFromQuaternion(orientation) * Matrix.CreateTranslation(position);
#elif USE_ROTATIONMATRIX
            world = Matrix.CreateScale(scale) * rotation * Matrix.CreateTranslation(position);
#else
            _world = Matrix.CreateScale(_scale) * Matrix.CreateWorld(_position, _forward, _up);
#endif
        }

        public float? Select(Ray selectionRay) {
            return selectionRay.Intersects(BoundingBox);
        }

        public virtual void Draw(GraphicsDevice gd = null) {
            var modelTransforms = new Matrix[EntityModel.Bones.Count];
            EntityModel.CopyAbsoluteBoneTransformsTo(modelTransforms);

            if (Shader == ShaderType.BasicEffect) {
                foreach (var modelmesh in EntityModel.Meshes) {
                    foreach (var meshpart in modelmesh.MeshParts) {
                        if (meshpart.Effect.GetType() != typeof(BasicEffect)) {
                            meshpart.Effect = ((MeshTag) meshpart.Tag).cachedBasicEffect;
                        }
                        var effect = (BasicEffect) meshpart.Effect;

                        effect.World = modelTransforms[modelmesh.ParentBone.Index] * _world;
                        effect.View = Engine.View;
                        effect.Projection = Engine.Projection;
                        effect.EnableDefaultLighting();
                    }

                    modelmesh.Draw();
                }
            }
            else {
                foreach (var mesh in EntityModel.Meshes) {
                    foreach (var part in mesh.MeshParts) {
                        part.Effect = effect;
                        effect.Parameters["World"].SetValue(modelTransforms[mesh.ParentBone.Index] * _world);
                        effect.Parameters["View"].SetValue(Camera.viewMatrix);
                        effect.Parameters["Projection"].SetValue(Camera.projMatrix);

                        var worldInverseTransposeMatrix = Matrix.Transpose(Matrix.Invert(modelTransforms[mesh.ParentBone.Index] * _world));

                        if (EffectParameterNames.Contains("WorldInverseTranspose")) {
                            effect.Parameters["WorldInverseTranspose"].SetValue(worldInverseTransposeMatrix);
                        }
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
                    }

                    mesh.Draw();
                }
            }

            RenderBB(gd);
        }

        protected void RenderBB(GraphicsDevice gd) {
            if (gd != null)
            {
                BoundingBoxRenderer.Render(BoundingBox, gd, Camera.viewMatrix, Camera.projMatrix, Color.HotPink);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}