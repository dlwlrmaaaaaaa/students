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

            cboCourse.Items.Add("BSCS");
            cboYear.Items.Add("1st");
            cboYear.Items.Add("2nd");
            cboYear.Items.Add("3rd");
            cboYear.Items.Add("4th");
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }




        private void cmdSave_Click(object sender, EventArgs e)
        {
            string lname = txtLastName.Text;
            string fname = txtFirstName.Text;
            string mname = txtMiddleName.Text;
            string address = txtAddress.Text;
            string course = cboCourse.SelectedItem.ToString();
            string year = cboYear.SelectedItem.ToString();


            string sql = "INSERT INTO tbl_students (last_name,first_name,middle_name,address,course,s_year) values " +
                "('"+lname+"','" + fname + "', '" + mname + "','" + address + "','" + course + "','"+year+"')";
            myconn = new MySqlConnection(mycon);
            myconn.Open();
            mycommand = new MySqlCommand(sql, myconn);
            mycommand.ExecuteNonQuery();
            myconn.Close();
            myconn.Dispose();
          
        }
        private void loadCourse(ComboBox combobox)
        {
            combobox.Items.Clear();
            string sql = "SELECT * FROM tbl_year";
            myconn = new MySqlConnection(mycon);
            myconn.Open();
            mycommand = new MySqlCommand(sql, myconn);
            mycommand.ExecuteNonQuery();
            rdr = mycommand.ExecuteReader();
            while (rdr.Read()){
                combobox.Items.Add(rdr[1].ToString());
            }
            myconn.Close();
            myconn.Dispose();

                
                    
                    
                    }



       
    }
}
