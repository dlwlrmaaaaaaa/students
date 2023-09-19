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
        public string getID;
         MySqlConnection myconn;
         string mycon = "Server=localhost;Database=crud;Uid=root;Pwd=";
         MySqlCommand mycommand;
         MySqlDataReader rdr;
        private void Form1_Load(object sender, EventArgs e)
        {
            myconn = new MySqlConnection(mycon);
            myconn.Open();
            loadCourse(cboCourse);
            loadYear(cboYear);
            loadData();
            SBtnDisable();
            txtBoxDisabled();
        
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

        private void loadYear(System.Windows.Forms.ComboBox comboBox)
        {
            try
            {
                comboBox.Items.Clear();
                string sql = "SELECT * FROM tbl_syear";
                myconn = new MySqlConnection(mycon);
                myconn.Open();
                mycommand = new MySqlCommand(sql, myconn);
                mycommand.ExecuteNonQuery();
                rdr = mycommand.ExecuteReader();
                while (rdr.Read())
                {
                    comboBox.Items.Add(rdr[1].ToString());
                }
                myconn.Close();
                myconn.Dispose();



            }
            catch (Exception ex)
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
            cboCourse.SelectedIndex = -1;
            cboYear.SelectedIndex = -1;

            cmdSave.Enabled = false;
            cmdNew.Enabled = true;
            
        }

        public void BtnDisable()
        {
            cmdEdit.Enabled = false;
            cmdNew.Enabled = false;
            cmdDelete.Enabled = false;
            cmdUpdate.Enabled = false;
            txtID.Enabled = false;
        }
        public void SBtnDisable()
        {
            cmdCancel.Enabled = false;
            cmdEdit.Enabled = false;
            cmdSave.Enabled = false;
            cmdDelete.Enabled = false;
            cmdUpdate.Enabled = false;
            txtID.Enabled = false;
        }

        

        private void ListView_MouseClick(object sender, MouseEventArgs e)
        {
            cmdEdit.Enabled = true;
            cmdDelete.Enabled = true;
            cmdNew.Enabled = false;
            if (ListView.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = ListView.SelectedItems[0];
                getID = selectedItem.SubItems[0].Text;
                try
                {
                    string sql = "SELECT * from tbl_students where id=" + Convert.ToInt32(getID);
                    myconn = new MySqlConnection(mycon);
                    myconn.Open();
                    mycommand = new MySqlCommand(sql, myconn);
                    mycommand.ExecuteNonQuery();
                    rdr = mycommand.ExecuteReader();
                    while (rdr.Read())
                    {
                        txtID.Text = rdr.GetString(0);
                        txtLastName.Text = rdr.GetString(1);
                        txtFirstName.Text = rdr.GetString(2);
                        txtMiddleName.Text = rdr.GetString(3);
                        txtAddress.Text = rdr.GetString(4);
                        cboCourse.Text = rdr.GetString(5);
                        cboYear.Text = rdr.GetString(6);
                    }
                    myconn.Close();
                    myconn.Dispose();




                }
                catch (Exception ex)
                {
                    MessageBox.Show("An Error Occured: " + ex.Message);
                }




            }
           
        }      
        public void txtBoxDisabled()
        {
            txtAddress.Enabled = false;
            txtFirstName.Enabled = false;
            txtLastName.Enabled = false;
            txtMiddleName.Enabled = false;
            cboCourse.Enabled = false; 
            cboYear.Enabled = false;
        }
        public void txtBoxEnabled()
        {
            txtAddress.Enabled = true;
            txtFirstName.Enabled = true;
            txtLastName.Enabled = true;
            txtMiddleName.Enabled = true;
            cboCourse.Enabled = true;
            cboYear.Enabled = true;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            myconn = new MySqlConnection(mycon);
            myconn.Open();
            loadCourse(cboCourse);
            loadYear(cboYear);
            loadData();
            SBtnDisable();
            resetAll();
            txtBoxDisabled();
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

                if (maxID != null && !maxID.Equals(DBNull.Value))
                {
                    int nextID = Convert.ToInt32(maxID) + 1;
                    txtID.Text = nextID.ToString();
                }
                else
                {
                    txtID.Text = "1";
                }
                myconn.Close();
                myconn.Dispose();
                cmdSave.Enabled = true;
                cmdCancel.Enabled = true;
                BtnDisable();
                txtBoxEnabled();

            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error Occured: " + ex.Message);
            }

        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "DELETE FROM `tbl_students` WHERE id =" + Convert.ToInt32(getID);
                myconn = new MySqlConnection(mycon);
                myconn.Open();
                mycommand = new MySqlCommand(sql, myconn);
                mycommand.ExecuteNonQuery();
                myconn.Close();

                MessageBox.Show("Record Deleted!");
                if (ListView.SelectedItems.Count > 0)
                {
                    ListViewItem selectedItem = ListView.SelectedItems[0];

                    // Update the ListView item with the edited data.
                    selectedItem.SubItems[0].Text = "";
                    selectedItem.SubItems[1].Text = txtLastName.Text;
                    selectedItem.SubItems[2].Text = txtFirstName.Text;
                    selectedItem.SubItems[3].Text = txtMiddleName.Text;
                    selectedItem.SubItems[4].Text = txtAddress.Text;
                    selectedItem.SubItems[5].Text = cboCourse.Text;
                    selectedItem.SubItems[6].Text = cboYear.Text;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                if(!string.IsNullOrEmpty(lname) && !string.IsNullOrEmpty(fname) && !string.IsNullOrEmpty(address)
                    && !string.IsNullOrEmpty(course) && !string.IsNullOrEmpty(year))
                {
                    string sql = "INSERT INTO tbl_students (last_name,first_name,middle_name,address,course,s_year) values " +
                                   "('" + lname + "','" + fname + "', '" + mname + "','" + address + "','" + course + "','" + year + "')";
                    myconn = new MySqlConnection(mycon);
                    myconn.Open();
                    mycommand = new MySqlCommand(sql, myconn);
                    mycommand.ExecuteNonQuery();
                    MessageBox.Show("Record added!");
                    loadData();
                    myconn.Close();
                    myconn.Dispose();
                    resetAll();
                }
                else
                {
                    MessageBox.Show("Please fill up all missing information!", "Missing Details", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured: " + ex.Message);
            }

        }
        private void cmdEdit_Click(object sender, EventArgs e)
        {
            cmdEdit.Enabled = false;
            cmdUpdate.Enabled = true;
            cmdNew.Enabled = false;
            cmdDelete.Enabled = false;
            txtBoxEnabled();

          


        }
        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "UPDATE `tbl_students` SET `last_name` = '" + txtLastName.Text + "', `first_name`= '" + txtFirstName.Text + "'," +
                    "`middle_name`='" + txtMiddleName.Text + "', `address`='" + txtAddress.Text + "', `course`='" + cboCourse.Text + "',`s_year`='" + cboYear.Text + "'WHERE `tbl_students`.`id` = 2";
                myconn = new MySqlConnection(mycon);
                myconn.Open();
                mycommand = new MySqlCommand(sql, myconn);
                mycommand.ExecuteNonQuery();
                myconn.Close();
                myconn.Dispose();
                MessageBox.Show("Record updated successfully.");

                if (ListView.SelectedItems.Count > 0)
                {
                    ListViewItem selectedItem = ListView.SelectedItems[0];                 
                    // Update the ListView item with the edited data.
                    selectedItem.SubItems[1].Text = txtLastName.Text;
                    selectedItem.SubItems[2].Text = txtFirstName.Text;
                    selectedItem.SubItems[3].Text = txtMiddleName.Text;
                    selectedItem.SubItems[4].Text = txtAddress.Text;
                    selectedItem.SubItems[5].Text = cboCourse.Text;
                    selectedItem.SubItems[6].Text = cboYear.Text;                

                }
                ListView.SelectedItems[0].Selected = false;
                resetAll();
                cmdEdit.Enabled = false;           
                cmdUpdate.Enabled = false;
                cmdNew.Enabled = true;



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!e.IsSelected)
            {
                cmdEdit.Enabled = false;
                cmdDelete.Enabled = false;
                cmdNew.Enabled = true;
                resetAll();
            }
        }
    }
}
