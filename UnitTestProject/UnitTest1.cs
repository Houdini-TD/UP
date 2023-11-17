using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace UnitTestProject
{
    [TestClass]
    public class SQLTests
    {
        [TestMethod]
        public void ExecuteScalar()
        {
            string query = "SELECT COUNT(*) FROM History";
            int result = SQL.ExecuteScalar<int>(query);

            Assert.AreEqual(14, result);
        }

        [TestMethod]
        public void ExecuteReader_ReturnsValidDataReader()
        {
            string query = "SELECT * FROM History";
            SqlDataReader reader = SQL.ExecuteReader(query);

            Assert.IsNotNull(reader);
        }

        [TestMethod]
        public void ExecuteQuery_ReturnsValidQueryResult()
        {
            string query = "SELECT * FROM History";
            SQL.QueryResult result = SQL.ExecuteQuery(query);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.DataTable);
            Assert.IsNotNull(result.SqlDataAdapter);
            Assert.IsNotNull(result.Command);
        }

        [TestMethod]
        public void ExecuteReader_WithValidQuery_ReturnsValidDataReader()
        {
            string query = "SELECT * FROM History";
            SqlDataReader reader = SQL.ExecuteReader(query);

            // Assert
            Assert.IsNotNull(reader);
            Assert.IsTrue(reader.HasRows);
        }

        [TestMethod]
        public void LoadDataToComboBox_WithValidQuery_FillsComboBox()
        {
            string validQuery = "SELECT * FROM History";
            string keyColumn = "ID";
            string valueColumn = "Station";
            ComboBox comboBox = new ComboBox();
            SQL.LoadDataToComboBox(validQuery, keyColumn, valueColumn, comboBox);

            Assert.IsTrue(comboBox.DataSource != null);
        }

        [TestMethod]
        public void LoadDataToCheckedListBox_WithInvalidQuery_DoesNotFillCheckedListBox()
        {
            string invalidQuery = "SELECT * FROM NonExistentTable";
            string keyColumn = "Key";
            string valueColumn = "Value";
            CheckedListBox checkedListBox = new CheckedListBox();
            SQL.LoadDataToCheckedListBox(invalidQuery, keyColumn, valueColumn, checkedListBox);

            Assert.AreEqual(0, checkedListBox.Items.Count);
        }

        [TestMethod]
        public void LoadDataToListBox_WithInvalidQuery_NotFillsListBox()
        {
            string validQuery = "SELECT * FROM Test";
            string keyColumn = "ID";
            string valueColumn = "Station";
            ListBox listBox = new ListBox();
            SQL.LoadDataToListBox(validQuery, keyColumn, valueColumn, listBox);

            Assert.IsTrue(listBox.Items.Count == 0);
        }

        [TestMethod]
        public void ExecuteReader_WithInvalidQuery_ReturnsNullReader()
        {
            string invalidQuery = "SELECT * FROM NonExistentTable";
            SqlDataReader reader = SQL.ExecuteReader(invalidQuery);

            Assert.IsNull(reader);
        }

        [TestMethod]
        public void LoadDataToCheckedListBox_WithValidQuery_FillsCheckedListBox()
        {
            string validQuery = "SELECT * FROM History";
            string keyColumn = "ID";
            string valueColumn = "Station";
            CheckedListBox checkedListBox = new CheckedListBox();
            SQL.LoadDataToCheckedListBox(validQuery, keyColumn, valueColumn, checkedListBox);
        }

        [TestMethod]
        public void GetConnection_ReturnsValidSqlConnection()
        {
            SqlConnection connection = SQL.getConnection();

            Assert.IsNotNull(connection);
        }



    }
}
