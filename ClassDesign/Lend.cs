using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ClassDesign
{
    public partial class Lend : Form
    {
        private string real_id;
        Login login = null;
        private MySqlConnection conn = null;//连接
        private MySqlCommand cmd = null;//提交指令
        private MySqlDataReader reader = null;//结果集
        private String constr = "Server=localhost; Database=book_manage; UID=root;";
        private MySqlDataAdapter apt = null;
        private BindingSource source_ok = null;
        private BindingSource source_get = null;
        private DataTable tb_all = null;
        private DataTable tb_ok = null;
        private DataTable tb_get = null;
        public Lend(Login xx,string x)
        {
            InitializeComponent();
            real_id = x;
            login = xx;
        }
        private void update_ok()
        {
            tb_ok.Clear(); 
            string str_ok = "book_status='ok'";
            DataRow[] Row_ok = tb_all.Select(str_ok);
            foreach (DataRow x in Row_ok)
            {
                tb_ok.Rows.Add(x[0], x[1], x[2]);
            }
        }
        private void update_get()
        {
            tb_get.Clear();
            string str_get = "book_status='" + real_id + "'";
            DataRow[] Row_get = tb_all.Select(str_get);
            foreach (DataRow x in Row_get)
            {
                tb_get.Rows.Add(x[0], x[1], x[2]);
            }
        }
        private void Lend_Load(object sender, EventArgs e)
        {
            DateTime time = new DateTime();
            time = DateTime.Now;
            int hour = time.Hour;
            string[] strTime = new string[24];
            for (int i = 0; i < 6; i++)
                strTime[i] = "凌晨了，多注意身体\n";
            for (int i = 6; i < 9; i++)
                strTime[i] = "早上好！吃过早饭了吗\n";
            for (int i = 9; i < 12; i++)
                strTime[i] = "上午好，今天也要加油哦\n";
            for (int i = 12; i < 14; i++)
                strTime[i] = "中午好，可以睡个午觉呢\n";
            for (int i = 14; i < 18; i++)
                strTime[i] = "下午好，多喝热水身体好\n";
            for (int i = 18; i < 24; i++)
                strTime[i] = "晚上好，今天也是美好的一天呢\n";
            conn = new MySqlConnection(constr);
            conn.Open();
            string sql_name = "SELECT user_name FROM usr WHERE user_id = '"+real_id+"'";
            cmd = new MySqlCommand(sql_name, conn);
            reader = cmd.ExecuteReader();
            reader.Read();
            string name = reader[0].ToString();
            label1.Text = "欢迎使用本借书系统!\n" + strTime[hour] + name;
            reader.Close();
            apt = new MySqlDataAdapter();
            tb_all = new DataTable();tb_ok = new DataTable();tb_get = new DataTable();
            DataTable test = new DataTable();
            string sql = "SELECT * FROM book";
            cmd = new MySqlCommand(sql, conn);
            apt.SelectCommand = cmd;
            tb_all.Clear();
            apt.Fill(tb_all);

            source_ok = new BindingSource();source_get = new BindingSource();
            dataGridView1.DataSource = source_ok;dataGridView2.DataSource = source_get;
            conn.Close();

            
            tb_ok.Columns.Add("book_id", typeof(string));
            tb_ok.Columns.Add("book_name", typeof(string));
            tb_ok.Columns.Add("book_author", typeof(string));
            tb_get.Columns.Add("book_id", typeof(string));
            tb_get.Columns.Add("book_name", typeof(string));
            tb_get.Columns.Add("book_author", typeof(string));

            update_ok();
            update_get();
            source_ok.DataSource = tb_ok;
            source_get.DataSource = tb_get;
        }

        private void Lend_FormClosing(object sender, FormClosingEventArgs e)
        {
            MySqlCommandBuilder builder = new MySqlCommandBuilder(apt);
            apt.Update(tb_all);
            login.Show();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            int rows = dataGridView1.CurrentRow.Index;
            string id = dataGridView1.Rows[rows].Cells[0].Value.ToString();
            for(int i = 0; i < tb_all.Rows.Count; i++)
            {
                if(tb_all.Rows[i]["book_id"].ToString()==id)
                {
                    tb_all.Rows[i]["book_status"] = real_id;
                }
            }
            update_ok();
            update_get();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            int rows = dataGridView2.CurrentRow.Index;
            string id = dataGridView2.Rows[rows].Cells[0].Value.ToString();
            for (int i = 0; i < tb_all.Rows.Count; i++)
            {
                if (tb_all.Rows[i]["book_id"].ToString() == id)
                {
                    tb_all.Rows[i]["book_status"] = "ok";
                }
            }
            update_ok();
            update_get();
        }
    }
}
