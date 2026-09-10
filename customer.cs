using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace jewelary_shop
{
    public partial class customer : Form
    {
        
        public customer()
        {
            InitializeComponent();
            displaycustomer();
        }
   readonly SqlConnection con = new SqlConnection(connectionString: @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\jewelary shop\jewelary shop.mdf;Integrated Security=True;Connect Timeout=30");
        private void displaycustomer()
        {
            try
            {
                con.Open();
                String query = "select *from [customer]";
                SqlDataAdapter adapter = new SqlDataAdapter(query,con);
                SqlCommandBuilder scb = new SqlCommandBuilder(adapter);
                var ds = new DataSet();
                adapter.Fill(ds);
                customerdgv.DataSource = ds.Tables[0]; // customerdgv is panel name in customerpage
                con.Close();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void Label9_Click(object sender, EventArgs e)
        {

        }

        private void Btn_cross_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Btn_reset2_Click(object sender, EventArgs e)
        {
            txt_cid.Text = " ";
            txt_cname.Text = " ";
            txt_cphone.Text = " ";
        }

        private void Btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                if(txt_cid.Text==" "|| txt_cname.Text==" "||txt_cphone.Text==" ")
                {
                    MessageBox.Show("information missing");
                }
                else
                {
                    con.Open();
                    string query = "insert into [customer] values('"+txt_cid.Text+"','"+txt_cname.Text+"','"+txt_cphone.Text+"')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("record insert successfully");
                    displaycustomer();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void Btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_cid.Text == " " || txt_cname.Text == " " || txt_cphone.Text == " ")
                {
                    MessageBox.Show("information missing");
                }
                else
                {
                    con.Open();
                    string query = "update [customer] set cusname=@cn,cusphone=@cp where cusid=@cid";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue(@"cid", txt_cid.Text);
                    cmd.Parameters.AddWithValue(@"cn", txt_cname.Text);
                    cmd.Parameters.AddWithValue(@"cp", txt_cphone.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("record update successfully");
                    displaycustomer();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void Btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_cid.Text == " ")
                {
                    MessageBox.Show("information missing");
                }
                else
                {
                    con.Open();
                    string query = "delete from [customer] where cusid=@cid ";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue(@"cid", txt_cid.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("record deleted successfully");
                    displaycustomer();


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void Customer_Load(object sender, EventArgs e)
        {
            customerdgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            customerdgv.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            customerdgv.RowsDefaultCellStyle.BackColor = Color.Black;
            customerdgv.BackgroundColor = Color.Black;
            customerdgv.ForeColor = Color.White;
            customerdgv.RowsDefaultCellStyle.SelectionForeColor = Color.Black;
            customerdgv.RowsDefaultCellStyle.SelectionBackColor = Color.Yellow;
            customerdgv.RowsDefaultCellStyle.BackColor = Color.Black;


        }

        private void Customerdgv_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                txt_cid.Text = customerdgv.SelectedRows[0].Cells[0].Value.ToString();
                txt_cname.Text = customerdgv.SelectedRows[0].Cells[1].Value.ToString();
                txt_cphone.Text = customerdgv.SelectedRows[0].Cells[2].Value.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void Lbl_product_Click(object sender, EventArgs e)
        {
            product obj = new product();
            obj.Show();
            this.Hide();
        }

        private void Lbl_bill_Click(object sender, EventArgs e)
        {
            bill obj = new bill();
            obj.Show();
            this.Hide();
        }

        private void Lbl_logout_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void Lbl_customer_Click(object sender, EventArgs e)
        {
            customer obj = new customer();
            obj.Show();
            this.Hide();
        }

        private void Customerdgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    }

