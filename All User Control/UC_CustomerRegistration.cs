using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_Management_System.All_User_Control
{
    public partial class UC_CustomerRegistration : UserControl
    {
        functions fn = new functions();
        String query;
        public UC_CustomerRegistration()
        {
            InitializeComponent();
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }
        
        public void setComboBox(String query,ComboBox combo)
        {
            SqlDataReader sdr = fn.getForCombo(query);
            while(sdr.Read())
            {
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    combo.Items.Add(sdr.GetString(i));
                }
            }
            sdr.Close();
        }

        private void txtRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRoomNo.Items.Clear();
            txtPrice.Clear();
            query = "select roomNo from rooms where roomStyle = '" + txtRoomStyle.Text + "' and roomType = '" + txtRoomType.Text + "' and booked = 'NO' ";
            setComboBox(query, txtRoomNo);
        }

        private void txtBed_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRoomType.SelectedIndex = -1;
            txtRoomNo.Items.Clear();
            txtPrice.Clear();
        }
        int rid;

        private void txtRoomNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            query = "select price,roomid from rooms where roomNo = '" + txtRoomNo.Text + "'";
            DataSet ds = fn.getData(query);
            txtPrice.Text = ds.Tables[0].Rows[0][0].ToString();
            rid = int.Parse(ds.Tables[0].Rows[0][1].ToString());

        }

        private void btnAlloteRoom_Click(object sender, EventArgs e)
        {
            if(txtName.Text != "" && txtMobile.Text != "" && txtNationality.Text != "" && txtGender.Text != "" && txtDob.Text != "" && txtIdProof.Text != "" && txtAddress.Text != "" && txtCheckIn.Text != "" && txtPrice.Text != "")
            {
                String name = txtName.Text;
                String mobile = txtMobile.Text;
                String national = txtNationality.Text;
                String gender = txtGender.Text;
                String dob = txtDob.Text;
                String idproof = txtIdProof.Text;
                String address = txtAddress.Text;
                String checkin = txtCheckIn.Text;


                query = "insert into customer (cname,mobile,nationality,gender,dob,idproof,addres,checkin,roomid) values ('" + name + "','" + mobile + "','" + national + "','" + gender + "','" + dob + "','" + idproof + "','" + address + "','" + checkin + "', " + rid + ") update rooms set booked ='YES' where roomNo = '"+txtRoomNo.Text+"'";
                fn.setData(query, "Room No " + txtRoomNo.Text + " Allocation Succesful.");
                clearAll();
            }
            else
            {
                MessageBox.Show("All fields are madetory.", "Information !!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void clearAll()
        {
            txtName.Clear();
            txtMobile.Clear();
            txtNationality.Clear();
            txtGender.SelectedIndex = -1;
            txtDob.ResetText();
            txtIdProof.Clear();
            txtAddress.Clear();
            txtCheckIn.ResetText();
            txtRoomStyle.SelectedIndex = -1;
            txtRoomType.SelectedIndex = -1;
            txtRoomNo.Items.Clear();
            txtPrice.Clear();
            BF_checkBox.Checked = false;
        }

        private void UC_CustomerRegistration_Leave(object sender, EventArgs e)
        {
            clearAll();
        }
        Int64 sum = 120;

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(txtRoomStyle.Text == "" || txtRoomType.Text == "" || txtRoomNo.Text == "" || txtPrice.Text == ""  )
            {
                BF_checkBox.Checked = false;
            }
            if (BF_checkBox.Checked==true && txtRoomType.Text == "Single Room")
            {
                checkBox_BF_Price_Add(120);
            }
            if (BF_checkBox.Checked == false && txtRoomType.Text == "Single Room")
            {
                checkBox_BF_Price_Low(120);
            }
            if (BF_checkBox.Checked == true && txtRoomType.Text == "Double Room")
            {
                checkBox_BF_Price_Add(200);
            }
            if (BF_checkBox.Checked == false && txtRoomType.Text == "Double Room")
            {
                checkBox_BF_Price_Low(120);
            }
            if (BF_checkBox.Checked == true && txtRoomType.Text == "Family Room")
            {
                checkBox_BF_Price_Add(350);
            }
            if (BF_checkBox.Checked == false && txtRoomType.Text == "Family Room")
            {

                checkBox_BF_Price_Low(350);
            }
            if (BF_checkBox.Checked == true && txtRoomType.Text == "Suite Room")
            {
                checkBox_BF_Price_Add(350);
            }
            if (BF_checkBox.Checked == false && txtRoomType.Text == "Suite Room")
            {
                checkBox_BF_Price_Low(350);
            }

        }

        public void checkBox_BF_Price_Add (int num)
        {
            Int64 price = Int64.Parse(txtPrice.Text) + num;
            txtPrice.Text = price.ToString();
        }
        public void checkBox_BF_Price_Low(int num)
        {
            Int64 price = Int64.Parse(txtPrice.Text) - num;
            txtPrice.Text = price.ToString();
        }
    }
}
