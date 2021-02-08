using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace WindowsFormsApp1
{
    public interface ICamera
    {
        Matrix4 LookAtMatrix { get; }
        void Update(double time, double delta);
    }
    public class StaticCamera : ICamera
    {
        public Matrix4 LookAtMatrix { get; }
        public StaticCamera()
        {
            Vector3 position;
            position.X = -0.1f;
            position.Y = 0.2f;
            position.Z = 0;
            LookAtMatrix = Matrix4.LookAt(position, -Vector3.UnitZ, Vector3.UnitY);
        }
        public StaticCamera(Vector3 position, Vector3 target)
        {
            LookAtMatrix = Matrix4.LookAt(position, target, Vector3.UnitY);
        }
        public void Update(double time, double delta)
        { }
    }

    class Light
    {
        public Light(Vector3 position, Vector3 color, float diffuseintensity = 1.0f, float ambientintensity = 1.0f)
        {
            Position = position;
            Color = color;

            DiffuseIntensity = diffuseintensity;
            AmbientIntensity = ambientintensity;
        }

        public Vector3 Position;
        public Vector3 Color = new Vector3();
        public float DiffuseIntensity = 1.0f;
        public float AmbientIntensity = 0.1f;
    }

    class Visualizer : GameWindow
    {
        public GameWindow Window;
        private Graph[,,] Map;
        private List<Location> Path;
        View view;
        Light activeLight = new Light(new Vector3(), new Vector3(0.9f, 0.80f, 0.8f));

        private float uShift;
        private float wShift;
        private float shiftStep = 0.1f;


        public Visualizer(Graph[,,] map, List<Location> path)
            : base(800, 600, GraphicsMode.Default, "OpenTK Quick Start Sample")
        {
            VSync = VSyncMode.On;
            Map = map;
            Path = path;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(0.1f, 0.2f, 0.5f, 0.0f);
            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);

            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 64.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (Keyboard.GetState().IsKeyDown(Key.W))
                uShift += shiftStep;

            if (Keyboard.GetState().IsKeyDown(Key.S))
                uShift -= shiftStep;

            if (Keyboard.GetState().IsKeyDown(Key.A))
                wShift -= shiftStep;

            if (Keyboard.GetState().IsKeyDown(Key.D))
                wShift += shiftStep;
        }
        
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            float x = (float)(uShift * Math.Cos(Math.PI / 4) - wShift * Math.Sin(Math.PI / 4));
            float y = (float)(uShift * Math.Sin(Math.PI / 4) + wShift * Math.Cos(Math.PI / 4));

            Matrix4 modelview = Matrix4.LookAt(new Vector3(-2+x, -2+y,-4), new Vector3(2+x,2+y,0), -Vector3.UnitZ);
           
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(0.85f, 0.85f, 0.9f);
            GL.Vertex3(0, 0, 1);
            GL.Vertex3(0, 41, 1);
            GL.Vertex3(41, 41, 1);
            GL.Vertex3(41, 0, 1);
            GL.End();

            DrawMap();
            DrawPath();

            SwapBuffers();
        }

        private void DrawMap()
        {
            int mapSize = Map.Length/4;
            mapSize = (int)Math.Sqrt(mapSize);

            for (int x = 0; x < mapSize; x++)
            {
                for (int y = 0; y < mapSize; y++)
                {
                    for (int z = 0; z < 4; z++)
                    {
                        if (!Map[x, y, z].passable)
                        {
                            DrawBox(0.95f, new Vector3(x, y, -z), Vector3.One);
                        }
                    }
                }
            }
        }

        private void DrawPath()
        {
            int mapSize = Map.Length / 4;
            mapSize = (int)Math.Sqrt(mapSize);

            for (int i = 1; i < Path.Count - 1; i++)
            {
                Vector3 color = new Vector3(0, 1, 1);
                if (i == 1)
                    color = new Vector3(0, 1, 0);
                if (i == Path.Count - 2)
                    color = new Vector3(1, 0, 0);
                DrawBox(0.5f, new Vector3(Path[i].x, Path[i].y, -Path[i].z), color);
            }
        }

        private void DrawBox(float size, Vector3 position, Vector3 color)
        {

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(1.0f * color.X, 1.0f * color.Y, 1.0f * color.Z);
            GL.Vertex3(position.X + 0, position.Y + 0, position.Z + 0);
            GL.Vertex3(position.X + size, position.Y + 0, position.Z + 0);
            GL.Vertex3(position.X + size, position.Y + size, position.Z + 0);
            GL.Vertex3(position.X + 0, position.Y + size, position.Z + 0);
            GL.End();

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(0.2f * color.X, 0.2f * color.Y, 0.2f * color.Z);
            GL.Vertex3(position.X + 0, position.Y + 0, position.Z + size);
            GL.Vertex3(position.X + size, position.Y + 0, position.Z + size);
            GL.Vertex3(position.X + size, position.Y + size, position.Z + size);
            GL.Vertex3(position.X + 0, position.Y + size, position.Z + size);
            GL.End();

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(0.9f * color.X, 0.9f * color.Y, 0.9f * color.Z);
            GL.Vertex3(position.X + 0, position.Y + size, position.Z + 0);
            GL.Vertex3(position.X + size, position.Y + size, position.Z + 0);
            GL.Vertex3(position.X + size, position.Y + size, position.Z + size);
            GL.Vertex3(position.X + 0, position.Y + size, position.Z + size);
            GL.End();

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(0.4f * color.X, 0.4f * color.Y, 0.4f * color.Z);
            GL.Vertex3(position.X + 0, position.Y + 0, position.Z + 0);
            GL.Vertex3(position.X + size, position.Y + 0, position.Z + 0);
            GL.Vertex3(position.X + size, position.Y + 0, position.Z + size);
            GL.Vertex3(position.X + 0, position.Y + 0, position.Z + size);
            GL.End();

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(0.5f * color.X, 0.5f * color.Y, 0.5f * color.Z);
            GL.Vertex3(position.X + size, position.Y + size, position.Z + 0);
            GL.Vertex3(position.X + size, position.Y + 0, position.Z + 0);
            GL.Vertex3(position.X + size, position.Y + 0, position.Z + size);
            GL.Vertex3(position.X + size, position.Y + size, position.Z + size);
            GL.End();

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(0.45f * color.X, 0.45f * color.Y, 0.45f * color.Z);
            GL.Vertex3(position.X + 0, position.Y + size, position.Z + 0);
            GL.Vertex3(position.X + 0, position.Y + 0, position.Z + 0);
            GL.Vertex3(position.X + 0, position.Y + 0, position.Z + size);
            GL.Vertex3(position.X + 0, position.Y + size, position.Z + size);
            GL.End();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
