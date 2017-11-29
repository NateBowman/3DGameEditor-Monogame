using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SharedGameData.Input;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace SharedGameData
{
    public static class STATIC_GLOBAL_INPUT
    {
        public static NativeKeyboardInput KeyboardInputHandler { get; set; }
        public static MGForms_MouseInput MouseInputHandler { get; set; }
        public static KeyboardState prevKeys;
        public static KeyboardState currKeys;
        public static MouseState prevMouse;
        public static MouseState currMouse;
        private static int newWheelVal = 0;
        private static int currWheelVal = 0;    //decrements/increments towards newWheelVal
        private static bool inputHandlersReady = false;

        public static void InitialiseInputHandlers(System.Windows.Forms.Control mouseFocalControl)
        {
            KeyboardInputHandler = new NativeKeyboardInput();
            MouseInputHandler = new MGForms_MouseInput(mouseFocalControl);
            //  System.Windows.Forms.Application.Idle += delegate { UpdateInputStates(); };
            inputHandlersReady = true;
        }

        public static void UpdateInputStates(bool adjustPosition = false)
        {
            if (!inputHandlersReady)
                return;
            prevKeys = currKeys;
            currKeys = KeyboardInputHandler.GetState();



            if (adjustPosition)
                prevMouse = currMouse;
            else
            {
                MouseState newState = new MouseState(prevMouse.X, prevMouse.Y, currMouse.ScrollWheelValue, currMouse.LeftButton, currMouse.MiddleButton, currMouse.RightButton, currMouse.XButton1, currMouse.XButton2);
                prevMouse = newState;
            }
            currMouse = MouseInputHandler.MouseState;
            currWheelVal = prevMouse.ScrollWheelValue;
            newWheelVal = currMouse.ScrollWheelValue;
            //if (currWheelVal != newWheelVal)
            //{
            //    if (newWheelVal > currWheelVal)
            //        currWheelVal+= 60/ 6;
            //    else
            //        currWheelVal -= 60 / 6;
            //}
        }

        public static bool IsNewKeyPress(Keys key)
        {
            return prevKeys.IsKeyUp(key) && currKeys.IsKeyDown(key);
        }

        public static bool IsNewLeftClick()
        {
            return prevMouse.LeftButton == ButtonState.Released && currMouse.LeftButton == ButtonState.Pressed;
        }

        public static bool IsNewMiddleClick()
        {
            return prevMouse.MiddleButton == ButtonState.Released && currMouse.MiddleButton == ButtonState.Pressed;
        }

        public static bool IsNewRightClick()
        {
            return prevMouse.RightButton == ButtonState.Released && currMouse.RightButton == ButtonState.Pressed;
        }

        public static Microsoft.Xna.Framework.Point GetMousePosDelta()
        {
            return currMouse.Position - prevMouse.Position;
        }

        public static void HandleMouseButtons(System.Windows.Forms.MouseEventArgs e, bool buttonPressed)
        {
            Console.WriteLine(buttonPressed);
            MouseInputHandler.HandleMouseButtons(e, buttonPressed);
            UpdateInputStates();
        }

        public static void HandleMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            MouseInputHandler.HandleMouseMove(e);
            UpdateInputStates(true);
        }

        public static void HandleMouseWheel(System.Windows.Forms.MouseEventArgs e)
        {
            MouseInputHandler.HandleMouseWheel(e);
            UpdateInputStates();
        }

        public static Vector2 GetMousePos()
        {
            return new Vector2(currMouse.X, currMouse.Y);
        }

        public static int GetPreviousWheelDelta()
        {
            return currWheelVal;
        }

        public static int GetCurrentWheelDelta()
        {
            return newWheelVal;
        }
    }
}