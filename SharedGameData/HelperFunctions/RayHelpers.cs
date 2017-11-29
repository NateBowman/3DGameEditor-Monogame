using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedGameData.HelperFunctions
{
    public static class RayHelpers
    {

        public static Ray ConvertMouseToRay(Vector2 mousePosition, GraphicsDevice _graphics, Matrix view, Matrix projection)
        {
            var near = new Vector3(mousePosition, 0);
            var far = new Vector3(mousePosition, 1);

            near = _graphics.Viewport.Unproject(near, projection, view, Matrix.Identity);
            far = _graphics.Viewport.Unproject(far, projection, view, Matrix.Identity);

            var direction = far - near;
            direction.Normalize();

            return new Ray(near, direction);
        }

    }
}
