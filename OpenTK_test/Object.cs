using System;
using System.Collections.Generic;
using System.Linq;
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
    internal class Object
    {
        public float rotation = 0f;//поле вращения
        public float moving_x = 0f;//движение по осям
        public float moving_y = 0f;
        public float moving_z = -1f;
        public List<Vector3> vertices;//вершины
        public uint[] indices;//порядок наложения текстуры
        public List<Vector2> texCoords;//координаты для текстуры
        public int textureID;//дескриптор текстуры
        public string texture_path;//имя текстуры
        public float spin_coeff = 0f;//коэффициент вращения по орбите
        float size;//размер небесного тела

        public Object()
        {
            vertices = new List<Vector3>();
            indices = new uint[1];
            texCoords = new List<Vector2>();
            texture_path = "";
        }

        public void Create_Skybox()
        {
            size = 1500f;
            vertices = new List<Vector3>()
            { //перед
                new Vector3(-size, size, -size),//верхняя левая 0
                new Vector3(size, size, -size),//верхняя правая 1
                new Vector3(size, -size, -size),//нижняя правая 2
                new Vector3(-size, -size, -size),//нижняя левая 3
              //право
                new Vector3(size, size, size),//верхняя левая 4
                new Vector3(size, size, -size),//верхняя правая 5
                new Vector3(size, -size, -size),//нижняя правая 6
                new Vector3(size, -size, size),//нижняя левая 7
              //зад
                new Vector3(-size, size, size),//верхняя левая 8
                new Vector3(size, size, size),//верхняя правая 9
                new Vector3(size, -size, size),//нижняя правая 10
                new Vector3(-size, -size, size),//нижняя левая 11
              //лево
                new Vector3(-size, size, -size),//верхняя левая 12
                new Vector3(-size, size, size),//верхняя правая 13
                new Vector3(-size, -size, size),//нижняя правая 14
                new Vector3(-size, -size, -size),//нижняя левая 15
              //верх
                new Vector3(-size, size, -size),//верхняя левая 16
                new Vector3(size, size, -size),//верхняя правая 17
                new Vector3(size, size, size),//нижняя правая 18
                new Vector3(-size, size, size),//нижняя левая 19
              //низ
                new Vector3(-size, -size, size),//верхняя левая 20
                new Vector3(size, -size, size),//верхняя правая 21
                new Vector3(size, -size, -size),//нижняя правая 22
                new Vector3(-size, -size, -size),//нижняя левая 23
            };
            indices = new uint[] { 0, 1, 2, 2, 3, 0, 4, 5, 6, 6, 7, 4, 8, 9, 10, 10, 11, 8, 12, 13, 14, 14, 15, 12, 16, 17, 18, 18, 19, 16, 20, 21, 22, 22, 23, 20 };
            texCoords = new List<Vector2>() { 
            //перед
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
            //право
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
            //зад
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
            //лево
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
            //верх
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
            //низ
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f)};
            texture_path = "../../../Textures/skybox.jpg";
        }

        public void Create_Earth()
        {
            size = 1f;
            vertices = new List<Vector3>()
            { //перед
                new Vector3(-size, size, -size),//верхняя левая 0
                new Vector3(size, size, -size),//верхняя правая 1
                new Vector3(size, -size, -size),//нижняя правая 2
                new Vector3(-size, -size, -size),//нижняя левая 3
              //право
                new Vector3(size, size, size),//верхняя левая 4
                new Vector3(size, size, -size),//верхняя правая 5
                new Vector3(size, -size, -size),//нижняя правая 6
                new Vector3(size, -size, size),//нижняя левая 7
              //зад
                new Vector3(-size, size, size),//верхняя левая 8
                new Vector3(size, size, size),//верхняя правая 9
                new Vector3(size, -size, size),//нижняя правая 10
                new Vector3(-size, -size, size),//нижняя левая 11
              //лево
                new Vector3(-size, size, -size),//верхняя левая 12
                new Vector3(-size, size, size),//верхняя правая 13
                new Vector3(-size, -size, size),//нижняя правая 14
                new Vector3(-size, -size, -size),//нижняя левая 15
              //верх
                new Vector3(-size, size, -size),//верхняя левая 16
                new Vector3(size, size, -size),//верхняя правая 17
                new Vector3(size, size, size),//нижняя правая 18
                new Vector3(-size, size, size),//нижняя левая 19
              //низ
                new Vector3(-size, -size, size),//верхняя левая 20
                new Vector3(size, -size, size),//верхняя правая 21
                new Vector3(size, -size, -size),//нижняя правая 22
                new Vector3(-size, -size, -size),//нижняя левая 23
            };
            indices = new uint[] { 0, 1, 2, 2, 3, 0, 4, 5, 6, 6, 7, 4, 8, 9, 10, 10, 11, 8, 12, 13, 14, 14, 15, 12, 16, 17, 18, 18, 19, 16, 20, 21, 22, 22, 23, 20 };
            texCoords = new List<Vector2>() { 
            //перед
            new Vector2(0f, 0.5f),
            new Vector2(0.3333f, 0.5f),
            new Vector2(0.3333f, 0f),
            new Vector2(0f, 0f),
            //право
            new Vector2(0.6666f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0.5f),
            new Vector2(0.6666f, 0.5f),     
            //зад
            new Vector2(0f, 1f),
            new Vector2(0.3333f, 1f),
            new Vector2(0.3333f, 0.5f),
            new Vector2(0f, 0.5f),
            //лево 
            new Vector2(0.3333f, 1f),
            new Vector2(0.6666f, 1f),
            new Vector2(0.6666f, 0.5f),
            new Vector2(0.3333f, 0.5f),
            //верх
            new Vector2(0.3333f, 0.5f),
            new Vector2(0.6666f, 0.5f),
            new Vector2(0.6666f, 0f),
            new Vector2(0.3333f, 0f),
            //низ
            new Vector2(0.6666f, 0.5f),
            new Vector2(1f, 0.5f),
            new Vector2(1f, 0f),
            new Vector2(0.6666f, 0f)
            };
            texture_path = "../../../Textures/earth.jpg";
        }

        public void Create_Sun()
        {
            size = 10f;
            vertices = new List<Vector3>()
            { //перед
                new Vector3(-size, size, -size),//верхняя левая 0
                new Vector3(size, size, -size),//верхняя правая 1
                new Vector3(size, -size, -size),//нижняя правая 2
                new Vector3(-size, -size, -size),//нижняя левая 3
              //право
                new Vector3(size, size, size),//верхняя левая 4
                new Vector3(size, size, -size),//верхняя правая 5
                new Vector3(size, -size, -size),//нижняя правая 6
                new Vector3(size, -size, size),//нижняя левая 7
              //зад
                new Vector3(-size, size, size),//верхняя левая 8
                new Vector3(size, size, size),//верхняя правая 9
                new Vector3(size, -size, size),//нижняя правая 10
                new Vector3(-size, -size, size),//нижняя левая 11
              //лево
                new Vector3(-size, size, -size),//верхняя левая 12
                new Vector3(-size, size, size),//верхняя правая 13
                new Vector3(-size, -size, size),//нижняя правая 14
                new Vector3(-size, -size, -size),//нижняя левая 15
              //верх
                new Vector3(-size, size, -size),//верхняя левая 16
                new Vector3(size, size, -size),//верхняя правая 17
                new Vector3(size, size, size),//нижняя правая 18
                new Vector3(-size, size, size),//нижняя левая 19
              //низ
                new Vector3(-size, -size, size),//верхняя левая 20
                new Vector3(size, -size, size),//верхняя правая 21
                new Vector3(size, -size, -size),//нижняя правая 22
                new Vector3(-size, -size, -size),//нижняя левая 23
            };
            indices = new uint[] { 0, 1, 2, 2, 3, 0, 4, 5, 6, 6, 7, 4, 8, 9, 10, 10, 11, 8, 12, 13, 14, 14, 15, 12, 16, 17, 18, 18, 19, 16, 20, 21, 22, 22, 23, 20 };
            texCoords = new List<Vector2>() { 
            //перед
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
            //право
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
            //зад
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
            //лево
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
            //верх
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
            //низ
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f)};
            texture_path = "../../../Textures/sun.jpg";
        }

        public void Create_Mercury()
        {
            size = 0.3f;
            vertices = new List<Vector3>()
            { //перед
                new Vector3(-size, size, -size),//верхняя левая 0
                new Vector3(size, size, -size),//верхняя правая 1
                new Vector3(size, -size, -size),//нижняя правая 2
                new Vector3(-size, -size, -size),//нижняя левая 3
              //право
                new Vector3(size, size, size),//верхняя левая 4
                new Vector3(size, size, -size),//верхняя правая 5
                new Vector3(size, -size, -size),//нижняя правая 6
                new Vector3(size, -size, size),//нижняя левая 7
              //зад
                new Vector3(-size, size, size),//верхняя левая 8
                new Vector3(size, size, size),//верхняя правая 9
                new Vector3(size, -size, size),//нижняя правая 10
                new Vector3(-size, -size, size),//нижняя левая 11
              //лево
                new Vector3(-size, size, -size),//верхняя левая 12
                new Vector3(-size, size, size),//верхняя правая 13
                new Vector3(-size, -size, size),//нижняя правая 14
                new Vector3(-size, -size, -size),//нижняя левая 15
              //верх
                new Vector3(-size, size, -size),//верхняя левая 16
                new Vector3(size, size, -size),//верхняя правая 17
                new Vector3(size, size, size),//нижняя правая 18
                new Vector3(-size, size, size),//нижняя левая 19
              //низ
                new Vector3(-size, -size, size),//верхняя левая 20
                new Vector3(size, -size, size),//верхняя правая 21
                new Vector3(size, -size, -size),//нижняя правая 22
                new Vector3(-size, -size, -size),//нижняя левая 23
            };
            indices = new uint[] { 0, 1, 2, 2, 3, 0, 4, 5, 6, 6, 7, 4, 8, 9, 10, 10, 11, 8, 12, 13, 14, 14, 15, 12, 16, 17, 18, 18, 19, 16, 20, 21, 22, 22, 23, 20 };
            texCoords = new List<Vector2>() { 
            //перед
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
            //право
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
            //зад
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
            //лево
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
            //верх
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
            //низ
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f)};
            texture_path = "../../../Textures/mercury.jpg";
        }
        
        public void Create_Venus()
        {
            size = 0.8f;
            vertices = new List<Vector3>()
            { //перед
                new Vector3(-size, size, -size),//верхняя левая 0
                new Vector3(size, size, -size),//верхняя правая 1
                new Vector3(size, -size, -size),//нижняя правая 2
                new Vector3(-size, -size, -size),//нижняя левая 3
              //право
                new Vector3(size, size, size),//верхняя левая 4
                new Vector3(size, size, -size),//верхняя правая 5
                new Vector3(size, -size, -size),//нижняя правая 6
                new Vector3(size, -size, size),//нижняя левая 7
              //зад
                new Vector3(-size, size, size),//верхняя левая 8
                new Vector3(size, size, size),//верхняя правая 9
                new Vector3(size, -size, size),//нижняя правая 10
                new Vector3(-size, -size, size),//нижняя левая 11
              //лево
                new Vector3(-size, size, -size),//верхняя левая 12
                new Vector3(-size, size, size),//верхняя правая 13
                new Vector3(-size, -size, size),//нижняя правая 14
                new Vector3(-size, -size, -size),//нижняя левая 15
              //верх
                new Vector3(-size, size, -size),//верхняя левая 16
                new Vector3(size, size, -size),//верхняя правая 17
                new Vector3(size, size, size),//нижняя правая 18
                new Vector3(-size, size, size),//нижняя левая 19
              //низ
                new Vector3(-size, -size, size),//верхняя левая 20
                new Vector3(size, -size, size),//верхняя правая 21
                new Vector3(size, -size, -size),//нижняя правая 22
                new Vector3(-size, -size, -size),//нижняя левая 23
            };
            indices = new uint[] { 0, 1, 2, 2, 3, 0, 4, 5, 6, 6, 7, 4, 8, 9, 10, 10, 11, 8, 12, 13, 14, 14, 15, 12, 16, 17, 18, 18, 19, 16, 20, 21, 22, 22, 23, 20 };
            texCoords = new List<Vector2>() { 
            //перед
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
            //право
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
            //зад
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
            //лево
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
            //верх
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
            //низ
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f)};
            texture_path = "../../../Textures/venus.jpg";
        }

        public void Create_Mars()
        {
            size = 0.65f;
            vertices = new List<Vector3>()
            { //перед
                new Vector3(-size, size, -size),//верхняя левая 0
                new Vector3(size, size, -size),//верхняя правая 1
                new Vector3(size, -size, -size),//нижняя правая 2
                new Vector3(-size, -size, -size),//нижняя левая 3
              //право
                new Vector3(size, size, size),//верхняя левая 4
                new Vector3(size, size, -size),//верхняя правая 5
                new Vector3(size, -size, -size),//нижняя правая 6
                new Vector3(size, -size, size),//нижняя левая 7
              //зад
                new Vector3(-size, size, size),//верхняя левая 8
                new Vector3(size, size, size),//верхняя правая 9
                new Vector3(size, -size, size),//нижняя правая 10
                new Vector3(-size, -size, size),//нижняя левая 11
              //лево
                new Vector3(-size, size, -size),//верхняя левая 12
                new Vector3(-size, size, size),//верхняя правая 13
                new Vector3(-size, -size, size),//нижняя правая 14
                new Vector3(-size, -size, -size),//нижняя левая 15
              //верх
                new Vector3(-size, size, -size),//верхняя левая 16
                new Vector3(size, size, -size),//верхняя правая 17
                new Vector3(size, size, size),//нижняя правая 18
                new Vector3(-size, size, size),//нижняя левая 19
              //низ
                new Vector3(-size, -size, size),//верхняя левая 20
                new Vector3(size, -size, size),//верхняя правая 21
                new Vector3(size, -size, -size),//нижняя правая 22
                new Vector3(-size, -size, -size),//нижняя левая 23
            };
            indices = new uint[] { 0, 1, 2, 2, 3, 0, 4, 5, 6, 6, 7, 4, 8, 9, 10, 10, 11, 8, 12, 13, 14, 14, 15, 12, 16, 17, 18, 18, 19, 16, 20, 21, 22, 22, 23, 20 };
            texCoords = new List<Vector2>() { 
            //перед
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
            //право
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
            //зад
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
            //лево
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
            //верх
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
            //низ
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f)};
            texture_path = "../../../Textures/mars.jpg";
        }
        
        public void Create_Jupiter()
        {
            size = 5.5f;
            vertices = new List<Vector3>()
            { //перед
                new Vector3(-size, size, -size),//верхняя левая 0
                new Vector3(size, size, -size),//верхняя правая 1
                new Vector3(size, -size, -size),//нижняя правая 2
                new Vector3(-size, -size, -size),//нижняя левая 3
              //право
                new Vector3(size, size, size),//верхняя левая 4
                new Vector3(size, size, -size),//верхняя правая 5
                new Vector3(size, -size, -size),//нижняя правая 6
                new Vector3(size, -size, size),//нижняя левая 7
              //зад
                new Vector3(-size, size, size),//верхняя левая 8
                new Vector3(size, size, size),//верхняя правая 9
                new Vector3(size, -size, size),//нижняя правая 10
                new Vector3(-size, -size, size),//нижняя левая 11
              //лево
                new Vector3(-size, size, -size),//верхняя левая 12
                new Vector3(-size, size, size),//верхняя правая 13
                new Vector3(-size, -size, size),//нижняя правая 14
                new Vector3(-size, -size, -size),//нижняя левая 15
              //верх
                new Vector3(-size, size, -size),//верхняя левая 16
                new Vector3(size, size, -size),//верхняя правая 17
                new Vector3(size, size, size),//нижняя правая 18
                new Vector3(-size, size, size),//нижняя левая 19
              //низ
                new Vector3(-size, -size, size),//верхняя левая 20
                new Vector3(size, -size, size),//верхняя правая 21
                new Vector3(size, -size, -size),//нижняя правая 22
                new Vector3(-size, -size, -size),//нижняя левая 23
            };
            indices = new uint[] { 0, 1, 2, 2, 3, 0, 4, 5, 6, 6, 7, 4, 8, 9, 10, 10, 11, 8, 12, 13, 14, 14, 15, 12, 16, 17, 18, 18, 19, 16, 20, 21, 22, 22, 23, 20 };
            texCoords = new List<Vector2>() { 
            //перед
            new Vector2(0.25f, 0.5f),
            new Vector2(0.5f, 0.5f),
            new Vector2(0.5f, 0f),
            new Vector2(0.25f, 0f),     
            //право
            new Vector2(0.75f, 0.5f),
            new Vector2(1f, 0.5f),
            new Vector2(1f, 0f),
            new Vector2(0.75f, 0f),
            //зад
            new Vector2(0.5f, 0.5f),
            new Vector2(0.75f, 0.5f),
            new Vector2(0.75f, 0f),
            new Vector2(0.5f, 0f),
            //лево
            new Vector2(0.25f, 0.5f),
            new Vector2(0f, 0.5f),
            new Vector2(0f, 0f),
            new Vector2(0.25f, 0f),
            //верх
            new Vector2(0.25f, 1f),
            new Vector2(0.5f, 1f),
            new Vector2(0.5f, 0.5f),
            new Vector2(0.25f, 0.5f),
            //низ
            new Vector2(0.5f, 1f),
            new Vector2(0.75f, 1f),
            new Vector2(0.75f, 0.5f),
            new Vector2(0.5f, 0.5f)};
            texture_path = "../../../Textures/jupiter.png";
        }

        public void Create_Saturn()
        {
            size = 3f;
            vertices = new List<Vector3>()
            { //перед
                new Vector3(-size, size, -size),//верхняя левая 0
                new Vector3(size, size, -size),//верхняя правая 1
                new Vector3(size, -size, -size),//нижняя правая 2
                new Vector3(-size, -size, -size),//нижняя левая 3
              //право
                new Vector3(size, size, size),//верхняя левая 4
                new Vector3(size, size, -size),//верхняя правая 5
                new Vector3(size, -size, -size),//нижняя правая 6
                new Vector3(size, -size, size),//нижняя левая 7
              //зад
                new Vector3(-size, size, size),//верхняя левая 8
                new Vector3(size, size, size),//верхняя правая 9
                new Vector3(size, -size, size),//нижняя правая 10
                new Vector3(-size, -size, size),//нижняя левая 11
              //лево
                new Vector3(-size, size, -size),//верхняя левая 12
                new Vector3(-size, size, size),//верхняя правая 13
                new Vector3(-size, -size, size),//нижняя правая 14
                new Vector3(-size, -size, -size),//нижняя левая 15
              //верх
                new Vector3(-size, size, -size),//верхняя левая 16
                new Vector3(size, size, -size),//верхняя правая 17
                new Vector3(size, size, size),//нижняя правая 18
                new Vector3(-size, size, size),//нижняя левая 19
              //низ
                new Vector3(-size, -size, size),//верхняя левая 20
                new Vector3(size, -size, size),//верхняя правая 21
                new Vector3(size, -size, -size),//нижняя правая 22
                new Vector3(-size, -size, -size),//нижняя левая 23
            };
            indices = new uint[] { 0, 1, 2, 2, 3, 0, 4, 5, 6, 6, 7, 4, 8, 9, 10, 10, 11, 8, 12, 13, 14, 14, 15, 12, 16, 17, 18, 18, 19, 16, 20, 21, 22, 22, 23, 20 };
            texCoords = new List<Vector2>() { 
            //перед
            new Vector2(0.25f, 0f),
            new Vector2(0.5f, 0f),
            new Vector2(0.5f, 0.5f),
            new Vector2(0.25f, 0.5f),
            //право
            new Vector2(0.75f, 0.5f),
            new Vector2(1f, 0.5f),
            new Vector2(1f, 0f),
            new Vector2(0.75f, 0f),
            //зад
            new Vector2(0.5f, 0.5f),
            new Vector2(0.75f, 0.5f),
            new Vector2(0.75f, 0f),
            new Vector2(0.5f, 0f),
            //лево
            new Vector2(0.25f, 0f),
            new Vector2(0f, 0f),
            new Vector2(0f, 0.5f),
            new Vector2(0.25f, 0.5f),
            //верх
            new Vector2(0.5f, 1f),
            new Vector2(0.25f, 1f),
            new Vector2(0.25f, 0.5f),
            new Vector2(0.5f, 0.5f),
            //низ
            new Vector2(0.5f, 0.5f),
            new Vector2(0.75f, 0.5f),
            new Vector2(0.75f, 1f),
            new Vector2(0.5f, 1f)
            };
            texture_path = "../../../Textures/saturn.png";
        }

        public void Create_Saturn_Rings()
        {
            size = 13f;
            vertices = new List<Vector3>()
            { //перед
                new Vector3(-size, 4f, size),//верхняя левая 0
                new Vector3(size, 4f, size),//верхняя правая 1
                new Vector3(size, -4f, -size),//нижняя правая 2
                new Vector3(-size, -4f, -size),//нижняя левая 3
            };
            indices = new uint[] { 0, 1, 2, 2, 3, 0, 4, 5, 6, 6, 7, 4, 8, 9, 10, 10, 11, 8, 12, 13, 14, 14, 15, 12, 16, 17, 18, 18, 19, 16, 20, 21, 22, 22, 23, 20 };
            texCoords = new List<Vector2>() { 
            //перед
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f)
            };
            texture_path = "../../../Textures/saturn_rings.png";
        }

        public void Create_Neptune()
        {
            size = 3.5f;
            vertices = new List<Vector3>()
            { //перед
                new Vector3(-size, size, -size),//верхняя левая 0
                new Vector3(size, size, -size),//верхняя правая 1
                new Vector3(size, -size, -size),//нижняя правая 2
                new Vector3(-size, -size, -size),//нижняя левая 3
              //право
                new Vector3(size, size, size),//верхняя левая 4
                new Vector3(size, size, -size),//верхняя правая 5
                new Vector3(size, -size, -size),//нижняя правая 6
                new Vector3(size, -size, size),//нижняя левая 7
              //зад
                new Vector3(-size, size, size),//верхняя левая 8
                new Vector3(size, size, size),//верхняя правая 9
                new Vector3(size, -size, size),//нижняя правая 10
                new Vector3(-size, -size, size),//нижняя левая 11
              //лево
                new Vector3(-size, size, -size),//верхняя левая 12
                new Vector3(-size, size, size),//верхняя правая 13
                new Vector3(-size, -size, size),//нижняя правая 14
                new Vector3(-size, -size, -size),//нижняя левая 15
              //верх
                new Vector3(-size, size, -size),//верхняя левая 16
                new Vector3(size, size, -size),//верхняя правая 17
                new Vector3(size, size, size),//нижняя правая 18
                new Vector3(-size, size, size),//нижняя левая 19
              //низ
                new Vector3(-size, -size, size),//верхняя левая 20
                new Vector3(size, -size, size),//верхняя правая 21
                new Vector3(size, -size, -size),//нижняя правая 22
                new Vector3(-size, -size, -size),//нижняя левая 23
            };
            indices = new uint[] { 0, 1, 2, 2, 3, 0, 4, 5, 6, 6, 7, 4, 8, 9, 10, 10, 11, 8, 12, 13, 14, 14, 15, 12, 16, 17, 18, 18, 19, 16, 20, 21, 22, 22, 23, 20 };
            texCoords = new List<Vector2>() { 
            //перед
            new Vector2(0.25f, 0.5f),
            new Vector2(0.5f, 0.5f),
            new Vector2(0.5f, 0f),
            new Vector2(0.25f, 0f),     
            //право
            new Vector2(0.75f, 0.5f),
            new Vector2(0.5f, 0.5f),
            new Vector2(0.5f, 0f),
            new Vector2(0.75f, 0f),
            //зад
            new Vector2(0.25f, 0.5f),
            new Vector2(0f, 0.5f),
            new Vector2(0f, 0f),
            new Vector2(0.25f, 0f),
            //лево
            new Vector2(0.75f, 0.5f),
            new Vector2(1f, 0.5f),
            new Vector2(1f, 0f),
            new Vector2(0.75f, 0f),
            //верх
            new Vector2(0.5f, 0.5f),
            new Vector2(0.25f, 0.5f),
            new Vector2(0.25f, 1f),
            new Vector2(0.5f, 1f),
            //низ
            new Vector2(0.5f, 1f),
            new Vector2(0.75f, 1f),
            new Vector2(0.75f, 0.5f),
            new Vector2(0.5f, 0.5f)};
            texture_path = "../../../Textures/neptune.png";
        }

        public void Create_Uranus()
        {
            size = 4f;
            vertices = new List<Vector3>()
            { //перед
                new Vector3(-size, size, -size),//верхняя левая 0
                new Vector3(size, size, -size),//верхняя правая 1
                new Vector3(size, -size, -size),//нижняя правая 2
                new Vector3(-size, -size, -size),//нижняя левая 3
              //право
                new Vector3(size, size, size),//верхняя левая 4
                new Vector3(size, size, -size),//верхняя правая 5
                new Vector3(size, -size, -size),//нижняя правая 6
                new Vector3(size, -size, size),//нижняя левая 7
              //зад
                new Vector3(-size, size, size),//верхняя левая 8
                new Vector3(size, size, size),//верхняя правая 9
                new Vector3(size, -size, size),//нижняя правая 10
                new Vector3(-size, -size, size),//нижняя левая 11
              //лево
                new Vector3(-size, size, -size),//верхняя левая 12
                new Vector3(-size, size, size),//верхняя правая 13
                new Vector3(-size, -size, size),//нижняя правая 14
                new Vector3(-size, -size, -size),//нижняя левая 15
              //верх
                new Vector3(-size, size, -size),//верхняя левая 16
                new Vector3(size, size, -size),//верхняя правая 17
                new Vector3(size, size, size),//нижняя правая 18
                new Vector3(-size, size, size),//нижняя левая 19
              //низ
                new Vector3(-size, -size, size),//верхняя левая 20
                new Vector3(size, -size, size),//верхняя правая 21
                new Vector3(size, -size, -size),//нижняя правая 22
                new Vector3(-size, -size, -size),//нижняя левая 23
            };
            indices = new uint[] { 0, 1, 2, 2, 3, 0, 4, 5, 6, 6, 7, 4, 8, 9, 10, 10, 11, 8, 12, 13, 14, 14, 15, 12, 16, 17, 18, 18, 19, 16, 20, 21, 22, 22, 23, 20 };
            texCoords = new List<Vector2>() { 
            //перед
            new Vector2(0.25f, 0.5f),
            new Vector2(0.5f, 0.5f),
            new Vector2(0.5f, 0f),
            new Vector2(0.25f, 0f),     
            //право
            new Vector2(0.75f, 0.5f),
            new Vector2(0.5f, 0.5f),
            new Vector2(0.5f, 0f),
            new Vector2(0.75f, 0f),
            //зад
            new Vector2(0.25f, 0.5f),
            new Vector2(0f, 0.5f),
            new Vector2(0f, 0f),
            new Vector2(0.25f, 0f),
            //лево
            new Vector2(0.75f, 0.5f),
            new Vector2(1f, 0.5f),
            new Vector2(1f, 0f),
            new Vector2(0.75f, 0f),
            //верх
            new Vector2(0.5f, 0.5f),
            new Vector2(0.25f, 0.5f),
            new Vector2(0.25f, 1f),
            new Vector2(0.5f, 1f),
            //низ
            new Vector2(0.5f, 1f),
            new Vector2(0.75f, 1f),
            new Vector2(0.75f, 0.5f),
            new Vector2(0.5f, 0.5f)};
            texture_path = "../../../Textures/uranus.png";
        }

        public void Create_Uranus_Rings()
        {
            size = 10f;
            vertices = new List<Vector3>()
            { //перед
                new Vector3(-size, 6f, size),//верхняя левая 0
                new Vector3(size, 6f, size),//верхняя правая 1
                new Vector3(size, -6f, -size),//нижняя правая 2
                new Vector3(-size, -6f, -size),//нижняя левая 3
            };
            indices = new uint[] { 0, 1, 2, 2, 3, 0, 4, 5, 6, 6, 7, 4, 8, 9, 10, 10, 11, 8, 12, 13, 14, 14, 15, 12, 16, 17, 18, 18, 19, 16, 20, 21, 22, 22, 23, 20 };
            texCoords = new List<Vector2>() { 
            //перед
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f)
            };
            texture_path = "../../../Textures/uranus_rings.png";
        }

        public void Create_Moon()
        {
            size = 0.5f;
            vertices = new List<Vector3>()
            { //перед
                new Vector3(-size, size, -size),//верхняя левая 0
                new Vector3(size, size, -size),//верхняя правая 1
                new Vector3(size, -size, -size),//нижняя правая 2
                new Vector3(-size, -size, -size),//нижняя левая 3
              //право
                new Vector3(size, size, size),//верхняя левая 4
                new Vector3(size, size, -size),//верхняя правая 5
                new Vector3(size, -size, -size),//нижняя правая 6
                new Vector3(size, -size, size),//нижняя левая 7
              //зад
                new Vector3(-size, size, size),//верхняя левая 8
                new Vector3(size, size, size),//верхняя правая 9
                new Vector3(size, -size, size),//нижняя правая 10
                new Vector3(-size, -size, size),//нижняя левая 11
              //лево
                new Vector3(-size, size, -size),//верхняя левая 12
                new Vector3(-size, size, size),//верхняя правая 13
                new Vector3(-size, -size, size),//нижняя правая 14
                new Vector3(-size, -size, -size),//нижняя левая 15
              //верх
                new Vector3(-size, size, -size),//верхняя левая 16
                new Vector3(size, size, -size),//верхняя правая 17
                new Vector3(size, size, size),//нижняя правая 18
                new Vector3(-size, size, size),//нижняя левая 19
              //низ
                new Vector3(-size, -size, size),//верхняя левая 20
                new Vector3(size, -size, size),//верхняя правая 21
                new Vector3(size, -size, -size),//нижняя правая 22
                new Vector3(-size, -size, -size),//нижняя левая 23
            };
            indices = new uint[] { 0, 1, 2, 2, 3, 0, 4, 5, 6, 6, 7, 4, 8, 9, 10, 10, 11, 8, 12, 13, 14, 14, 15, 12, 16, 17, 18, 18, 19, 16, 20, 21, 22, 22, 23, 20 };
            texCoords = new List<Vector2>() { 
            //перед
            new Vector2(0.25f, 0.5f),
            new Vector2(0.5f, 0.5f),
            new Vector2(0.5f, 0f),
            new Vector2(0.25f, 0f),     
            //право
            new Vector2(0.75f, 0.5f),
            new Vector2(0.5f, 0.5f),
            new Vector2(0.5f, 0f),
            new Vector2(0.75f, 0f),
            //зад
            new Vector2(0.25f, 0.5f),
            new Vector2(0f, 0.5f),
            new Vector2(0f, 0f),
            new Vector2(0.25f, 0f),
            //лево
            new Vector2(0.75f, 0.5f),
            new Vector2(1f, 0.5f),
            new Vector2(1f, 0f),
            new Vector2(0.75f, 0f),
            //верх
            new Vector2(0.5f, 0.5f),
            new Vector2(0.25f, 0.5f),
            new Vector2(0.25f, 1f),
            new Vector2(0.5f, 1f),
            //низ
            new Vector2(0.5f, 1f),
            new Vector2(0.75f, 1f),
            new Vector2(0.75f, 0.5f),
            new Vector2(0.5f, 0.5f)};
            texture_path = "../../../Textures/moon.png";
        }

        public void Create_Ganimed()
        {
            size = 0.8f;
            vertices = new List<Vector3>()
            { //перед
                new Vector3(-size, size, -size),//верхняя левая 0
                new Vector3(size, size, -size),//верхняя правая 1
                new Vector3(size, -size, -size),//нижняя правая 2
                new Vector3(-size, -size, -size),//нижняя левая 3
              //право
                new Vector3(size, size, size),//верхняя левая 4
                new Vector3(size, size, -size),//верхняя правая 5
                new Vector3(size, -size, -size),//нижняя правая 6
                new Vector3(size, -size, size),//нижняя левая 7
              //зад
                new Vector3(-size, size, size),//верхняя левая 8
                new Vector3(size, size, size),//верхняя правая 9
                new Vector3(size, -size, size),//нижняя правая 10
                new Vector3(-size, -size, size),//нижняя левая 11
              //лево
                new Vector3(-size, size, -size),//верхняя левая 12
                new Vector3(-size, size, size),//верхняя правая 13
                new Vector3(-size, -size, size),//нижняя правая 14
                new Vector3(-size, -size, -size),//нижняя левая 15
              //верх
                new Vector3(-size, size, -size),//верхняя левая 16
                new Vector3(size, size, -size),//верхняя правая 17
                new Vector3(size, size, size),//нижняя правая 18
                new Vector3(-size, size, size),//нижняя левая 19
              //низ
                new Vector3(-size, -size, size),//верхняя левая 20
                new Vector3(size, -size, size),//верхняя правая 21
                new Vector3(size, -size, -size),//нижняя правая 22
                new Vector3(-size, -size, -size),//нижняя левая 23
            };
            indices = new uint[] { 0, 1, 2, 2, 3, 0, 4, 5, 6, 6, 7, 4, 8, 9, 10, 10, 11, 8, 12, 13, 14, 14, 15, 12, 16, 17, 18, 18, 19, 16, 20, 21, 22, 22, 23, 20 };
            texCoords = new List<Vector2>() { 
            //перед
            new Vector2(0.25f, 0.5f),
            new Vector2(0.5f, 0.5f),
            new Vector2(0.5f, 0f),
            new Vector2(0.25f, 0f),     
            //право
            new Vector2(0.75f, 0.5f),
            new Vector2(0.5f, 0.5f),
            new Vector2(0.5f, 0f),
            new Vector2(0.75f, 0f),
            //зад
            new Vector2(0.25f, 0.5f),
            new Vector2(0f, 0.5f),
            new Vector2(0f, 0f),
            new Vector2(0.25f, 0f),
            //лево
            new Vector2(0.75f, 0.5f),
            new Vector2(1f, 0.5f),
            new Vector2(1f, 0f),
            new Vector2(0.75f, 0f),
            //верх
            new Vector2(0.5f, 0.5f),
            new Vector2(0.25f, 0.5f),
            new Vector2(0.25f, 1f),
            new Vector2(0.5f, 1f),
            //низ
            new Vector2(0.5f, 1f),
            new Vector2(0.75f, 1f),
            new Vector2(0.75f, 0.5f),
            new Vector2(0.5f, 0.5f)};
            texture_path = "../../../Textures/ganimed.png";
        }

        public void Create_Europa()
        {
            size = 0.4f;
            vertices = new List<Vector3>()
            { //перед
                new Vector3(-size, size, -size),//верхняя левая 0
                new Vector3(size, size, -size),//верхняя правая 1
                new Vector3(size, -size, -size),//нижняя правая 2
                new Vector3(-size, -size, -size),//нижняя левая 3
              //право
                new Vector3(size, size, size),//верхняя левая 4
                new Vector3(size, size, -size),//верхняя правая 5
                new Vector3(size, -size, -size),//нижняя правая 6
                new Vector3(size, -size, size),//нижняя левая 7
              //зад
                new Vector3(-size, size, size),//верхняя левая 8
                new Vector3(size, size, size),//верхняя правая 9
                new Vector3(size, -size, size),//нижняя правая 10
                new Vector3(-size, -size, size),//нижняя левая 11
              //лево
                new Vector3(-size, size, -size),//верхняя левая 12
                new Vector3(-size, size, size),//верхняя правая 13
                new Vector3(-size, -size, size),//нижняя правая 14
                new Vector3(-size, -size, -size),//нижняя левая 15
              //верх
                new Vector3(-size, size, -size),//верхняя левая 16
                new Vector3(size, size, -size),//верхняя правая 17
                new Vector3(size, size, size),//нижняя правая 18
                new Vector3(-size, size, size),//нижняя левая 19
              //низ
                new Vector3(-size, -size, size),//верхняя левая 20
                new Vector3(size, -size, size),//верхняя правая 21
                new Vector3(size, -size, -size),//нижняя правая 22
                new Vector3(-size, -size, -size),//нижняя левая 23
            };
            indices = new uint[] { 0, 1, 2, 2, 3, 0, 4, 5, 6, 6, 7, 4, 8, 9, 10, 10, 11, 8, 12, 13, 14, 14, 15, 12, 16, 17, 18, 18, 19, 16, 20, 21, 22, 22, 23, 20 };
            texCoords = new List<Vector2>() { 
            //перед
            new Vector2(0.25f, 0.5f),
            new Vector2(0.5f, 0.5f),
            new Vector2(0.5f, 0f),
            new Vector2(0.25f, 0f),     
            //право
            new Vector2(0.75f, 0.5f),
            new Vector2(0.5f, 0.5f),
            new Vector2(0.5f, 0f),
            new Vector2(0.75f, 0f),
            //зад
            new Vector2(0.25f, 0.5f),
            new Vector2(0f, 0.5f),
            new Vector2(0f, 0f),
            new Vector2(0.25f, 0f),
            //лево
            new Vector2(0.75f, 0.5f),
            new Vector2(1f, 0.5f),
            new Vector2(1f, 0f),
            new Vector2(0.75f, 0f),
            //верх
            new Vector2(0.5f, 0.5f),
            new Vector2(0.25f, 0.5f),
            new Vector2(0.25f, 1f),
            new Vector2(0.5f, 1f),
            //низ
            new Vector2(0.5f, 1f),
            new Vector2(0.75f, 1f),
            new Vector2(0.75f, 0.5f),
            new Vector2(0.5f, 0.5f)};
            texture_path = "../../../Textures/europa.png";
        }

        public void Create_Callisto()
        {
            size = 0.65f;
            vertices = new List<Vector3>()
            { //перед
                new Vector3(-size, size, -size),//верхняя левая 0
                new Vector3(size, size, -size),//верхняя правая 1
                new Vector3(size, -size, -size),//нижняя правая 2
                new Vector3(-size, -size, -size),//нижняя левая 3
              //право
                new Vector3(size, size, size),//верхняя левая 4
                new Vector3(size, size, -size),//верхняя правая 5
                new Vector3(size, -size, -size),//нижняя правая 6
                new Vector3(size, -size, size),//нижняя левая 7
              //зад
                new Vector3(-size, size, size),//верхняя левая 8
                new Vector3(size, size, size),//верхняя правая 9
                new Vector3(size, -size, size),//нижняя правая 10
                new Vector3(-size, -size, size),//нижняя левая 11
              //лево
                new Vector3(-size, size, -size),//верхняя левая 12
                new Vector3(-size, size, size),//верхняя правая 13
                new Vector3(-size, -size, size),//нижняя правая 14
                new Vector3(-size, -size, -size),//нижняя левая 15
              //верх
                new Vector3(-size, size, -size),//верхняя левая 16
                new Vector3(size, size, -size),//верхняя правая 17
                new Vector3(size, size, size),//нижняя правая 18
                new Vector3(-size, size, size),//нижняя левая 19
              //низ
                new Vector3(-size, -size, size),//верхняя левая 20
                new Vector3(size, -size, size),//верхняя правая 21
                new Vector3(size, -size, -size),//нижняя правая 22
                new Vector3(-size, -size, -size),//нижняя левая 23
            };
            indices = new uint[] { 0, 1, 2, 2, 3, 0, 4, 5, 6, 6, 7, 4, 8, 9, 10, 10, 11, 8, 12, 13, 14, 14, 15, 12, 16, 17, 18, 18, 19, 16, 20, 21, 22, 22, 23, 20 };
            texCoords = new List<Vector2>() { 
            //перед
            new Vector2(0.25f, 0.5f),
            new Vector2(0.5f, 0.5f),
            new Vector2(0.5f, 0f),
            new Vector2(0.25f, 0f),     
            //право
            new Vector2(0.75f, 0.5f),
            new Vector2(0.5f, 0.5f),
            new Vector2(0.5f, 0f),
            new Vector2(0.75f, 0f),
            //зад
            new Vector2(0.25f, 0.5f),
            new Vector2(0f, 0.5f),
            new Vector2(0f, 0f),
            new Vector2(0.25f, 0f),
            //лево
            new Vector2(0.75f, 0.5f),
            new Vector2(1f, 0.5f),
            new Vector2(1f, 0f),
            new Vector2(0.75f, 0f),
            //верх
            new Vector2(0.5f, 0.5f),
            new Vector2(0.25f, 0.5f),
            new Vector2(0.25f, 1f),
            new Vector2(0.5f, 1f),
            //низ
            new Vector2(0.5f, 1f),
            new Vector2(0.75f, 1f),
            new Vector2(0.75f, 0.5f),
            new Vector2(0.5f, 0.5f)};
            texture_path = "../../../Textures/callisto.png";
        }

        public void Create_Io()
        {
            size = 0.5f;
            vertices = new List<Vector3>()
            { //перед
                new Vector3(-size, size, -size),//верхняя левая 0
                new Vector3(size, size, -size),//верхняя правая 1
                new Vector3(size, -size, -size),//нижняя правая 2
                new Vector3(-size, -size, -size),//нижняя левая 3
              //право
                new Vector3(size, size, size),//верхняя левая 4
                new Vector3(size, size, -size),//верхняя правая 5
                new Vector3(size, -size, -size),//нижняя правая 6
                new Vector3(size, -size, size),//нижняя левая 7
              //зад
                new Vector3(-size, size, size),//верхняя левая 8
                new Vector3(size, size, size),//верхняя правая 9
                new Vector3(size, -size, size),//нижняя правая 10
                new Vector3(-size, -size, size),//нижняя левая 11
              //лево
                new Vector3(-size, size, -size),//верхняя левая 12
                new Vector3(-size, size, size),//верхняя правая 13
                new Vector3(-size, -size, size),//нижняя правая 14
                new Vector3(-size, -size, -size),//нижняя левая 15
              //верх
                new Vector3(-size, size, -size),//верхняя левая 16
                new Vector3(size, size, -size),//верхняя правая 17
                new Vector3(size, size, size),//нижняя правая 18
                new Vector3(-size, size, size),//нижняя левая 19
              //низ
                new Vector3(-size, -size, size),//верхняя левая 20
                new Vector3(size, -size, size),//верхняя правая 21
                new Vector3(size, -size, -size),//нижняя правая 22
                new Vector3(-size, -size, -size),//нижняя левая 23
            };
            indices = new uint[] { 0, 1, 2, 2, 3, 0, 4, 5, 6, 6, 7, 4, 8, 9, 10, 10, 11, 8, 12, 13, 14, 14, 15, 12, 16, 17, 18, 18, 19, 16, 20, 21, 22, 22, 23, 20 };
            texCoords = new List<Vector2>() { 
            //перед
            new Vector2(0.25f, 0.5f),
            new Vector2(0.5f, 0.5f),
            new Vector2(0.5f, 0f),
            new Vector2(0.25f, 0f),     
            //право
            new Vector2(0.75f, 0.5f),
            new Vector2(0.5f, 0.5f),
            new Vector2(0.5f, 0f),
            new Vector2(0.75f, 0f),
            //зад
            new Vector2(0.25f, 0.5f),
            new Vector2(0f, 0.5f),
            new Vector2(0f, 0f),
            new Vector2(0.25f, 0f),
            //лево
            new Vector2(0.75f, 0.5f),
            new Vector2(1f, 0.5f),
            new Vector2(1f, 0f),
            new Vector2(0.75f, 0f),
            //верх
            new Vector2(0.5f, 0.5f),
            new Vector2(0.25f, 0.5f),
            new Vector2(0.25f, 1f),
            new Vector2(0.5f, 1f),
            //низ
            new Vector2(0.5f, 1f),
            new Vector2(0.75f, 1f),
            new Vector2(0.75f, 0.5f),
            new Vector2(0.5f, 0.5f)};
            texture_path = "../../../Textures/io.png";
        }

        public void Create_Phobos()
        {
            size = 0.2f;
            vertices = new List<Vector3>()
            { //перед
                new Vector3(-size, size, -size),//верхняя левая 0
                new Vector3(size, size, -size),//верхняя правая 1
                new Vector3(size, -size, -size),//нижняя правая 2
                new Vector3(-size, -size, -size),//нижняя левая 3
              //право
                new Vector3(size, size, size),//верхняя левая 4
                new Vector3(size, size, -size),//верхняя правая 5
                new Vector3(size, -size, -size),//нижняя правая 6
                new Vector3(size, -size, size),//нижняя левая 7
              //зад
                new Vector3(-size, size, size),//верхняя левая 8
                new Vector3(size, size, size),//верхняя правая 9
                new Vector3(size, -size, size),//нижняя правая 10
                new Vector3(-size, -size, size),//нижняя левая 11
              //лево
                new Vector3(-size, size, -size),//верхняя левая 12
                new Vector3(-size, size, size),//верхняя правая 13
                new Vector3(-size, -size, size),//нижняя правая 14
                new Vector3(-size, -size, -size),//нижняя левая 15
              //верх
                new Vector3(-size, size, -size),//верхняя левая 16
                new Vector3(size, size, -size),//верхняя правая 17
                new Vector3(size, size, size),//нижняя правая 18
                new Vector3(-size, size, size),//нижняя левая 19
              //низ
                new Vector3(-size, -size, size),//верхняя левая 20
                new Vector3(size, -size, size),//верхняя правая 21
                new Vector3(size, -size, -size),//нижняя правая 22
                new Vector3(-size, -size, -size),//нижняя левая 23
            };
            indices = new uint[] { 0, 1, 2, 2, 3, 0, 4, 5, 6, 6, 7, 4, 8, 9, 10, 10, 11, 8, 12, 13, 14, 14, 15, 12, 16, 17, 18, 18, 19, 16, 20, 21, 22, 22, 23, 20 };
            texCoords = new List<Vector2>() { 
            //перед
            new Vector2(0.25f, 0.5f),
            new Vector2(0.5f, 0.5f),
            new Vector2(0.5f, 0f),
            new Vector2(0.25f, 0f),     
            //право
            new Vector2(0.75f, 0.5f),
            new Vector2(0.5f, 0.5f),
            new Vector2(0.5f, 0f),
            new Vector2(0.75f, 0f),
            //зад
            new Vector2(0.25f, 0.5f),
            new Vector2(0f, 0.5f),
            new Vector2(0f, 0f),
            new Vector2(0.25f, 0f),
            //лево
            new Vector2(0.75f, 0.5f),
            new Vector2(1f, 0.5f),
            new Vector2(1f, 0f),
            new Vector2(0.75f, 0f),
            //верх
            new Vector2(0.5f, 0.5f),
            new Vector2(0.25f, 0.5f),
            new Vector2(0.25f, 1f),
            new Vector2(0.5f, 1f),
            //низ
            new Vector2(0.5f, 1f),
            new Vector2(0.75f, 1f),
            new Vector2(0.75f, 0.5f),
            new Vector2(0.5f, 0.5f)};
            texture_path = "../../../Textures/phobos.png";
        }

        public void Create_Nibiru()
        {
            size = 1f;
            vertices = new List<Vector3>()
            { //перед
                new Vector3(-size, size, -size),//верхняя левая 0
                new Vector3(size, size, -size),//верхняя правая 1
                new Vector3(size, -size, -size),//нижняя правая 2
                new Vector3(-size, -size, -size),//нижняя левая 3
              //право
                new Vector3(size, size, size),//верхняя левая 4
                new Vector3(size, size, -size),//верхняя правая 5
                new Vector3(size, -size, -size),//нижняя правая 6
                new Vector3(size, -size, size),//нижняя левая 7
              //зад
                new Vector3(-size, size, size),//верхняя левая 8
                new Vector3(size, size, size),//верхняя правая 9
                new Vector3(size, -size, size),//нижняя правая 10
                new Vector3(-size, -size, size),//нижняя левая 11
              //лево
                new Vector3(-size, size, -size),//верхняя левая 12
                new Vector3(-size, size, size),//верхняя правая 13
                new Vector3(-size, -size, size),//нижняя правая 14
                new Vector3(-size, -size, -size),//нижняя левая 15
              //верх
                new Vector3(-size, size, -size),//верхняя левая 16
                new Vector3(size, size, -size),//верхняя правая 17
                new Vector3(size, size, size),//нижняя правая 18
                new Vector3(-size, size, size),//нижняя левая 19
              //низ
                new Vector3(-size, -size, size),//верхняя левая 20
                new Vector3(size, -size, size),//верхняя правая 21
                new Vector3(size, -size, -size),//нижняя правая 22
                new Vector3(-size, -size, -size),//нижняя левая 23
            };
            indices = new uint[] { 0, 1, 2, 2, 3, 0, 4, 5, 6, 6, 7, 4, 8, 9, 10, 10, 11, 8, 12, 13, 14, 14, 15, 12, 16, 17, 18, 18, 19, 16, 20, 21, 22, 22, 23, 20 };
            texCoords = new List<Vector2>() { 
            //перед
            new Vector2(0.5f, 0.5f),
            new Vector2(0.25f, 0.5f),
            new Vector2(0.25f, 0f),
            new Vector2(0.5f, 0f),
            //право
            new Vector2(0f, 0.5f),
            new Vector2(0.25f, 0.5f),
            new Vector2(0.25f, 0f),
            new Vector2(0f, 0f),
            //зад
            new Vector2(0.75f, 0f),
            new Vector2(1f, 0f),
            new Vector2(1f, 0.5f),
            new Vector2(0.75f, 0.5f),
            //лево
            new Vector2(0.5f, 0.5f),
            new Vector2(0.75f, 0.5f),
            new Vector2(0.5f, 0f),
            new Vector2(0.75f, 0f),
            //верх
            new Vector2(0.5f, 1f),
            new Vector2(0.75f, 1f),
            new Vector2(0.75f, 0.5f),
            new Vector2(0.5f, 0.5f),
            //низ
            new Vector2(0.5f, 0.5f),
            new Vector2(0.25f, 0.5f),
            new Vector2(0.25f, 1f),
            new Vector2(0.5f, 1f)};
            texture_path = "../../../Textures/nibiru.png";
        }
        
        public void Create_Asteroid1()
        {
            size = 0.3f;
            vertices = new List<Vector3>()
            { //перед
                new Vector3(-size, size, -size),//верхняя левая 0
                new Vector3(size, size, -size),//верхняя правая 1
                new Vector3(size, -size, -size),//нижняя правая 2
                new Vector3(-size, -size, -size),//нижняя левая 3
              //право
                new Vector3(size, size, size),//верхняя левая 4
                new Vector3(size, size, -size),//верхняя правая 5
                new Vector3(size, -size, -size),//нижняя правая 6
                new Vector3(size, -size, size),//нижняя левая 7
              //зад
                new Vector3(-size, size, size),//верхняя левая 8
                new Vector3(size, size, size),//верхняя правая 9
                new Vector3(size, -size, size),//нижняя правая 10
                new Vector3(-size, -size, size),//нижняя левая 11
              //лево
                new Vector3(-size, size, -size),//верхняя левая 12
                new Vector3(-size, size, size),//верхняя правая 13
                new Vector3(-size, -size, size),//нижняя правая 14
                new Vector3(-size, -size, -size),//нижняя левая 15
              //верх
                new Vector3(-size, size, -size),//верхняя левая 16
                new Vector3(size, size, -size),//верхняя правая 17
                new Vector3(size, size, size),//нижняя правая 18
                new Vector3(-size, size, size),//нижняя левая 19
              //низ
                new Vector3(-size, -size, size),//верхняя левая 20
                new Vector3(size, -size, size),//верхняя правая 21
                new Vector3(size, -size, -size),//нижняя правая 22
                new Vector3(-size, -size, -size),//нижняя левая 23
            };
            indices = new uint[] { 0, 1, 2, 2, 3, 0, 4, 5, 6, 6, 7, 4, 8, 9, 10, 10, 11, 8, 12, 13, 14, 14, 15, 12, 16, 17, 18, 18, 19, 16, 20, 21, 22, 22, 23, 20 };
            texCoords = new List<Vector2>() { 
            //перед
            new Vector2(0.25f, 0.5f),
            new Vector2(0.5f, 0.5f),
            new Vector2(0.5f, 0f),
            new Vector2(0.25f, 0f),     
            //право
            new Vector2(0.75f, 0.5f),
            new Vector2(0.5f, 0.5f),
            new Vector2(0.5f, 0f),
            new Vector2(0.75f, 0f),
            //зад
            new Vector2(0.25f, 0.5f),
            new Vector2(0f, 0.5f),
            new Vector2(0f, 0f),
            new Vector2(0.25f, 0f),
            //лево
            new Vector2(0.75f, 0.5f),
            new Vector2(1f, 0.5f),
            new Vector2(1f, 0f),
            new Vector2(0.75f, 0f),
            //верх
            new Vector2(0.5f, 0.5f),
            new Vector2(0.25f, 0.5f),
            new Vector2(0.25f, 1f),
            new Vector2(0.5f, 1f),
            //низ
            new Vector2(0.5f, 1f),
            new Vector2(0.75f, 1f),
            new Vector2(0.75f, 0.5f),
            new Vector2(0.5f, 0.5f)};
            texture_path = "../../../Textures/asteroid1.png";
        }

        public void Create_Asteroid2()
        {
            size = 0.35f;
            vertices = new List<Vector3>()
            { //перед
                new Vector3(-size, size, -size),//верхняя левая 0
                new Vector3(size, size, -size),//верхняя правая 1
                new Vector3(size, -size, -size),//нижняя правая 2
                new Vector3(-size, -size, -size),//нижняя левая 3
              //право
                new Vector3(size, size, size),//верхняя левая 4
                new Vector3(size, size, -size),//верхняя правая 5
                new Vector3(size, -size, -size),//нижняя правая 6
                new Vector3(size, -size, size),//нижняя левая 7
              //зад
                new Vector3(-size, size, size),//верхняя левая 8
                new Vector3(size, size, size),//верхняя правая 9
                new Vector3(size, -size, size),//нижняя правая 10
                new Vector3(-size, -size, size),//нижняя левая 11
              //лево
                new Vector3(-size, size, -size),//верхняя левая 12
                new Vector3(-size, size, size),//верхняя правая 13
                new Vector3(-size, -size, size),//нижняя правая 14
                new Vector3(-size, -size, -size),//нижняя левая 15
              //верх
                new Vector3(-size, size, -size),//верхняя левая 16
                new Vector3(size, size, -size),//верхняя правая 17
                new Vector3(size, size, size),//нижняя правая 18
                new Vector3(-size, size, size),//нижняя левая 19
              //низ
                new Vector3(-size, -size, size),//верхняя левая 20
                new Vector3(size, -size, size),//верхняя правая 21
                new Vector3(size, -size, -size),//нижняя правая 22
                new Vector3(-size, -size, -size),//нижняя левая 23
            };
            indices = new uint[] { 0, 1, 2, 2, 3, 0, 4, 5, 6, 6, 7, 4, 8, 9, 10, 10, 11, 8, 12, 13, 14, 14, 15, 12, 16, 17, 18, 18, 19, 16, 20, 21, 22, 22, 23, 20 };
            texCoords = new List<Vector2>() { 
            //перед
            new Vector2(0.25f, 0.5f),
            new Vector2(0.5f, 0.5f),
            new Vector2(0.5f, 0f),
            new Vector2(0.25f, 0f),     
            //право
            new Vector2(0.75f, 0.5f),
            new Vector2(0.5f, 0.5f),
            new Vector2(0.5f, 0f),
            new Vector2(0.75f, 0f),
            //зад
            new Vector2(0.25f, 0.5f),
            new Vector2(0f, 0.5f),
            new Vector2(0f, 0f),
            new Vector2(0.25f, 0f),
            //лево
            new Vector2(0.75f, 0.5f),
            new Vector2(1f, 0.5f),
            new Vector2(1f, 0f),
            new Vector2(0.75f, 0f),
            //верх
            new Vector2(0.5f, 0.5f),
            new Vector2(0.25f, 0.5f),
            new Vector2(0.25f, 1f),
            new Vector2(0.5f, 1f),
            //низ
            new Vector2(0.5f, 1f),
            new Vector2(0.75f, 1f),
            new Vector2(0.75f, 0.5f),
            new Vector2(0.5f, 0.5f)};
            texture_path = "../../../Textures/asteroid2.png";
        }

        public void Create_UFO()
        {
            size = 0.5f;
            vertices = new List<Vector3>()
            { //перед
                new Vector3(-size, -0.4f, -size),//верхняя левая 0
                new Vector3(size, -0.4f, -size),//верхняя правая 1
                new Vector3(size, -0.9f, -size),//нижняя правая 2
                new Vector3(-size, -0.9f, -size),//нижняя левая 3
            };
            indices = new uint[] { 0, 1, 2, 2, 3, 0};
            texCoords = new List<Vector2>() { 
            //перед
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f)};
            texture_path = "../../../Textures/ufo.png";
        }
    }
}
