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
        private String constr = "Server=localhost; Database=book_manage; UID=root; PWD=3.141592654;";
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
            bindingSource = new BindingSource();
            bindingSource.DataSource = dataTable;
            dataGridView1.DataSource = bindingSource;
            bindingNavigator1.BindingSource = bindingSource;
            conn.Close();
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
                dataTable.Rows.Add(get_id, "", "", "ok");
            }
        }
    }
}
