using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedGameData.ExtensionMethods
{
    public static class Vector2Extensions
    {
        public static System.Drawing.Point ToSysPoint(this Microsoft.Xna.Framework.Vector2 v2)
        {
            return new System.Drawing.Point((int)v2.X, (int)v2.Y);
        }
    }
}
