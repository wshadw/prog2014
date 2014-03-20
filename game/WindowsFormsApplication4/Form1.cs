using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        public int pause = 0;
        public int fly = 0;
        public int obuchenie=0;
        public int scor = 0;
        public int scorefile = 0;
        public int CountTimer = 0;
        public int sb = 2, sb2 = 2;
        public int sr = 0;
        public int sl = 0;
        public float score = 0;
        public int count = 0;
        public int lives = 3;
        public int temp1 = 0;
        public int temp2 = 0;
        public int spd = 1;
        public int difficult = 1;
        public int slow = 1;
        public int start = 0;
        public int t4 = 0, t6 = 0, t7 = 0;
        public float kf = 0;
        private int kfs = 0;
        public Form1()
        {
            InitializeComponent();
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            Random rnd = new Random(DateTime.Now.Second * DateTime.Now.Millisecond);
            pictureBox2.Top = -50;
            pictureBox2.Left = rnd.Next(0, this.Width - 50);
            pictureBox3.Top = -50;
            pictureBox3.Left = rnd.Next(0, this.Width - 50);
            pictureBox4.Top = -50;
            pictureBox4.Left = rnd.Next(0, this.Width - 50);
            pictureBox5.Top = -50;
            pictureBox5.Left = rnd.Next(0, this.Width - 50);

            label5.Text = "Чтобы начать нажмите \nвлево или вправо.";
            CountTimer = 2;
            timer3.Start();
            if (File.Exists("record.s"))

            {
                var xs = new XmlSerializer(typeof (AppData));
                var file = File.Open("record.s", FileMode.Open);
                var ad = (AppData) xs.Deserialize(file);
                file.Close();
                scorefile = ad.scores;
                label7.Text = "Рекорд: " + ad.scores;

            }
            if (scorefile == 0)
            {
                var res = MessageBox.Show("Пройти обучение?", "Внимание!", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    obuchenie = 1;
                    MessageBox.Show("Изменяя размер экрана меняется коэффициент сложности, в зависимости от которого начисляются очки.", "Обучение", MessageBoxButtons.OK);
                    MessageBox.Show("Для того чтобы поставить игру на паузу, необходимо нажать пробел.", "Обучение", MessageBoxButtons.OK);
                    MessageBox.Show("Необходимо собирать красные шарики, пропустив которые теряется жизнь.", "Обучение", MessageBoxButtons.OK);
                }
            }
            
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            label4.Text = String.Format("({0}, {1}) {2}x{3}",
            this.Location.X, this.Location.Y, this.Width, Height);
        } 
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.Right&&pause==0) timer1.Start();
            if (e.KeyData == Keys.Left&&pause==0) timer2.Start();
        }
        private void Form1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
         if (e.KeyData == Keys.Right) timer1.Stop();
         if (e.KeyData == Keys.Left) timer2.Stop();
            if (e.KeyData == Keys.Space&&start==1)
            {
                if (pause == 0)
                {
                    pause = 1;
                    label3.Text = "      ПАУЗА";
                    timer1.Stop();
                    timer2.Stop();
                    timer4.Stop();
                    timer5.Stop();
                    timer6.Stop();
                    timer7.Stop();
                }
                else
                {
                    pause = 0;
                    label3.Text = "";
                    if(t4==1)timer4.Start();
                    if(t6==1)timer6.Start();
                    if(t7==1)timer7.Start();
                    timer5.Start();
                }
            }
            if (pause==0)timer5.Start();
         sl = sr = 0;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            sl++;
            if (sl < 8) pictureBox1.Left += 2;  else pictureBox1.Left += (2 + spd*kfs)+(int)((kf*kf)/1.5);
            if (pictureBox1.Left > this.Width - pictureBox1.Width/2) pictureBox1.Left = -pictureBox1.Width/2;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            sr++;
            if (sr < 8) pictureBox1.Left -= 2; else pictureBox1.Left -= (2 + spd*kfs)+(int)((kf*kf)/1.5);
            if (pictureBox1.Left < -pictureBox1.Width/2) pictureBox1.Left = this.Width - pictureBox1.Width/2;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            Random random = new Random(DateTime.Now.Second * DateTime.Now.Millisecond);
            kf = ((float)this.Width / Height) ;
            label4.Text = "Коэффициент сложности: " + Math.Round(kf*kf, 1);
            label1.Text = "Ваш счёт: " + Math.Round(score, 0);
            label2.Text = "Жизни: " + lives;
            label6.Text = "";
            kfs=(int)((kf*kf)/1.5);
            if (kfs < 1) kfs = 1;
            pictureBox5.Width =(int)(this.Width/8);
            if (random.Next(0, 29)==1 && fly==0 && start==1&&pause==0) {
                if (obuchenie == 1 || obuchenie == 2)
                {
                    start = 0;
                    obuchenie = 3;
                    timer1.Stop();
                    timer2.Stop();
                    timer4.Stop();
                    timer7.Stop();
                    timer5.Stop();
                    MessageBox.Show("Синий шарик, бонусный, его ловить не обязательно, он уменьшает скорость падения красных.", "Обучение", MessageBoxButtons.OK);
                    timer5.Start();
                    if (t4==1)timer4.Start();
                    if (t7==1)timer7.Start();
                    start = 1;
                }
                timer6.Start();
                t6 = 1;
                fly = 1;

            }
            if (random.Next(0, 15) == 2 && fly == 0 && start == 1&&pause==0)
            {
                timer7.Start();
                fly = 1;
                t7 = 1;
            }
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
            if (pictureBox3.Left > pictureBox1.Left - pictureBox1.Width * 0.15 && pictureBox3.Left < pictureBox1.Left + pictureBox1.Width * 0.85 &&
                pictureBox3.Top > Height - 100 && pictureBox3.Top < Height - 50)
            {
                if (spd < 4) spd++;
                lives++;
                pictureBox3.Top = -50;
                pictureBox3.Left = random.Next(0, this.Width - 50);
                if (spd < 4) label3.Text = "Скорость - х" + spd; else label3.Text = "Скорость MAX";
                label5.Text = "Жизни - " + lives;
                CountTimer = 50;
                t4 = 0;
                timer4.Stop();
            }
            if (pictureBox3.Top > Height - 30) { 
            pictureBox3.Top = -50;
            pictureBox3.Left = random.Next(0, this.Width - 50);
                t4 = 0;
            timer4.Stop();
            }
        }


        public class result
        {
            
        }
        private void timer5_Tick(object sender, EventArgs e)
        {
            if (start == 0) start = 1;
            label6.Left = pictureBox1.Left;
            label6.Top = pictureBox1.Top - 50;
            if (CountTimer > 0) CountTimer--;
            if (CountTimer == 1) { label3.Text = ""; label5.Text = ""; } 
            Random random = new Random(DateTime.Now.Second*DateTime.Now.Millisecond);
            
            int popadanie=0;

            pictureBox2.Top += 3 + difficult -slow;
            if (pictureBox2.Left > pictureBox1.Left -pictureBox1.Width*0.3 && pictureBox2.Left < pictureBox1.Left + pictureBox1.Width*0.8 &&
                pictureBox2.Top > Height - 87 && pictureBox2.Top < Height - 60)
            {
                label6.Text = "+" + Math.Round(100 * kf * difficult, 0);
                lives++;
                count++;
                score = (score/(kf) + 100 * difficult)*(kf);
                popadanie = 1;

            }

            if (pictureBox2.Top > Height - 30 || popadanie == 1)
            {
                if (popadanie == 0) {label3.Text = "Промах";
                    label5.Text = "Жизни - " + (lives-1);
                    CountTimer = 30;
                }
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
                if (obuchenie == 1||obuchenie==3)
                {
                    timer4.Stop();
                    timer6.Stop();
                    timer7.Stop();
                    timer5.Stop();
                    timer1.Stop();
                    timer2.Stop();
                    MessageBox.Show("Вначале каждого уровня скорость движения красных шариков увеличивается, и падает зелёный.", "Обучение", MessageBoxButtons.OK);
                    MessageBox.Show("Зелёный шарик, бонусный. Его ловить не обязательно. Он даёт +1 жизнь, +1 к скорости.", "Обучение", MessageBoxButtons.OK);
                    if (obuchenie == 3) obuchenie = 4;
                    else obuchenie = 2;
                    if (t4==1)timer4.Start();
                    if (t6==1)timer6.Start();
                    if (t7==1)timer7.Start();
                    timer5.Start();
                    
                }
                timer4.Start();
                t4 = 1;
            }
            if (lives==0)
            {
                start = 0;
                label3.Text="Вы проиграли!";
                label5.Text = "";
                timer1.Stop();
                timer2.Stop();
                timer4.Stop();
                t4 = 0;
                timer5.Stop();
                timer6.Stop();
                t6 = 0;
                timer7.Stop();
                t7 = 0;
                scor = (int)Math.Round(score, 0);
                var fileName = "record.s";
              
                var ad = CreateAppData();



                XmlSerializer xs = new XmlSerializer(typeof(AppData));
                var fileStream = File.Create(fileName);
                xs.Serialize(fileStream, ad);
                fileStream.Close();

                var res = MessageBox.Show("Начать сначала?", "Вы проиграли!", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    kfs = 0;
                    fly = 0;
                    start = 1;
                    scor = 0;
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
                    label5.Text = "";
                    label3.Text = "";
                    pictureBox2.Top = -50;
                    pictureBox2.Left = random.Next(0, this.Width - 50);
                    pictureBox3.Top = -50;
                    pictureBox3.Left = random.Next(0, this.Width - 50);
                    pictureBox4.Top = -50;
                    pictureBox4.Left = random.Next(0, this.Width - 50);
                    pictureBox5.Top = -50;
                    pictureBox5.Left = random.Next(0, this.Width - 50);
                    slow = 1;
                    timer5.Start();
                }
                else Application.Exit();
            }
        }
        private AppData CreateAppData()
        {
            AppData ad = new AppData();
            if (scorefile < scor)
            {
                scorefile = scor;
                ad.scores = scor;
                label7.Text = "Рекорд: " + ad.scores;
            }
            else
            {
                ad.scores = scorefile;
            }
            return ad;
        }

        public class AppData
        {
            public int scores { get; set; }
        }

        private void label2_Click(object sender, EventArgs e)
        {
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void timer6_Tick(object sender, EventArgs e)
        {
            Random random = new Random(DateTime.Now.Second * DateTime.Now.Millisecond);
            pictureBox4.Left += sb2;
            pictureBox4.Top += Math.Abs(sb2 / 2);
            if (pictureBox4.Left > this.Width - 30 || pictureBox4.Left < 0) sb2 = -sb2;
            if (pictureBox4.Left > pictureBox1.Left - pictureBox1.Width * 0.3 && pictureBox4.Left < pictureBox1.Left + pictureBox1.Width * 0.8 &&
                pictureBox4.Top > Height - 100 && pictureBox4.Top < Height - 50)
            {
                if (slow<=difficult) slow++;
                pictureBox4.Top = -50;
                pictureBox4.Left = random.Next(0, this.Width - 50);
                label3.Text = "Замедление - х" + slow;
                CountTimer = 50;
                fly = 0;
                t6 = 0;
                timer6.Stop();
            }
            if (pictureBox4.Top > Height - 30)
            {
                pictureBox4.Top = -50;
                pictureBox4.Left = random.Next(0, this.Width - 50);
                fly = 0;
                t6 = 0;
                timer6.Stop();
            }
        }

        private void timer7_Tick(object sender, EventArgs e)
        {
            Random random = new Random(DateTime.Now.Second * DateTime.Now.Millisecond);
            pictureBox5.Top += Math.Abs(sb);
            if (pictureBox5.Left > pictureBox1.Left - pictureBox5.Width+5 && pictureBox5.Left < pictureBox1.Left + pictureBox1.Width &&
                pictureBox5.Top > Height - 110 && pictureBox5.Top < Height - 50)
            {
                lives--;
                pictureBox5.Top = -50;
                pictureBox5.Left = random.Next(0, this.Width - pictureBox5.Width);
                label3.Text = "Разрушение";
                label5.Text = "Потеря жизни";
                CountTimer = 50;
                fly = 0;
                t7 = 0;
                timer7.Stop();
            }
            if (pictureBox5.Top > Height - 30)
            {
                pictureBox5.Top = -50;
                pictureBox5.Left = random.Next(0, this.Width - pictureBox5.Width);
                t7 = 0;
                fly = 0;
                timer7.Stop();
            }
        }
    }
}
