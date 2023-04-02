using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;
using NAudio.Wave;


namespace mp3player
{
    public partial class Form1 : Form
    {
        private Mp3FileReader mp3Reader;
        private WaveOut waveOut;
        private SoundPlayer player;
        private string currentFile;

        double num1, num2, result;
        char op;
        bool isDecimal = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 Files (*.mp3)|*.mp3|All files (*.*)|*.*|WAV Files (*.wav)|*.wav";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (mp3Reader != null)
                    {
                        mp3Reader.Dispose();
                        mp3Reader = null;
                    }
                    if (waveOut != null)
                    {
                        //waveOut.Stop();
                        waveOut.Dispose();
                        waveOut = null;
                    }
                    if (player != null)
                    {
                        player.Dispose();
                        player = null;
                    }
                    currentFile = openFileDialog.FileName;
                    if(currentFile != null)
                    {
                        labelFileName.Text = Path.GetFileName(currentFile);

                        if (currentFile.ToLower().EndsWith(".wav"))
                        {
                            player = new SoundPlayer(currentFile);
                        }
                        else if (currentFile.ToLower().EndsWith(".mp3"))
                        { 
                            mp3Reader = new Mp3FileReader(openFileDialog.FileName);   
                        }
                    }
                   
                    //waveOut.Play();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not open file. Original error: " + ex.Message);
                }
            }
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            if (player != null)
            {
                //player = new SoundPlayer(currentFile);
                player.Play();
            }
            else if (mp3Reader != null)
            {
                waveOut = new WaveOut();
                waveOut.Init(mp3Reader);
                waveOut.Play();
            }
            
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (waveOut != null)
            {
                waveOut.Stop();
                //waveOut.Dispose();
                //waveOut = null;
            }
            if (player != null)
            {
                player.Stop();
            }
        }


        private void btnNumber_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (isDecimal)
            {
                if (txtDisplay.Text == "0")
                    txtDisplay.Text = btn.Text;
                else
                    txtDisplay.Text += btn.Text;
            }
            else
            {
                if (txtDisplay.Text == "0")
                    txtDisplay.Text = btn.Text;
                else
                    txtDisplay.Text += btn.Text;
            }
        }

        private void btnDecimal_Click(object sender, EventArgs e)
        {
            if (!isDecimal)
            {
                isDecimal = true;
                txtDisplay.Text += ".";
            }
        }

        private void btnOperator_Click(object sender, EventArgs e)
        {
          
            Button btn = (Button)sender;
            num1 = double.Parse(txtDisplay.Text);
            lblNum1.Text = num1.ToString();
            op = btn.Text[0];
            lblOp.Text = btn.Text;
            txtDisplay.Text = "0";
            lblNum2.Text = "0";
            isDecimal = false;
             if(op== '√')
            {
                result = Math.Sqrt(num1);
                txtDisplay.Text = result.ToString();
                isDecimal = false;
                num1 = result;
                num2 = 0;
            }
           
            
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            num2 = double.Parse(txtDisplay.Text);
            lblNum2.Text = num2.ToString();
            switch (op)
            {
                 
                case '+':
                    lblOp.Text = op.ToString();
                    result = num1 + num2;
                    break;
                case '-':
                    result = num1 - num2;
                    break;
                case '×':
                    result = num1 * num2;
                    break;
                case '÷':
                    result = num1 / num2;
                    break;
                case '^':
                    result = Math.Pow(num1, num2);
                    break;
               
                default:
                    lblOp.Text = op.ToString();
                    break;
            }

            txtDisplay.Text = result.ToString();
            isDecimal = false;
            num1 = result;
            num2 = 0;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            isDecimal = false;
            lblNum1.Text = "0";
            lblNum2.Text = "0";
            lblOp.Text = "+";
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            if (txtDisplay.Text.Length > 1)
                txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1);
            else
                txtDisplay.Text = "0";

            if (txtDisplay.Text.IndexOf(".") == -1)
                isDecimal = false;
        }
     
    }

}
