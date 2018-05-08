using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myproject
{
   

    public partial class Dashboard : Form
    {
        SqlDataAdapter sqlDataAdapter;
        SqlCommandBuilder sqlCommandBuilder;
        DataTable dataTable;
        SqlConnection conn = DBConnection.Instance();

        int userId = 0;
        int datId = 0;
        int houseId = 0;
        string username = "";
        string password = "";

        //fields of house management
        Dictionary<string, string> fieldHouses = new Dictionary<string, string>();
        Dictionary<string, Control> fieldOHouses = new Dictionary<string, Control>();

        public Dashboard()
        {
            InitializeComponent();
            this.CenterToScreen();

            //set username_logged
            username_logged.Text = Program.getUsername();

            //Console.WriteLine(Program.username);

            this.loadUsers();
            this.loadDat();
            this.loadHouses();

            //DateTime dt = DateTime.Now;
            thoi_gian.ShowUpDown = true;
            this.thoi_gian.CustomFormat = "dd/MM/yyyy";
            thoi_gian.Format = System.Windows.Forms.DateTimePickerFormat.Custom;

            //thoi gian nha
            thoi_gian_nha.ShowUpDown = true;
            thoi_gian_nha.CustomFormat = "dd/MM/yyyy";
            thoi_gian_nha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;

            /*string path = Application.StartupPath + "\\maps.html";

            Console.WriteLine(path);
            
            webBrowser1.Url = new Uri(path);*/

            //set role datasource
            role.DataSource = new RoleItem[]
            {
                new RoleItem{ID=1, Text="Admin" },
                new RoleItem{ID=2, Text="User" },
            };


            //buttons

            add_user_btn.Enabled = true;
            user_update_btn.Enabled = false;

            //buttons dat
            update_dat_btn.Enabled = false;

            //load user items
            loadUserItem();

            //get all textbox of house
            fieldHouses = getAllTextboxOfHouseTab();

            //button houses
            update_house.Enabled = false;


            
        }


        public Dictionary<string, Control> getAllControlOfHouseTab()
        {
            Dictionary<string, Control> r = new Dictionary<string, Control>();

            foreach (Control c in tabControl1.TabPages[2].Controls)
            {
                if (c is TextBox || c is ComboBox || c is DateTimePicker )
                {
                    r.Add(c.Name, c);
                }
            }

            return r;

        }

        public Dictionary<string, string> getAllTextboxOfHouseTab()
        {
             Dictionary<string, string> fieldHouses = new Dictionary<string, string>(); 
                
             foreach (Control c in tabControl1.TabPages[2].Controls)
             {
                 
                  if (c is TextBox){
                    //MessageBox.Show(cc.Name);
                    //Console.WriteLine(c.Name);
                        fieldHouses.Add(c.Name, c.Text);
                  }

                 
                  
             }

             return fieldHouses;
        }

        public void loadUserItem()
        {
            SqlConnection conn = DBConnection.Instance();
            conn.Open();

            SqlCommand command = new SqlCommand("Select * from users where role_id=2", conn);

            List<UserItem> userItems = new List<UserItem>();
           
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    //Console.WriteLine(String.Format("{0}", reader["id"]));

                    userItems.Add(new UserItem { id = (int)reader["id"], username = ((string)reader["username"]).Trim() });
                }
            }

            user_id.DataSource = userItems;

            conn.Close();
        }

        public void loadDat()
        {
            var conn = DBConnection.Instance();
            sqlDataAdapter = new SqlDataAdapter(@"SELECT ID, ID_DAT, MO_TA, DUONG_DAN, LOAI, GHI_CHU, THOI_GIAN FROM TBL_HO_SO_DAT ", conn);
            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;
            //dataGridView1.Rows.RemoveAt(2);
        }

        public void loadHouses()
        {
            sqlDataAdapter = new SqlDataAdapter(@"SELECT  id as ID, ten as Tên, dia_chi as 'Địa Chỉ', nam_xay_dung as 'Năm Xây Dựng', nam_dua_vao_sd as 'Năm Đưa Vào Sử Dụng', so_nam_sd_con_lai as 'Số Năm Sử Dụng Còn Lại', nam_ket_thuc as 'Năm Kết Thúc',so_tang as 'Số Tầng', dm_cap_cong_trinh as  'Danh Mục Cấp Công Trình', dien_tich as 'Diện Tích', dm_hang_muc_cong_trinh as 'Danh Mục Hạng Mục Công Trình', nguyen_gia as 'Nguyên Giá', ty_le_con_lai as 'Tỷ Lệ Còn Lại', ty_le_khau_hao as 'Tỷ Lệ Khấu Hao', luy_ke as 'Lũy Kế', gia_tri_con_lai as 'Giá Trị Còn Lại', so_do as 'Sổ Đỏ', dm_tinh_trang_su_dung as 'Danh Mục Tình Trạng Sử Dụng', ghi_chu_nha as 'Ghi Chú', CONVERT(CHAR(10), thoi_gian, 103) as 'Thời Gian' , Username = (SELECT username FROM users WHERE id=user_id) FROM nha ", conn);
            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            dataGridViewHouse.DataSource = dataTable;
        }

        public void loadUsers()
        {
            var conn = DBConnection.Instance();
            sqlDataAdapter = new SqlDataAdapter(@"SELECT id, username, password, role_id FROM users ", conn);
            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            userDataGridView.DataSource = dataTable;

            
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            userId = Convert.ToInt32( userDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
            username = userDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            password = userDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
            //roleId = Convert.ToInt32(userDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString());

            MessageBox.Show(username);


        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'quanlydatDataSet.users' table. You can move, or remove it, as needed.
            //this.usersTableAdapter.Fill(this.quanlydatDataSet.users);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void add_user_btn_Click(object sender, EventArgs e)
        {
            

            RoleItem roleItem = (RoleItem)role.SelectedItem;
            int roleId = roleItem.ID;

            //MessageBox.Show(roleId.ToString());

            if (add_user_username.Text == "" || add_user_password.Text == "")
            {
                MessageBox.Show("Vui lòng nhập Username và Password");
                return;
            }

            try
            {
                //Create SqlConnection


                
                SqlCommand cmd = new SqlCommand("INSERT INTO users(username, password, role_id) VALUES (@username,@password, @role_id)", conn );
                cmd.Parameters.AddWithValue("@username", add_user_username.Text);
                cmd.Parameters.AddWithValue("@password", add_user_password.Text);
                cmd.Parameters.AddWithValue("@role_id", roleId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                this.loadUsers();
                
                MessageBox.Show("Đã thêm user thành công !");


                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void logout_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form1 form1 = new Form1();
            form1.Show();
        }

        private void user_update_btn_Click(object sender, EventArgs e)
        {
            //sqlCommandBuilder = new SqlCommandBuilder(sqlDataAdapter);
            //sqlDataAdapter.Update(dataTable);
            //MessageBox.Show(userId.ToString());
            //update user

            RoleItem roleItem = (RoleItem)role.SelectedItem;
            int roleId = roleItem.ID;

            if (userId != 0 )
            {
                SqlCommand cmd = new SqlCommand("UPDATE users SET username=@username, password=@password, role_id=@role_id WHERE id=@id", conn);
                cmd.Parameters.AddWithValue("@username", add_user_username.Text);
                cmd.Parameters.AddWithValue("@password", add_user_password.Text);
                cmd.Parameters.AddWithValue("@role_id", roleId);
                cmd.Parameters.AddWithValue("@id", userId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                this.loadUsers();

                MessageBox.Show("Cập nhật user thành công !");

                add_user_username.Text = "";
                add_user_password.Text = "";
                role.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Không thể cập nhật user !");
            }


            add_user_btn.Enabled = true;
            user_update_btn.Enabled = false;

        }

        private void userDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //var selectedRow = e.RowIndex;

            userId = Convert.ToInt32(userDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());

            //MessageBox.Show(userDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
        }

        private void user_delete_btn_Click(object sender, EventArgs e)
        {
            try
            {
                
                SqlCommand cmd = new SqlCommand("DELETE FROM users WHERE id=@id", conn);
                cmd.Parameters.AddWithValue("@id", userId);
                
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                this.loadUsers();

                MessageBox.Show("Đã xóa user thành công !");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void them_dat_btn_Click(object sender, EventArgs e)
        {
            //MessageBox.Show( thoi_gian.Value.ToString() );

            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO TBL_HO_SO_DAT(ID_DAT,MO_TA, DUONG_DAN, LOAI, GHI_CHU, THOI_GIAN) VALUES (@ID_DAT,@MO_TA, @DUONG_DAN, @LOAI, @GHI_CHU, @THOI_GIAN)", conn);
                cmd.Parameters.AddWithValue("@ID_DAT", id_dat.Text);
                cmd.Parameters.AddWithValue("@MO_TA", mo_ta.Text);
                cmd.Parameters.AddWithValue("@DUONG_DAN", duong_dan.Text);
                cmd.Parameters.AddWithValue("@LOAI", loai.Text);
                cmd.Parameters.AddWithValue("@GHI_CHU", ghi_chu.Text);
                cmd.Parameters.AddWithValue("@THOI_GIAN", thoi_gian.Value.Date);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                this.loadDat();

                MessageBox.Show("Đã thêm Hồ sơ đất thành công !");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //webBrowser1.Url = new Uri("https://google.com.vn");
            //webBrowser1.Navigate("http://www.google.com");
        }

        private void userDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if(userDataGridView.CurrentRow.Index != -1)
            {
                add_user_username.Text = userDataGridView.CurrentRow.Cells[1].Value.ToString();
                add_user_password.Text = userDataGridView.CurrentRow.Cells[2].Value.ToString();
                userId = Convert.ToInt32(userDataGridView.CurrentRow.Cells[0].Value.ToString());

                int _roleId = Convert.ToInt32(userDataGridView.CurrentRow.Cells[3].Value.ToString());

                if(_roleId == 1)
                {
                    //var _item = (RoleItem)role.Items[0];
                    //_item.Selected = true;

                    role.SelectedIndex = 0;
                }
                else
                {
                    role.SelectedIndex = 1;
                }

                add_user_btn.Enabled = false;
                user_update_btn.Enabled = true;
            }
        }

        private void reset_Click(object sender, EventArgs e)
        {
            add_user_btn.Enabled = true;
            user_update_btn.Enabled = false;
            add_user_username.Text = "";
            add_user_password.Text = "";
            role.SelectedIndex = 0;
        }

        private void reset_dat_btn_Click(object sender, EventArgs e)
        {
            them_dat_btn.Enabled = true;
            update_dat_btn.Enabled = false;

            id_dat.Text = "";
            mo_ta.Text = "";
            duong_dan.Text = "";
            loai.Text = "";
            ghi_chu.Text = "";

        }

        private void delete_dat_btn_Click(object sender, EventArgs e)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("DELETE FROM TBL_HO_SO_DAT WHERE ID=@id", conn);
                cmd.Parameters.AddWithValue("@id", datId);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                this.loadDat();

                MessageBox.Show("Đã xóa hồ sơ đất thành công !");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void dataGridView1_RowHeaderMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            datId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

            //MessageBox.Show(datId.ToString());
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                datId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());

                id_dat.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                mo_ta.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                duong_dan.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                loai.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                ghi_chu.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                thoi_gian.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[6].Value.ToString() , CultureInfo.InvariantCulture);




               

                them_dat_btn.Enabled = false;
                update_dat_btn.Enabled = true;
            }
        }

        private void update_dat_btn_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("UPDATE TBL_HO_SO_DAT SET ID_DAT=@ID_DAT, MO_TA=@MO_TA, DUONG_DAN=@DUONG_DAN, LOAI=@LOAI, GHI_CHU=@GHI_CHU, THOI_GIAN=@THOI_GIAN WHERE ID=@ID", conn);
            cmd.Parameters.AddWithValue("@ID_DAT", id_dat.Text);
            cmd.Parameters.AddWithValue("@MO_TA", mo_ta.Text);
            cmd.Parameters.AddWithValue("@DUONG_DAN", duong_dan.Text);
            cmd.Parameters.AddWithValue("@LOAI", loai.Text);
            cmd.Parameters.AddWithValue("@GHI_CHU", ghi_chu.Text);
            cmd.Parameters.AddWithValue("@THOI_GIAN", thoi_gian.Value.Date);

            cmd.Parameters.AddWithValue("@ID", datId);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            this.loadDat();

            MessageBox.Show("Cập nhật hồ sơ đất thành công !");

            id_dat.Text = "";
            mo_ta.Text = "";
            duong_dan.Text = "";
            loai.Text = "";
            ghi_chu.Text = "";
            thoi_gian.CustomFormat = "dd/MM/yyyy";
            thoi_gian.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            thoi_gian.Value = DateTime.Now;

            them_dat_btn.Enabled = true;
            update_dat_btn.Enabled = false;

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void save_house_Click(object sender, EventArgs e)
        {
            string fields = "";
            string value_fields = "";

            fieldHouses = getAllTextboxOfHouseTab();


            foreach (var fieldName in fieldHouses)
            {
                //MessageBox.Show(fieldName);

                fields += fieldName.Key + "," ;
                value_fields += "@" + fieldName.Key + ",";

                
            }

            fields = fields.Remove(fields.Length - 1) + ",thoi_gian,user_id";
            value_fields = value_fields.Remove(value_fields.Length - 1) + ",@thoi_gian,@user_id";

            Console.WriteLine(fields);
            Console.WriteLine(value_fields);

            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO nha(" + fields + ") VALUES (" + value_fields + ")", conn);

                foreach (var fieldName in fieldHouses)
                {
                    if(fieldName.Key == "so_tang")
                    {
                        cmd.Parameters.AddWithValue("@" + fieldName.Key, int.Parse(fieldName.Value) );
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@" + fieldName.Key, fieldName.Value);
                    }
                }

                cmd.Parameters.AddWithValue("@thoi_gian", thoi_gian_nha.Value.Date);
                cmd.Parameters.AddWithValue("@user_id", ((UserItem)user_id.SelectedItem).id);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Thêm hồ sơ nhà thành công !");

                loadHouses();
            }
            catch(Exception _ex){
                Console.WriteLine("Save house ex : " + _ex);
            }

            

            
        }

        private void dataGridViewHouse_DoubleClick(object sender, EventArgs e)
        {
            save_house.Enabled = false;
            update_house.Enabled = true;

            houseId = Convert.ToInt32(dataGridViewHouse.CurrentRow.Cells[0].Value.ToString());


            SqlConnection conn = DBConnection.Instance();
            conn.Open();
            SqlCommand command = new SqlCommand("Select * from nha where id=@id", conn);

            command.Parameters.AddWithValue("@id", houseId);



            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    foreach(var control in getAllControlOfHouseTab())
                    {
                        if(control.Value is TextBox)
                        {
                            control.Value.Text = reader[control.Key].ToString();
                        }
                        if(control.Value is DateTimePicker)
                        {
                            DateTimePicker _c = (DateTimePicker)control.Value;
                            _c.Value = Convert.ToDateTime(reader["thoi_gian"].ToString(), CultureInfo.InvariantCulture);
                        }

                        if (control.Value is ComboBox)
                        {
                            int _index = 0;

                            foreach(UserItem item in user_id.Items)
                            {
                                if (item.id == (int)reader["user_id"] )
                                {
                                    user_id.SelectedIndex = _index;
                                }

                                _index ++;
                            }
                        }

                    }
                }
            }

            
        }

        private void delete_house_Click(object sender, EventArgs e)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("DELETE FROM nha WHERE ID=@id", conn);
                cmd.Parameters.AddWithValue("@id", houseId);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                loadHouses();

                MessageBox.Show("Đã xóa hồ sơ nhà thành công !");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void dataGridViewHouse_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            houseId = Convert.ToInt32(dataGridViewHouse.CurrentRow.Cells[0].Value);
        }

        private void reset_house_Click(object sender, EventArgs e)
        {
            save_house.Enabled = true;
            update_house.Enabled = false;

            foreach (var control in getAllControlOfHouseTab())
            {
                if (control.Value is TextBox)
                {
                    control.Value.Text = "";
                }

            }

            so_tang.Text = "1";

            loadUserItem();
            //thoi gian nha

            thoi_gian_nha.ShowUpDown = true;
            thoi_gian_nha.CustomFormat = "dd/MM/yyyy";
            thoi_gian_nha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            thoi_gian_nha.Value = DateTime.Now;

            //MessageBox.Show("Reset Successfully !");
        }

        private void update_house_Click(object sender, EventArgs e)
        {
            string fieldsToUpdate = "";

            foreach(var control in getAllControlOfHouseTab())
            {

                if(control.Key == "thoi_gian_nha")
                {
                    fieldsToUpdate += "thoi_gian=@thoi_gian,";
                }
                else
                {
                    fieldsToUpdate += control.Key + "=@" + control.Key + ",";
                }
                
            }

            fieldsToUpdate = fieldsToUpdate.Remove(fieldsToUpdate.Length - 1);
        

            string sqlCmd = String.Format("UPDATE nha SET {0} WHERE id=@id", fieldsToUpdate);

            SqlCommand cmd = new SqlCommand(sqlCmd, conn);

            foreach (var control in getAllControlOfHouseTab())
            {

                if (control.Key == "thoi_gian_nha")
                {
                    cmd.Parameters.AddWithValue("@thoi_gian", thoi_gian_nha.Value.Date);
                }
                
                if(control.Value is TextBox)
                {
                    if(control.Key == "so_tang")
                    {
                        cmd.Parameters.AddWithValue("@" + control.Key, Convert.ToInt32(control.Value.Text));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@" + control.Key, control.Value.Text);
                    }
                    
                }

                if(control.Key == "user_id")
                {
                    cmd.Parameters.AddWithValue("@user_id", ( (UserItem)user_id.SelectedItem ).id );
                }

            }
            cmd.Parameters.AddWithValue("@id", houseId);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            loadHouses();

            MessageBox.Show("Đã cập nhật hồ sơ nhà thành công !");

            
        }

        private void maps_Click(object sender, EventArgs e)
        {
            Process.Start("E:\\FreelancerProject\\Quản Lý Đất Đai\\myproject\\myproject\\bin\\Maps\\Demo.WindowsForms.exe");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    class RoleItem
    {
        public int ID { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }

    }

    class UserItem
    {
        public int id { get; set; }
        public string username { get; set; }

        public override string ToString()
        {
            return username;
        }

    }
}
