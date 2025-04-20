namespace Lab3
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            panel1 = new Panel();
            splitContainer1 = new SplitContainer();
            pictureBox1 = new PictureBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            button11 = new Button();
            button10 = new Button();
            button9 = new Button();
            label1 = new Label();
            button8 = new Button();
            button1 = new Button();
            button7 = new Button();
            button2 = new Button();
            button6 = new Button();
            button3 = new Button();
            button5 = new Button();
            button4 = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(splitContainer1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(919, 526);
            panel1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Margin = new Padding(3, 2, 3, 2);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(pictureBox1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(textBox2);
            splitContainer1.Panel2.Controls.Add(textBox1);
            splitContainer1.Panel2.Controls.Add(button11);
            splitContainer1.Panel2.Controls.Add(button10);
            splitContainer1.Panel2.Controls.Add(button9);
            splitContainer1.Panel2.Controls.Add(label1);
            splitContainer1.Panel2.Controls.Add(button8);
            splitContainer1.Panel2.Controls.Add(button1);
            splitContainer1.Panel2.Controls.Add(button7);
            splitContainer1.Panel2.Controls.Add(button2);
            splitContainer1.Panel2.Controls.Add(button6);
            splitContainer1.Panel2.Controls.Add(button3);
            splitContainer1.Panel2.Controls.Add(button5);
            splitContainer1.Panel2.Controls.Add(button4);
            splitContainer1.Size = new Size(919, 526);
            splitContainer1.SplitterDistance = 646;
            splitContainer1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(3, 2);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(640, 533);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(24, 423);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(221, 23);
            textBox2.TabIndex = 13;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(24, 484);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(221, 23);
            textBox1.TabIndex = 12;
            // 
            // button11
            // 
            button11.Location = new Point(24, 452);
            button11.Name = "button11";
            button11.Size = new Size(221, 26);
            button11.TabIndex = 11;
            button11.Text = "Масштабирование";
            button11.UseVisualStyleBackColor = true;
            button11.Click += button11_Click;
            // 
            // button10
            // 
            button10.Location = new Point(24, 388);
            button10.Name = "button10";
            button10.Size = new Size(221, 29);
            button10.TabIndex = 10;
            button10.Text = "Поворот";
            button10.UseVisualStyleBackColor = true;
            button10.Click += button10_Click;
            // 
            // button9
            // 
            button9.Location = new Point(24, 352);
            button9.Name = "button9";
            button9.Size = new Size(221, 30);
            button9.TabIndex = 9;
            button9.Text = "Отражение";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 35.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(51, 134);
            label1.Name = "label1";
            label1.Size = new Size(173, 62);
            label1.TabIndex = 3;
            label1.Text = "СДВИГ";
            // 
            // button8
            // 
            button8.Location = new Point(24, 316);
            button8.Margin = new Padding(3, 2, 3, 2);
            button8.Name = "button8";
            button8.Size = new Size(221, 31);
            button8.TabIndex = 8;
            button8.Text = "Старт";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // button1
            // 
            button1.Location = new Point(24, 30);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(221, 29);
            button1.TabIndex = 0;
            button1.Text = "Нарисовать оси";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button7
            // 
            button7.Location = new Point(79, 198);
            button7.Margin = new Padding(3, 2, 3, 2);
            button7.Name = "button7";
            button7.Size = new Size(118, 38);
            button7.TabIndex = 7;
            button7.Text = "По оси ОУ вверх";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button2
            // 
            button2.Location = new Point(24, 63);
            button2.Margin = new Padding(3, 2, 3, 2);
            button2.Name = "button2";
            button2.Size = new Size(221, 34);
            button2.TabIndex = 1;
            button2.Text = "Нарисовать фигуру";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button6
            // 
            button6.Location = new Point(79, 279);
            button6.Margin = new Padding(3, 2, 3, 2);
            button6.Name = "button6";
            button6.Size = new Size(118, 33);
            button6.TabIndex = 6;
            button6.Text = "По оси ОУ вниз";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button3
            // 
            button3.Location = new Point(24, 101);
            button3.Margin = new Padding(3, 2, 3, 2);
            button3.Name = "button3";
            button3.Size = new Size(221, 31);
            button3.TabIndex = 2;
            button3.Text = "Очистить";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button5
            // 
            button5.Location = new Point(15, 240);
            button5.Margin = new Padding(3, 2, 3, 2);
            button5.Name = "button5";
            button5.Size = new Size(118, 36);
            button5.TabIndex = 5;
            button5.Text = "По оси ОХ влево";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button4
            // 
            button4.Location = new Point(139, 240);
            button4.Margin = new Padding(3, 2, 3, 2);
            button4.Name = "button4";
            button4.Size = new Size(118, 35);
            button4.TabIndex = 4;
            button4.Text = "По оси ОХ вправо";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(919, 526);
            Controls.Add(panel1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Преобразования на плоскости";
            panel1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private SplitContainer splitContainer1;
        private PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Label label1;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
        private Button button10;
        private Button button11;
        private TextBox textBox1;
        private TextBox textBox2;
    }
}
