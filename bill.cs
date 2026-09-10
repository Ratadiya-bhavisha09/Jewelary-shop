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
    public partial class bill : Form
    {
        public bill()
        {
            InitializeComponent();
            displaybill();
            getcusid();
            displayproduct();

        }
        readonly SqlConnection con = new SqlConnection(connectionString: @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\jewelary shop\jewelary shop.mdf;Integrated Security=True;Connect Timeout=30");
        private void displaybill()
        {
            try
            {
                con.Open();
                String query = "select *from [bill]";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                SqlCommandBuilder scb = new SqlCommandBuilder(adapter);
                var ds = new DataSet();
                adapter.Fill(ds);
                billdgv.DataSource = ds.Tables[0]; // customerdgv is panel name in customerpage
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
        private void getcusid()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select cusid from customer",con);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("cusid", typeof(int));
                dt.Load(rdr);
                cmb_cid.ValueMember = ("cusid");
                cmb_cid.DataSource = dt;
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
        String cname;
        private void displaycusname()
        {
            try
            {
                con.Open();
                String ss="select * from customer where cusid=@cusid";
                SqlCommand cmd = new SqlCommand(ss, con);
                int csid = Convert.ToInt32(cmb_cid.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@cusid", csid);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach(DataRow dr in dt.Rows)
                {
                    cname = dr["cusname"].ToString();
                    txt_cname.Text = cname;
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

        private void Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Btn_reset2_Click(object sender, EventArgs e)
        {
            txt_billid.Text = " ";
            cmb_pname.Text = "";
            cmb_cid.Text = "";
            txt_cname.Text = "";
            txt_pqty.Text = "";
            txt_uprice.Text = "";
            txt_totprice.Text = "";
        }

        private void Bill_Load(object sender, EventArgs e)
        {
            displaycusname();

            productdgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            productdgv.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            productdgv.RowsDefaultCellStyle.BackColor = Color.Black;
            productdgv.BackgroundColor = Color.Black;
            productdgv.ForeColor = Color.White;
            productdgv.RowsDefaultCellStyle.SelectionForeColor = Color.Black;
            productdgv.RowsDefaultCellStyle.SelectionBackColor = Color.Yellow;
            productdgv.RowsDefaultCellStyle.BackColor = Color.Black;

            billdgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
           billdgv.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            billdgv.RowsDefaultCellStyle.BackColor = Color.Black;
            billdgv.BackgroundColor = Color.Black;
            billdgv.ForeColor = Color.White;
            billdgv.RowsDefaultCellStyle.SelectionForeColor = Color.Black;
            billdgv.RowsDefaultCellStyle.SelectionBackColor = Color.Yellow;
            billdgv.RowsDefaultCellStyle.BackColor = Color.Black;


        }
        int stock;
        int flag = 0;
        private void Productdgv_DoubleClick_1(object sender, EventArgs e)
        {
            try
            {
                cmb_pname.Text = productdgv.SelectedRows[0].Cells[1].Value.ToString();
                txt_uprice.Text = productdgv.SelectedRows[0].Cells[4].Value.ToString();
                stock = Convert.ToInt32(productdgv.SelectedRows[0].Cells[3].Value);
                flag = 1;

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


        int sum;
        private void Btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                if(txt_billid.Text==" "||txt_pqty.Text=="")
                {
                    MessageBox.Show("missing information");
                }
                /*else if(Convert.ToInt32(txt_pqty.Text)>stock)
                {
                    MessageBox.Show("not enough stock avaliable");

                }*/
                else
                {
                    con.Open();
                    int total = Convert.ToInt32(txt_pqty.Text) * Convert.ToInt32(txt_uprice.Text);
                    sum = sum + total;
                    txt_totprice.Text = sum.ToString();
                    String query = "insert into [bill] (billid,product,cusid,cusname,qty,unitprice,total)values(@billid,@product,@cusid,@cusname,@qty,@unitprice,@total)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@billid", Convert.ToInt32(txt_billid.Text));
                    cmd.Parameters.AddWithValue("@product",cmb_pname.Text);
                    cmd.Parameters.AddWithValue("@cusid",cmb_cid.SelectedValue);
                    cmd.Parameters.AddWithValue("@cusname",txt_cname.Text);
                    cmd.Parameters.AddWithValue("@qty",Convert.ToInt32(txt_pqty.Text));
                    cmd.Parameters.AddWithValue("@unitprice", Convert.ToInt32(txt_uprice.Text));
                    cmd.Parameters.AddWithValue("@total", Convert.ToInt32(txt_totprice.Text));

                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Recorde inserted Successfully");
                    displaybill();
                    updateproduct();
                    int qty;
                    if(!int.TryParse(txt_pqty.Text,out qty))
                    {
                        MessageBox.Show("qty  show");
                        return;
                    }
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
        void updateproduct()
        {
            try
            {
                String id = productdgv.SelectedRows[0].Cells[0].Value.ToString();
                int qty1 = stock - Convert.ToInt32(txt_pqty.Text);
                if(qty1<0)
                {
                    MessageBox.Show("operation failed");
                }
                else
                {
                    con.Open();
                    String query = "update product set qty='" + qty1 + "'where proid=" + id + ";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
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
       
        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

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

        private void Btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_billid.Text == " ")
                {
                    MessageBox.Show("information missing");
                }
                else
                {
                    con.Open();
                    string query = "delete from [bill] where billid=@billid ";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue(@"billid", txt_billid.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("record deleted successfully");
                    displaybill();


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
                if (txt_billid.Text == " " || cmb_pname.Text == "" || txt_pqty.Text == "" ||cmb_cid.Text==""||txt_uprice.Text=="")
                {
                    MessageBox.Show("missing information");
                }
                else
                {
                    
                    con.Open();
                    int total = Convert.ToInt32(txt_pqty.Text) * Convert.ToInt32(txt_uprice.Text);
                    sum = sum + total;
                    txt_totprice.Text = sum.ToString();

                    string query = "update [bill] set product=@product,cusid=@cusid,cusname=@cusname,qty=@qty, unitprice=@unitprice, total=@total where billid=@billid";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@billid", Convert.ToInt32(txt_billid.Text));
                    cmd.Parameters.AddWithValue("@product", cmb_pname.Text);
                    cmd.Parameters.AddWithValue("@cusid", cmb_cid.SelectedValue);
                    cmd.Parameters.AddWithValue("@cusname", txt_cname.Text);
                    cmd.Parameters.AddWithValue("@qty", Convert.ToInt32(txt_pqty.Text));
                    cmd.Parameters.AddWithValue("@unitprice", Convert.ToInt32(txt_uprice.Text));
                    cmd.Parameters.AddWithValue("@total", Convert.ToInt32(txt_totprice.Text));
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("record update successfully");
                    displaybill();

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

        private void Billdgv_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                txt_billid.Text = billdgv.SelectedRows[0].Cells[0].Value.ToString();
                cmb_pname.Text =billdgv.SelectedRows[0].Cells[1].Value.ToString();
                cmb_pname.Text =billdgv.SelectedRows[0].Cells[2].Value.ToString();
                txt_cname.Text= billdgv.SelectedRows[0].Cells[3].Value.ToString();
                txt_pqty.Text = billdgv.SelectedRows[0].Cells[4].Value.ToString();
                txt_uprice.Text= billdgv.SelectedRows[0].Cells[5].Value.ToString();
                txt_totprice.Text= billdgv.SelectedRows[0].Cells[6].Value.ToString();

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

        private void Cmb_cid_SelectionChangeCommitted(object sender, EventArgs e)
        {
            displaycusname();
        }

        private void Txt_totprice_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
