using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = "Возраст: " + trackBar1.Value;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ed = new EducData
            {
                NameUnv = textBox1.Text,
                SpecDip = textBox2.Text,
                YearEnd = textBox3.Text
            };
            listBox1.Items.Add(ed);
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog() { Filter = "файл заявки|*.txt" };
            var result = sfd.ShowDialog(this);
            if (result==DialogResult.OK)
            {
                var fileName = sfd.FileName;
                AppData ad = new AppData();
                if (radioButton1.Checked)
                    ad.exp = Exp.one;
                else
                    if (radioButton2.Checked)
                        ad.exp = Exp.three;
                    else
                        ad.exp = Exp.five;
                if (radioButton4.Checked)
                    ad.worktime = WorkTime.five;
                else
                    if (radioButton5.Checked)
                        ad.worktime = WorkTime.twotwo;
                    else
                        ad.worktime = WorkTime.onethree;
                if (radioButton7.Checked) ad.drivelicense = DriveLicense.A;
                if (radioButton8.Checked) ad.drivelicense = DriveLicense.B;
                if (radioButton9.Checked) ad.drivelicense = DriveLicense.C;
                if (radioButton10.Checked) ad.drivelicense = DriveLicense.D;
                if (radioButton11.Checked) ad.drivelicense = DriveLicense.E;
                if (radioButton12.Checked) ad.drivelicense = DriveLicense.No;
                if (checkBox1.Checked)
                    ad.alc = Alc.on;
                else
                    ad.alc = Alc.off;
                if (checkBox2.Checked)
                    ad.smoke = Smoke.on;
                else
                    ad.smoke = Smoke.off;

                ad.Educ = new List<EducData>();
                foreach (EducData ed in listBox1.Items)
                {
                    ad.Educ.Add(ed);
                }

                ad.AAge = trackBar1.Value;

             XmlSerializer xs = new XmlSerializer(typeof(AppData));
                var fileStream = File.Create(fileName);
                xs.Serialize(fileStream, ad);
                fileStream.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog() { Filter = "файл заявки|*.txt" };
            var result = ofd.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                var xs = new XmlSerializer(typeof(AppData));
                var file = File.Open(ofd.FileName,FileMode.Open);
                var ad = (AppData)xs.Deserialize(file);
                file.Close();
                label1.Text = "Возраст: " + ad.AAge;
                trackBar1.Value = ad.AAge;
                radioButton1.Checked = ad.exp == Exp.one;
                radioButton2.Checked = ad.exp == Exp.three;
                radioButton3.Checked = ad.exp == Exp.five;
                radioButton4.Checked = ad.worktime == WorkTime.five;
                radioButton5.Checked = ad.worktime == WorkTime.twotwo;
                radioButton6.Checked = ad.worktime == WorkTime.onethree;
                radioButton7.Checked = ad.drivelicense == DriveLicense.A;
                radioButton8.Checked = ad.drivelicense == DriveLicense.B;
                radioButton9.Checked = ad.drivelicense == DriveLicense.C;
                radioButton10.Checked = ad.drivelicense == DriveLicense.D;
                radioButton11.Checked = ad.drivelicense == DriveLicense.E;
                radioButton12.Checked = ad.drivelicense == DriveLicense.No;
                checkBox1.Checked = ad.smoke == Smoke.on;
                checkBox2.Checked = ad.alc == Alc.on;
                listBox1.Items.Clear();
                foreach (var EducData in ad.Educ)
                {
                    listBox1.Items.Add(EducData);
                }

            }
        }

    }
   public class AppData
    {
        public Exp exp { get; set; }
        public WorkTime worktime { get; set; }
        public DriveLicense drivelicense { get; set; }
        public Smoke smoke { get; set; }
        public Alc alc { get; set; }
        public List<EducData> Educ { get; set; }
        public int AAge { get; set; }
    }   
      public class AAgeData
      {  
      }
      public enum Alc { on, off}
      public enum Smoke { on, off }
      public enum Exp
    {
        one,
        three,
        five
    }
      public enum WorkTime
    {
        five,
        twotwo,
        onethree
    }
      public enum DriveLicense
    {
        A,
        B,
        C,
        D,
        E,
        No
    }

      public class EducData
    {
        public string NameUnv { get; set; }
        public string SpecDip { get; set; }
        public string YearEnd { get; set; }
        public override string ToString()
        {
            var s = NameUnv + " " + SpecDip + " " + YearEnd;
            return s;
        }
    }
}
