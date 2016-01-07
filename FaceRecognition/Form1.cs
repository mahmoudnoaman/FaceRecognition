using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace FaceRecognition
{
    public partial class Form1 : Form
    {
        ReadImages TrainingImages = new ReadImages();
        Setting S = new Setting();
       
        public Form1()
        {
            InitializeComponent();
        }

        private void Load_Click(object sender, EventArgs e)
        {
            string Path;
            int X = 0;
            if(OpenFolder.ShowDialog()==DialogResult.OK)
            {
                int Count = 0;
                Path=OpenFolder.SelectedPath;
                TrainingImages.ListAllFiles(Path);
                for(int j=0;j<TrainingImages.Table.Count;j++)
                {

                    X = j / 8;
                    label2.Text = (X + 1).ToString();
                    this.Refresh();
                    this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox1.Image = new Bitmap(TrainingImages.Table[j]);
                    var stopwatch = Stopwatch.StartNew();
                    stopwatch = Stopwatch.StartNew();
                    System.Threading.Thread.Sleep(500);
                    stopwatch.Stop();
                    pictureBox1.Refresh();
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void test_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                dlg.Filter = "bmp files (*.JPG)|*.JPG";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // Create a new Bitmap object from the picture file on disk,
                    // and assign that to the PictureBox.Image property
                    this.pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox2.Image = new Bitmap(dlg.FileName);
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            S.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
