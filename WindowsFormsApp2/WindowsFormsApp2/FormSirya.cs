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
    public partial class FormSirya : Form
    {
        interface Programm
        {
            void reload_list(DataGridView dt);
            void GetListUsers(string selectCommand);
            void delegatt(DataGridView dt);
            void delegatt2(string selectCommand);

        }

        class Sirya : Programm
        {
            public static string connStr = "server = chuc.caseum.ru; user = st_2_18_2; database = is_2_18_st2_VKR; password = 58791144 ; port = 33333";
            public static MySqlConnection conn = new MySqlConnection(connStr);
            public static DataTable dt_list_users = new DataTable();
            public static BindingSource bs_list_users = new BindingSource();
            public static MySqlDataAdapter list_users_adapter = new MySqlDataAdapter();
            public static string id_selected_rows;
            public static string commandString;
            public static string index_selected_rows;
            public static string sql;
            public void reload_list(DataGridView dt)
            {
                dt_list_users.Clear();
                delegatt2(commandString);
                list_users_adapter.Update((DataTable)bs_list_users.DataSource);
                dt.Update();


            }
            public void GetListUsers(string selectCommand)
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


            delegate void Mess(DataGridView dt);
            delegate void Mess2(string selectCommand);
           

            public void delegatt(DataGridView dt)
            {
                Mess mes = reload_list;
                mes(dt);
            }

            public void delegatt2(string selectCommand)
            {
                Mess2 mes2 = GetListUsers;
                mes2(selectCommand);
            }


        }


        Sirya sr = new Sirya();
        public FormSirya()
        {
            InitializeComponent();
        }
      

        private void FormSirya_Load(object sender, EventArgs e)
        {
            string connStr = "server = chuc.caseum.ru; user = st_2_18_2; database = is_2_18_st2_VKR; password = 58791144 ; port = 33333";
            MySqlConnection conn = new MySqlConnection(connStr);
            Sirya.sql = "SELECT ID_Sirya, Naimenovanie_Sirya, Stoimost FROM Sirye ";
            conn.Open();
            dataGridView1.DataSource = Sirya.bs_list_users;


            sr.delegatt2(Sirya.sql);


            dataGridView1.Columns[0].HeaderText = "ID Cырья";
            dataGridView1.Columns[1].HeaderText = "Наименование";
            dataGridView1.Columns[2].HeaderText = "Стоимость";



            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns[2].Visible = true;







            ////Режим для полей "Только для чтения"

            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;




            //Растягивание полей грида
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
  







            //Убираем заголовки строк
            dataGridView1.RowHeadersVisible = false;

            conn.Close();

            string connStr2 = "server = chuc.caseum.ru; user = st_2_18_2; database = is_2_18_st2_VKR; password = 58791144 ; port = 33333";
            MySqlConnection conn2 = new MySqlConnection(connStr2);
            conn2.Open();
            string sql2 = "SELECT Foto FROM Sirye WHERE ID_Sirya = " + edit_test.test_id;
            MySqlCommand comm2 = new MySqlCommand(sql2, conn2);
            MySqlDataReader reader2 = comm2.ExecuteReader();
            while (reader2.Read())
            {
                pictureBox1.ImageLocation = (reader2[0].ToString());


            }
            conn2.Close();

          


        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            string connStr = "server = chuc.caseum.ru; user = st_2_18_2; database = is_2_18_st2_VKR; password = 58791144 ; port = 33333";
            MySqlConnection conn = new MySqlConnection(connStr);
            string sql = "DELETE FROM Sirye WHERE ID_Sirya = " + edit_test.test_id;
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
            sr.delegatt(dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connStr = "server = chuc.caseum.ru; user = st_2_18_2; database = is_2_18_st2_VKR; password = 58791144 ; port = 33333";
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "UPDATE Sirye SET  Naimenovanie_Sirya= '" + textBox2.Text + "',  Opisanie= '" + textBox1.Text + "', Stoimost = " + textBox7.Text + ",  Foto = '" + textBox4.Text +  " WHERE ID_Sirya= " + edit_test.test_id;
            MySqlCommand comm = new MySqlCommand(sql, conn);
            comm.ExecuteNonQuery();
            conn.Close();
            sr.delegatt(dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string connStr = "server = chuc.caseum.ru; user = st_2_18_2; database = is_2_18_st2_VKR; password = 58791144 ; port = 33333";
                MySqlConnection conn = new MySqlConnection(connStr);
                conn.Open();
                string sql = "INSERT INTO Sirye (Naimenovanie_Sirya, Opisanie, Stoimost,  Foto ) VALUE ('" + textBox5.Text + "','" + textBox6.Text + "'," + textBox8.Text + ",'" + textBox3.Text + "')";
                MySqlCommand comn = new MySqlCommand(sql, conn);
                MySqlDataReader reader = comn.ExecuteReader();


                while (reader.Read())
                {


                    textBox5.Text = (reader[0].ToString());
                    textBox6.Text = (reader[1].ToString());
                    textBox8.Text = (reader[2].ToString());
                    textBox3.Text = (reader[3].ToString());




                }

                conn.Close();
                sr.delegatt(dataGridView1);


            }
            catch
            {
                if (textBox5.Text == "" || textBox6.Text == "" || textBox8.Text == "" || textBox3.Text == "")
                {
                    MessageBox.Show("Заполните поля");
                }
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                Sirya.index_selected_rows = dataGridView1.SelectedCells[0].RowIndex.ToString();
                //ID 
                Sirya.id_selected_rows = dataGridView1.Rows[Convert.ToInt32(Sirya.index_selected_rows)].Cells[0].Value.ToString();

                edit_test.test_id = Convert.ToInt32(Sirya.id_selected_rows);
            }
            catch
            {
                MessageBox.Show("Выберете заполненные поля");

            }
            string sql;
            string connStr = "server = chuc.caseum.ru; user = st_2_18_2; database = is_2_18_st2_VKR; password = 58791144 ; port = 33333";
            MySqlConnection conn = new MySqlConnection(connStr);

            sql = "SELECT Naimenovanie_Sirya, Opisanie, Stoimost, Foto FROM Sirye WHERE ID_Sirya = " + edit_test.test_id;


            conn.Open();

            MySqlCommand comm = new MySqlCommand(sql, conn);
            MySqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {

                textBox2.Text = (reader[0].ToString());
                
                textBox4.Text = (reader[1].ToString());      
                textBox7.Text = (reader[2].ToString());
                textBox1.Text = (reader[3].ToString());




            }
            conn.Close();

           
        }
    }
    
}
