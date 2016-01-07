using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaceRecognition
{
    public partial class Setting : Form
    {
        public int inputLayerSize;
        public int OutPultLayerSize;
        public int[] HiddenLayers;
        public double NetworkLearningRate;
        public double StoppingError;
        public int OutPutPCA=0;
        public int NoOfIteration=0;
        public double PCALearningRate=0;
        public bool PCAflag = false;
        public Setting()
        {
            InitializeComponent();
            groupBox1.Enabled = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked==true)
            {
                groupBox1.Enabled = true;
                PCAflag = true;
            }
            else if(checkBox1.Checked==false)
            {
                groupBox1.Enabled = false;
                PCAflag = false;
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            string[] temp;
            inputLayerSize = Convert.ToInt32(textBox1.Text.ToString());
            OutPultLayerSize = Convert.ToInt32(textBox2.Text.ToString());
            NetworkLearningRate = Convert.ToDouble(textBox3.Text.ToString());
            StoppingError = Convert.ToDouble(textBox4.Text.ToString());
            temp = textBox5.Text.ToString().Split(',');
            HiddenLayers = new int[temp.Length];
            for (int j = 0; j < temp.Length; j++)
            {
                HiddenLayers[j] = Convert.ToInt32(temp[j]);
            }
            if (PCAflag)
            {
                OutPutPCA = Convert.ToInt32(textBox6.Text.ToString());
                PCALearningRate = Convert.ToDouble(textBox7.Text.ToString());
                NoOfIteration = Convert.ToInt32(textBox8.Text.ToString());
            }
            this.Close();
        }

        private void Setting_Load(object sender, EventArgs e)
        {

        }
    }
}
