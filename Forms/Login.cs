using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace jewelary_shop
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            txt_pwd.UseSystemPasswordChar = true;
        }

        private void Btn_cross_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }

        private void Btn_reset_Click(object sender, EventArgs e)
        {
            txt_uname.Text = " ";
            txt_pwd.Text = " ";
        }

        private void Btn_login_Click(object sender, EventArgs e)
        {
            try
            {
                if(txt_uname.Text==" " && txt_pwd.Text==" ")
                {
                    MessageBox.Show("Missing username or password");
                } 
                else if(txt_uname.Text == "admin" && txt_pwd.Text == "password")
                {
                    MessageBox.Show("Login sucessfull");
                    customer obj = new customer();
                    obj.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("please enter correct username or password");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                txt_pwd.UseSystemPasswordChar = false;
            }
            else
            {
                txt_pwd.UseSystemPasswordChar = true;//passsword checkbox click hoi dekhade
            }
        }
    }
}
