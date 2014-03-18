using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        public int CountTimer = 0;
        public int sb = 2;
        public int sr = 0;
        public int sl = 0;
        public float score = 0;
        public int count = 0;
        public int lives = 3;
        public int temp1 = 0;
        public int temp2 = 0;
        public int spd = 1;
        public int difficult = 1;
        float kf = 0;        
        public Form1()
        {
            InitializeComponent();
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            Random rnd = new Random(DateTime.Now.Second * DateTime.Now.Millisecond);
            pictureBox2.Top = -50;
            pictureBox2.Left = rnd.Next(0, this.Width-50);
            pictureBox3.Top = -50;
            pictureBox3.Left = rnd.Next(0, this.Width - 50);
            timer3.Start();

          
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            label4.Text = String.Format("({0}, {1}) {2}x{3}",
            this.Location.X, this.Location.Y, this.Width, Height);
        } 
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.Right) timer1.Start(); else timer1.Stop();
            if (e.KeyData == Keys.Left) timer2.Start(); else timer2.Stop();
        }
        private void Form1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
         timer5.Start();
         sl = sr = 0;
         timer1.Stop();
         timer2.Stop();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            sl++;
            if (sl < 3) pictureBox1.Left += 2;  else pictureBox1.Left += 2 + spd;
            if (pictureBox1.Left > this.Width - 75) pictureBox1.Left = -50;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            sr++;
            if (sr < 3) pictureBox1.Left -= 2; else pictureBox1.Left -= 2 + spd;
            if (pictureBox1.Left < -50) pictureBox1.Left = this.Width - 75;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            kf = ((float)this.Width / Height) * ((float)this.Width / Height);
            label4.Text = "Коэффициент сложности: " + Math.Round(kf, 1);
            label1.Text = "Ваш счёт: " + Math.Round(score, 0);
            label2.Text = "Жизни: " + lives;
            label6.Text = "";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            Random random = new Random(DateTime.Now.Second * DateTime.Now.Millisecond);
            pictureBox3.Left += sb;
            pictureBox3.Top += Math.Abs(sb/2);
            if (pictureBox3.Left > this.Width - 30||pictureBox3.Left< 0) sb = -sb;
            if (pictureBox3.Left > pictureBox1.Left + 8 && pictureBox3.Left < pictureBox1.Left + 68 &&
                pictureBox3.Top > Height - 100 && pictureBox3.Top < Height - 60)
            {
                if (spd < 5) spd++;
                lives++;
                pictureBox3.Top = -50;
                pictureBox3.Left = random.Next(0, this.Width - 50);
                if (spd < 5) label3.Text = "Скорость - х" + spd; else label3.Text = "Скорость MAX";
                label5.Text = "Жизни - " + lives;
                CountTimer = 50;
                timer4.Stop();
            }
            if (pictureBox3.Top > Height - 30) { 
            pictureBox3.Top = -50;
            pictureBox3.Left = random.Next(0, this.Width - 50);
            timer4.Stop();
            }
        }


        public class result
        {
            
        }
        private void timer5_Tick(object sender, EventArgs e)
        {
            label6.Left = pictureBox1.Left+30;
            label6.Top = pictureBox1.Top - 50;
            if (CountTimer > 0) CountTimer--;
            if (CountTimer == 1) { label3.Text = ""; label5.Text = ""; } 
            Random random = new Random(DateTime.Now.Second*DateTime.Now.Millisecond);
            
            int popadanie=0;

            pictureBox2.Top += 2 + difficult;
            if (pictureBox2.Left > pictureBox1.Left + 12 && pictureBox2.Left < pictureBox1.Left + 64 &&
                pictureBox2.Top > Height - 100 && pictureBox2.Top < Height - 60)
            {
                label6.Text = "+" + Math.Round(100 * kf * difficult, 0);
                lives++;
                count++;
                score = (score/kf + 100 * difficult)*kf;
                popadanie = 1;

            }

            if (pictureBox2.Top > Height - 30 || popadanie == 1)
            {
                pictureBox2.Top = -50;
                pictureBox2.Left = random.Next(0, this.Width - 50);
                lives--;
            }
   //         label1.Text = "Ваш счёт: " + Math.Round(score, 0);
   //         label2.Text = "Жизни: " + lives;
            temp1 = count/10;
            if (temp1 > temp2)
            {
                temp2 = temp1;
                difficult++;
                label3.Text = "Уровень " + difficult;
                CountTimer = 35;
            //    if (spd<5)spd++;
                timer4.Start();
            }
            if (lives==0)
            {
                label3.Text="Вы проиграли!";
                timer1.Stop();
                timer2.Stop();
                timer5.Stop();
                var res = MessageBox.Show("Начать сналчала?", "Вы проиграли!", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    sl = sr = 0;
                    score = 0;
                    difficult=1;
                    lives = 3;
                    temp1 = 0;
                    temp2 = 0;
                    spd = 1;
                    count = 0;
                    CountTimer = 0;
                    label6.Text = "";
                    label3.Text = "";
                    timer5.Start();
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            lives = 3;
            count = 0;
            timer5.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
