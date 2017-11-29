using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SharedGameData.ExtensionMethods
{
    public static class RectangleExtensions
    {
        public static Vector2[] GetCorners(this Rectangle rect)
        {
            return new Vector2[] 
            {   new Vector2(rect.X, rect.Y), new Vector2(rect.X+rect.Width, rect.Y),
                new Vector2(rect.X + rect.Width, rect.Y+rect.Height), new Vector2(rect.X, rect.Y+rect.Height)
            };
        }

        public static bool Contains(this Microsoft.Xna.Framework.Rectangle rect, System.Drawing.Point p)
        {
            //create MonoGame version of System.Drawing.Point
            Microsoft.Xna.Framework.Point convPoint = new Microsoft.Xna.Framework.Point(p.X, p.Y);
            if (rect.Contains(convPoint))
                return true;

            return false;
        }
    }
}
