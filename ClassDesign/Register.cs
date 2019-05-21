using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
namespace ClassDesign
{
    public partial class Register : Form
    {
        private Login login = null;
        private bool[] flag=new bool[4];
        private MySqlConnection conn = null;//连接
        private MySqlCommand cmd = null;//提交指令
        private MySqlDataReader reader = null;//结果集
        private String constr = "Server=localhost; Database=book_manage; UID=root; PWD=3.141592654;";
        public Register(Login x)
        {
            InitializeComponent();
            login = x;
        }
        private void Register_FormClosing(object sender, FormClosingEventArgs e)
        {
            login.Show();
            conn.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            foreach(bool x in flag)
            {
                if (!x)
                {
                    MessageBox.Show("信息输入有误，请按照提示操作", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            string ID = textBox1.Text;
            string NAME = textBox2.Text;
            string PWD1 = textBox3.Text;
            string sql = "INSERT INTO usr VALUES ('" + ID + "', '" + PWD1 + "', '" + NAME + "', 'usr')";
            cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            textBox1.Clear(); textBox2.Clear();
            textBox3.Clear(); textBox4.Clear();
            MessageBox.Show("注册成功！", "success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            string ID = textBox1.Text;
            if (ID == "")
            {
                label7.Text = "请输入账号";
                flag[0] = false;
            }
            else
            {
                label7.Text = "";
                flag[0] = true;
            }
            string sql = "SELECT user_id FROM usr WHERE user_id = '" + ID + "'";
            cmd = new MySqlCommand(sql, conn);
            reader = cmd.ExecuteReader();
            if(reader.HasRows)
            {
                label7.Text = "账号重复，请更换";
                flag[0] = false;
            }
            reader.Close();
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            string NAME = textBox2.Text;
            if (NAME == "")
            {
                label8.Text = "请输入姓名";
                flag[1] = false;
            }
            else
            {
                label8.Text = "";
                flag[1] = true;
            }
        }
        void check_pwd()
        {
            string PWD1 = textBox3.Text;
            string PWD2 = textBox4.Text;
            if (PWD1 == PWD2)
            {
                label6.Text = "";
                flag[3] = true;
            }
            else
            {
                label6.Text = "密码不一致";
                flag[3] = false;
            }
        }
        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            string p = "^[A-Za-z0-9]{6,24}$";
            string PWD1 = textBox3.Text;
            if (Regex.IsMatch(PWD1, p))
            {
                label5.Text = "";
                flag[2] = true;
            }
            else
            {
                label5.Text = "请输入大小写字母和数字\n组成的6~24位的密码";
                flag[2] = false;
            }
            check_pwd();
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            check_pwd();
        }

        private void Register_Load(object sender, EventArgs e)
        {
            
            conn = new MySqlConnection(constr);
            conn.Open();
            for (int i = 0; i < 4; i++)
                flag[i] = false;
        }
    }
}
