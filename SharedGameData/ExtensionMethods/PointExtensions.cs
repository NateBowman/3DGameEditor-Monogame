using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedGameData.ExtensionMethods
{
    public static class PointExtensions
    {
        public static Microsoft.Xna.Framework.Vector2 ToVector2(this System.Drawing.Point p)
        {
            Microsoft.Xna.Framework.Vector2 v2 = new Microsoft.Xna.Framework.Vector2(p.X, p.Y);
            return v2;
        }
    }
}
