using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiofriaSoundboard
{
    public partial class AboutMe : UserControl
    {
        public AboutMe()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        void OpenUrl(string url)
        {
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenUrl("https://youtube.com/@BarlowKeep?si=g9049dZaphdOxeaa");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            OpenUrl("https://ko-fi.com/barlowkeep");
        }
    }
}
