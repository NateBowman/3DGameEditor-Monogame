using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;

namespace SharedGameData.Input
{
    /// <summary>
    /// Helper class that accesses a native API to get the current keystate.
    /// <see cref="GetKeys"/> returns the same keys as the XNA framework methods.
    /// </summary>
    public class NativeKeyboardInput
    {
        [DllImport("user32.dll", EntryPoint = "GetKeyboardState", SetLastError = true)]
        private static extern bool NativeGetKeyboardState([Out] byte[] keyStates);

        /// <summary>
        /// Creates a new instance of the keyboard helper.
        /// </summary>
        /// <param name="focusElement">The element that will be used as the focus point. Only if this element is correctly focused, key events will be registered. It may be required to call <see cref="System.Windows.Input.Keyboard.Focus"/> on this element.</param>
        public NativeKeyboardInput()
        {
     
        }

        private byte[] GetKeysState()
        {
            // the buffer must be exactly 256 bytes long as per API definition
            var keyStates = new byte[256];

            if (!NativeGetKeyboardState(keyStates))
                throw new Win32Exception(Marshal.GetLastWin32Error());
            return keyStates;
        }

        private Keys XnaKeyFromIndex(int index)
        {
            // surprisingly simple; props to the XNA guys for using the same indices, I'd have suspected that a large switch statement is required here to manually convert everything
            // but this has worked just fine on a German keyboard (both with German and US keyboard layout).
            // there might be some edge cases that I haven't figured out yet,
            return (Keys)index;
        }

        private Keys[] GetKeys()
        {
            //TODO: handle potential exception
            var bytes = GetKeysState();

            var pressedKeys = new List<Keys>();
            //if (_focusElement.IsKeyboardFocused)
            {
                // skip the first 8 entries as they are actually mouse events and not keyboard keys
                for (int i = 8; i < bytes.Length; i++)
                {
                    byte key = bytes[i];

                    //Logical 'and' so we can drop the low-order bit for toggled keys, else that key will appear with the value 1!
                    if ((key & 0x80) != 0)
                    {

                        //This is just for a short demo, you may want this to return
                        //multiple keys!
                        if (key != 0)
                            pressedKeys.Add(XnaKeyFromIndex(i));
                    }
                }
            }
            return pressedKeys.ToArray();
        }

        /// <summary>
        /// Gets the active keyboardstate.
        /// </summary>
        /// <returns></returns>
        public KeyboardState GetState()
        {
            return new KeyboardState(GetKeys());
        }
    }

}