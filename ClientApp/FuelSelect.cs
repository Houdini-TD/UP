using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientApp.Properties;
using SQLLib;

namespace ClientApp
{
    public partial class FuelSelect : Form
    {
        private OrderData orderData;

        public FuelSelect(OrderData orderData)
        {
            InitializeComponent();
            loadComboBox();

            this.orderData = orderData;
        }

        private void FuelSelect_Load(object sender, EventArgs e)
        {

        }

        private void loadComboBox()
        {
            string query = "Select Type, ID from Fuel";
            SQL.LoadDataToComboBox(query, "ID", "Type", comboBox1);
            comboBox1.SelectedIndex = 1;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bitmap image = Resources.Нет_изображения;
            switch (comboBox1.Text)
            {
                case "АИ-92":
                    image = Resources.AI92;
                    break;
                case "АИ-95":
                    image = Resources.AI95;
                    break;
                case "ДТ":
                    image = Resources.DT;
                    break;
                case "Газ":
                    image = Resources.Gas;
                    break;
                case "Пропан":
                    image = Resources.Propan;
                    break;
            }
            pictureBox1.Image = image;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            orderData.FuelType = ((KeyValuePair<string, string>)comboBox1.SelectedItem).Key;
            orderData.Amount = (int)numericUpDown1.Value;

            Payment payment = new Payment(orderData);
            this.Hide();
            payment.ShowDialog();
            this.Show();
            payment.Dispose();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void controlChanged(object sender, EventArgs e)
        {
            decimal price = SQL.ExecuteScalar<decimal>($"select Price from Fuel where ID = {((KeyValuePair<string, string>)comboBox1.SelectedItem).Key}");
            double dPrice = double.Parse(price.ToString());
            tbPrice.Text = (dPrice * (int)numericUpDown1.Value).ToString();
        }
    }
}
