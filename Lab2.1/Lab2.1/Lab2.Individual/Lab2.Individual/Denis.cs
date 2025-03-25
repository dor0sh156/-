namespace Lab2.Individual
{
    public partial class Denis : Form
    {
        private Bitmap myBitmap;
        private Graphics g;
        private Color drawColor = Color.Black; // Цвет окружности по умолчанию
        private int radius = 50;

        public Denis()
        {
            InitializeComponent();
            myBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(myBitmap);
            g.Clear(Color.White);
            pictureBox1.Image = myBitmap;
        }

        private void DrawCircleBresenham(int x0, int y0, int radius)
        {
            int x = 0;
            int y = radius;
            int d = 3 - 2 * radius;

            while (x <= y)
            {
                PlotCirclePoints(x0, y0, x, y);
                x++;
                if (d > 0)
                {
                    y--;
                    d += 4 * (x - y) + 10;
                }

                else
                {
                    d += 4 * x + 6;
                }
            }
            pictureBox1.Image = myBitmap;
        }

        private void PlotCirclePoints(int x0, int y0, int x, int y)
        {
            if (x0 + x >= 0 && x0 + x < myBitmap.Width && y0 + y >= 0 && y0 + y < myBitmap.Height)
                myBitmap.SetPixel(x0 + x, y0 + y, drawColor);

            if (x0 - x >= 0 && x0 - x < myBitmap.Width && y0 + y >= 0 && y0 + y < myBitmap.Height)
                myBitmap.SetPixel(x0 - x, y0 + y, drawColor);

            if (x0 + x >= 0 && x0 + x < myBitmap.Width && y0 - y >= 0 && y0 - y < myBitmap.Height)
                myBitmap.SetPixel(x0 + x, y0 - y, drawColor);

            if (x0 - x >= 0 && x0 - x < myBitmap.Width && y0 - y >= 0 && y0 - y < myBitmap.Height)
                myBitmap.SetPixel(x0 - x, y0 - y, drawColor);

            if (x0 + y >= 0 && x0 + y < myBitmap.Width && y0 + x >= 0 && y0 + x < myBitmap.Height)
                myBitmap.SetPixel(x0 + y, y0 + x, drawColor);

            if (x0 - y >= 0 && x0 - y < myBitmap.Width && y0 + x >= 0 && y0 + x < myBitmap.Height)
                myBitmap.SetPixel(x0 - y, y0 + x, drawColor);

            if (x0 + y >= 0 && x0 + y < myBitmap.Width && y0 - x >= 0 && y0 - x < myBitmap.Height)
                myBitmap.SetPixel(x0 + y, y0 - x, drawColor);

            if (x0 - y >= 0 && x0 - y < myBitmap.Width && y0 - x >= 0 && y0 - x < myBitmap.Height)
                myBitmap.SetPixel(x0 - y, y0 - x, drawColor);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int centerX = pictureBox1.Width / 2;
            int centerY = pictureBox1.Height / 2;
            DrawCircleBresenham(centerX, centerY, radius);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            pictureBox1.Image = myBitmap;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                drawColor = colorDialog1.Color;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Проверяем, что введенное значение является числом и больше 0
            if (int.TryParse(textBox1.Text, out int newRadius) && newRadius > 0)
            {
                radius = newRadius; // Обновляем радиус
                MessageBox.Show($"Новый радиус: {radius}", "Уведомление");
            }

            else
            {
                MessageBox.Show("Пожалуйста, введите корректное значение радиуса.", "Ошибка");
            }
        }
    }
}
