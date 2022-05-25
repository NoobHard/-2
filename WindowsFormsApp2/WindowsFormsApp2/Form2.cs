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
    public partial class Form2 : Form
    {
       
      
        public Form2()
        {
          
            InitializeComponent();
            this.CenterToScreen();
            Opacity = 0;
            bool fadingIn = true;
            Timer timer = new Timer();
            timer.Tick += new EventHandler((s, e1) =>
            {
                if (fadingIn)
                {
                    if ((Opacity += 0.02d) >= 1)
                    {
                        fadingIn = false;
                        
                        
                    }
                }
                else
                {
                    if ((Opacity -= 0.02d) <= 0)
                    {

                        timer.Stop();
                        Form1 form = new Form1();
                        form.Show();


                        this.Visible = false;
                    }
                 

                }

            });
            timer.Interval = 60;
            timer.Start();

        }
       


        private void Form2_Load(object sender, EventArgs e)
        {




          


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
