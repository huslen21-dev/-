using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace SK23D003_Chinhuslen
{
    public partial class Form1 : Form
    {
        int IDD;
        int flag = 0;
        public Form1()
        {
            InitializeComponent();
            this.Width = 685;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connect = MyConnection.getConnection())
                {
                    //string Database = "server=L307-14;database=library_registration;";
                    //Database = Database + "Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                    //SqlConnection connect = new SqlConnection(Database);
                    connect.Open();

                    MessageBox.Show("Chadlaa!!");

                    connect.Close();


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connect = MyConnection.getConnection()) { 
               /* string Database = "server=L307-14;database=library_registration;";
                Database = Database + "Integrated Security=True;Encrypt=True;TrustServerCertificate=true";
                SqlConnection connect = new SqlConnection(Database);*/
                connect.Open();

                string sql = "select * from books";
                SqlCommand Table = new SqlCommand(sql);
                Table.Connection = connect;

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(Table);

                DataTable Record = new DataTable();
                sqlDataAdapter.Fill(Record);

                dataGridView1.DataSource = Record;


                DataSet ds = new DataSet();
                sqlDataAdapter.Fill(ds, "books");

                listBox1.DataSource = ds.Tables["books"];
                listBox1.DisplayMember = "title";
                listBox1.ValueMember = "book_id";

                DataGridViewCheckBoxColumn dgvCmb = new DataGridViewCheckBoxColumn();

                dgvCmb.ValueType = typeof(bool);
                dgvCmb.Name = "Chk";

                dgvCmb.FalseValue = "False";
                dgvCmb.TrueValue = "True";
                dgvCmb.HeaderText = "Сонгох";
                dataGridView1.Columns.Add(dgvCmb);
            
                connect.Close();}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var connect = MyConnection.getConnection()) { 
             /*   string Database = "server=L307-14;database=library_registration;";
            Database = Database + "Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
            SqlConnection connect = new SqlConnection(Database);*/
            connect.Open();

            string sql = "select * from books";
            SqlCommand Table = new SqlCommand(sql);
            Table.Connection = connect;


            SqlDataReader rdr = Table.ExecuteReader();
            DataTable Dtble = new DataTable();

            Dtble.Columns.Add("id");
            Dtble.Columns.Add("garchig");
            Dtble.Columns.Add("zohiogch");
            Dtble.Columns.Add("Hevlegdsen on");
            Dtble.Columns.Add("gg");
            Dtble.Columns.Add("yy");

            while (rdr.Read())
            {
                DataRow row = Dtble.NewRow();
                row["id"] = rdr["book_id"];
                row["garchig"] = rdr["title"];
                row["zohiogch"] = rdr["author"];
                row["Hevlegdsen on"] = rdr["published_year"];
                row["gg"] = rdr["genre"];
                row["yy"] = rdr["book_page"];
                Dtble.Rows.Add(row);
            }

            dataGridView1.DataSource = Dtble;


            connect.Close();
        }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            flag = 1;
            this.Width = 990;

        }
        private void button7_Click(object sender, EventArgs e)
        {
            flag = 2;
            this.Width = 990;
            dataGridView1_SelectionChanged_1(sender, e);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            this.Width = 685;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            button2_Click(sender, e);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            string SQL;
            using (var connect = MyConnection.getConnection())
            {
                /*string Database = "server=L307-14;database=library_registration;";
            Database = Database + "Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
            SqlConnection connect = new SqlConnection(Database);*/
                connect.Open();
                { //INSERT   
                    if (flag == 1)
                    {
                        SQL = "insert into books (title, author, published_year, genre, book_page)";
                        SQL = SQL + " values('" + textBox1.Text + "' , '" + textBox2.Text + "' , '" + maskedTextBox1.Text + "' , '"
                                                + textBox3.Text + "' , '" + textBox4.Text + "' )";


                        SqlCommand table = new SqlCommand(SQL, connect);

                        table.ExecuteNonQuery();
                    }
                    else
                    //UPDATE
                    if (flag == 2)
                    {
                        SQL = "update books set title=' " + textBox1.Text;
                        SQL = SQL + "', author=' " + textBox2.Text
                                      + "' , published_year=' " + maskedTextBox1.Text
                                                + "' ,genre=' " + textBox3.Text
                                            + "' ,book_page=' " + textBox4.Text + "' where book_id=" + IDD;

                        SqlCommand table = new SqlCommand(SQL, connect);

                        table.ExecuteNonQuery();
                    }
                    MessageBox.Show("Amjilttai");
                }

                /*//PROCEDURE
                            SqlCommand cmd = new SqlCommand("set_books", connect);
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (IDD == 0) 
                                cmd.Parameters.Add(new SqlParameter("@id", ""));

                            else  
                                cmd.Parameters.Add(new SqlParameter("@id", IDD));
                                cmd.Parameters.Add(new SqlParameter("@bookN",textBox1.Text.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@authorN", textBox2.Text.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@Published_year", maskedTextBox1.Text.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@genre",textBox3.Text.ToString()));
                            SqlDataReader rdr = cmd.ExecuteReader(); 

                            DataTable Dtble = new DataTable();


                            connect.Close();

                            this.Width = 685;
                            textBox1.Text = "";
                            textBox2.Text = "";
                            maskedTextBox1.Text = "";
                            textBox3.Text = "";
                            textBox4.Text = "";
                            this.Form1_Load(sender, e);
                            button2_Click(sender, e);*/
            }
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                IDD = Convert.ToInt16(row.Cells[0].Value);
                textBox1.Text = row.Cells[1].Value?.ToString();
                textBox2.Text = row.Cells[2].Value?.ToString();
                maskedTextBox1.Text = row.Cells[3].Value?.ToString();
                textBox3.Text = row.Cells[4].Value?.ToString();
                textBox4.Text = row.Cells[5].Value?.ToString();
                grid();
            }
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var gv = sender as DataGridView;
            var column = gv.Columns[e.ColumnIndex];
            gv[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Yellow;
            gv.InvalidateCell(gv.Rows[e.RowIndex].Cells[e.ColumnIndex]);
        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var gv = sender as DataGridView;
            var column = gv.Columns[e.ColumnIndex];
            gv[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.White;
            gv.InvalidateCell(gv.Rows[e.RowIndex].Cells[e.ColumnIndex]);
        }

        private void grid()
        {
            using (var connect = MyConnection.getConnection())
            {
                //string Database = "server=L307-14;database=library_registration;";
                //Database = Database + "Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                //SqlConnection connect = new SqlConnection(Database);
                connect.Open();

                string sql = "select genre.genre from books inner " +
                    "join genre on books.genre=genre.genre_id where book_id=" + IDD;
                SqlCommand cmd = new SqlCommand(sql, connect);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataTable Record = new DataTable();

                sqlDataAdapter.Fill(Record);

                dataGridView2.DataSource = Record;
                dataGridView2.Refresh();
                connect.Close();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                const string message = "Бичлэг устгахдаа итгэлтэй байна уу?";
                const string caption = "УСТГАХ ҮЙЛДЭЛ";
                var result = MessageBox.Show(message, caption,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    using (var connect = MyConnection.getConnection())
                    {
                        //string Database = "server=L307-14;database=library_registration;";
                        //Database = Database + "Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                        //SqlConnection connect = new SqlConnection(Database);
                        connect.Open();

                        string sql = "delete from books where book_id='" + IDD + "'";
                        SqlCommand command = new SqlCommand(sql, connect);

                        command.ExecuteReader();
                        MessageBox.Show("Амжилттай устгалаа");
                        connect.Close();

                        button3_Click(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            {
                DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                ch1 =
            (DataGridViewCheckBoxCell)dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[6];
                if (ch1.Value == null)
                    ch1.Value = false;
                switch (ch1.Value.ToString())
                {
                    case "True":
                        ch1.Value = false;

                        break;
                    case "False":
                        ch1.Value = true;

                        break;

                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int i = 0; //тоолуур ажиллаж эхлэнэ
            foreach (DataGridViewRow row in dataGridView1.Rows) //жагсаалтын мөр хооронд гүйх
{
                bool isSelected = Convert.ToBoolean(row.Cells["Chk"].Value);//checkbox check хийгдсэн эсэхийг шалгана.
                if (isSelected) //хэрвээ check хийгдсэн бол устгах үйлдэл хийглэнэ
{
                    using (var connect = MyConnection.getConnection())
                    {
                        //string Database = "server=L307-14;database=library_registration;";
                        //Database = Database + "Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                        //SqlConnection connect = new SqlConnection(Database);
                        connect.Open();
                        string sql = "delete from borrowed_books where book_id=" + row.Cells["book_id"].Value;
                        sql = sql + "; delete from books where book_id=" + row.Cells["book_id"].Value;

                        SqlCommand cmd = new SqlCommand(sql, connect);
                        cmd.ExecuteReader();
                        connect.Close();
                        i++;
                    }
                }
            }
            if (i > 0)
            {
                dataGridView1.Columns.RemoveAt(6);
                Form1_Load(sender, e);
                MessageBox.Show(i + " мөр бичлэг устгалаа.");
            }
        }
    }
}
