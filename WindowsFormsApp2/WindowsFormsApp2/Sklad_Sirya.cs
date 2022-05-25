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
    public partial class Sklad_Sirya : Form
    {
        interface Programm
        {
            void reload_list(DataGridView dt);
            void GetListUsers(string selectCommand);
            void delegatt(DataGridView dt);
            void delegatt2(string selectCommand);

        }

        class Sklad : Programm
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
            delegate void Mess3(int id);

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


        Sklad sk = new Sklad();

        public Sklad_Sirya()
        {
            InitializeComponent();
            this.CenterToScreen();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connStr = "server = chuc.caseum.ru; user = st_2_18_2; database = is_2_18_st2_VKR; password = 58791144 ; port = 33333";
            MySqlConnection conn = new MySqlConnection(connStr);
            string sql = "DELETE FROM Sklad_Sirya WHERE ID_Sirya = " + edit_test.test_id;
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
            sk.delegatt(dataGridView1);
        }

        private void Sklad_Sirya_Load(object sender, EventArgs e)
        {
            string connStr = "server = chuc.caseum.ru; user = st_2_18_2; database = is_2_18_st2_VKR; password = 58791144 ; port = 33333";
            MySqlConnection conn = new MySqlConnection(connStr);
            Sklad.sql = "SELECT * FROM Sklad_Sirya";

            conn.Open();
            dataGridView1.DataSource = Sklad.bs_list_users;


            sk.delegatt2(Sklad.sql);


            dataGridView1.Columns[0].HeaderText = "ID Сырья";
            dataGridView1.Columns[1].HeaderText = "Наименование сырья";
            dataGridView1.Columns[2].HeaderText = "Количество ";
            dataGridView1.Columns[3].HeaderText = "ФИО Сотрудника заведующий складом";






            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns[2].Visible = true;
            dataGridView1.Columns[3].Visible = true;







            ////Режим для полей "Только для чтения"

            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;



            //Растягивание полей грида
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;








            //Убираем заголовки строк
            dataGridView1.RowHeadersVisible = false;

            sk.delegatt(dataGridView1);
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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string connStr = "server = chuc.caseum.ru; user = st_2_18_2; database = is_2_18_st2_VKR; password = 58791144 ; port = 33333";
                MySqlConnection conn = new MySqlConnection(connStr);
                conn.Open();
                string sql = "INSERT INTO Sklad_Sirya (Naimenovanie_Sirya, Kolichestvo_Sirya, FIO_Sotrudnika) VALUE ('" + comboBox2.Text + "'," + textBox5.Text + ",'" + textBox1.Text + "')";
                MySqlCommand comn = new MySqlCommand(sql, conn);
                MySqlDataReader reader = comn.ExecuteReader();


                while (reader.Read())
                {


                    comboBox2.Text = (reader[0].ToString());
                    textBox5.Text = (reader[1].ToString());
                    textBox1.Text = (reader[2].ToString());





                }


                conn.Close();
                sk.delegatt(dataGridView1);


            }
            catch
            {


                if (comboBox2.Text == "" || textBox5.Text == "" || textBox1.Text == "")
                {
                    MessageBox.Show("Заполните поля");
                }
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                Sklad.index_selected_rows = dataGridView1.SelectedCells[0].RowIndex.ToString();
                //ID 
                Sklad.id_selected_rows = dataGridView1.Rows[Convert.ToInt32(Sklad.index_selected_rows)].Cells[0].Value.ToString();
                //вызов метода выбора пользователя с передачей параметра ID пользователя

                edit_test.test_id = Convert.ToInt32(Sklad.id_selected_rows);
            }
            catch
            {
                MessageBox.Show("Выберете заполненные поля");

            }
            string sql;
            string connStr = "server = chuc.caseum.ru; user = st_2_18_2; database = is_2_18_st2_VKR; password = 58791144 ; port = 33333";
            MySqlConnection conn = new MySqlConnection(connStr);

            sql = "SELECT Naimenovanie_Sirya, Kolichestvo_Sirya, FIO_Sotrudnika FROM Sklad_Sirya WHERE ID_Sirya = " + edit_test.test_id;


            conn.Open();

            MySqlCommand comm = new MySqlCommand(sql, conn);
            MySqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {

                comboBox1.Text = (reader[0].ToString());
                textBox4.Text = (reader[1].ToString());
                textBox7.Text = (reader[2].ToString());




            }
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
        }
    }
}
