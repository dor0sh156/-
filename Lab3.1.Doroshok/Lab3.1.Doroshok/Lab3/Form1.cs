using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Lab3
{
    public partial class Form1 : Form
    {
        private const int EarthRadius = 50;
        private const int Orbit1Radius = 150;
        private const int Orbit2Radius = 200;
        private const double Speed1 = 0.02;
        private const double Speed2 = 0.015;
        private double angle1 = 0;
        private double angle2 = Math.PI;
        private System.Windows.Forms.Timer animationTimer = new System.Windows.Forms.Timer();

        private Matrix spacecraft1Transform = new Matrix();
        private Matrix spacecraft2Transform = new Matrix();

        // Определение формы космического корабля в виде массива точек (треугольник с вырезом)
        private PointF[] spacecraftShape = new PointF[]
        {
            new PointF(0, -10),   // Верхняя вершина
            new PointF(10, 10),  // Правая нижняя вершина
            new PointF(5, 5),    // Правая внутренняя вершина
            new PointF(-5, 5),   // Левая внутренняя вершина
            new PointF(-10, 10)  // Левая нижняя вершина
        };

        public Form1()
        {
            InitializeComponent(); // Инициализация компонентов формы, сгенерированных дизайнером
            animationTimer.Interval = 30; // Установка интервала таймера (в миллисекундах), определяющего частоту обновления экрана
            animationTimer.Tick += AnimationTimer_Tick; // Привязка события Tick таймера к обработчику AnimationTimer_Tick
            animationTimer.Start(); // Запуск таймера, что приводит к периодическому вызову события Tick
        }

        // Обработчик события Tick таймера, вызываемый через заданный интервал
        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            angle1 += Speed1; // Увеличение угла первого корабля для его движения по орбите
            angle2 += Speed2; // Увеличение угла второго корабля для его движения по орбите

            ClearDrawing(); // Очистка области рисования
            DrawEarth(); // Отрисовка Земли в центре
            DrawSpacecraft(angle1, Orbit1Radius, Color.Green, spacecraft1Transform); // Отрисовка первого корабля на первой орбите
            DrawSpacecraft(angle2, Orbit2Radius, Color.Orange, spacecraft2Transform); // Отрисовка второго корабля на второй орбите
        }

        private void ClearDrawing()
        {
            pictureBox1.Image = null; // Удаление текущего изображения из PictureBox
            pictureBox1.Refresh(); // Перерисовка PictureBox для отображения изменений (в данном случае - пустоты)
        }

        // Метод для отрисовки Земли в центре PictureBox
        private void DrawEarth()
        {
            int centerX = pictureBox1.Width / 2; // Вычисление X-координаты центра PictureBox
            int centerY = pictureBox1.Height / 2; // Вычисление Y-координаты центра PictureBox
            int x = centerX - EarthRadius; // Вычисление X-координаты верхнего левого угла эллипса Земли
            int y = centerY - EarthRadius; // Вычисление Y-координаты верхнего левого угла эллипса Земли
            int diameter = 2 * EarthRadius; // Вычисление диаметра эллипса Земли

            // Использование объекта Graphics для рисования на PictureBox
            using (Graphics g = Graphics.FromHwnd(pictureBox1.Handle))
            // Использование SolidBrush для заливки эллипса синим цветом (цвет Земли)
            using (SolidBrush earthBrush = new SolidBrush(Color.Blue))
            {
                g.FillEllipse(earthBrush, x, y, diameter, diameter); // Заполнение эллипса (рисование круга)
            }
        }

        // Метод для отрисовки космического корабля на заданной орбите
        private void DrawSpacecraft(double angle, int orbitRadius, Color color, Matrix transformMatrix)
        {
            int centerX = pictureBox1.Width / 2; // Вычисление X-координаты центра PictureBox
            int centerY = pictureBox1.Height / 2; // Вычисление Y-координаты центра PictureBox

            // Вычисление X и Y координат центра корабля на орбите с использованием параметрических уравнений окружности
            float orbitX = centerX + orbitRadius * (float)Math.Cos(angle);
            float orbitY = centerY + orbitRadius * (float)Math.Sin(angle);

            // Переменная для хранения коэффициента масштабирования корабля
            float scaleFactor = 1.0f;
            // Пороговое значение Y-координаты, ниже которого начинается увеличение размера
            float lowerThreshold = pictureBox1.Height * 0.6f;
            // Пороговое значение Y-координаты, выше которого начинается уменьшение размера
            float upperThreshold = pictureBox1.Height * 0.3f;
            // Максимальное увеличение размера корабля (в два раза от исходного)
            float maxScaleIncrease = 1.0f;
            // Минимальный размер корабля (половина от исходного)
            float maxScaleDecrease = 0.5f;

            // Условие для увеличения размера корабля при приближении к нижней части экрана
            if (orbitY > lowerThreshold)
            {
                // Линейное масштабирование от 1 до maxScaleIncrease в зависимости от положения по Y
                scaleFactor = 1 + (orbitY - lowerThreshold) / (pictureBox1.Height - lowerThreshold) * maxScaleIncrease;
            }
            // Условие для уменьшения размера корабля при приближении к верхней части экрана
            else if (orbitY < upperThreshold)
            {
                // Линейное масштабирование от 1 до maxScaleDecrease в зависимости от положения по Y
                scaleFactor = 1 - (upperThreshold - orbitY) / upperThreshold * (1 - maxScaleDecrease);
                // Гарантируем, что масштаб не будет меньше минимального значения
                if (scaleFactor < maxScaleDecrease) scaleFactor = maxScaleDecrease;
            }

            // Сброс матрицы преобразований для текущего корабля
            transformMatrix.Reset();

            // Создание и применение матрицы масштабирования относительно центра формы корабля
            using (Matrix scaleMatrix = new Matrix())
            {
                float centerXShape = 0;   // X-координата центра формы корабля
                float centerYShape = -5;  // Y-координата центра формы корабля
                scaleMatrix.Translate(-centerXShape, -centerYShape); // Перемещение центра формы в начало координат
                scaleMatrix.Scale(scaleFactor, scaleFactor);       // Применение масштабирования
                scaleMatrix.Translate(centerXShape, centerYShape);  // Возврат центра формы на прежнее место

                transformMatrix.Multiply(scaleMatrix); // Умножение текущей матрицы преобразований на матрицу масштабирования (применение масштаба)
            }

            // Перемещение корабля на вычисленные координаты орбиты
            transformMatrix.Translate(orbitX, orbitY);

            // Создание копии массива точек, определяющих форму корабля
            PointF[] transformedShape = (PointF[])spacecraftShape.Clone();
            // Применение матрицы преобразований (масштабирование + перемещение) к точкам формы корабля
            transformMatrix.TransformPoints(transformedShape);

            // Использование объекта Graphics для рисования на PictureBox
            using (Graphics g = Graphics.FromHwnd(pictureBox1.Handle))
            // Использование SolidBrush для заливки многоугольника цветом корабля
            using (SolidBrush spacecraftBrush = new SolidBrush(color))
            {
                g.FillPolygon(spacecraftBrush, transformedShape); // Заполнение многоугольника (рисование корабля)
            }
        }
    }
}