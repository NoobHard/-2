using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private Form currentChildForm;
        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();
            castomizeDesing();
        }

       

        private void OpenChildForm(Form childForm)
        {
            //open only form
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            //End
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel4.Controls.Add(childForm);
            panel4.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }



        private void Form1_Load(object sender, EventArgs e)
        {

            timer1.Interval = 10;
            timer1.Enabled = true;
            timer1.Start();
            
        }
        private void castomizeDesing()
        {
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            panel8.Visible = false;
        }
        private void hideSubMenu()
        {
            if (panel5.Visible == true)
            {
                panel5.Visible = false;
            }
               
            if (panel6.Visible == true)
            {
                panel6.Visible = false;
            }
               
            if (panel7.Visible == true)
            {
                panel7.Visible = false;
            }

            if (panel8.Visible == true)
            {
                panel8.Visible = false;
            }


        }
        private void showSubMenu(Panel panel)
        {
            if (panel.Visible == false)
            {
                hideSubMenu();
                panel.Visible = true;
            }
            else
            {
                panel.Visible = false;
            }
               
        }


        private void button1_Click(object sender, EventArgs e)
        {
            showSubMenu(panel5);
          
        }


        private void button6_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            OpenChildForm(new Sklad_Tovarov());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            OpenChildForm(new FormTovar());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            showSubMenu(panel6);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            showSubMenu(panel8);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            showSubMenu(panel7);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            showSubMenu(panel6);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            OpenChildForm(new FormSirya());
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            hideSubMenu();
            OpenChildForm(new Sklad_Sirya());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongTimeString();
            label2.Text = DateTime.Now.ToLongDateString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            OpenChildForm(new FormZakazi());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            OpenChildForm(new FormKlient());
        }

        private void button11_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            OpenChildForm(new Spisok_Postavok());
        }

        private void button10_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            OpenChildForm(new FormPostavshiki());
        }
        
       

        private void button13_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       
           
        

        private void button14_Click_1(object sender, EventArgs e)
        {

            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;


        }


        private void button15_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

      
    }
}
