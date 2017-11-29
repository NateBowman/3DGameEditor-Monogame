using Microsoft.Xna.Framework.Input;
using System;
using System.Windows;
using System.Windows.Input;
 

namespace SharedGameData.Input
{
    /// <summary>
    /// Helper class that converts WPF mouse input to the XNA/MonoGame <see cref="MouseState"/>.
    /// </summary>
    public class MGForms_MouseInput
    {
        private readonly System.Windows.Forms.Control _focusElement;

        private MouseState _mouseState;

        /// <summary>
        /// The current mousestate.
        /// </summary>
        public MouseState MouseState
        {
            get { return _mouseState; }
        }

        /// <summary>
        /// Creates a new instance of the keyboard helper.
        /// </summary>
        /// <param name="focusElement">The element that will be used as the focus point. Only if this element is correctly focused, mouse events will be handled.</param>
        public MGForms_MouseInput(System.Windows.Forms.Control focusElement)
        {
            _focusElement = focusElement;
        }

        /// <summary>
        /// Handles all button related events.
        /// </summary>
        /// <param name="e">If <see cref="e.Handled"/> is true, it will be ignored, otherwise if the reference element is focused, the event will be handled and <see cref="e.Handled"/> will be set to true.</param>
        public void HandleMouseButtons(System.Windows.Forms.MouseEventArgs e, bool buttonPressed)
        {
            var conv =
                new Func<MouseButtonState, ButtonState>(
                    state => state == MouseButtonState.Pressed ? ButtonState.Pressed : ButtonState.Released);

            var m = _mouseState;
            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Left:
                    _mouseState = new MouseState(m.X, m.Y, m.ScrollWheelValue, buttonPressed ? ButtonState.Pressed : ButtonState.Released, m.MiddleButton, m.RightButton, m.XButton1, m.XButton2);
                    break;
                case System.Windows.Forms.MouseButtons.Middle:
                    _mouseState = new MouseState(m.X, m.Y, m.ScrollWheelValue, m.LeftButton, buttonPressed ? ButtonState.Pressed : ButtonState.Released, m.RightButton, m.XButton1, m.XButton2);
                    break;
                case System.Windows.Forms.MouseButtons.Right:
                    _mouseState = new MouseState(m.X, m.Y, m.ScrollWheelValue, m.LeftButton, m.MiddleButton, buttonPressed ? ButtonState.Pressed : ButtonState.Released, m.XButton1, m.XButton2);
                    break;
                // this code works, but the MonoGame MouseState class doesn't care for XButton1/XButton2 state - the properties always return ButtonState.Released
                case System.Windows.Forms.MouseButtons.XButton1:
                    _mouseState = new MouseState(m.X, m.Y, m.ScrollWheelValue, m.LeftButton, m.MiddleButton, m.RightButton, buttonPressed ? ButtonState.Pressed : ButtonState.Released, m.XButton2);
                    break;
                case System.Windows.Forms.MouseButtons.XButton2:
                    _mouseState = new MouseState(m.X, m.Y, m.ScrollWheelValue, m.LeftButton, m.MiddleButton, m.RightButton, m.XButton1, buttonPressed ? ButtonState.Pressed : ButtonState.Released);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Handles all move related events.
        /// </summary>
        /// <param name="e">If <see cref="e.Handled"/> is true, it will be ignored, otherwise if the reference element is focused, the event will be handled and <see cref="e.Handled"/> will be set to true.</param>
        public void HandleMouseMove(System.Windows.Forms.MouseEventArgs e)
        {            
            var m = _mouseState;
            var pos = _focusElement.PointToClient(System.Windows.Forms.Cursor.Position); 
            _mouseState = new MouseState((int)pos.X, (int)pos.Y, m.ScrollWheelValue, m.LeftButton, m.MiddleButton, m.RightButton, m.XButton1, m.XButton2);
        }

        /// <summary>
        /// Handles all button related events.
        /// </summary>
        /// <param name="e">If <see cref="e.Handled"/> is true, it will be ignored, otherwise if the reference element is focused, the event will be handled and <see cref="e.Handled"/> will be set to true.</param>
        public void HandleMouseWheel(System.Windows.Forms.MouseEventArgs e)
        {            
            var m = _mouseState;
            _mouseState = new MouseState(m.X, m.Y, e.Delta + m.ScrollWheelValue, m.LeftButton, m.MiddleButton, m.RightButton, m.XButton1, m.XButton2);
        }
    }
}