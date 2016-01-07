using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
namespace FaceRecognition
{
    public class ReadImages
    {
        public List<string> Table;
        int NoOfImages=0;
        bool[] CheckImages;
        public void ListAllFiles(string path)
        {
            Table = new List<string>();
            string filepath = path;
            DirectoryInfo d = new DirectoryInfo(filepath);
            foreach (var file in d.GetFiles("*.*", SearchOption.AllDirectories))
            {
                Table.Add(file.FullName.ToString());
            }
            NoOfImages = Table.Count;
            CheckImages = new bool[NoOfImages];
            for(int j=0;j<NoOfImages;j++)
            {
                CheckImages[j] = false;
            }
        }
        public string GetImage()
        {
            NoOfImages = Table.Count;
            Random random = new Random();
            int RandomIndex;
            bool flag=false;
            while(!flag)
            {
                RandomIndex = random.Next(0, NoOfImages);
                if(CheckImages[RandomIndex]==false)
                {
                    CheckImages[RandomIndex] = true;
                    flag = true;
                    return Table[RandomIndex];
                }
            }
            return null;
        }
    }
}
