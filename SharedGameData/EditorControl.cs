using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharedGameData.Editor;
using SharedGameData.Level_Classes;
using SharedGameData.ExtensionMethods;
using SharedGameData;
using XNAGizmo;
using SharedGameData.Grid;
using SharedGameData.HelperFunctions;

namespace WinFormsGraphicsDevice
{
    public delegate void UIDelegate(object sender, System.Windows.Forms.KeyEventArgs e);
    public delegate void SelectionChangeDelegate();
    
    public class EditorControl : GraphicsDeviceControl
    {

        public event UIDelegate Translation_GUI_Update;
        public event UIDelegate Rotation_GUI_Update;
        public event UIDelegate ScaleUniform_GUI_Update;
        public event UIDelegate ScaleNonUniform_GUI_Update;
        public event SelectionChangeDelegate SelectionChange;

        public event EventHandler OnInitialize;
        public event EventHandler OnDraw;

        ContentManager content;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Random randomiser;
        PrimitiveBatch primitiveBatch;

        public bool bDoNotDraw = false;

        string localCoOrds = "";
        string worldCoOrds = "";

        System.Diagnostics.Stopwatch timer;
        TimeSpan lastUpdate;    //time the last update occured 
        TimeSpan total;         //total time elapsed since the program launched
        TimeSpan elapsed;   //the elapsed time since the last update
        GameTime gameTime;  //create fromthe above 3 values

        Camera _Camera3d;
        Grid3D _grid;
        public Ray ray;

        public GizmoComponent Gizmo;


        void InitXNA() {
            content = new ContentManager(Services, "Content");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            STATIC_EDITOR_MODE.contentMan = this.content;
            font = content.Load<SpriteFont>("hudfont");
            primitiveBatch = new PrimitiveBatch(GraphicsDevice);
            _Camera3d = new Camera(new Vector3(0, 20, 0));
            _Camera3d.LoadContent(content, GraphicsDevice);
            _grid = new Grid3D(Vector3.Zero, 200, 200, 50, 50, Color.Red, GraphicsDevice);
        }

        void InitGizmo()
        {
            Gizmo = new GizmoComponent(GraphicsDevice, spriteBatch, font);
            Gizmo.SetSelectionPool(STATIC_EDITOR_MODE.LevelInstance.Entities);

            // Events
            Gizmo.TranslateEvent += OnGizmoTranslate;
            Gizmo.RotateEvent += OnGizmoRotate;
            Gizmo.ScaleEvent += OnGizmoScale;


        }

        protected override void Initialize()
        {
            InitXNA();

            System.Windows.Forms.Application.Idle += delegate { Update(); };
            System.Windows.Forms.Application.Idle += delegate { Invalidate(); };

            randomiser = new Random();

            timer = new System.Diagnostics.Stopwatch();
            timer.Start();

            InitGizmo();
            
            // subscribe to level changed
            STATIC_EDITOR_MODE.LevelChanged += (object o) => { InitGizmo(); };

        }

        private const float transformScaleFactor = 0.5f;

        private void OnGizmoScale(ITransformable transformable, TransformationEventArgs e) {
            var delta = (Vector3)e.Value * transformScaleFactor;

            if (Gizmo.ActiveMode == GizmoMode.UniformScale) {
                if (transformable is SceneEntity entity) {
                    STATIC_EDITOR_MODE.ExecuteOnSelfAndChildEntities(entity, sceneEntity => sceneEntity.Scale *= 1 + ((delta.X + delta.Y + delta.Z) / 3));
                }
                else {
                    transformable.Scale *= 1 + ((delta.X + delta.Y + delta.Z) / 3);
                }
            }
            else {
                if (transformable is SceneEntity entity)
                {
                    STATIC_EDITOR_MODE.ExecuteOnSelfAndChildEntities(entity, sceneEntity => sceneEntity.Scale += delta);
                }
                else
                {
                    transformable.Scale += delta;

                }
            }
            transformable.Scale = Vector3.Clamp(transformable.Scale, Vector3.Zero, transformable.Scale);
        }

        private void OnGizmoRotate(ITransformable transformable, TransformationEventArgs e) {
            Gizmo.RotationHelper(transformable, e);
            if (transformable is SceneEntity entity)
            {
                STATIC_EDITOR_MODE.ExecuteOnChildEntities(entity, sceneEntity => Gizmo.RotationHelper(sceneEntity, e));
            }
        }

        private void OnGizmoTranslate(ITransformable transformable, TransformationEventArgs e) {
            transformable.Position += (Vector3) e.Value;
            if (transformable is SceneEntity entity) {
                STATIC_EDITOR_MODE.ExecuteOnChildEntities(entity, sceneEntity => sceneEntity.Position += (Vector3)e.Value);
            }
        }

        public void Update()
        {
            UpdateTime();
            _Camera3d.Update(gameTime);
            Engine.CameraPosition = _Camera3d.CameraPos;
            Engine.View = Camera.viewMatrix;
            Engine.Projection = Camera.projMatrix;

            UpdateGizmo();
            ray = RayHelpers.ConvertMouseToRay(STATIC_GLOBAL_INPUT.GetMousePos(), GraphicsDevice, Camera.viewMatrix, Camera.projMatrix);
        }

        private void UpdateTime()
        {
            // total game time
            total = timer.Elapsed;

            // time elapsed since the last update call
            elapsed = total - lastUpdate;

            // create the game time using those values
            gameTime = new Microsoft.Xna.Framework.GameTime(total, elapsed, false);

            //store the time of the last update for the next call of this function
            lastUpdate = total;
        }

        [Flags]
        public enum EditorDrawMode {
            Shaded = 1,
            Wireframe = 2,
//            WireframeShaded = 3,
        }

        public EditorDrawMode DrawingMode { get; set; } = EditorDrawMode.Shaded;
        public bool DrawDrid { get; set; } = true;

        protected override void Draw()
        {
            if (bDoNotDraw)
                return;

            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            if (DrawDrid) {
                _grid.Draw();
            }

            if (DrawingMode.HasFlag(EditorDrawMode.Shaded)) {
                Engine.Draw(GraphicsDevice);
            }

            if (DrawingMode.HasFlag(EditorDrawMode.Wireframe)) {
                var oldrs = GraphicsDevice.RasterizerState;
                var rs = new RasterizerState { FillMode = FillMode.WireFrame };
                GraphicsDevice.RasterizerState = rs;

                Engine.Draw(GraphicsDevice);

                GraphicsDevice.RasterizerState = oldrs;
            }

            Gizmo.UpdateCameraProperties(Camera.viewMatrix, Camera.projMatrix, _Camera3d.CameraPos);
            Gizmo.Draw();

        }


        

        public void SetSelectionTo(SceneEntity entity)
        {
            if (!Gizmo.GetSelectionPool().Contains(entity)) {
                return;
            }

            Gizmo.Selection.Clear();
            Gizmo.Selection.Add(entity);
            SelectionChange?.Invoke();
        }

        private void UpdateGizmo()
        {
            // update camera properties for rendering and ray-casting.
            Gizmo.UpdateCameraProperties(Engine.View, Engine.Projection, Engine.CameraPosition);

            if (this.Focused)
            {
                // select entities with your cursor (add the desired keys for add-to / remove-from -selection)
                if (STATIC_GLOBAL_INPUT.IsNewLeftClick())
                {
                    //if (System.Windows.Input.Mouse.LeftButton == MouseButtonState.Pressed)
                    Gizmo.SelectEntities(STATIC_GLOBAL_INPUT.currMouse.Position.ToVector2(),
                                          STATIC_GLOBAL_INPUT.currKeys.IsKeyDown(Keys.LeftControl),
                                          STATIC_GLOBAL_INPUT.currKeys.IsKeyDown(Keys.LeftAlt));
                    SelectionChange?.Invoke();
                }


                // set the active mode like translate or rotate
                if (STATIC_GLOBAL_INPUT.currKeys.IsKeyDown(Keys.D1))
                {
                    Gizmo.ActiveMode = GizmoMode.Translate;
                    Translation_GUI_Update?.Invoke(null, null);
                }
                if (STATIC_GLOBAL_INPUT.currKeys.IsKeyDown(Keys.D2))
                {
                    Gizmo.ActiveMode = GizmoMode.Rotate;
                    Rotation_GUI_Update?.Invoke(null, null);
                }
                if (STATIC_GLOBAL_INPUT.currKeys.IsKeyDown(Keys.D3))
                {
                    Gizmo.ActiveMode = GizmoMode.NonUniformScale;
                    ScaleNonUniform_GUI_Update?.Invoke(null, null);
                }
                if (STATIC_GLOBAL_INPUT.currKeys.IsKeyDown(Keys.D4))
                {
                    Gizmo.ActiveMode = GizmoMode.UniformScale;
                    ScaleUniform_GUI_Update?.Invoke(null, null);
                }
                // toggle precision mode
                if (STATIC_GLOBAL_INPUT.currKeys.IsKeyDown(Keys.LeftShift) || STATIC_GLOBAL_INPUT.currKeys.IsKeyDown(Keys.RightShift))
                    Gizmo.PrecisionModeEnabled = true;
                else
                    Gizmo.PrecisionModeEnabled = false;

                // toggle active space
                if (STATIC_GLOBAL_INPUT.currKeys.IsKeyDown(Keys.O))
                    Gizmo.ToggleActiveSpace();

                // toggle snapping
                if (STATIC_GLOBAL_INPUT.currKeys.IsKeyDown(Keys.I))
                    Gizmo.SnapEnabled = !Gizmo.SnapEnabled;

                // select pivot types
                if (STATIC_GLOBAL_INPUT.currKeys.IsKeyDown(Keys.P))
                    Gizmo.NextPivotType();

                // clear selection
                if (STATIC_GLOBAL_INPUT.currKeys.IsKeyDown(Keys.Escape))
                    Gizmo.Clear();
            }

            Gizmo.Update(gameTime);
            Engine.Update();
        }


    }
}
