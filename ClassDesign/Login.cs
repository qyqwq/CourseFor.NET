using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ClassDesign
{
    public partial class Login : Form
    {
        private MySqlConnection conn = null;//连接
        private MySqlCommand cmd = null;//提交指令
        private MySqlDataReader reader = null;//结果集
        private String constr = "Server=localhost; Database=book_manage; UID=root;";
        public Login()
        {
            InitializeComponent();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            textBox1.Select();
            string ID = textBox1.Text;
            string PWD = textBox2.Text;
            textBox1.Clear();
            textBox2.Clear();
            string sql = "SELECT user_id,user_pwd,user_type FROM usr WHERE user_id = '" + ID + "'";
            
            cmd = new MySqlCommand(sql, conn);
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                string id = reader[0].ToString();
                string pwd = reader[1].ToString();
                string type = reader[2].ToString();
                if(PWD==pwd)
                {
                    if (type == "rot")
                    {
                        Admin x = new Admin(this);
                        x.Show();
                    }
                    else
                    {
                        Lend x = new Lend(this,id);
                        x.Show();
                    }
                    this.Hide();
                }
                else
                    MessageBox.Show("密码错误，请确认后再次输入","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("没有该用户，请确认后再次输入", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            reader.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Register x = new Register(this);
            x.Show();
            this.Hide();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            conn = new MySqlConnection(constr);
            conn.Open();
        }
    }
}
