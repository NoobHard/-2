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
    public partial class Spisok_Postavok : Form
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
        public Spisok_Postavok()
        {
            InitializeComponent();
            this.CenterToScreen();
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
            string sql = "DELETE FROM Postavki WHERE ID_Postavki = " + edit_test.test_id;
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

        private void Spisok_Postavok_Load(object sender, EventArgs e)
        {
            string connStr = "server = chuc.caseum.ru; user = st_2_18_2; database = is_2_18_st2_VKR; password = 58791144 ; port = 33333";
            MySqlConnection conn = new MySqlConnection(connStr);
            sql = "SELECT ID_Postavki, Postavshik, Naimenovanie_Sirya, Kolihestvo, Stoimost, Data_Postavki  FROM Postavki ";
            conn.Open();
            dataGridView1.DataSource = bs_list_users;


            GetListUsers(sql);


            dataGridView1.Columns[0].HeaderText = "ID Поставки";
            dataGridView1.Columns[1].HeaderText = "Поставщик";
            dataGridView1.Columns[2].HeaderText = "Название сырья";
            dataGridView1.Columns[3].HeaderText = "Количество";
            dataGridView1.Columns[4].HeaderText = "Стоимость";
            dataGridView1.Columns[5].HeaderText = "Дата поставки";



            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns[2].Visible = true;
            dataGridView1.Columns[3].Visible = true;
            dataGridView1.Columns[4].Visible = true;
            dataGridView1.Columns[5].Visible = true;



            ////Режим для полей "Только для чтения"

            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[4].ReadOnly = true;
            dataGridView1.Columns[5].ReadOnly = true;



            //Растягивание полей грида
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;







            //Убираем заголовки строк
            dataGridView1.RowHeadersVisible = false;

            conn.Close();


            // создаём объект для подключения к БД
            MySqlConnection conn_db;
            conn_db = new MySqlConnection(connStr);
            //Формирование списка статусов
            DataTable list_stud_table = new DataTable();
            MySqlCommand list_stud_command = new MySqlCommand();
            //Открываем соединение
            conn_db.Open();
            //Формируем столбцы для комбобокса списка ЦП
            list_stud_table.Columns.Add(new DataColumn("ID_Sirya", System.Type.GetType("System.Int32")));
            list_stud_table.Columns.Add(new DataColumn("Naimenovanie_Sirya", System.Type.GetType("System.String")));
            //Настройка видимости полей комбобокса
            comboBox2.DataSource = list_stud_table;
            comboBox2.DisplayMember = "Naimenovanie_Sirya";
            comboBox2.ValueMember = "ID_Sirya";
            //Формируем строку запроса на отображение списка статусов прав пользователя
            string sql_list_users = "SELECT ID_Sirya, Naimenovanie_Sirya FROM Sirye";
            list_stud_command.CommandText = sql_list_users;
            list_stud_command.Connection = conn_db;
            //Формирование списка ЦП для combobox'a
            MySqlDataReader list_stud_reader;
            try
            {
                //Инициализируем ридер
                list_stud_reader = list_stud_command.ExecuteReader();
                while (list_stud_reader.Read())
                {
                    DataRow rowToAdd = list_stud_table.NewRow();
                    rowToAdd["ID_Sirya"] = Convert.ToInt32(list_stud_reader[0]);
                    rowToAdd["Naimenovanie_Sirya"] = list_stud_reader[1].ToString();
                    list_stud_table.Rows.Add(rowToAdd);
                }
                list_stud_reader.Close();
                conn_db.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка чтения списка ЦП \n\n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            finally
            {
                conn_db.Close();
            }

            // создаём объект для подключения к БД
            MySqlConnection conn_db2;
            conn_db2 = new MySqlConnection(connStr);
            //Формирование списка статусов
            DataTable list_stud_table2 = new DataTable();
            MySqlCommand list_stud_command2 = new MySqlCommand();
            //Открываем соединение
            conn_db2.Open();
            //Формируем столбцы для комбобокса списка ЦП
            list_stud_table2.Columns.Add(new DataColumn("ID_Postavshika", System.Type.GetType("System.Int32")));
            list_stud_table2.Columns.Add(new DataColumn("Naimenovanie_Kompanii", System.Type.GetType("System.String")));
            //Настройка видимости полей комбобокса
            comboBox4.DataSource = list_stud_table2;
            comboBox4.DisplayMember = "Naimenovanie_Kompanii";
            comboBox4.ValueMember = "ID_Postavshika";
            //Формируем строку запроса на отображение списка статусов прав пользователя
            string sql_list_users2 = "SELECT ID_Postavshika, Naimenovanie_Kompanii FROM Postavshik";
            list_stud_command2.CommandText = sql_list_users2;
            list_stud_command2.Connection = conn_db2;
            //Формирование списка ЦП для combobox'a
            MySqlDataReader list_stud_reader2;
            try
            {
                //Инициализируем ридер
                list_stud_reader2 = list_stud_command2.ExecuteReader();
                while (list_stud_reader2.Read())
                {
                    DataRow rowToAdd2 = list_stud_table2.NewRow();
                    rowToAdd2["ID_Postavshika"] = Convert.ToInt32(list_stud_reader2[0]);
                    rowToAdd2["Naimenovanie_Kompanii"] = list_stud_reader2[1].ToString();
                    list_stud_table2.Rows.Add(rowToAdd2);
                }
                list_stud_reader2.Close();
                conn_db2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка чтения списка ЦП \n\n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            finally
            {
                conn_db2.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connStr = "server = chuc.caseum.ru; user = st_2_18_2; database = is_2_18_st2_VKR; password = 58791144 ; port = 33333";
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "UPDATE Postavki SET Postavshik= '" + comboBox2.Text + "', Naimenovanie_Sirya= '" + comboBox1.Text + "', Kolihestvo= '" + textBox1.Text + "', Data_Postavki= '" + textBox7.Text + "' WHERE ID_Postavki= " + edit_test.test_id;
            MySqlCommand comm = new MySqlCommand(sql, conn);
            comm.ExecuteNonQuery();
            conn.Close();
            reload_list();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string connStr = "server = chuc.caseum.ru; user = st_2_18_2; database = is_2_18_st2_VKR; password = 58791144 ; port = 33333";
                MySqlConnection conn = new MySqlConnection(connStr);
                conn.Open();
                string sql = "INSERT INTO Postavki(Postavshik, Naimenovanie_Sirya, Kolihestvo, Data_Postavki) VALUE ('" + comboBox4.Text + "','" + comboBox2.Text + "'," + textBox5.Text + "," + textBox3.Text + ")";
                MySqlCommand comn = new MySqlCommand(sql, conn);
                MySqlDataReader reader = comn.ExecuteReader();


                while (reader.Read())
                {

                    comboBox4.Text = (reader[0].ToString());
                    comboBox2.Text = (reader[1].ToString());
                    textBox5.Text = (reader[2].ToString());
                    textBox3.Text = (reader[3].ToString());






                }

                conn.Close();
                reload_list();

            }
            catch
            {

                
                    if (textBox5.Text == "" || textBox3.Text == "")
                    {
                        MessageBox.Show("Заполните поля");
                    }
                
             }
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

            sql = "SELECT Postavshik, Naimenovanie_Sirya, Kolihestvo, Data_Postavki FROM Postavki WHERE ID_Postavok = " + edit_test.test_id;


            conn.Open();

            MySqlCommand comm = new MySqlCommand(sql, conn);
            MySqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                comboBox2.Text = (reader[0].ToString());
                comboBox1.Text = (reader[1].ToString());
                label3.Text = (reader[2].ToString());
                textBox1.Text = (reader[3].ToString());
                textBox7.Text = (reader[4].ToString());





            }
            conn.Close();

            // создаём объект для подключения к БД
            MySqlConnection conn_db;
            conn_db = new MySqlConnection(connStr);
            //Формирование списка статусов
            DataTable list_stud_table = new DataTable();
            MySqlCommand list_stud_command = new MySqlCommand();
            //Открываем соединение
            conn_db.Open();
            //Формируем столбцы для комбобокса списка ЦП
            list_stud_table.Columns.Add(new DataColumn("ID_Postavki", System.Type.GetType("System.Int32")));
            list_stud_table.Columns.Add(new DataColumn("Naimenovanie_Sirya", System.Type.GetType("System.String")));
            //Настройка видимости полей комбобокса
            comboBox1.DataSource = list_stud_table;
            comboBox1.DisplayMember = "Naimenovanie_Sirya";
            comboBox1.ValueMember = "ID_Sirya";
            //Формируем строку запроса на отображение списка статусов прав пользователя
            string sql_list_users = "SELECT ID_Sirya, Naimenovanie_Sirya FROM Sirye";
            list_stud_command.CommandText = sql_list_users;
            list_stud_command.Connection = conn_db;
            //Формирование списка ЦП для combobox'a
            MySqlDataReader list_stud_reader;
            try
            {
                //Инициализируем ридер
                list_stud_reader = list_stud_command.ExecuteReader();
                while (list_stud_reader.Read())
                {
                    DataRow rowToAdd = list_stud_table.NewRow();
                    rowToAdd["ID_Sirya"] = Convert.ToInt32(list_stud_reader[0]);
                    rowToAdd["Naimenovanie_Sirya"] = list_stud_reader[1].ToString();
                    list_stud_table.Rows.Add(rowToAdd);
                }
                list_stud_reader.Close();
                conn_db.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка чтения списка ЦП \n\n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            finally
            {
                conn_db.Close();
            }

            // создаём объект для подключения к БД
            MySqlConnection conn_db2;
            conn_db2 = new MySqlConnection(connStr);
            //Формирование списка статусов
            DataTable list_stud_table2 = new DataTable();
            MySqlCommand list_stud_command2 = new MySqlCommand();
            //Открываем соединение
            conn_db2.Open();
            //Формируем столбцы для комбобокса списка ЦП
            list_stud_table2.Columns.Add(new DataColumn("ID_Postavshika", System.Type.GetType("System.Int32")));
            list_stud_table2.Columns.Add(new DataColumn("Naimenovanie_Postavshika", System.Type.GetType("System.String")));
            //Настройка видимости полей комбобокса
            comboBox3.DataSource = list_stud_table2;
            comboBox3.DisplayMember = "Naimenovanie_Postavshika";
            comboBox3.ValueMember = "ID_Postavshika";
            //Формируем строку запроса на отображение списка статусов прав пользователя
            string sql_list_users2 = "SELECT ID_Postavshika, Naimenovanie_Postavshika FROM Postavshik";
            list_stud_command2.CommandText = sql_list_users2;
            list_stud_command2.Connection = conn_db2;
            //Формирование списка ЦП для combobox'a
            MySqlDataReader list_stud_reader2;
            try
            {
                //Инициализируем ридер
                list_stud_reader2 = list_stud_command2.ExecuteReader();
                while (list_stud_reader2.Read())
                {
                    DataRow rowToAdd = list_stud_table.NewRow();
                    rowToAdd["ID_Postavshika"] = Convert.ToInt32(list_stud_reader2[0]);
                    rowToAdd["Naimenovanie_Postavshika"] = list_stud_reader2[1].ToString();
                    list_stud_table2.Rows.Add(rowToAdd);
                }
                list_stud_reader2.Close();
                conn_db2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка чтения списка ЦП \n\n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            finally
            {
                conn_db2.Close();
            }
        }
    }
}
