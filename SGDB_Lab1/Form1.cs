using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SGDB_Lab1
{
    public partial class Form1 : Form
    {

        DataSet dataSet = new DataSet();
        string connectionString = @"Server=DESKTOP-MMFE70L\SQLEXPRESS;Database=Turism;Integrated Security=true";
        SqlDataAdapter parentAdapter = new SqlDataAdapter();
        SqlDataAdapter childAdapter = new SqlDataAdapter();
        BindingSource bsParent = new BindingSource();
        BindingSource bsChiled = new BindingSource();

        SqlConnection connection = new SqlConnection(@"Server=DESKTOP-MMFE70L\SQLEXPRESS;Database=Turism;Integrated Security=true");
        

        public Form1()
        {
            InitializeComponent();
            loadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
       
        

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (textBoxDenumire.Text == "" || textBoxIdAtractie.Text == "" || textBoxPret.Text == "" || textBoxTip.Text == "" || textBoxLocatie.Text == "")
                {
                    MessageBox.Show("Campuri incomplecte!");
                }
                else
                {

                    childAdapter.InsertCommand = new SqlCommand("INSERT INTO Atractii VALUES (@denumire, @pret, @tip, @locatie)", connection);

                    childAdapter.InsertCommand.Parameters.Add("@denumire", SqlDbType.VarChar).Value = textBoxDenumire.Text;
                    childAdapter.InsertCommand.Parameters.Add("@pret", SqlDbType.VarChar).Value = textBoxPret.Text;
                    childAdapter.InsertCommand.Parameters.Add("@tip", SqlDbType.VarChar).Value = textBoxTip.Text;
                    childAdapter.InsertCommand.Parameters.Add("@locatie", SqlDbType.VarChar).Value = textBoxLocatie.Text;

                    connection.Open();
                    childAdapter.InsertCommand.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Adaugare realizata");

                    refreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void refreshData() {
            try
            {
                dataSet.Clear();

                parentAdapter.SelectCommand = new SqlCommand("SELECT * FROM Locatii;", connection);
                childAdapter.SelectCommand = new SqlCommand("SELECT * FROM Atractii;", connection);


                parentAdapter.Fill(dataSet, "Locatii");
                childAdapter.Fill(dataSet, "Atractii");

                bsParent.DataSource = dataSet.Tables["Locatii"];
                dataGridParent.DataSource = bsParent;


                DataColumn pkColumn = dataSet.Tables["Locatii"].Columns["IDLocatie"];
                DataColumn fkColumn = dataSet.Tables["Atractii"].Columns["IDLocatie"];


                bsChiled.DataSource = bsParent;
                bsChiled.DataMember = "fk_parent_child";
                dataGridChild.DataSource = bsChiled;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void loadData()
        {
            
            try
            {

                parentAdapter.SelectCommand = new SqlCommand("SELECT * FROM Locatii;", connection);
                childAdapter.SelectCommand = new SqlCommand("SELECT * FROM Atractii;", connection);

                parentAdapter.Fill(dataSet, "Locatii");
                childAdapter.Fill(dataSet, "Atractii");

                bsParent.DataSource = dataSet.Tables["Locatii"];
                dataGridParent.DataSource = bsParent;


                DataColumn pkColumn = dataSet.Tables["Locatii"].Columns["IDLocatie"];
                DataColumn fkColumn = dataSet.Tables["Atractii"].Columns["IDLocatie"];

                DataRelation relation = new DataRelation("fk_parent_child", pkColumn, fkColumn, true);
                dataSet.Relations.Add(relation);
                bsChiled.DataSource = bsParent;
                bsChiled.DataMember = "fk_parent_child";
                dataGridChild.DataSource = bsChiled;

                textBoxLocatie.DataBindings.Add("Text", bsParent, "IDLocatie", true);
                labelContinent.DataBindings.Add("Text", bsParent, "Continent", true);
                labelTara.DataBindings.Add("Text", bsParent, "Tara", true);
                labelOras.DataBindings.Add("Text", bsParent, "Oras", true);

                textBoxDenumire.DataBindings.Add("Text", bsChiled, "Denumire", true);
                textBoxPret.DataBindings.Add("Text", bsChiled, "PretBilet", true);
                textBoxTip.DataBindings.Add("Text", bsChiled, "Tip", true);
                textBoxIdAtractie.DataBindings.Add("Text", bsChiled, "IDAtractie", true);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void labelOras_Click(object sender, EventArgs e)
        {

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxDenumire.Text == "" || textBoxIdAtractie.Text == "" || textBoxPret.Text == "" || textBoxTip.Text == "" || textBoxLocatie.Text == "")
                {
                    MessageBox.Show("Campuri incomplecte!");
                }
                else
                {

                    int x;
                    childAdapter.UpdateCommand = new SqlCommand("Update Atractii set Denumire = @denumire, PretBilet = @pret, Tip = @tip, IDLocatie = @locatie where IDAtractie = @id", connection);

                    childAdapter.UpdateCommand.Parameters.Add("@denumire", SqlDbType.VarChar).Value = textBoxDenumire.Text;
                    childAdapter.UpdateCommand.Parameters.Add("@pret", SqlDbType.VarChar).Value = textBoxPret.Text;
                    childAdapter.UpdateCommand.Parameters.Add("@tip", SqlDbType.VarChar).Value = textBoxTip.Text;
                    childAdapter.UpdateCommand.Parameters.Add("@locatie", SqlDbType.VarChar).Value = textBoxLocatie.Text;

                    childAdapter.UpdateCommand.Parameters.Add("@id", SqlDbType.Int).Value = textBoxIdAtractie.Text;

                    connection.Open();
                    x = childAdapter.UpdateCommand.ExecuteNonQuery();
                    connection.Close();

                    if (x >= 1)
                    {
                        MessageBox.Show("Modificare realizata");
                    }

                    refreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }


        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try {

                if (textBoxDenumire.Text == "" || textBoxIdAtractie.Text == "" || textBoxPret.Text == "" || textBoxTip.Text == "" || textBoxLocatie.Text == "")
                {
                    MessageBox.Show("Campuri incomplecte!");
                }
                else
                {
                    DialogResult dr;
                    dr = MessageBox.Show("Atractia va fi stearsa!", "Confirm", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        childAdapter.DeleteCommand = new SqlCommand("Delete from Atractii where IDAtractie = @id", connection);

                        childAdapter.DeleteCommand.Parameters.Add("@id", SqlDbType.Int).Value = textBoxIdAtractie.Text;

                        connection.Open();
                        childAdapter.DeleteCommand.ExecuteNonQuery();
                        connection.Close();


                        MessageBox.Show("Stergere realizata");

                        refreshData();
                    }
                    else
                    {
                        MessageBox.Show("Stergere anulata");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            refreshData();
            //loadData();
        }

        private bool isDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}
