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
using System.Globalization;
using System.Data.SqlClient;

namespace ClientApp
{
    public partial class Payment : Form
    {
        OrderData orderData;
        public Payment(OrderData orderData)
        {
            this.orderData = orderData;
            InitializeComponent();
            loadData();
        }

        private void loadData()
        {
            string query = $"select CardNum, Fio from Clients where Phone = {orderData.Phone}";
            SqlDataReader reader = SQL.ExecuteReader(query);
            if (reader.Read())
            {
                tbCardNum.Text = reader.GetValue(0).ToString();
                tbName.Text = reader.GetValue(1).ToString();
            }
            else
            {
                SQL.ExecuteNonQuery($"insert into Clients (Phone) values ({orderData.Phone})");
            }
        }

        private void tbSecondName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnShowPas_Click(object sender, EventArgs e)
        {
            if (tbCardNum.PasswordChar == '*')
                tbCardNum.PasswordChar = '\0';
            else if (tbCardNum.PasswordChar == '\0')
                tbCardNum.PasswordChar = '*';
        }

        private void btnPay_Click(object sender, EventArgs e) { 

            string query = $"insert into Sales values ('{DateTime.Now}', {orderData.FuelType}, {orderData.Amount.ToString()}, '{orderData.Phone}', {orderData.StationID})";
            SQL.ExecuteNonQuery(query);
            MessageBox.Show("Успешно!");
            this.Close();
        }

        private void tbCardNum_TextChanged_1(object sender, EventArgs e)
        {
            if (tbCardNum.Text.Length == 12 && tbName.Text != "")
            {
                btnPay.Enabled = true;
            }
            else
            {
                btnPay.Enabled = false;
            }
        }

        private void tbCardNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void tbName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
    }
}
