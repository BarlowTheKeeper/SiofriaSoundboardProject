using SiofriaSoundboard.Packages;
using SiofriaSoundboard.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiofriaSoundboard.Forms
{
    public partial class PkgMgrWnd : UserControl
    {
        private PackageManager pkgMgr;
        private Form1 parent = null; //ouch...

        public PkgMgrWnd(PackageManager packageMgr, Form1 f)
        {
            InitializeComponent();
            parent = f;
            pkgMgr = packageMgr;
            pkgMgr.GetPackageList().ForEach(pkg => listBox1.Items.Add(pkg));
        }

        private void Control_Load(object sender, EventArgs e)
        {
            ///pkgMgr.GetPackageList();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            pkgMgr.Import();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string packageToDelete = (string)listBox1.SelectedItem;
            DialogResult r = MessageBox.Show("Deleting package: " + packageToDelete, "Are you sure?", MessageBoxButtons.OKCancel);
            if (r == DialogResult.Cancel)
                return;

            if (!pkgMgr.DeletePackage(packageToDelete))
                return;

            parent.refreshDatagridAndPanels();
            listBox1.Items.Remove(packageToDelete);
            listBox1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pkgMgr.LoadPackageByName((string)listBox1.SelectedItem);
            parent.refreshDatagridAndPanels();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string src = (string)listBox1.SelectedItem;

            string dest = "";
            DialogResult r = InputBox.Show("Give a new name to that package!", ref dest);
            if (r == DialogResult.Cancel)
                return;

            pkgMgr.DuplicatePackage(src, dest);
            listBox1.Items.Clear();
            pkgMgr.GetPackageList().ForEach(pkg => listBox1.Items.Add(pkg));
            MessageBox.Show("Package copied successfully!");
        }
    }
}
