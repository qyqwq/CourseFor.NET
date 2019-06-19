using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ClassDesign
{
    public partial class Admin : Form
    {
        Login login = null;
        string get_id = null;
        private MySqlConnection conn = null;//连接
        private MySqlCommand cmd = null;//提交指令
        private String constr = "Server=localhost; Database=book_manage; UID=root; ";
        private MySqlDataAdapter apt = null;
        private DataTable dataTable = null;
        private BindingSource bindingSource = null;
        public Admin(Login x)
        {
            login = x;
            InitializeComponent();
        }
        private void Admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            MySqlCommandBuilder builder = new MySqlCommandBuilder(apt);
            apt.Update(dataTable);
            login.Show();
        }
        private void Admin_Load(object sender, EventArgs e)
        {
            conn = new MySqlConnection(constr);
            conn.Open();
            apt = new MySqlDataAdapter();
            dataTable = new DataTable();
            string sql = "SELECT * FROM book";
            cmd = new MySqlCommand(sql, conn);
            apt.SelectCommand = cmd;
            dataTable.Clear();
            apt.Fill(dataTable);

            DataTable sel = InitializeData(dataTable);

            bindingSource = new BindingSource();
            bindingSource.DataSource = sel;
            dataGridView1.DataSource = bindingSource;
            bindingNavigator1.BindingSource = bindingSource;
            conn.Close();
        }
        private string get_time_str(string stampStr)
        {
            long.TryParse(stampStr, out long stamp);
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
            DateTime dt = startTime.AddSeconds(stamp);
            return dt.ToString("yyyy/MM/dd");
        }
        private string find_name(string id)
        {

            if (id == "ok")
            {
                return "未被借走";
            }
            MySqlConnection conn_name = new MySqlConnection(constr);
            conn_name.Open();
            string sql_name = "SELECT user_name FROM usr WHERE user_id = '" + id + "'";
            MySqlCommand cmd_name = new MySqlCommand(sql_name, conn_name);
            MySqlDataReader reader_name = cmd_name.ExecuteReader();
            reader_name.Read();
            string name = reader_name[0].ToString();
            reader_name.Close();
            conn_name.Close();
            return name;
        }
        private DataTable InitializeData(DataTable dataTable)
        {
            DataTable sel = new DataTable();
            sel.Columns.Add("book_id", typeof(string));
            sel.Columns.Add("book_name", typeof(string));
            sel.Columns.Add("book_author", typeof(string));
            sel.Columns.Add("book_status", typeof(string));
            sel.Columns.Add("lend_time", typeof(string));
            sel.Columns.Add("back_time", typeof(string));
            DataRow[] dataRow = dataTable.Select();
            foreach (DataRow x in dataRow)
            {
                if(x[3].ToString() == "ok")
                {
                    sel.Rows.Add(x[0], x[1], x[2], find_name(x[3].ToString()), "-", "-");
                }
                else
                {
                    sel.Rows.Add(x[0], x[1], x[2], find_name(x[3].ToString()), get_time_str(x[4].ToString()), get_time_str(x[5].ToString()));
                }
                
            }
            return sel;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            string sql = "book_id LIKE '%"+text+"%' OR book_name LIKE '%"+text+"%' OR book_author LIKE '%"+text+"%' OR book_status LIKE'%"+text+"%'";
            DataTable sel = new DataTable();
            sel = dataTable.Copy();
            sel.Clear();
            DataRow[] dataRow = dataTable.Select(sql);
            foreach(DataRow x in dataRow)
            {
                sel.Rows.Add(x.ItemArray);
            }
            sel = InitializeData(sel);
            bindingSource.DataSource = sel;
        }
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            if(text=="")
                bindingSource.DataSource = dataTable;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认删除吗？", "删除前确认", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                dataTable.Rows[bindingNavigator1.BindingSource.Position].Delete();
            }
        }

        private void BindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            GetID getID = new GetID();
            DialogResult result = getID.ShowDialog();
            if (result == DialogResult.OK)
            {
                get_id = getID.text;
                getID.Close();
                if (get_id == "")
                {
                    MessageBox.Show("输入为空!", "错误");
                    return;
                }
                bindingSource.DataSource = dataTable;
                string sql = "book_id = '" + get_id + "'";
                DataRow[] dataRow = dataTable.Select(sql);
                if (dataRow.Length != 0)
                {
                    MessageBox.Show("出版号重复！", "错误");
                    return;
                }
                dataTable.Rows.Add(get_id, "", "", "ok",0,0);
            }
        }
    }
}
