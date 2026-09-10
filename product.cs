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
    public partial class product : Form
    {
        public product()
        {
            InitializeComponent();
            displayproduct();
        }
        readonly SqlConnection con = new SqlConnection(connectionString: @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\jewelary shop\jewelary shop.mdf;Integrated Security=True;Connect Timeout=30");
        private void displayproduct()
        {
            try
            {
                con.Open();
                String query = "select *from [product]";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                SqlCommandBuilder scb = new SqlCommandBuilder(adapter);
                var ds = new DataSet();
                adapter.Fill(ds);
                productdgv.DataSource = ds.Tables[0]; // customerdgv is panel name in customerpage
                con.Close();

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

        private void Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Btn_reset2_Click(object sender, EventArgs e)
        { 
            txt_pid.Text = " ";
            cmb_pname.Text = " ";
            cmb_pcategory.Text = " ";
            txt_pqty.Text = " ";
            txt_unitprice.Text = " ";
        }

        private void Product_Load(object sender, EventArgs e)
        {
            
            productdgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            productdgv.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            productdgv.RowsDefaultCellStyle.BackColor = Color.Black;
            productdgv.BackgroundColor = Color.Black;
            productdgv.ForeColor = Color.White;
            productdgv.RowsDefaultCellStyle.SelectionForeColor = Color.Black;
            productdgv.RowsDefaultCellStyle.SelectionBackColor = Color.Yellow;
            productdgv.RowsDefaultCellStyle.BackColor = Color.Black;

        }

        private void Btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_pid.Text == " " || cmb_pname.Text == " " || cmb_pcategory.Text == " " || txt_pqty.Text==" "|| txt_unitprice.Text==" ")
                {
                    MessageBox.Show("information missing");
                }
                else
                {
                    con.Open();
                    string query = "insert into [product] values(" + txt_pid.Text + ",'" + cmb_pname.Text + "','" + cmb_pcategory.Text + "','" + txt_pqty.Text +"','"+ txt_unitprice.Text+"')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("record insert successfully");
                    displayproduct();
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

        private void Productdgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_pid.Text == " " || cmb_pname.Text == " " || cmb_pcategory.Text == " " || txt_pqty.Text == " " || txt_unitprice.Text == " ")
                {
                    MessageBox.Show("information missing");
                }
                else
                {
                    con.Open();
                    string query = "update [product] set proname=@proname, procategory=@procategory, qty=@qty, unitprice=@unitprice where   proid=@proid";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue(@"proid", txt_pid.Text);
                    cmd.Parameters.AddWithValue(@"proname", cmb_pname.Text);
                    cmd.Parameters.AddWithValue(@"procategory", cmb_pcategory.Text);
                    cmd.Parameters.AddWithValue(@"qty", txt_pqty.Text);
                    cmd.Parameters.AddWithValue(@"unitprice", txt_unitprice.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("record update successfully");
                    displayproduct();

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
                if (txt_pid.Text == " ")
                {
                    MessageBox.Show("information missing");
                }
                else
                {
                    con.Open();
                    string query = "delete from [product] where proid=@proid ";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue(@"proid", txt_pid.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("record deleted successfully");
                    displayproduct();


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

        private void Productdgv_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                txt_pid.Text = productdgv.SelectedRows[0].Cells[0].Value.ToString();
                cmb_pname.Text = productdgv.SelectedRows[0].Cells[1].Value.ToString();
                cmb_pcategory.Text = productdgv.SelectedRows[0].Cells[2].Value.ToString();
                txt_pqty.Text = productdgv.SelectedRows[0].Cells[3].Value.ToString();
                txt_unitprice.Text = productdgv.SelectedRows[0].Cells[4].Value.ToString();

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

        private void Lbl_customer_Click(object sender, EventArgs e)
        {
            customer obj = new customer();
            obj.Show();
            this.Hide();
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
    }
    }

