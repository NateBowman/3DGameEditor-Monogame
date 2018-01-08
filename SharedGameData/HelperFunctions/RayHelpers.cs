namespace SharedGameData.HelperFunctions {
    #region Usings

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    public static class RayHelpers {
        public static Ray ConvertMouseToRay(Vector2 mousePosition, GraphicsDevice _graphics, Matrix view, Matrix projection) {
            var nearPoint = new Vector3(mousePosition, 0);
            var farPoint = new Vector3(mousePosition, 1);

            nearPoint = _graphics.Viewport.Unproject(nearPoint, projection, view, Matrix.Identity);
            farPoint = _graphics.Viewport.Unproject(farPoint, projection, view, Matrix.Identity);

            var direction = farPoint - nearPoint;
            direction.Normalize();

            return new Ray(nearPoint, direction);
        }

        public static float? GetHit(Ray ray, BoundingBox bb) {
            var distance = ray.Intersects(bb);

            return distance;
        }

        public static Vector3? GetHitPoint(Ray ray, BoundingBox bb) {
            var distance = ray.Intersects(bb);
            if (distance != null) {
                var Pos = (Vector3) (ray.Position + (ray.Direction * distance));
                return Pos;
            }

            return null;
        }
    }
}