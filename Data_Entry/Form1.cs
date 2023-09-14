using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Data_Entry
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
         MySqlConnection myconn;
         string mycon = "Server=localhost;Database=crud;Uid=root;Pwd=";
         MySqlCommand mycommand;
         MySqlDataReader rdr;
        private void Form1_Load(object sender, EventArgs e)
        {
            myconn = new MySqlConnection(mycon);
            myconn.Open();
            loadCourse(cboCourse);
            loadData();

            cboYear.Items.Add("1st");
            cboYear.Items.Add("2nd");
            cboYear.Items.Add("3rd");
            cboYear.Items.Add("4th");
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
            string lname = txtLastName.Text;
            string fname = txtFirstName.Text;
            string mname = txtMiddleName.Text;
            string address = txtAddress.Text;
            string course = cboCourse.SelectedItem.ToString();
            string year = cboYear.SelectedItem.ToString();
            try
            {
                string sql = "INSERT INTO tbl_students (last_name,first_name,middle_name,address,course,s_year) values " +
                "('" + lname + "','" + fname + "', '" + mname + "','" + address + "','" + course + "','" + year + "')";
                myconn = new MySqlConnection(mycon);
                myconn.Open();
                mycommand = new MySqlCommand(sql, myconn);
                mycommand.ExecuteNonQuery();
                MessageBox.Show("Record added!");
                myconn.Close();
                myconn.Dispose();
                resetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured: " + ex.Message);
            }
          
        }
        public void loadData()
        {
            string sql = "SELECT * from tbl_students";
            myconn = new MySqlConnection(mycon);
            myconn.Open();
            mycommand = new MySqlCommand(sql, myconn);
            ListView.Items.Clear();
            mycommand.ExecuteNonQuery();
            rdr = mycommand.ExecuteReader();
            while (rdr.Read())
            {
                ListViewItem item = new ListViewItem(rdr["id"].ToString());
                item.SubItems.Add(rdr["first_name"].ToString());
                item.SubItems.Add(rdr["last_name"].ToString());
                item.SubItems.Add(rdr["middle_name"].ToString());
                item.SubItems.Add(rdr["address"].ToString());
                item.SubItems.Add(rdr["course"].ToString());
                item.SubItems.Add(rdr["s_year"].ToString());
                ListView.Items.Add(item);
            }
            myconn.Close();
            myconn.Dispose();

        }

        private void loadCourse(System.Windows.Forms.ComboBox combobox)
        {
            try
            {
                combobox.Items.Clear();
                string sql = "SELECT * FROM tbl_year";
                myconn = new MySqlConnection(mycon);
                myconn.Open();
                mycommand = new MySqlCommand(sql, myconn);
                mycommand.ExecuteNonQuery();
                rdr = mycommand.ExecuteReader();
                while (rdr.Read())
                {
                    combobox.Items.Add(rdr[1].ToString());
                }
                myconn.Close();
                myconn.Dispose();

            }catch(Exception ex)
            {
                MessageBox.Show("An error occured: " + ex.Message);
            }
         }

        public void resetAll()
        {
            txtID.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtMiddleName.Text = "";
            txtAddress.Text = "";
            cboCourse.Text = "";
            cboYear.Text = "";

            cmdSave.Enabled = false;
            cmdNew.Enabled = true;
            
        }

        public void BtnDisable()
        {
            cmdCancel.Enabled = false;
            cmdEdit.Enabled = false;
            cmdNew.Enabled = false;
            cmdDelete.Enabled = false;
            cmdUpdate.Enabled = false;
            txtID.Enabled = false;

        }

        private void cmdNew_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT max(id) from tbl_students";
                myconn = new MySqlConnection(mycon);
                myconn.Open();
                mycommand = new MySqlCommand(sql, myconn);
                object maxID = mycommand.ExecuteScalar();

                if(maxID != null && !maxID.Equals(DBNull.Value))
                {
                    int nextID = Convert.ToInt32(maxID) + 1;
                    txtID.Text = nextID.ToString();
                }
                else{
                    txtID.Text = "1";
                }
                myconn.Close();
                myconn.Dispose();
                BtnDisable();
            }
            catch(Exception ex)
            {
                MessageBox.Show("An Error Occured: " + ex.Message);
            }
        
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
