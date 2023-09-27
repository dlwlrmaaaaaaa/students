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
            loadStudents(ListView);
           // loadData();
            SBtnDisable();
            txtBoxDisabled();
        
        }

        private void loadStudents(System.Windows.Forms.ListView lvwlist)
        {
            string sql = "SELECT * FROM tbl_students order by id";
            myconn = new MySqlConnection(mycon);
            myconn.Open();
            mycommand = new MySqlCommand(sql, myconn);         
            rdr = mycommand.ExecuteReader();
            ListView.Items.Clear();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                     lvwlist.Items.Add(rdr.GetString(0));
                    lvwlist.Items[lvwlist.Items.Count - 1].SubItems.Add(rdr[1].ToString());
                    lvwlist.Items[lvwlist.Items.Count - 1].SubItems.Add(rdr[2].ToString());
                    lvwlist.Items[lvwlist.Items.Count - 1].SubItems.Add(rdr[3].ToString());
                    lvwlist.Items[lvwlist.Items.Count - 1].SubItems.Add(rdr[4].ToString());
                    lvwlist.Items[lvwlist.Items.Count - 1].SubItems.Add(rdr[5].ToString());
                    lvwlist.Items[lvwlist.Items.Count - 1].SubItems.Add(rdr[6].ToString());

                }
            }
        }
      /*  public void loadData()
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

        }*/

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
            ListView.Enabled = true;
            cmdSave.Enabled = false;
            cmdCancel.Enabled = false;
            cmdNew.Enabled = true;
            
        }
        public void resetNewAll()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtMiddleName.Text = "";
            txtAddress.Text = "";
            cboCourse.SelectedIndex = -1;
            cboYear.SelectedIndex = -1;
            cmdSave.Enabled = true;

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



      /* private void ListView_MouseClick(object sender, MouseEventArgs e)
          {
              cmdEdit.Enabled = true;
              cmdDelete.Enabled = true;
              cmdNew.Enabled = true;
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

    }*/
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
           // loadData();
            SBtnDisable();
            resetAll();
            txtBoxDisabled();
        }
        private void cmdNew_Click(object sender, EventArgs e)
        {
            try
            {
                int sID = 0;
                string sql = "SELECT * from tbl_students order by id desc";
                myconn = new MySqlConnection(mycon);
                myconn.Open();
                mycommand = new MySqlCommand(sql, myconn);
                rdr = mycommand.ExecuteReader();

                if (rdr.HasRows){
                    while (rdr.Read())
                    {
                        sID = int.Parse(rdr[0].ToString());
                        int nID = sID + 1;
                        txtID.Text = nID.ToString();
                        break;
                    }
                }

               /* if (maxID != null && !maxID.Equals(DBNull.Value))
                {
                    int nextID = Convert.ToInt32(maxID) + 1;
                    txtID.Text = nextID.ToString();
                }
                else
                {
                    txtID.Text = "1";
                }*/
                myconn.Close();
                myconn.Dispose();
                cmdSave.Enabled = true;
                cmdCancel.Enabled = true;
                ListView.Enabled = false;
                BtnDisable();
                resetNewAll();
                txtBoxEnabled();

            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error Occured: " + ex.Message);
            }

        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
           // string sql = "DELETE FROM crud.tbl_students WHERE ID = '" + txtID.Text + "'";
           // myconn = new MySqlConnection(mycon);
           // myconn.Open();
           // mycommand = new MySqlCommand(sql, myconn);
           // mycommand.ExecuteNonQuery();
            try
            {
                DialogResult rslt = MessageBox.Show("Do you really want to delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
               if(rslt == DialogResult.Yes)
                {
                    string sql = "DELETE FROM tbl_students WHERE ID = '" + txtID.Text + "'";
                    myconn = new MySqlConnection(mycon);
                    myconn.Open();
                    mycommand = new MySqlCommand(sql, myconn);
                    mycommand.ExecuteNonQuery();
                    myconn.Close();

                    MessageBox.Show("Record Deleted!");
                    loadStudents(ListView);
                    resetAll();
                    cmdEdit.Enabled = false;
                    cmdDelete.Enabled = false;
                }
                else
                {

                    cmdDelete.Enabled = true;
                }
                /*if (ListView.SelectedItems.Count > 0)
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
                }*/
               


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
          
            try
            {
                string lname = txtLastName.Text;
                string fname = txtFirstName.Text;
                string mname = txtMiddleName.Text;
                string address = txtAddress.Text;
                string course = cboCourse.SelectedItem?.ToString();
                string year = cboYear.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(lname) && !string.IsNullOrEmpty(fname) && !string.IsNullOrEmpty(address)
                    && !string.IsNullOrEmpty(course) && !string.IsNullOrEmpty(year))
                {
                    string sql = "INSERT INTO tbl_students (last_name,first_name,middle_name,address,course,s_year) values " +
                                   "('" + lname + "','" + fname + "', '" + mname + "','" + address + "','" + course + "','" + year + "')";
                    myconn = new MySqlConnection(mycon);
                    myconn.Open();
                    mycommand = new MySqlCommand(sql, myconn);
                    mycommand.ExecuteNonQuery();
                    MessageBox.Show("Record added!");
                    // loadData();
                    loadStudents(ListView);
                    myconn.Close();
                    myconn.Dispose();
                    resetAll();
                    txtBoxDisabled();

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
            cmdCancel.Enabled = true;
            txtBoxEnabled();

          


        }
        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtID.Text);
                string sql = "UPDATE tbl_students SET last_name = '" + txtLastName.Text + "', first_name = '" + txtFirstName.Text + "'," +
                    "middle_name ='" + txtMiddleName.Text + "', address='" + txtAddress.Text + "', course ='" + cboCourse.Text + "', s_year= '" + cboYear.Text + "'WHERE id =" + id;
                myconn = new MySqlConnection(mycon);
                myconn.Open();
                mycommand = new MySqlCommand(sql, myconn);
                mycommand.ExecuteNonQuery();
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
                MessageBox.Show("Record updated successfully.", "Record Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListView.SelectedItems[0].Selected = false;
                resetAll();
                cmdEdit.Enabled = false;           
                cmdUpdate.Enabled = false;
                cmdNew.Enabled = true;
                myconn.Close();
                myconn.Dispose();


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

        private void ListView_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem itm = ListView.SelectedItems[0];
            txtID.Text = itm.Text;
            txtLastName.Text = itm.SubItems[1].Text;
            txtFirstName.Text = itm.SubItems[2].Text;
            txtMiddleName.Text = itm.SubItems[3].Text;
            txtAddress.Text = itm.SubItems[4].Text;
            cboCourse.Text = itm.SubItems[5].Text;
            cboYear.Text = itm.SubItems[6].Text;
            txtBoxDisabled();
            cmdDelete.Enabled = true;
            cmdEdit.Enabled = true;
        }

        private void txtLastName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtFirstName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtMiddleName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
