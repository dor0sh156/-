namespace Lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Init_matr_rot(rotationAngle); // Инициализация матрицы поворота при создании формы
        }

        private bool isReflectedOverX = false; // Флаг отражения по оси X
        int[,] figure = new int[6, 3]; // матрица фигуры (6 вершин)
        int[,] osi = new int[4, 3];    // матрица координат осей
        int[,] matr_sdv = new int[3, 3]; // матрица преобразования
        double[,] matr_rot = new double[3, 3]; // матрица поворота (теперь double)
        int k, l; // элементы матрицы сдвига
        bool f = true;
        int rotationAngle = 0; // Текущий угол поворота (0, 90, 180, 270 градусов)

        private void ClearDrawing()
        {
            pictureBox1.Image = null;
            pictureBox1.Refresh();
        }

        private void DrawStaticAxes()
        {
            int centerX = pictureBox1.Width / 2;
            int centerY = pictureBox1.Height / 2;

            Pen axisPen = new Pen(Color.Red, 1);
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

            g.DrawLine(axisPen, 0, centerY, pictureBox1.Width, centerY); // Ось X
            g.DrawLine(axisPen, centerX, 0, centerX, pictureBox1.Height); // Ось Y

            g.Dispose();
            axisPen.Dispose();
        }

        private void Init_figure()
        {
            // Верхняя часть (широкая)
            figure[0, 0] = -95; figure[0, 1] = -50; figure[0, 2] = 1; // Левый верхний
            figure[1, 0] = 95; figure[1, 1] = -50; figure[1, 2] = 1;  // Правый верхний

            // Нижняя часть с вырезом (ровные линии)
            figure[2, 0] = 30; figure[2, 1] = 30; figure[2, 2] = 1;    // Правый внутренний (Y совпадает с 3)
            figure[3, 0] = -30; figure[3, 1] = 30; figure[3, 2] = 1;  // Левый внутренний (Y совпадает с 2)
            figure[4, 0] = -30; figure[4, 1] = 85; figure[4, 2] = 1;  // Левый внешний (X совпадает с 3)
            figure[5, 0] = 30; figure[5, 1] = 85; figure[5, 2] = 1;    // Правый внешний (X совпадает с 2)
        }

        private void Init_matr_preob(int k1, int l1)
        {
            matr_sdv[0, 0] = 1; matr_sdv[0, 1] = 0; matr_sdv[0, 2] = 0;
            matr_sdv[1, 0] = 0; matr_sdv[1, 1] = 1; matr_sdv[1, 2] = 0;
            matr_sdv[2, 0] = k1; matr_sdv[2, 1] = l1; matr_sdv[2, 2] = 1;
        }

        private void Init_matr_rot(int angle)
        {
            double radians = angle * Math.PI / 180;
            double cos = Math.Cos(radians);
            double sin = Math.Sin(radians);

            matr_rot[0, 0] = cos; matr_rot[0, 1] = -sin; matr_rot[0, 2] = 0;
            matr_rot[1, 0] = sin; matr_rot[1, 1] = cos; matr_rot[1, 2] = 0;
            matr_rot[2, 0] = 0; matr_rot[2, 1] = 0; matr_rot[2, 2] = 1;
        }

        private void Init_osi()
        {
            osi[0, 0] = -500; osi[0, 1] = 0; osi[0, 2] = 1;
            osi[1, 0] = 500; osi[1, 1] = 0; osi[1, 2] = 1;
            osi[2, 0] = 0; osi[2, 1] = 500; osi[2, 2] = 1;
            osi[3, 0] = 0; osi[3, 1] = -500; osi[3, 2] = 1;
        }

        private int[,] Multiply_matr(int[,] a, double[,] b)
        {
            int n = a.GetLength(0);
            int m = b.GetLength(1);
            int[,] r = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    r[i, j] = 0;
                    for (int k = 0; k < a.GetLength(1); k++)
                    {
                        r[i, j] += (int)Math.Round(a[i, k] * b[k, j]);
                    }
                }
            }
            return r;
        }

        private int[,] Multiply_matr(int[,] a, int[,] b)
        {
            int n = a.GetLength(0);
            int m = b.GetLength(1);
            int[,] r = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    r[i, j] = 0;
                    for (int k = 0; k < a.GetLength(1); k++)
                    {
                        r[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return r;
        }

        private void Draw_Figure()
        {
            Init_figure();
            Init_matr_preob(k, l);

            // Применяем поворот
            int[,] rotatedFigure = Multiply_matr(figure, matr_rot);

            // Применяем отражение по оси X, если включено
            if (isReflectedOverX)
            {
                rotatedFigure = Multiply_matr(rotatedFigure, new int[,] { { 1, 0, 0 }, { 0, -1, 0 }, { 0, 0, 1 } });
            }

            int[,] figure1 = Multiply_matr(rotatedFigure, matr_sdv);

            Pen myPen = new Pen(Color.Blue, 2);
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

            // Рисуем верхнюю часть
            g.DrawLine(myPen, figure1[0, 0], figure1[0, 1], figure1[1, 0], figure1[1, 1]);

            // Рисуем левую сторону
            g.DrawLine(myPen, figure1[0, 0], figure1[0, 1], figure1[4, 0], figure1[4, 1]);

            // Рисуем правую сторону
            g.DrawLine(myPen, figure1[1, 0], figure1[1, 1], figure1[5, 0], figure1[5, 1]);

            // Рисуем нижнюю часть с вырезом
            g.DrawLine(myPen, figure1[4, 0], figure1[4, 1], figure1[3, 0], figure1[3, 1]);
            g.DrawLine(myPen, figure1[3, 0], figure1[3, 1], figure1[2, 0], figure1[2, 1]);
            g.DrawLine(myPen, figure1[2, 0], figure1[2, 1], figure1[5, 0], figure1[5, 1]);

            g.Dispose();
            myPen.Dispose();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // Попытка получить угол из textBox2
            if (int.TryParse(textBox2.Text, out int newRotationAngle))
            {
                rotationAngle = newRotationAngle % 360; // Учитываем полный оборот
                Init_matr_rot(rotationAngle);
                ClearDrawing();
                DrawStaticAxes();
                Draw_Figure();
            }
            else
            {
                MessageBox.Show("Введите корректное целочисленное значение угла поворота в TextBox2", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            k = pictureBox1.Width / 2;
            l = pictureBox1.Height / 2;
            Init_matr_rot(rotationAngle);
            ClearDrawing();
            DrawStaticAxes();
            Draw_Figure();
        }

        private void Draw_osi()
        {
            Init_osi();
            Init_matr_preob(k, l);
            int[,] osi1 = Multiply_matr(osi, matr_sdv);
            Pen myPen = new Pen(Color.Red, 1);
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            g.DrawLine(myPen, osi1[0, 0], osi1[0, 1], osi1[1, 0], osi1[1, 1]);
            g.DrawLine(myPen, osi1[2, 0], osi1[2, 1], osi1[3, 0], osi1[3, 1]);
            g.Dispose();
            myPen.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            k = pictureBox1.Width / 2;
            l = pictureBox1.Height / 2;
            ClearDrawing();
            Draw_osi();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClearDrawing();
            DrawStaticAxes();
            k += 5;
            Draw_Figure();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            timer1.Interval = 100;
            button8.Text = f ? "Стоп" : "Старт";

            if (f) timer1.Start();
            else timer1.Stop();

            f = !f;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            k++;
            ClearDrawing();
            DrawStaticAxes();
            Draw_Figure();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ClearDrawing();
            DrawStaticAxes();
            k -= 5;
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
            // Переключаем флаг отражения
            isReflectedOverX = !isReflectedOverX;

            // Обновляем текст кнопки
            button9.Text = isReflectedOverX ? "Отражение: OX (вкл)" : "Отражение: OX (выкл)";

            // Перерисовываем фигуру
            ClearDrawing();
            DrawStaticAxes();
            Draw_Figure();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox1.Text, out double scaleFactor))
            {
                // Проверяем, что масштаб положительный и не равен нулю
                if (scaleFactor > 0)
                {
                    // Сохраняем текущий угол поворота, так как масштабирование перезапишет матрицу
                    int savedAngle = rotationAngle;

                    // Инициализируем матрицу масштабирования
                    Init_matr_scale(scaleFactor);

                    // Перерисовываем фигуру
                    ClearDrawing();
                    DrawStaticAxes();
                    Draw_Figure();

                    // Восстанавливаем матрицу поворота
                    rotationAngle = savedAngle;
                    Init_matr_rot(rotationAngle);
                }
                else
                {
                    MessageBox.Show("Масштаб должен быть положительным числом больше нуля", "Ошибка",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Введите корректное числовое значение масштаба", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Init_matr_scale(double scaleFactor)
        {
            // Матрица масштабирования
            matr_rot[0, 0] = scaleFactor; matr_rot[0, 1] = 0; matr_rot[0, 2] = 0;
            matr_rot[1, 0] = 0; matr_rot[1, 1] = scaleFactor; matr_rot[1, 2] = 0;
            matr_rot[2, 0] = 0; matr_rot[2, 1] = 0; matr_rot[2, 2] = 1;
        }
    }
}