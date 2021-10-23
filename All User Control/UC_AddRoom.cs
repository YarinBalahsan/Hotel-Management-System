using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_Management_System.All_User_Control
{
    public partial class UC_AddRoom : UserControl
    {
        functions fn = new functions();
        String query;
        public UC_AddRoom()
        {
            InitializeComponent();
        }

        private void UC_AddRoom_Load(object sender, EventArgs e)
        {
            query = "select * from rooms";
            DataSet ds = fn.getData(query);
            guna2DataGridView1.DataSource = ds.Tables[0];
            
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            if(txtRoomNumber.Text != "" && txtRoomType.Text != "" && txtRoomStyle.Text != "" && txtPrice.Text != "" )
            {
                String roomNumber = txtRoomNumber.Text;
                String roomType = txtRoomType.Text;
                String roomStyle = txtRoomStyle.Text;
                Int64 price = Int64.Parse(txtPrice.Text);

                query = "INSERT INTO rooms (roomNo,RoomType,RoomStyle,price) VALUES ('" + roomNumber+ "','" +roomType+ "', '" +roomStyle+ "', '" +price+"')";
                fn.setData(query, "Room Added.");

                UC_AddRoom_Load(this, null);
                clearAll();
            }
            else
            {
                MessageBox.Show("Fill All Fields.","Warning !",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
        public void clearAll()
        {
            txtRoomNumber.Clear();
            txtRoomType.SelectedIndex = -1;
            txtRoomStyle.SelectedIndex = -1;
            txtPrice.Clear();
        }

        private void UC_AddRoom_Leave(object sender, EventArgs e)
        {
            clearAll();
        }

        private void UC_AddRoom_Enter(object sender, EventArgs e)
        {
            UC_AddRoom_Load(this, null);
        }
    }
}
