using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace SharedGameData
{
    public class Camera
    {
        private static Camera self;
        static float leftrightRot = 0;  //amount of left/right rotation applied to the camera 
        static float updownRot = 0;     //amount of up/down rotation applied to the camera 

        //These 2 values will limit how fast we want our camera to respond on mouse and keyboard input, and should therefore remain constant
        const float const_rotationSpeed = 1f;
        const float const_moveSpeed = 500.0f;

        MouseState originalMouseState;

        public static Matrix viewMatrix;
        public static Matrix projMatrix;

        private Vector3 cameraPosition;
        public Vector3 CameraPos
        {
            get
            {
                return cameraPosition;
            }
            set
            {
                cameraPosition = value;
            }
        }

        public Camera()
        {
            cameraPosition = new Vector3(0, 30, -150);
            self = this;
        }

        public Camera(Vector3 pos)
        {
            cameraPosition = pos;
            self = this;
        }

        public void LoadContent(ContentManager Content, GraphicsDevice gdevice)
        {
            UpdateViewMatrix();

            projMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                gdevice.Viewport.AspectRatio,
                0.1f,
                10000f);

            originalMouseState = Mouse.GetState();
        }

        public void Update(GameTime gameTime)
        {
            ProcessInput((float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000);
        }


        //creates a view matrix based upon the current camera position and rotation values.
        private void UpdateViewMatrix()
        {
            //The first line creates the camera rotation matrix, by combining the rotation around the X axis (looking up-down) 
            //by the rotation around the Y axis (looking left-right).
            Matrix cameraRotation = Matrix.CreateRotationX(updownRot) * Matrix.CreateRotationY(leftrightRot);

            Vector3 cameraOriginalTarget = new Vector3(0, 0, 1);
            Vector3 cameraRotatedTarget = Vector3.Transform(cameraOriginalTarget, cameraRotation);
            Vector3 cameraFinalTarget = cameraPosition + cameraRotatedTarget;

            Vector3 cameraOriginalUpVector = new Vector3(0, 1, 0);
            Vector3 cameraRotatedUpVector = Vector3.Transform(cameraOriginalUpVector, cameraRotation);


            // As target point for our camera, we take the position of our camera, plus the (0,0,-1) ‘forward’ vector. 
            // We need to transform this forward vector with the rotation of the camera, so it becomes the forward vector of the camera. 
            // We find the ‘Up’ vector the same way: by transforming it with the camera rotation.


            viewMatrix = Matrix.CreateLookAt(cameraPosition, cameraFinalTarget, cameraRotatedUpVector);
        }

        /// <summary>
        /// Hanles user input and adjusts camera position/rotation accordingly
        /// </summary>
        /// <param name="amount">Amount of time passed since the last time the functon was called.
        /// </param>
        private void ProcessInput(float amount)
        {
            Vector3 moveVector = Vector3.Zero;
            KeyboardState keyState = Keyboard.GetState();

            if (STATIC_GLOBAL_INPUT.currKeys.IsKeyDown(Keys.W) || (STATIC_GLOBAL_INPUT.GetCurrentWheelDelta() > STATIC_GLOBAL_INPUT.GetPreviousWheelDelta()))
                moveVector += new Vector3(0, 0, 1);
            if (STATIC_GLOBAL_INPUT.currKeys.IsKeyDown(Keys.S) || (STATIC_GLOBAL_INPUT.GetCurrentWheelDelta() < STATIC_GLOBAL_INPUT.GetPreviousWheelDelta()))
                moveVector += new Vector3(0, 0, -1);
            if (STATIC_GLOBAL_INPUT.currKeys.IsKeyDown(Keys.D))
                moveVector += new Vector3(-1, 0, 0);
            if (STATIC_GLOBAL_INPUT.currKeys.IsKeyDown(Keys.A))
                moveVector += new Vector3(1, 0, 0);
            if (STATIC_GLOBAL_INPUT.currKeys.IsKeyDown(Keys.Q))
                moveVector += new Vector3(0, 1, 0);
            if (STATIC_GLOBAL_INPUT.currKeys.IsKeyDown(Keys.Z))
                moveVector += new Vector3(0, -1, 0);

            #region Allow movement of the camera via the mouse if the user is holding the middle mouse button
            //if (currentMouseState.MiddleButton == ButtonState.Pressed)
            if (STATIC_GLOBAL_INPUT.currMouse.MiddleButton == ButtonState.Pressed)
            {
                if (STATIC_GLOBAL_INPUT.currMouse != STATIC_GLOBAL_INPUT.prevMouse)
                {
                    //get the difference in position between the current and original mouse position
                    // NOTE: const_rotationSpeed is also factored in below
                    float xDifference = (STATIC_GLOBAL_INPUT.currMouse.X - STATIC_GLOBAL_INPUT.prevMouse.X);/* * const_rotationSpeed*/;
                    float yDifference = (STATIC_GLOBAL_INPUT.currMouse.Y - STATIC_GLOBAL_INPUT.prevMouse.Y); /** const_rotationSpeed*/;

                    // NOTE: Left/right rotation controls are not normally inverted
                    if(Math.Abs(xDifference) > 1) {
                        leftrightRot += const_rotationSpeed * xDifference * amount; //adjust the left/right rotation by the difference value * speed * time passed
                        Console.WriteLine($"{leftrightRot} / {xDifference}");
                    }

                    if(Math.Abs(yDifference) > 1 ) {
                        updownRot -= const_rotationSpeed * yDifference * amount;    // " for up / down rotation 
                    }

                    UpdateViewMatrix(); //update the camera's view matrix
                }
            }
            #endregion

            //moveVector gets multiplied by amount, which holds the amount of time passed since the last call. 
            AddToCameraPosition(moveVector * amount);
        }

        /// <summary>
        /// Adjusts camera position by adding a vector3 to position 
        /// </summary>
        /// <param name="vectorToAdd"></param>
        private void AddToCameraPosition(Vector3 vectorToAdd)
        {
            //create a rotation matrix of the camera's current UP/DOWN & LEFT/RIGHT rotation values
            Matrix cameraRotation = Matrix.CreateRotationX(updownRot) * Matrix.CreateRotationY(leftrightRot);

            //Using the rotation matrix, we transform the moveDirection so that it points in the correct direction relative to the camera
            Vector3 rotatedVector = Vector3.Transform(vectorToAdd, cameraRotation);

            //Transforms the vector by the rotation of our camera, so ‘Forward’ will actually be ‘Forward’ relative to our camera and updates the camera position.
            cameraPosition += (const_moveSpeed * rotatedVector);

            UpdateViewMatrix();
        }

        public Vector3 GetCamTarget()
        {
            //The first line creates the camera rotation matrix, by combining the rotation around the X axis (looking up-down) 
            //by the rotation around the Y axis (looking left-right).
            Matrix cameraRotation = Matrix.CreateRotationX(updownRot) * Matrix.CreateRotationY(leftrightRot);

            Vector3 cameraOriginalTarget = new Vector3(0, 0, 1);
            Vector3 cameraRotatedTarget = Vector3.Transform(cameraOriginalTarget, cameraRotation);
            Vector3 cameraFinalTarget = cameraPosition + cameraRotatedTarget;

            return cameraFinalTarget;
        }

        public Matrix GetCameraRotation()
        {
            Matrix cameraRotation = Matrix.CreateRotationX(updownRot) * Matrix.CreateRotationY(leftrightRot);

            return cameraRotation;
        }

        public static Vector3 GetViewVector()
        {
            Vector3 viewVector = Vector3.Transform(self.GetCamTarget() - self.CameraPos, self.GetCameraRotation());
            viewVector.Normalize();
            return viewVector;
        }

    }
}
