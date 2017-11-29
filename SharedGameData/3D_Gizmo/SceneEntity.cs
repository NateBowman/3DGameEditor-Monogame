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
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
//using SharedGameData.HelperFunctions;
using XNAGizmo;
using System.ComponentModel;
#endregion

namespace SharedGameData
{
    public enum ShaderType
    {
        BasicEffect,
        Ambient,
        Diffuse,
        Specular,
        Textured        
    }

    [Serializable]
    public class SceneEntity : ITransformable
    {

        #region Fields & Properties
        private Texture2D texture;

        private Vector3 _position = Vector3.Zero;

        [NonSerialized]
        private List<float[]> verts;
        private void RebuildBB()
        {

        }

        public string Name
        {
            get;
            set;
        }

        public Vector3 Position
        {
            get { return _position; }
            set { _position = value; RebuildBB(); }
        }

        private Vector3 _scale = Vector3.One;
        public Vector3 Scale
        {
            get { return _scale; }
            set { _scale = value; RebuildBB(); }
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
        private Vector3 _forward = Vector3.Forward;
        private Vector3 _up = Vector3.Up;

        public Vector3 Forward
        {
            get { return _forward; }
            set
            {
                _forward = value;
                _forward.Normalize();
            }
        }

        public Vector3 Up
        {
            get { return _up; }
            set
            {
                _up = value;
                _up.Normalize();
            }
        }

        private Model _model;
        private Matrix _world;

        const float LENGTH = 5f;
        private BoundingBox bounds;
        public BoundingBox BoundingBox
        {
            get
            {
                return bounds;
            }
        }

        private ShaderType shader = ShaderType.BasicEffect;
        [CategoryAttribute("Shaders"), DescriptionAttribute("Selected Shader")]
        public ShaderType Shader
        {
            get
            {
                return shader;
            }
            set
            {
                shader = value;
                LoadAsset(SceneEntity.Content);
            }
        }

        private bool _generatedTags = false;

        #region Shader Related Variables
        //custom shader
        private Effect effect;

        //holds the string names of all parameters (variables) in a shader
        public List<string> EffectParameterNames;

        private Vector4 ambientColor = new Color(1, 1, 1, 1).ToVector4();
        [CategoryAttribute("Shaders"), DescriptionAttribute("Ambient colour")]
        public Vector4 AmbientColor
        {
            get
            {
                return ambientColor;
            }
            set
            {
                ambientColor = value;
            }
        }

        private float ambientIntensity = 0.1f;
        [CategoryAttribute("Shaders"), DescriptionAttribute("Ambient intensity")]
        public float AmbientIntensity
        {
            get
            {
                return ambientIntensity;
            }
            set
            {
                ambientIntensity = value;
            }
        }

        private Vector3 diffuseLightDirection = new Vector3(1, 0, 0);
        [CategoryAttribute("Shaders"), DescriptionAttribute("Diffuse light dir")]
        public Vector3 DiffuseLightDirection
        {
            get
            {
                return diffuseLightDirection;
            }
            set
            {
                diffuseLightDirection = value;
            }
        }

        private Vector4 diffuseColor = new Vector4(1, 1, 1, 1);
        [CategoryAttribute("Shaders"), DescriptionAttribute("Diffuse Light colour")]
        public Vector4 DiffuseColor
        {
            get
            {
                return diffuseColor;
            }
            set
            {
                diffuseColor = value;
            }
        }

        private float diffuseIntensity = 1.0f;
        [CategoryAttribute("Shaders"), DescriptionAttribute("Diffuse intensity")]
        public float DiffuseIntensity
        {
            get
            {
                return diffuseIntensity;
            }
            set
            {
                diffuseIntensity = value;
            }
        }
        #endregion

        public static ContentManager Content;

        #endregion

        public SceneEntity()
        { }

        public SceneEntity(Model model, string modelName)
        {
            _model = model;
            Name = modelName;
        }

        public void LoadAsset(ContentManager cm)
        {
            if (Content == null)
                Content = cm;

            if (!_generatedTags)
            {
                _model = cm.Load<Model>(Path.Combine("Models", Name));
                generateTags();
                _generatedTags = true;
            }
            //find textureName from meshtag, this is automatically populated with information from the model itself with the generateTags method
            texture = ((MeshTag)(_model.Meshes[0].MeshParts[0].Tag)).Texture;

            EffectParameterNames = new List<string>();
            string effectName = Shader.ToString();

            if (shader != ShaderType.BasicEffect)
            {
                effect = cm.Load<Effect>(Path.Combine("Shaders", effectName));

                //extract a list of all the parameters in the shader, we can then use this list to test if the parameter exists before sending it to the shader
                EffectParameterCollection eParamColl = effect.Parameters;

                for (int i = 0; i < eParamColl.Count; i++)
                {
                    EffectParameterNames.Add(eParamColl[i].Name);
                }
            }

            RebuildBB();
        }

        private void generateTags()
        {
            foreach (ModelMesh mesh in _model.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    if (part.Effect is BasicEffect)
                    {
                        BasicEffect effect = (BasicEffect)part.Effect;
                        MeshTag tag = new MeshTag(
                            effect.DiffuseColor,
                            effect.Texture,
                            effect.SpecularPower,
                            (BasicEffect)part.Effect);
                        part.Tag = tag;
                    }
                }
            }
        }

        public void Update()
        {
#if USE_QUATERNION

      world = Matrix.CreateScale(scale) * Matrix.CreateFromQuaternion(orientation) * Matrix.CreateTranslation(position);
#elif USE_ROTATIONMATRIX
            world = Matrix.CreateScale(scale) * rotation * Matrix.CreateTranslation(position);
#else
            _world = Matrix.CreateScale(_scale) * Matrix.CreateWorld(_position, _forward, _up);
#endif
        }

        public float? Select(Ray selectionRay)
        {
            return selectionRay.Intersects(BoundingBox);
        }

        public void Draw(GraphicsDevice gd = null)
        {
            Matrix[] modelTransforms = new Matrix[_model.Bones.Count];
            _model.CopyAbsoluteBoneTransformsTo(modelTransforms);

            if (Shader == ShaderType.BasicEffect)
            {
                foreach (ModelMesh modelmesh in _model.Meshes)
                {
                    foreach (ModelMeshPart meshpart in modelmesh.MeshParts)
                    {
                        if (meshpart.Effect.GetType() != typeof(BasicEffect))
                        {
                            meshpart.Effect = ((MeshTag)meshpart.Tag).cachedBasicEffect;
                        }
                        BasicEffect effect = (BasicEffect)meshpart.Effect;

                        effect.World = modelTransforms[modelmesh.ParentBone.Index] * _world;
                        effect.View = Engine.View;
                        effect.Projection = Engine.Projection;
                        effect.EnableDefaultLighting();
                    }
                    modelmesh.Draw();
                }
            }
            else
            {
                foreach (ModelMesh mesh in _model.Meshes)
                {
                    foreach (ModelMeshPart part in mesh.MeshParts)
                    {
                        part.Effect = effect;
                        effect.Parameters["World"].SetValue(modelTransforms[mesh.ParentBone.Index] * _world);
                        effect.Parameters["View"].SetValue(Camera.viewMatrix);
                        effect.Parameters["Projection"].SetValue(Camera.projMatrix);

                        Matrix worldInverseTransposeMatrix = Matrix.Transpose(Matrix.Invert(modelTransforms[mesh.ParentBone.Index] * _world));

                        if (EffectParameterNames.Contains("WorldInverseTranspose"))
                            effect.Parameters["WorldInverseTranspose"].SetValue(worldInverseTransposeMatrix);
                        if (EffectParameterNames.Contains("ViewVector"))
                            effect.Parameters["ViewVector"].SetValue(Camera.GetViewVector());
                        if (EffectParameterNames.Contains("ModelTexture"))
                            effect.Parameters["ModelTexture"].SetValue(texture);

                        //Additional parameters example:

                        if (EffectParameterNames.Contains("AmbientColor"))
                            effect.Parameters["AmbientColor"].SetValue(AmbientColor);
                        if (EffectParameterNames.Contains("AmbientIntensity"))
                            effect.Parameters["AmbientIntensity"].SetValue(AmbientIntensity);
                        if (EffectParameterNames.Contains("DiffuseLightDirection"))
                            effect.Parameters["DiffuseLightDirection"].SetValue(-DiffuseLightDirection);
                        if (EffectParameterNames.Contains("DiffuseColor"))
                            effect.Parameters["DiffuseColor"].SetValue(DiffuseColor);
                        if (EffectParameterNames.Contains("DiffuseIntensity"))
                            effect.Parameters["DiffuseIntensity"].SetValue(DiffuseIntensity);


                    }

                    mesh.Draw();
                }


            }

            //     if (gd != null)
            //       BoundingBoxRenderer.Render(BoundingBox, gd, Camera.viewMatrix, Camera.projMatrix, Color.HotPink);
        }
    }

}
