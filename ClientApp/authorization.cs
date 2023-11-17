using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SQLLib;

namespace ClientApp
{
    public partial class Form1 : Form
    {
        private string text = String.Empty;

        private Bitmap CreateImage(int Width, int Height)
        {
            Random rnd = new Random();

            //Создадим изображение
            Bitmap result = new Bitmap(Width, Height);

            //Вычислим позицию текста
            int Xpos = rnd.Next(0, Width - 50);
            int Ypos = rnd.Next(15, Height - 15);

            //Добавим различные цвета
            Brush[] colors = { Brushes.Black,
                     Brushes.Red,
                     Brushes.RoyalBlue,
                     Brushes.Green };

            //Укажем где рисовать
            Graphics g = Graphics.FromImage((Image)result);

            //Пусть фон картинки будет серым
            g.Clear(Color.Gray);

            //Сгенерируем текст
            text = String.Empty;
            string ALF = "1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
            for (int i = 0; i < 5; ++i)
                text += ALF[rnd.Next(ALF.Length)];

            //Нарисуем сгенирируемый текст
            g.DrawString(text,
                         new Font("Arial", 15),
                         colors[rnd.Next(colors.Length)],
                         new PointF(Xpos, Ypos));

            //Добавим немного помех
            /////Линии из углов
            g.DrawLine(Pens.Black,
                       new Point(0, 0),
                       new Point(Width - 1, Height - 1));
            g.DrawLine(Pens.Black,
                       new Point(0, Height - 1),
                       new Point(Width - 1, 0));
            ////Белые точки
            for (int i = 0; i < Width; ++i)
                for (int j = 0; j < Height; ++j)
                    if (rnd.Next() % 20 == 0)
                        result.SetPixel(i, j, Color.White);

            return result;
        }

        public Form1()
        {
            InitializeComponent();
            loadComboBox();
            pictureBox1.Image = this.CreateImage(pictureBox1.Width, pictureBox1.Height);
        }

        private void loadComboBox()
        {
            string query = "Select Adress, ID From Stations";
            SQL.LoadDataToComboBox(query, "ID", "Adress", comboBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkCaptcha())
            {
                string stationID = ((KeyValuePair<string, string>)comboBox1.SelectedItem).Key;
                FuelSelect fs = new FuelSelect(new OrderData(textBox1.Text, stationID));

                string query = $"insert into History (datetime, Client_Phone, Station) values ('{DateTime.Now}', {textBox1.Text}, {stationID})";
                SQL.ExecuteNonQuery(query);

                this.Hide();
                fs.ShowDialog();
                this.Show();
                fs.Dispose();
            }
        }

        private bool checkCaptcha()
        {
            if (tbCaptcha.Text == text)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Капча введена неправильно");
                pictureBox1.Image = this.CreateImage(pictureBox1.Width, pictureBox1.Height);
                return false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void controlChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1 && textBox1.Text.Length == 11 && tbCaptcha.Text.Length == 5)
                button1.Enabled = true;
            else 
                button1.Enabled = false;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8) 
            {
                e.Handled = true; 
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = this.CreateImage(pictureBox1.Width, pictureBox1.Height);
        }


    }
}
