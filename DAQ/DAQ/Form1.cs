using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAQ
{

    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
        }
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        private double sampleCounter = 5.8;
        private double loggingCounter = 51;
        private void btnSampling_Click(object sender, EventArgs e)
        {
            //int maxIds = 4;
            //for (int i = 0; i < maxIds; i++)
            //{
            //    Sensors sensors = new Sensors(i);
            //    double sensorValue = Math.Round(sensors.GetValue() * 65536) / 65536; //Converts to 16 bit resolution
            //    txtSensorValues.Text += "\r\n" + $"{i+1}, {sensorValue}, {DateTime.Now} ";
            //}

            int analogIds = 3;
            int digitalIds = 1;
            for (int i = 0; i < analogIds; i++)
            {
                Sensors sensors = new Sensors(i);
                double analogValues = Math.Round(((sensors.GetValue() * 10) - 5) * 65536) / 65536; //Converts to 16 bit resolution
                txtSensorValues.Text += "\r\n" + $"{i + 1}, {analogValues}, {DateTime.Now} ";
            }
            for (int i = 0; i < digitalIds; i++)
            {
                Sensors sensors = new Sensors(i);
                int digitalValues = (Convert.ToInt32(sensors.GetValue()) * 10) -5;
                txtSensorValues.Text += "\r\n" + $"{analogIds + i + 1}, {digitalValues}, {DateTime.Now} ";
            }
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 100; // 1 ms
            timer1.Start();
            txtSampling.Text = sampleCounter.ToString();
            btnSampling.Enabled = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is a DAQ simulator.","Help", System.Windows.Forms.MessageBoxButtons.OK);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {           
            sampleCounter -= 0.1;
            if (Math.Round(sampleCounter, 1) <= 0)
            {
                timer1.Stop();
                btnSampling.Enabled = true;
                sampleCounter = 5.8;
            }
            txtSampling.Text = Math.Round(sampleCounter,1).ToString();

        }

        private void btnLogging_Click(object sender, EventArgs e)
        {

            if (saveFileDialog1.FileName == "")
            {
                saveFileDialog1.ShowDialog();
                lblFileName.Text = saveFileDialog1.FileName;
            }
            File.AppendAllText(saveFileDialog1.FileName, txtSensorValues.Text);

            //if (saveFileDialog1.FileName != "")
            //{
            //    using (var writer = new StreamWriter(saveFileDialog1.FileName))
            //    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            //    {
            //        csv.WriteRecords(txtSensorValues.Text);
            //    }
            //}
            timer2 = new System.Windows.Forms.Timer();
            timer2.Tick += new EventHandler(timer2_Tick);
            timer2.Interval = 1000; // 1 second
            timer2.Start();
            txtLogging.Text = loggingCounter.ToString();
            btnLogging.Enabled = false;
            txtSensorValues.Text = "";
        }

        private void selectFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            lblFileName.Text = saveFileDialog1.FileName;
        }

        private void lblFileName_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            lblFileName.Text = saveFileDialog1.FileName;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            loggingCounter --;
            if (loggingCounter <= 0)
            {
                timer2.Stop();
                btnLogging.Enabled = true;
                loggingCounter = 51;
            }
            txtLogging.Text = loggingCounter.ToString();
        }
    }
}
