using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharedGameData.Editor;
using SharedGameData.Level_Classes;
using SharedGameData.ExtensionMethods;
using SharedGameData;
using XNAGizmo;
using SharedGameData;

namespace WinFormsGraphicsDevice
{
    public class EditorControl : GraphicsDeviceControl
    {
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

        Camera cam;
        
        protected override void Initialize()
        {
            content = new ContentManager(Services, "Content");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            STATIC_EDITOR_MODE.contentMan = this.content;
            font = content.Load<SpriteFont>("hudfont");

            Application.Idle += delegate { Update(); };
            Application.Idle += delegate { Invalidate(); };

            randomiser = new Random();
            primitiveBatch = new PrimitiveBatch(GraphicsDevice);

            timer = new System.Diagnostics.Stopwatch();
            timer.Start();

            cam = new Camera(new Vector3(0,20,0));
            cam.LoadContent(content, GraphicsDevice);
        }

        public void Update()
        {
            UpdateTime();
            
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

        protected override void Draw()
        {
            if (bDoNotDraw)
                return;

            GraphicsDevice.Clear(Color.CornflowerBlue);
             
        }

    }
}
