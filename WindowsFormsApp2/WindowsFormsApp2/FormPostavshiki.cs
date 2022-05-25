using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp2
{
    public partial class FormPostavshiki : Form
    {
        static string connStr = "server = chuc.caseum.ru; user = st_2_18_2; database = is_2_18_st2_VKR; password = 58791144 ; port = 33333";
        MySqlConnection conn = new MySqlConnection(connStr);
        DataTable dt_list_users = new DataTable();
        BindingSource bs_list_users = new BindingSource();
        MySqlDataAdapter list_users_adapter = new MySqlDataAdapter();
        string id_selected_rows;
        string commandString;
        string index_selected_rows;
        public string sql;
        public FormPostavshiki()
        {
            InitializeComponent();
        }

        private void GetListUsers(string selectCommand)
        {
            try
            {
                MySqlDataAdapter list_doct_adapter = new MySqlDataAdapter(sql, conn);
                bs_list_users.DataSource = dt_list_users;
                list_doct_adapter.Fill(dt_list_users);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неизвестная ошибка при вызове метода GetData! \n\n\n" + ex, "Ошибка");
            }
        }
        public void reload_list()
        {
            dt_list_users.Clear();
            GetListUsers(commandString);
            list_users_adapter.Update((DataTable)bs_list_users.DataSource);
            dataGridView1.Update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connStr = "server = chuc.caseum.ru; user = st_2_18_2; database = is_2_18_st2_VKR; password = 58791144 ; port = 33333";
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "UPDATE Postavshik SET Naimenovanie_Postavshika= '" + textBox2.Text + "', Adres= '" + textBox4.Text + "', Telefon= '" + textBox7.Text + "', Pochta= '" + textBox3.Text + "' WHERE ID_Postavshika= " + edit_test.test_id;
            MySqlCommand comm = new MySqlCommand(sql, conn);
            comm.ExecuteNonQuery();
            conn.Close();
            reload_list();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connStr = "server = chuc.caseum.ru; user = st_2_18_2; database = is_2_18_st2_VKR; password = 58791144 ; port = 33333";
            MySqlConnection conn = new MySqlConnection(connStr);
            string sql = "DELETE FROM Postavshik WHERE ID_Postavshika = " + edit_test.test_id;
            MySqlCommand comm = new MySqlCommand(sql, conn);
            try
            {
                conn.Open();
                comm.ExecuteNonQuery();
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка удаления пользователя \n\n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
            reload_list();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connStr = "server = chuc.caseum.ru; user = st_2_18_2; database = is_2_18_st2_VKR; password = 58791144 ; port = 33333";
            MySqlConnection conn = new MySqlConnection(connStr);
            string sql = "DELETE FROM Postavshik WHERE ID_Postavshika = " + edit_test.test_id;
            MySqlCommand comm = new MySqlCommand(sql, conn);
            try
            {
                conn.Open();
                comm.ExecuteNonQuery();
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка удаления пользователя \n\n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
            reload_list();
        }

        private void FormPostavshiki_Load(object sender, EventArgs e)
        {
            string connStr = "server = chuc.caseum.ru; user = st_2_18_2; database = is_2_18_st2_VKR; password = 58791144 ; port = 33333";
            MySqlConnection conn = new MySqlConnection(connStr);
            sql = "SELECT * FROM Postavshik ";
            conn.Open();
            dataGridView1.DataSource = bs_list_users;


            GetListUsers(sql);


            dataGridView1.Columns[0].HeaderText = "ID Поставщика";
            dataGridView1.Columns[1].HeaderText = "Название компании";
            dataGridView1.Columns[2].HeaderText = "Адрес";
            dataGridView1.Columns[3].HeaderText = "Телефон";
            dataGridView1.Columns[4].HeaderText = "Почта";
          



            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns[2].Visible = true;
            dataGridView1.Columns[3].Visible = true;
            dataGridView1.Columns[4].Visible = true;
         


            ////Режим для полей "Только для чтения"

            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[4].ReadOnly = true;
        



            //Растягивание полей грида
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      







            //Убираем заголовки строк
            dataGridView1.RowHeadersVisible = false;

            conn.Close();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            index_selected_rows = dataGridView1.SelectedCells[0].RowIndex.ToString();
            //ID 
            id_selected_rows = dataGridView1.Rows[Convert.ToInt32(index_selected_rows)].Cells[0].Value.ToString();

            edit_test.test_id = Convert.ToInt32(id_selected_rows);
            string sql;
            string connStr = "server = chuc.caseum.ru; user = st_2_18_2; database = is_2_18_st2_VKR; password = 58791144 ; port = 33333";
            MySqlConnection conn = new MySqlConnection(connStr);

            sql = "SELECT Naimenovanie_Kompanii, Adres, Telefon, Pochta  FROM Postavshik WHERE ID_Postavshika = " + edit_test.test_id;


            conn.Open();

            MySqlCommand comm = new MySqlCommand(sql, conn);
            MySqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                textBox2.Text = (reader[0].ToString());
                textBox4.Text = (reader[1].ToString());
                textBox7.Text = (reader[2].ToString());
                textBox3.Text = (reader[3].ToString());





            }
            conn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string connStr = "server = chuc.caseum.ru; user = st_2_18_2; database = is_2_18_st2_VKR; password = 58791144 ; port = 33333";
                MySqlConnection conn = new MySqlConnection(connStr);
                conn.Open();
                string sql = "INSERT INTO Postavshik (Naimenovanie_Postavshika, Adres, Telefon, Pochta) VALUE ('" + textBox8.Text + "','" + textBox1.Text + "'," + textBox6.Text + ",'" + textBox5.Text + "')";
                MySqlCommand comn = new MySqlCommand(sql, conn);
                MySqlDataReader reader = comn.ExecuteReader();


                while (reader.Read())
                {

                    textBox8.Text = (reader[0].ToString());
                    textBox1.Text = (reader[1].ToString());
                    textBox6.Text = (reader[2].ToString());
                    textBox5.Text = (reader[3].ToString());






                }


                conn.Close();
                reload_list();
            }
            catch
            {


                if (textBox8.Text == "" || textBox1.Text == "" || textBox6.Text == "" || textBox5.Text == "")
                {
                    MessageBox.Show("Заполните поля");
                }

            }
        }
    }
}
