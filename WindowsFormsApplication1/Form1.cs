using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO.Ports;
using System.Drawing;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private int algorithmCount;
        private int algorithmNumber;
        private const int timer = 600;

        public Form1()
        {
            InitializeComponent();
        }
        
        private void comboBox1_Click(object sender, EventArgs e)
        {
            int num;
            comboBox1.Items.Clear();
            string[] ports = SerialPort.GetPortNames().OrderBy(a => a.Length > 3 && int.TryParse(a.Substring(3), out num) ? num : 0).ToArray();
            comboBox1.Items.AddRange(ports);
        }

        private void buttonOpenPort_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
                try
                {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.Open();
                    buttonOpenPort.Text = "Close";
                    comboBox1.Enabled = false;
                    button1.Visible = true;
                    button2.Visible = true;
                    panel1.Visible = true;
                    panel2.Visible = true;
                    panel3.Visible = true;
                    panel4.Visible = true;
                    panel5.Visible = true;
                    panel6.Visible = true;
                    panel7.Visible = true;
                    panel8.Visible = true;
                }
                catch
                {
                    MessageBox.Show("Port " + comboBox1.Text + " is invalid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            else
            {
                serialPort1.Close();
                buttonOpenPort.Text = "Open";
                comboBox1.Enabled = true;
                button1.Visible = false;
                button2.Visible = false;
                panel1.Visible = false;
                panel2.Visible = false;
                panel3.Visible = false;
                panel4.Visible = false;
                panel5.Visible = false;
                panel6.Visible = false;
                panel7.Visible = false;
                panel8.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.Write("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.Write("2");
        }

        private void clearAllLed()
        {
            panel1.BackColor = Color.Gray;
            panel2.BackColor = Color.Gray;
            panel3.BackColor = Color.Gray;
            panel4.BackColor = Color.Gray;
            panel5.BackColor = Color.Gray;
            panel6.BackColor = Color.Gray;
            panel7.BackColor = Color.Gray;
            panel8.BackColor = Color.Gray;
        }

        private void startTimer1()
        {
            timer2.Stop();
            algorithmCount = 7;
            timer1.Start();

        }
        private void startTimer2()
        {
            timer1.Stop();
            algorithmNumber = 0;
            timer2.Start();
           
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            clearAllLed();

            Panel[] panels = new Panel[8] { panel1, panel2, panel3, panel4, panel5, panel6, panel7, panel8 };


            if (algorithmNumber < panels.Length)
            {
                panels[algorithmNumber].BackColor = Color.Red;
            }



            algorithmNumber++;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
            clearAllLed();

            Panel[] panels = new Panel[8] { panel1, panel2, panel3, panel4, panel5, panel6, panel7, panel8 };


            if (algorithmCount >= 0 & algorithmCount % 2 == 1)
            {
                panels[algorithmCount].BackColor = Color.Red;

            }
            if (algorithmCount >= 0 & algorithmCount % 2 == 0)
            {
                panels[algorithmCount].BackColor = Color.Red;

            }
            if (algorithmCount == 1)
            {
                algorithmCount = panels.Length;

            }

            algorithmCount = algorithmCount -2;

        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            char signalFromArduino = (char)serialPort1.ReadChar();
            if (signalFromArduino == '3')
            {
                clearAllLed();
                this.BeginInvoke(new ThreadStart(startTimer1));

            }
            if (signalFromArduino == '4')
            {
                clearAllLed();
                this.BeginInvoke(new ThreadStart(startTimer2));
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}