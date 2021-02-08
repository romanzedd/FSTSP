using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace WindowsFormsApp1
{
    class View
    {
        public Vector2 position;
        public double rotation;
        public double zoom;

        public View(Vector2 startPosition, double startZoom = 1.0, double startRotation = 0.0)
        {
            this.position = startPosition;
            this.rotation = startRotation;
            this.zoom = startZoom;
        }
        public void Update()
        {

        }
        public void ApplyTransform()
        {
            Matrix4 transform = Matrix4.Identity;

            transform = Matrix4.Mult(transform, Matrix4.CreateTranslation(-position.X, -position.Y, 0));
            transform = Matrix4.Mult(transform, Matrix4.CreateRotationZ(-(float)rotation));
            transform = Matrix4.Mult(transform, Matrix4.CreateScale((float)zoom, (float)zoom, 1.0f));

            GL.MultMatrix(ref transform);
        }
    }
}
