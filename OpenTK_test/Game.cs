using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Audio.OpenAL;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using StbImageSharp;

namespace OpenTK_test
{
    internal class Game: GameWindow
    {
        int width, height;
        static int object_count = 32;//количество небесных тел
        int[] EBOs = new int[object_count];//element buffer object позволяет использовать вершины повторно
        int[] VAOs = new int[object_count];//vertex array object
        int[] VBOs = new int[object_count];//vertex buffer object
        int[] textureVBOs = new int[object_count];
        Object[] galaxy = new Object[object_count];
        Shader shaderProgram;
        Camera camera;

        public Game(int width, int height): base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            this.CenterWindow(new Vector2i(width, height));
            this.width = width;
            this.height = height;
            this.shaderProgram = new Shader();
        }
        protected void Big_Bang()//создание галактики (солнечной системы)
        {
            galaxy[0] = new Object();
            galaxy[0].Create_Skybox();

            galaxy[1] = new Object();
            galaxy[1].Create_Earth();

            galaxy[2] = new Object();
            galaxy[2].Create_Sun();

            galaxy[3] = new Object();
            galaxy[3].Create_Mercury();

            galaxy[4] = new Object();
            galaxy[4].Create_Venus();

            galaxy[5] = new Object();
            galaxy[5].Create_Mars();

            galaxy[6] = new Object();
            galaxy[6].Create_Jupiter();

            galaxy[7] = new Object();
            galaxy[7].Create_Saturn();

            galaxy[8] = new Object();
            galaxy[8].Create_Neptune();

            galaxy[9] = new Object();
            galaxy[9].Create_Uranus();

            galaxy[10] = new Object();
            galaxy[10].Create_Moon();

            for (int i = 11; i < 17; i++)
            {
                galaxy[i] = new Object();
                galaxy[i].Create_Asteroid1();
            }

            for (int i = 17; i < 23; i++)
            {
                galaxy[i] = new Object();
                galaxy[i].Create_Asteroid2();
            }

            galaxy[23] = new Object();
            galaxy[23].Create_Ganimed();

            galaxy[24] = new Object();
            galaxy[24].Create_Europa();

            galaxy[25] = new Object();
            galaxy[25].Create_Callisto();

            galaxy[26] = new Object();
            galaxy[26].Create_Io();

            galaxy[27] = new Object();
            galaxy[27].Create_Phobos();

            galaxy[28] = new Object();
            galaxy[28].Create_Nibiru();


            galaxy[object_count - 3] = new Object();
            galaxy[object_count - 3].Create_Saturn_Rings();

            galaxy[object_count - 2] = new Object();
            galaxy[object_count - 2].Create_Uranus_Rings();

            galaxy[object_count - 1] = new Object();
            galaxy[object_count - 1].Create_UFO();
        }
        protected override void OnLoad()
        {
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            Big_Bang();

            for (int i = 0; i < object_count; i++)
            {
                //работа с буферами
                VAOs[i] = GL.GenVertexArray();
                VBOs[i] = GL.GenBuffer();
                EBOs[i] = GL.GenBuffer();
                textureVBOs[i] = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, VBOs[i]);//привязка буфера
                GL.BufferData(BufferTarget.ArrayBuffer, galaxy[i].vertices.Count * Vector3.SizeInBytes * sizeof(float), galaxy[i].vertices.ToArray(), BufferUsageHint.StaticDraw);//поместили вершины в буфер
                GL.BindVertexArray(VAOs[i]);//привязали массив
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);//привязка слота номер 0
                GL.EnableVertexArrayAttrib(VAOs[i], 0);//запуск слота 0
                GL.BindBuffer(BufferTarget.ArrayBuffer, i);//отвязка vbo
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBOs[i]);//привязка буфера
                GL.BufferData(BufferTarget.ElementArrayBuffer, galaxy[i].indices.Length * sizeof(uint), galaxy[i].indices, BufferUsageHint.StaticDraw);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);//отвязка ebo
                GL.BindBuffer(BufferTarget.ArrayBuffer, textureVBOs[i]);//привязка буфера
                GL.BufferData(BufferTarget.ArrayBuffer, galaxy[i].texCoords.Count * Vector3.SizeInBytes * sizeof(float), galaxy[i].texCoords.ToArray(), BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
                GL.EnableVertexArrayAttrib(VAOs[i], 1);
                GL.BindVertexArray(0);//НЕ ТРОГАТЬ 

                //работа с текстурами
                //1. Загрузка текстуры
                galaxy[i].textureID = GL.GenTexture();//сгенерировали пустую
                GL.ActiveTexture(TextureUnit.Texture0);//активация текстуры
                GL.BindTexture(TextureTarget.Texture2D, galaxy[i].textureID);//привязка текстуры                                                         
                                                                                 //2. Параметры текстуры
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
                //3. Загрузка изображения
                StbImage.stbi_set_flip_vertically_on_load(1);
                ImageResult Texture = ImageResult.FromStream(File.OpenRead(galaxy[i].texture_path), ColorComponents.RedGreenBlueAlpha);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Texture.Width, Texture.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, Texture.Data);
                //4. Открепление
                GL.BindTexture(TextureTarget.Texture2D, 0);   
            }

            //работа с шейдерами
            shaderProgram.LoadShader();

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, shaderProgram.LoadShaderSource("shader.vert"));
            GL.CompileShader(vertexShader);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, shaderProgram.LoadShaderSource("shader.frag"));
            GL.CompileShader(fragmentShader);

            GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int success1);
            if (success1 == 0)
            {
                string infoLog = GL.GetShaderInfoLog(vertexShader);
                Console.WriteLine(infoLog);
            }
            GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out int success2);
            if (success2 == 0)
            {
                string infoLog = GL.GetShaderInfoLog(fragmentShader);
                Console.WriteLine(infoLog);
            }

            GL.AttachShader(shaderProgram.shaderHandle, vertexShader);
            GL.AttachShader(shaderProgram.shaderHandle, fragmentShader);
            GL.LinkProgram(shaderProgram.shaderHandle);
            GL.Enable(EnableCap.DepthTest);

            Vector3 position;
            position = new Vector3(40);//спавн не в центре карты
            camera = new Camera(width, height, position);
            CursorState = CursorState.Grabbed;
        }
        protected override void OnUnload() 
        {
            for (int i = 0; i  < object_count; i++)
            {
                GL.DeleteBuffer(VAOs[i]);
                GL.DeleteBuffer(VBOs[i]);
                GL.DeleteBuffer(EBOs[i]);
                GL.DeleteTexture(galaxy[i].textureID);
            }
            shaderProgram.DeleteShader(); 
        }
        protected void move_objects()
        {
            Object skybox = galaxy[0];
            Object earth = galaxy[1];
            Object sun = galaxy[2];
            Object mercury = galaxy[3];
            Object venus = galaxy[4];
            Object mars = galaxy[5];
            Object jupiter = galaxy[6];
            Object saturn = galaxy[7];
            Object neptune = galaxy[8];
            Object uranus = galaxy[9];
            Object moon = galaxy[10];
            Object asteroid1 = galaxy[11];
            Object asteroid2 = galaxy[12];
            Object asteroid3 = galaxy[13];
            Object asteroid4 = galaxy[14];
            Object asteroid5 = galaxy[15];
            Object asteroid6 = galaxy[16];
            Object asteroid7 = galaxy[17];
            Object asteroid8 = galaxy[18];
            Object asteroid9 = galaxy[19];
            Object asteroid10 = galaxy[20];
            Object asteroid11 = galaxy[21];
            Object asteroid12 = galaxy[22];
            Object ganimed = galaxy[23];
            Object europa = galaxy[24];
            Object callisto = galaxy[25];
            Object io = galaxy[26];
            Object phobos = galaxy[27];
            Object nibiru = galaxy[28];
            Object saturn_rings = galaxy[object_count - 3];
            Object uranus_rings = galaxy[object_count - 2];

            skybox.rotation += 0.000001f;
            earth.rotation += 0.0015f;
            sun.rotation += 0.00005f;
            mercury.rotation += 0.003f;
            venus.rotation += 0.001f;
            mars.rotation += 0.001f;
            jupiter.rotation += 0.00001f;
            saturn.rotation += 0.000015f;
            neptune.rotation += 0.0004f;
            uranus.rotation += 0.00037f;
            moon.rotation += 0.003f;
            asteroid1.rotation += 0.003f;
            asteroid2.rotation += 0.003f;
            asteroid3.rotation += 0.003f;
            asteroid4.rotation += 0.003f;
            asteroid5.rotation += 0.003f;
            asteroid6.rotation += 0.003f;
            asteroid7.rotation += 0.002f;
            asteroid8.rotation += 0.002f;
            asteroid9.rotation += 0.002f;
            asteroid10.rotation += 0.002f;
            asteroid11.rotation += 0.002f;
            asteroid12.rotation += 0.002f;
            ganimed.rotation += 0.002f;
            europa.rotation += 0.003f;
            callisto.rotation += 0.0025f;
            io.rotation += 0.001f;
            phobos.rotation += 0.001f;
            nibiru.rotation += 0.0001f;
            saturn_rings.rotation += 0.0006f;
            uranus_rings.rotation += 0.0006f;

            earth.moving_x = MathF.Sin(earth.spin_coeff) * 120f;
            earth.moving_z = MathF.Cos(earth.spin_coeff) * 120f;
            earth.spin_coeff += 0.00007f;

            mercury.moving_x = MathF.Sin(mercury.spin_coeff) * 50f;
            mercury.moving_z = MathF.Cos(mercury.spin_coeff) * 50f;
            mercury.spin_coeff += 0.0008f;

            venus.moving_x = MathF.Sin(venus.spin_coeff) * 80f;
            venus.moving_z = MathF.Cos(venus.spin_coeff) * 80f;
            venus.spin_coeff += 0.0002f;

            mars.moving_x = MathF.Sin(mars.spin_coeff) * 150f;
            mars.moving_z = MathF.Cos(mars.spin_coeff) * 150f;
            mars.spin_coeff += 0.000045f;

            phobos.moving_x = MathF.Sin(phobos.spin_coeff) * 3f + mars.moving_x;
            phobos.moving_z = MathF.Cos(phobos.spin_coeff) * 3f + mars.moving_z;
            phobos.moving_y = MathF.Cos(phobos.spin_coeff + 5f) * 3f;//окак могу
            phobos.spin_coeff += 0.002f;

            jupiter.moving_x = MathF.Sin(jupiter.spin_coeff) * 250f;
            jupiter.moving_z = MathF.Cos(jupiter.spin_coeff) * 250f;
            jupiter.spin_coeff += 0.00003f;

            ganimed.moving_x = MathF.Sin(ganimed.spin_coeff) * 14f + jupiter.moving_x;
            ganimed.moving_z = MathF.Cos(ganimed.spin_coeff) * 14f + jupiter.moving_z;
            ganimed.moving_y = MathF.Cos(ganimed.spin_coeff - 30f) * 14f;
            ganimed.spin_coeff += 0.003f;

            europa.moving_x = MathF.Sin(europa.spin_coeff) * 12f + jupiter.moving_x;
            europa.moving_z = MathF.Cos(europa.spin_coeff) * 12f + jupiter.moving_z;
            europa.moving_y = MathF.Sin(europa.spin_coeff) * 12f;
            europa.spin_coeff += 0.004f;

            callisto.moving_x = MathF.Sin(callisto.spin_coeff) * 16f + jupiter.moving_x;
            callisto.moving_z = MathF.Cos(callisto.spin_coeff) * 16f + jupiter.moving_z;
            callisto.moving_y = MathF.Sin(callisto.spin_coeff + 45f) * 16f;
            callisto.spin_coeff += 0.0025f;

            io.moving_x = MathF.Sin(io.spin_coeff) * 11f + jupiter.moving_x;
            io.moving_z = MathF.Cos(io.spin_coeff) * 11f + jupiter.moving_z;
            io.spin_coeff += 0.0045f;

            saturn.moving_x = MathF.Sin(saturn.spin_coeff) * 300f;
            saturn.moving_z = MathF.Cos(saturn.spin_coeff) * 300f;
            saturn.spin_coeff += 0.00006f;

            neptune.moving_x = MathF.Sin(neptune.spin_coeff) * 385f;
            neptune.moving_z = MathF.Cos(neptune.spin_coeff) * 385f;
            neptune.spin_coeff += 0.00004f;

            uranus.moving_x = MathF.Sin(uranus.spin_coeff) * 450f;
            uranus.moving_z = MathF.Cos(uranus.spin_coeff) * 450f;
            uranus.spin_coeff += 0.00005f;

            moon.moving_x = MathF.Sin(moon.spin_coeff) * 5f + earth.moving_x;
            moon.moving_z = MathF.Cos(moon.spin_coeff) * 5f + earth.moving_z;
            moon.spin_coeff += 0.003f;

            nibiru.moving_x = MathF.Sin(nibiru.spin_coeff) * 500f;
            nibiru.moving_z = MathF.Cos(nibiru.spin_coeff) * 500f;
            nibiru.spin_coeff += 0.00006f;

            saturn_rings.moving_x = MathF.Sin(saturn_rings.spin_coeff) * 300f;
            saturn_rings.moving_z = MathF.Cos(saturn_rings.spin_coeff) * 300f;
            saturn_rings.spin_coeff += 0.00006f;

            uranus_rings.moving_x = MathF.Sin(uranus_rings.spin_coeff) * 450f;
            uranus_rings.moving_z = MathF.Cos(uranus_rings.spin_coeff) * 450f;
            uranus_rings.spin_coeff += 0.00005f;

            asteroid1.moving_x = MathF.Sin(asteroid1.spin_coeff) * 180f;
            asteroid1.moving_z = MathF.Cos(asteroid1.spin_coeff) * 180f;
            asteroid1.spin_coeff += 0.00045f;

            asteroid2.moving_x = MathF.Sin(asteroid2.spin_coeff) * 182f;
            asteroid2.moving_z = MathF.Cos(asteroid2.spin_coeff) * 182f;
            asteroid2.spin_coeff += 0.0003f;

            asteroid3.moving_x = MathF.Sin(asteroid3.spin_coeff) * 184f;
            asteroid3.moving_z = MathF.Cos(asteroid3.spin_coeff) * 184f;
            asteroid3.spin_coeff += 0.0002f;

            asteroid4.moving_x = MathF.Sin(asteroid4.spin_coeff) * 179f;
            asteroid4.moving_z = MathF.Cos(asteroid4.spin_coeff) * 179f;
            asteroid4.spin_coeff += 0.0004f;

            asteroid5.moving_x = MathF.Sin(asteroid5.spin_coeff) * 181f;
            asteroid5.moving_z = MathF.Cos(asteroid5.spin_coeff) * 181f;
            asteroid5.spin_coeff += 0.00035f;

            asteroid6.moving_x = MathF.Sin(asteroid6.spin_coeff) * 184f;
            asteroid6.moving_z = MathF.Cos(asteroid6.spin_coeff) * 184f;
            asteroid6.spin_coeff += 0.00055f;

            asteroid7.moving_x = MathF.Sin(asteroid7.spin_coeff) * 180f;
            asteroid7.moving_z = MathF.Cos(asteroid7.spin_coeff) * 180f;
            asteroid7.spin_coeff += 0.0005f;

            asteroid8.moving_x = MathF.Sin(asteroid8.spin_coeff) * 182f;
            asteroid8.moving_z = MathF.Cos(asteroid8.spin_coeff) * 182f;
            asteroid8.spin_coeff += 0.0002f;

            asteroid9.moving_x = MathF.Sin(asteroid9.spin_coeff) * 184f;
            asteroid9.moving_z = MathF.Cos(asteroid9.spin_coeff) * 184f;
            asteroid9.spin_coeff += 0.00048f;

            asteroid10.moving_x = MathF.Sin(asteroid10.spin_coeff) * 179f;
            asteroid10.moving_z = MathF.Cos(asteroid10.spin_coeff) * 179f;
            asteroid10.spin_coeff += 0.00026f;

            asteroid11.moving_x = MathF.Sin(asteroid11.spin_coeff) * 181f;
            asteroid11.moving_z = MathF.Cos(asteroid11.spin_coeff) * 181f;
            asteroid11.spin_coeff += 0.00043f;

            asteroid12.moving_x = MathF.Sin(asteroid12.spin_coeff) * 184f;
            asteroid12.moving_z = MathF.Cos(asteroid12.spin_coeff) * 184f;
            asteroid12.spin_coeff += 0.00049f;
        }
        protected override void OnRenderFrame(FrameEventArgs args)//вызывается каждый кадр, связана с отрисовкой
        {
            

            GL.ClearColor(0f, 0f, 0f, 1f);//цвет
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);//очищает экран, ставя цвет из пред фции

            for (int i = 0; i < object_count; i++)
            {
                Matrix4 model;
                Matrix4 view;
                Matrix4 projection;

                if (i == object_count-1)
                {
                    shaderProgram.UseShader();
                    GL.BindTexture(TextureTarget.Texture2D, galaxy[i].textureID);
                    model = Matrix4.Identity;
                    view = Matrix4.Identity;
                    projection = Matrix4.Identity;
                }
                else
                {
                    shaderProgram.UseShader();
                    GL.BindTexture(TextureTarget.Texture2D, galaxy[i].textureID);
                    //трансформации
                    model = Matrix4.CreateRotationY(galaxy[i].rotation);
                    Matrix4 translation = Matrix4.CreateTranslation(galaxy[i].moving_x, galaxy[i].moving_y, galaxy[i].moving_z);//если поменять третий аргумент то будет отдалятся
                    model *= translation;
                    view = camera.GetVievMatrix();
                    projection = camera.GetProjection();//последние два - дистанция рендеринга   
                }
                int modelLocation = GL.GetUniformLocation(shaderProgram.shaderHandle, "model");
                int viewLocation = GL.GetUniformLocation(shaderProgram.shaderHandle, "view");
                int projectionLocation = GL.GetUniformLocation(shaderProgram.shaderHandle, "projection");
                //отправляем данные в uniforms
                GL.UniformMatrix4(modelLocation, true, ref model);
                GL.UniformMatrix4(viewLocation, true, ref view);
                GL.UniformMatrix4(projectionLocation, true, ref projection);
                GL.BindVertexArray(VAOs[i]);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBOs[i]);
                GL.DrawElements(PrimitiveType.Triangles, galaxy[i].indices.Length, DrawElementsType.UnsignedInt, 0);
                //PrimitiveType - примитив, необработанный треугольник
                //количество вершин для отрисовки
                //тип элементов ebo
                //смещение того, что хотим нарисовать. Мы хотим нарисовать все - поэтому 0
            }

            move_objects();//анимации вращения по орбитам и вокруг своей оси

            Context.SwapBuffers();//меняем местами два буфера

            base.OnRenderFrame(args);
        }
        protected override void OnUpdateFrame(FrameEventArgs args)//вызывается каждый кадр и обновляет окно при его готовности
        {
            MouseState mouse = MouseState;
            KeyboardState input = KeyboardState;
            base.OnUpdateFrame(args);
            camera.Update(input, mouse, args);

            if (KeyboardState.IsKeyDown(Keys.Escape))//если нажат esc то закрывает окно
            {
                Close();
            }
        }
        protected override void OnResize(ResizeEventArgs e)//сообщаем opengl о том, что поменяли размер окошка
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
            this.width = e.Width;
            this.height = e.Height;
        }    
    } 
}
