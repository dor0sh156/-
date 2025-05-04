using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace Doroshok4
{
    public partial class Egor4 : Form
    {
        int k, l; // элементы матрицы сдвига
        float[,] Tetraider = new float[4, 4]; // матрица тела фигуры (4 вершины)
        float[,] osi = new float[4, 3]; // матрица координат осей
        float[,] matr_sdv = new float[4, 4]; //матрица преобразования сдвига
        float[,] matr_pov = new float[4, 4]; //матрица преобразования поворота
        private double currentRotationAngleX = 0;
        private double currentRotationAngleY = 0;
        private double currentRotationAngleZ = 0;
        private float[,] originalTetraider = new float[4, 4];
        private float[,] matr_scale = new float[4, 4];
        private float[,] previousTetraider = null; // Переменная для хранения предыдущего состояния фигуры


        public Egor4()
        {
            InitializeComponent();
            Init_figure(); // Инициализируем правильный тетраэдр
            Array.Copy(Tetraider, originalTetraider, Tetraider.Length); // Сохраняем исходное состояние
            Init_osi(); // Инициализируем координаты осей XYZ
            this.Load += Form1_Load;
        }

        private void Init_matr_scale(float s)
        {
            matr_scale[0, 0] = s; matr_scale[0, 1] = 0; matr_scale[0, 2] = 0; matr_scale[0, 3] = 0;
            matr_scale[1, 0] = 0; matr_scale[1, 1] = s; matr_scale[1, 2] = 0; matr_scale[1, 3] = 0;
            matr_scale[2, 0] = 0; matr_scale[2, 1] = 0; matr_scale[2, 2] = s; matr_scale[2, 3] = 0;
            matr_scale[3, 0] = 0; matr_scale[3, 1] = 0; matr_scale[3, 2] = 0; matr_scale[3, 3] = 1;
        }

        private void DrawStaticAxes()
        {
            int centerX = pictureBox1.Width / 2;
            int centerY = pictureBox1.Height / 2;
            int length = 150;

            Pen axisPenX = new Pen(Color.Red, 2);
            Pen axisPenY = new Pen(Color.Green, 2);
            Pen axisPenZ = new Pen(Color.Blue, 2);
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            Font axisFont = new Font("Arial", 10);
            SolidBrush textBrush = new SolidBrush(Color.Black);

            float x0 = centerX;
            float y0 = centerY;

            float xX = x0 + length;
            float yX = y0;

            float xY = x0;
            float yY = y0 - length;

            // Ось Z теперь направлена влево-вниз
            float xZ = x0 - length / (float)Math.Sqrt(2);
            float yZ = y0 + length / (float)Math.Sqrt(2);

            g.DrawLine(axisPenX, x0, y0, xX, yX);
            g.DrawLine(axisPenY, x0, y0, xY, yY);
            g.DrawLine(axisPenZ, x0, y0, xZ, yZ);

            g.DrawString("X", axisFont, textBrush, xX + 5, yX);
            g.DrawString("Y", axisFont, textBrush, xY + 5, yY - 15);
            g.DrawString("Z", axisFont, textBrush, xZ - 15, yZ + 5);

            g.Dispose();
            axisPenX.Dispose();
            axisPenY.Dispose();
            axisPenZ.Dispose();
            axisFont.Dispose();
            textBrush.Dispose();
        }

        private void DrawFigureAndStaticAxes()
        {
            ClearDrawing();
            DrawStaticAxes();
        }

        private void ClearDrawing()
        {
            pictureBox1.Image = null;
            pictureBox1.Refresh();
        }

        private void ResetFigure()
        {
            Array.Copy(originalTetraider, Tetraider, originalTetraider.Length);
            CenterFigure(); // Центрируем фигуру после сброса
            Draw_Figure();
            currentRotationAngleX = 0;
            currentRotationAngleY = 0;
            currentRotationAngleZ = 0;
            Init_matr_pov_x(0);
            Init_matr_pov_y(0);
            Init_matr_pov_z(0);
        }

        private void Init_figure() // Инициализация правильного тетраэдра
        {
            float a = 150; // Длина ребра тетраэдра
            Tetraider[0, 0] = 0; Tetraider[0, 1] = a / (float)Math.Sqrt(6); Tetraider[0, 2] = a / (2 * (float)Math.Sqrt(3)); Tetraider[0, 3] = 1;
            Tetraider[1, 0] = -a / 2; Tetraider[1, 1] = -a / (float)Math.Sqrt(6); Tetraider[1, 2] = a / (2 * (float)Math.Sqrt(3)); Tetraider[1, 3] = 1;
            Tetraider[2, 0] = a / 2; Tetraider[2, 1] = -a / (float)Math.Sqrt(6); Tetraider[2, 2] = a / (2 * (float)Math.Sqrt(3)); Tetraider[2, 3] = 1;
            Tetraider[3, 0] = 0; Tetraider[3, 1] = 0; Tetraider[3, 2] = -a / (float)Math.Sqrt(3); Tetraider[3, 3] = 1;
        }

        private void CenterFigure()
        {
            k = pictureBox1.Width / 2;
            l = pictureBox1.Height / 2;
        }

        private void Draw_Figure()
        {
            Init_matr_sdv(k, l);
            float[,] temp = Multiply_matr(Tetraider, matr_pov);
            float[,] Tetraider1 = Multiply_matr(temp, matr_sdv);

            Pen myPen = new Pen(Color.Blue, 2);
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

            g.DrawLine(myPen, Tetraider1[0, 0], Tetraider1[0, 1], Tetraider1[1, 0], Tetraider1[1, 1]);
            g.DrawLine(myPen, Tetraider1[0, 0], Tetraider1[0, 1], Tetraider1[2, 0], Tetraider1[2, 1]);
            g.DrawLine(myPen, Tetraider1[0, 0], Tetraider1[0, 1], Tetraider1[3, 0], Tetraider1[3, 1]);
            g.DrawLine(myPen, Tetraider1[1, 0], Tetraider1[1, 1], Tetraider1[2, 0], Tetraider1[2, 1]);
            g.DrawLine(myPen, Tetraider1[2, 0], Tetraider1[2, 1], Tetraider1[3, 0], Tetraider1[3, 1]);
            g.DrawLine(myPen, Tetraider1[3, 0], Tetraider1[3, 1], Tetraider1[1, 0], Tetraider1[1, 1]);

            g.Dispose();
            myPen.Dispose();
        }

        private void Init_matr_sdv(int k1, int l1)
        {
            matr_sdv[0, 0] = 1; matr_sdv[0, 1] = 0; matr_sdv[0, 2] = 0; matr_sdv[0, 3] = 0;
            matr_sdv[1, 0] = 0; matr_sdv[1, 1] = 1; matr_sdv[1, 2] = 0; matr_sdv[1, 3] = 0;
            matr_sdv[2, 0] = 0; matr_sdv[2, 1] = 0; matr_sdv[2, 2] = 1; matr_sdv[2, 3] = 0;
            matr_sdv[3, 0] = k1; matr_sdv[3, 1] = l1; matr_sdv[3, 2] = 0; matr_sdv[3, 3] = 1;
        }

        private void Init_matr_pov_x(double angleRad)
        {
            double cosA = Math.Cos(angleRad);
            double sinA = Math.Sin(angleRad);
            matr_pov[0, 0] = 1; matr_pov[0, 1] = 0; matr_pov[0, 2] = 0; matr_pov[0, 3] = 0;
            matr_pov[1, 0] = 0; matr_pov[1, 1] = (float)cosA; matr_pov[1, 2] = (float)sinA; matr_pov[1, 3] = 0;
            matr_pov[2, 0] = 0; matr_pov[2, 1] = (float)-sinA; matr_pov[2, 2] = (float)cosA; matr_pov[2, 3] = 0;
            matr_pov[3, 0] = 0; matr_pov[3, 1] = 0; matr_pov[3, 2] = 0; matr_pov[3, 3] = 1;
        }

        private void Init_matr_pov_y(double angleRad)
        {
            double cosA = Math.Cos(angleRad);
            double sinA = Math.Sin(angleRad);
            matr_pov[0, 0] = (float)cosA; matr_pov[0, 1] = 0; matr_pov[0, 2] = (float)-sinA; matr_pov[0, 3] = 0;
            matr_pov[1, 0] = 0; matr_pov[1, 1] = 1; matr_pov[1, 2] = 0; matr_pov[1, 3] = 0;
            matr_pov[2, 0] = (float)sinA; matr_pov[2, 1] = 0; matr_pov[2, 2] = (float)cosA; matr_pov[2, 3] = 0;
            matr_pov[3, 0] = 0; matr_pov[3, 1] = 0; matr_pov[3, 2] = 0; matr_pov[3, 3] = 1;
        }

        private void Init_matr_pov_z(double angleRad)
        {
            double cosA = Math.Cos(angleRad);
            double sinA = Math.Sin(angleRad);
            matr_pov[0, 0] = (float)cosA; matr_pov[0, 1] = (float)sinA; matr_pov[0, 2] = 0; matr_pov[0, 3] = 0;
            matr_pov[1, 0] = (float)-sinA; matr_pov[1, 1] = (float)cosA; matr_pov[1, 2] = 0; matr_pov[1, 3] = 0;
            matr_pov[2, 0] = 0; matr_pov[2, 1] = 0; matr_pov[2, 2] = 1; matr_pov[2, 3] = 0;
            matr_pov[3, 0] = 0; matr_pov[3, 1] = 0; matr_pov[3, 2] = 0; matr_pov[3, 3] = 1;
        }

        private void Init_osi()
        {
            int length = 150; // Длина осей

            osi[0, 0] = 0; osi[0, 1] = 0; osi[0, 2] = 0; // Начало координат
            osi[1, 0] = length; osi[1, 1] = 0; osi[1, 2] = 0; // Ось X
            osi[2, 0] = 0; osi[2, 1] = length; osi[2, 2] = 0; // Ось Y
            osi[3, 0] = 0; osi[3, 1] = 0; osi[3, 2] = -length; // Ось Z (направлена "вглубь" экрана)
        }

        private float[,] Multiply_matr(float[,] a, float[,] b)
        {
            int n = a.GetLength(0);
            int m = b.GetLength(1);
            int m_a = a.GetLength(1);
            if (m_a != b.GetLength(0)) throw new Exception("Матрицы нельзя перемножить!");
            float[,] r = new float[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    r[i, j] = 0;
                    for (int ii = 0; ii < m_a; ii++)
                    {
                        r[i, j] += a[i, ii] * b[ii, j];
                    }
                }
            }
            return r;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ClearDrawing();
            DrawStaticAxes();
            k -= 5;
            Draw_Figure();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClearDrawing();
            DrawStaticAxes();
            k += 5;
            Draw_Figure();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ClearDrawing();
            DrawStaticAxes();
            l += 5;
            Draw_Figure();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ClearDrawing();
            DrawStaticAxes();
            l -= 5;
            Draw_Figure();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DrawFigureAndStaticAxes();
            if (string.IsNullOrEmpty(textBox1.Text) || !float.TryParse(textBox1.Text, out float scaleFactor))
            {
                MessageBox.Show("Введите корректное число для масштаба!", "Внимание");
                textBox1.Clear();
                return;
            }

            if (scaleFactor <= 0)
            {
                MessageBox.Show("Коэффициент масштаба должен быть больше нуля!", "Внимание");
                textBox1.Clear();
                return;
            }

            // Временно работаем с копией исходной фигуры
            float[,] tempTetraider = new float[4, 4];
            Array.Copy(originalTetraider, tempTetraider, originalTetraider.Length);

            Init_matr_scale(scaleFactor); // Инициализируем матрицу масштабирования

            // Умножаем копию исходной матрицы на матрицу масштабирования
            float[,] scaledTetraider = Multiply_matr(tempTetraider, matr_scale);

            // Обновляем текущие координаты фигуры
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Tetraider[i, j] = scaledTetraider[i, j];
                }
            }

            Draw_Figure();
            textBox1.Clear();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DrawFigureAndStaticAxes();
            // Матрица отражения относительно плоскости XOY
            float[,] reflectXOY = new float[4, 4] {
            { 1, 0, 0, 0 },
            { 0, 1, 0, 0 },
            { 0, 0, -1, 0 },
            { 0, 0, 0, 1 }
        };

            // Умножаем матрицу вершин на матрицу отражения
            float[,] reflectedTetraider = Multiply_matr(Tetraider, reflectXOY);

            // Обновляем координаты фигуры
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Tetraider[i, j] = reflectedTetraider[i, j];
                }
            }

            Draw_Figure();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DrawFigureAndStaticAxes();
            // Матрица отражения относительно плоскости YOZ
            float[,] reflectYOZ = new float[4, 4] {
            { -1, 0, 0, 0 },
            { 0, 1, 0, 0 },
            { 0, 0, 1, 0 },
            { 0, 0, 0, 1 }
        };

            // Умножаем матрицу вершин на матрицу отражения
            float[,] reflectedTetraider = Multiply_matr(Tetraider, reflectYOZ);

            // Обновляем координаты фигуры
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Tetraider[i, j] = reflectedTetraider[i, j];
                }
            }

            Draw_Figure();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            DrawFigureAndStaticAxes();
            // Матрица отражения относительно плоскости XOZ
            float[,] reflectXOZ = new float[4, 4] {
            { 1, 0, 0, 0 },
            { 0, -1, 0, 0 },
            { 0, 0, 1, 0 },
            { 0, 0, 0, 1 }
        };

            // Умножаем матрицу вершин на матрицу отражения
            float[,] reflectedTetraider = Multiply_matr(Tetraider, reflectXOZ);

            // Обновляем координаты фигуры
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Tetraider[i, j] = reflectedTetraider[i, j];
                }
            }

            Draw_Figure();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearDrawing();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ResetFigure();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Фигура не рисуется при загрузке, только оси
            CenterFigure();
            DrawStaticAxes();
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            ClearDrawing();
            DrawStaticAxes();

            float step = 5;

            for (int i = 0; i < 4; i++)
            {
                Tetraider[i, 0] += step / (float)Math.Sqrt(2); // Движение вправо
                Tetraider[i, 1] -= step / (float)Math.Sqrt(2); // Движение вверх
            }

            Draw_Figure();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            ClearDrawing();
            DrawStaticAxes();

            float step = 5;

            for (int i = 0; i < 4; i++)
            {
                Tetraider[i, 0] -= step / (float)Math.Sqrt(2); // Движение влево
                Tetraider[i, 1] += step / (float)Math.Sqrt(2); // Движение вниз
            }

            Draw_Figure();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DrawFigureAndStaticAxes(); // Очищаем и рисуем оси

            if (originalTetraider == null)
            {
                // Сохраняем исходное состояние при первом вызове
                originalTetraider = new float[Tetraider.GetLength(0), Tetraider.GetLength(1)];
                Array.Copy(Tetraider, originalTetraider, Tetraider.Length);
                previousTetraider = new float[Tetraider.GetLength(0), Tetraider.GetLength(1)];
                Array.Copy(Tetraider, previousTetraider, Tetraider.Length);
            }

            if (string.IsNullOrEmpty(textBox3.Text) || !float.TryParse(textBox3.Text, out float scaleFactor))
            {
                MessageBox.Show("Введите корректное число для коэффициента масштаба!", "Внимание");
                textBox3.Clear();
                return;
            }

            if (scaleFactor == 1f)
            {
                // Восстанавливаем исходное состояние
                if (originalTetraider != null)
                {
                    Array.Copy(originalTetraider, Tetraider, originalTetraider.Length);
                    previousTetraider = new float[Tetraider.GetLength(0), Tetraider.GetLength(1)];
                    Array.Copy(originalTetraider, previousTetraider, originalTetraider.Length); // Обновляем previousTetraider
                }
            }
            else
            {
                // Сохраняем текущее состояние перед масштабированием
                if (previousTetraider != null)
                {
                    Array.Copy(Tetraider, previousTetraider, Tetraider.Length);
                }
                else
                {
                    previousTetraider = new float[Tetraider.GetLength(0), Tetraider.GetLength(1)];
                    Array.Copy(Tetraider, previousTetraider, Tetraider.Length);
                }

                float sx = 1f;
                float sy = 1f;
                float sz = 1f;

                if (radioButton4.Checked)
                {
                    sx = scaleFactor;
                }
                if (radioButton5.Checked)
                {
                    sy = scaleFactor;
                }
                if (radioButton6.Checked)
                {
                    sz = scaleFactor;
                }

                // Матрица покоординатного масштабирования
                float[,] scaleMatrixCoordinate = new float[4, 4] {
                { sx, 0, 0, 0 },
                { 0, sy, 0, 0 },
                { 0, 0, sz, 0 },
                { 0, 0, 0, 1 }
            };

                // Умножаем матрицу вершин на матрицу масштабирования
                float[,] scaledTetraider = Multiply_matr(Tetraider, scaleMatrixCoordinate);

                // Обновляем координаты фигуры
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        Tetraider[i, j] = scaledTetraider[i, j];
                    }
                }
            }

            Draw_Figure(); // Перерисовываем фигуру
            textBox3.Clear();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Egor4_Load(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox2.Text, out double angleToRotate))
            {
                MessageBox.Show("Введите число для угла поворота");
                return;
            }
            double angleInRadians = angleToRotate * Math.PI / 180;

            if (radioButton1.Checked)
            {
                currentRotationAngleX += angleToRotate;
                Init_matr_pov_x(currentRotationAngleX * Math.PI / 180);
            }
            else if (radioButton2.Checked)
            {
                currentRotationAngleY += angleToRotate;
                Init_matr_pov_y(currentRotationAngleY * Math.PI / 180);
            }
            else if (radioButton3.Checked)
            {
                currentRotationAngleZ += angleToRotate;
                Init_matr_pov_z(currentRotationAngleZ * Math.PI / 180);
            }
            else
            {
                MessageBox.Show("Выберите ось поворота");
                return;
            }

            DrawFigureAndStaticAxes();
            Draw_Figure();
        }
    }
}