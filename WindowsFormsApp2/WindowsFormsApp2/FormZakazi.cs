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
    public partial class FormZakazi : Form
    {
        static string connStr = "server = chuc.caseum.ru; user = st_2_18_2; database = is_2_18_st2_VKR; password = 58791144 ; port = 33333";
        MySqlConnection conn = new MySqlConnection(connStr);
        DataTable dt_list_users = new DataTable();
        BindingSource bs_list_users = new BindingSource();
        MySqlDataAdapter list_users_adapter = new MySqlDataAdapter();
        string id_selected_rows;
        string commandString;
        string index_selected_rows;
        string sql;

        
    

        public FormZakazi()
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
            string sql = "UPDATE Zakazi SET FIO= '" + textBox1.Text + "', Nazvanie_Kompanii= '" + textBox2.Text + "', Naimenovanie_Tovara= '" + comboBox1.Text + "', Koliсhestvo = " + textBox8.Text + ", Summa = '" + label1.Text + "' WHERE ID_Zakaza= " + edit_test.test_id;
            MySqlCommand comm = new MySqlCommand(sql, conn);
            comm.ExecuteNonQuery();
            conn.Close();
            reload_list();
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

            sql = "SELECT FIO, Nazvanie_Kompanii, Naimenovanie_Tovara, Kolichestvo, Summa FROM Zakazi WHERE ID_Zakaza = " + edit_test.test_id;

         
            conn.Open();

            MySqlCommand comm = new MySqlCommand(sql, conn);
            MySqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                textBox1.Text = (reader[0].ToString());
                textBox2.Text = (reader[1].ToString());
                comboBox1.Text = (reader[2].ToString());
                textBox7.Text = (reader[3].ToString());
                label11.Text = (reader[4].ToString());
 




            }

            conn.Close();

            MySqlConnection conn_db;
            conn_db = new MySqlConnection(connStr);
            //Формирование списка статусов
            DataTable list_stud_table = new DataTable();
            MySqlCommand list_stud_command = new MySqlCommand();
            //Открываем соединение
            conn_db.Open();
            //Формируем столбцы для комбобокса списка ЦП
            list_stud_table.Columns.Add(new DataColumn("ID_Tovara", System.Type.GetType("System.Int32")));
            list_stud_table.Columns.Add(new DataColumn("Naimenovanie", System.Type.GetType("System.String")));
            //Настройка видимости полей комбобокса
            comboBox2.DataSource = list_stud_table;
            comboBox2.DisplayMember = "Naimenovanie";
            comboBox2.ValueMember = "ID_Tovara";
            //Формируем строку запроса на отображение списка статусов прав пользователя
            string sql_list_users = "SELECT ID_Tovara, Naimenovanie FROM Tovar";
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
                    rowToAdd["ID_Tovara"] = Convert.ToInt32(list_stud_reader[0]);
                    rowToAdd["Naimenovanie"] = list_stud_reader[1].ToString();
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
        
        private void FormZakazi_Load(object sender, EventArgs e)
        {
            string connStr = "server = chuc.caseum.ru; user = st_2_18_2; database = is_2_18_st2_VKR; password = 58791144 ; port = 33333";
            MySqlConnection conn = new MySqlConnection(connStr);
            sql = "SELECT * FROM Zakazi";
            conn.Open();
            dataGridView1.DataSource = bs_list_users;


            GetListUsers(sql);


            dataGridView1.Columns[0].HeaderText = "ID Закзаа";
            dataGridView1.Columns[1].HeaderText = "ФИО";
            dataGridView1.Columns[2].HeaderText = "Название компании";
            dataGridView1.Columns[3].HeaderText = "Название товара";
            dataGridView1.Columns[4].HeaderText = "Количество";
            dataGridView1.Columns[5].HeaderText = "Сумма";



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
            list_stud_table.Columns.Add(new DataColumn("ID_Tovara", System.Type.GetType("System.Int32")));
            list_stud_table.Columns.Add(new DataColumn("Naimenovanie", System.Type.GetType("System.String")));
            //Настройка видимости полей комбобокса
            comboBox2.DataSource = list_stud_table;
            comboBox2.DisplayMember = "Naimenovanie";
            comboBox2.ValueMember = "ID_Tovara";
            //Формируем строку запроса на отображение списка статусов прав пользователя
            string sql_list_users = "SELECT ID_Tovara, Naimenovanie FROM Tovar";
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
                    rowToAdd["ID_Tovara"] = Convert.ToInt32(list_stud_reader[0]);
                    rowToAdd["Naimenovanie"] = list_stud_reader[1].ToString();
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

            edit_test.name_tovar = "Цинк металлический — ГОСТ 3640-94";


        }
            
        private void button1_Click(object sender, EventArgs e)
        {

            string connStr = "server = chuc.caseum.ru; user = st_2_18_2; database = is_2_18_st2_VKR; password = 58791144 ; port = 33333";
            MySqlConnection conn = new MySqlConnection(connStr);
            string sql = "DELETE  FROM Zakazi WHERE ID_Zakaza = " + edit_test.test_id;
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
            try
            {
                string connStr = "server = chuc.caseum.ru; user = st_2_18_2; database = is_2_18_st2_VKR; password = 58791144 ; port = 33333";
                MySqlConnection conn = new MySqlConnection(connStr);
                conn.Open();
                string sql = "INSERT INTO Zakazi (FIO, Nazvanie_Kompanii, Naimenovanie_Tovara, Koliсhestvo , Summa) VALUE ('" + textBox6.Text + "','" + textBox5.Text + "','" + comboBox2.Text + "'," + textBox8.Text + ",'" + label2.Text + "')";
                MySqlCommand comn = new MySqlCommand(sql, conn);
                MySqlDataReader reader = comn.ExecuteReader();


                while (reader.Read())
                {

                    textBox6.Text = (reader[0].ToString());
                    textBox5.Text = (reader[1].ToString());
                    comboBox2.Text = (reader[2].ToString());
                    textBox8.Text = (reader[3].ToString());
                    label12.Text = (reader[4].ToString());





                }

                conn.Close();
                reload_list();


                

            }
            catch
            {
            

                if (textBox6.Text == "" || textBox5.Text == ""  || textBox8.Text == "")
                {
                    MessageBox.Show("Заполните поля");

                }
                
            }

        }
        
        private void textBox8_TextChanged(object sender, EventArgs e)
        {

          
           
            int amount;
            string connStr1 = "server = chuc.caseum.ru; user = st_2_18_2; database = is_2_18_st2_VKR; password = 58791144 ; port = 33333";
            MySqlConnection conn1 = new MySqlConnection(connStr1);
            string sql1 = $"SELECT Stoimost FROM `Tovar` WHERE `Naimenovanie` = '{edit_test.name_tovar}';"; 
            
 
            conn1.Open();
            MySqlCommand comn1 = new MySqlCommand(sql1, conn1);
            MySqlDataReader reader1 = comn1.ExecuteReader();
            while (reader1.Read())
            {
                
                amount = (Convert.ToInt32(reader1[0]));
                

                try
                {

                    
                    double pr = Convert.ToInt32(textBox8.Text) * Convert.ToInt32(amount);
                   
                    label12.Text = pr.ToString();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }












            conn1.Close();


        }

      
       

        private void button4_Click(object sender, EventArgs e)
        {
            string connStr = "server = chuc.caseum.ru; user = st_2_18_2; database = is_2_18_st2_VKR; password = 58791144 ; port = 33333";
            MySqlConnection conn = new MySqlConnection(connStr);
            string sql = "DELETE  FROM Zakazi WHERE ID_Zakaza = " + edit_test.test_id;
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            edit_test.name_tovar = comboBox2.Text;
           
           
        }
    }
}
