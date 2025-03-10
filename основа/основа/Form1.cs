using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace основа
{
    public partial class Form1 : Form
    {
        private bool isThickPen = false;//Перо

        public int xn, yn, xk, yk; // концы отрезка
        Bitmap myBitmap; // объект Bitmap для вывода отрезка
        Color currentBorderColor; // текущий цвет отрезка и текущий цвет заливки
        Color currentFillColor = Color.Green;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                xn = e.X;
                yn = e.Y;
            }

            else MessageBox.Show("Вы не выбрали алгоритм вывода фигуры!");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            int index, numberNodes;
            double xOutput, yOutput, dx, dy;

            // Определяем толщину пера в зависимости от флага
            float penWidth = isThickPen ? 8 : 1;

            // Объявляем объект "myPen" с текущей толщиной
            using (Pen myPen = new Pen(Color.Red, penWidth))
            using (Graphics g = Graphics.FromHwnd(pictureBox1.Handle))
            {
                xk = e.X;
                yk = e.Y;
                dx = xk - xn;
                dy = yk - yn;
                numberNodes = 200;
                xOutput = xn;
                yOutput = yn;

                for (index = 1; index <= numberNodes; index++)
                {
                    // Рисуем прямоугольник
                    g.DrawRectangle(myPen, (int)xOutput, (int)yOutput, 2, 2);

                    // Обновляем координаты для следующего узла
                    xOutput = xOutput + dx / numberNodes;
                    yOutput = yOutput + dy / numberNodes;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap myBitmap = new Bitmap(pictureBox1.Height, pictureBox1.Width);
            //Задаем цвет пикселя по схеме RGB (от 0 до 255 для каждого цвета)
            Color newPixelColor = Color.FromArgb(247, 249, 239);
            for (int x = 0; x < myBitmap.Width; x++)
            {
                for (int y = 0; y < myBitmap.Height; y++)
                {
                    myBitmap.SetPixel(x, y, newPixelColor);
                }
            }
            pictureBox1.Image = myBitmap;
            // pictureBox1.Image = null;
        }
        // вывод отрезка
        private void CDA(int xStart, int yStart, int xEnd, int yEnd)
        {
            int index, numberNodes;
            double xOutput, yOutput, dx, dy;

            xn = xStart;
            yn = yStart;
            xk = xEnd;
            yk = yEnd;
            dx = xk - xn;
            dy = yk - yn;
            numberNodes = 200;
            xOutput = xn;
            yOutput = yn;
            for (index = 1; index <= numberNodes; index++)
            {
                myBitmap.SetPixel((int)xOutput, (int)yOutput,
               currentBorderColor);
                xOutput = xOutput + dx / numberNodes;
                yOutput = yOutput + dy / numberNodes;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = colorDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                currentBorderColor = colorDialog1.Color; // Выбор цвета границы
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //отключаем кнопки
            button1.Enabled = false;
            button2.Enabled = false;
            //создаем новый экземпляр Bitmap размером с элемент
            //pictureBox1 (myBitmap)
            //на нем выводим попиксельно отрезок
            myBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (Graphics g = Graphics.FromHwnd(pictureBox1.Handle))
            {
                if (radioButton1.Checked == true)
                {
                    //рисуем прямоугольник
                    CDA(10, 10, 10, 110);
                    CDA(10, 10, 110, 10);
                    CDA(10, 110, 110, 110);
                    CDA(110, 10, 110, 110);
                    //рисуем треугольник
                    CDA(150, 10, 150, 200);
                    CDA(250, 50, 150, 200);
                    CDA(150, 10, 250, 150);
                }
                else
                {
                    if (radioButton2.Checked == true)
                    {
                        //получаем растр созданного рисунка в mybitmap
                        myBitmap = pictureBox1.Image as Bitmap;

                        // задаем координаты затравки
                        xn = 160;
                        yn = 40;
                        // вызываем рекурсивную процедуру заливки с затравкой
                        FloodFill(xn, yn);
                    }
                }
                //передаем полученный растр mybitmap в элемент pictureBox
                pictureBox1.Image = myBitmap;

                // обновляем pictureBox и активируем кнопки
                pictureBox1.Refresh();
                button1.Enabled = true;
                button2.Enabled = true;
            }
        }
        private void FloodFill(int x1, int y1)
        {
            Color oldPixelColor = myBitmap.GetPixel(x1, y1);
            if ((oldPixelColor.ToArgb() != currentBorderColor.ToArgb()) &&
                (oldPixelColor.ToArgb() != currentFillColor.ToArgb()))
            {
                myBitmap.SetPixel(x1, y1, currentFillColor);

                FloodFill(x1 + 1, y1);
                FloodFill(x1 - 1, y1);
                FloodFill(x1, y1 + 1);
                FloodFill(x1, y1 - 1);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            isThickPen = !isThickPen; // Переключаем флаг
            if (isThickPen)
            {
                MessageBox.Show("Толщина пера изменена на 8 пикселей.");
            }
            else
            {
                MessageBox.Show("Толщина пера изменена на 1 пиксель.");
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = colorDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                currentFillColor = colorDialog1.Color; // Выбор цвета заливки
            }

        }
    }
}
