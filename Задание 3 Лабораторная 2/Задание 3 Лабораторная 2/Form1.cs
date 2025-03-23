namespace Задание_3_Лабораторная_2
{
    public partial class Form1 : Form
    {
        private Bitmap myBitmap;
        private Graphics g;
        private Color drawColor = Color.Black; // Цвет отрезка по умолчанию
        private int radius; // Длина отрезка
        private double angle; // Угол наклона 

        public Form1()
        {
            InitializeComponent();
            myBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(myBitmap);
            g.Clear(Color.White);
            pictureBox1.Image = myBitmap;
        }

        private void DrawLineBresenham(int x0, int y0, int x1, int y1)
        {
            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);
            int sx = x0 < x1 ? 1 : -1;
            int sy = y0 < y1 ? 1 : -1;
            int err = dx - dy;

            while (true)
            {
                if (x0 >= 0 && x0 < myBitmap.Width && y0 >= 0 && y0 < myBitmap.Height)
                {
                    myBitmap.SetPixel(x0, y0, drawColor);
                }

                if (x0 == x1 && y0 == y1) break;

                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x0 += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }
            }
            pictureBox1.Image = myBitmap;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            pictureBox1.Image = myBitmap;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                drawColor = colorDialog1.Color;
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            // Проверяем, что введенное значение является числом и больше 0
            if (int.TryParse(textBox1.Text, out int newRadius) && newRadius > 0)
            {
                radius = newRadius; // Обновляем длину отрезка
                MessageBox.Show($"Новая длина отрезка: {radius}", "Уведомление");
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректное значение длины отрезка.", "Ошибка");
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            // Проверяем, что введенное значение является числом
            if (double.TryParse(textBox2.Text, out double newAngle))
            {
                angle = newAngle; // Обновляем угол
                MessageBox.Show($"Новый угол: {angle}°", "Уведомление");
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректное значение угла.", "Ошибка");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int startX = pictureBox1.Width / 2; // Начальная точка X
            int startY = pictureBox1.Height / 2; // Начальная точка Y

            // Вычисляем конечную точку на основе угла и длины отрезка
            double radians = angle * Math.PI / 180; // Переводим угол в радианы
            int endX = startX + (int)(radius * Math.Cos(radians));
            int endY = startY - (int)(radius * Math.Sin(radians)); // Ось Y направлена вниз, поэтому вычитаем

            DrawLineBresenham(startX, startY, endX, endY);
        }
    }
}

