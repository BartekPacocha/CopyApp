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

namespace CopyApp
{
    public partial class CopyApp : Form
    {
        public CopyApp()
        {
            InitializeComponent();
        }

        public int CreateDir(string path)
        {
            int status = 0;

            try
            {
                if (Directory.Exists(path))
                {
                    label1.Text = "Path already exist";
                    return 0;
                }

                DirectoryInfo di = Directory.CreateDirectory(path);
                label1.Text = "The directory was created successfully";
                status = 1;
            }
            catch
            {
                label1.Text = "created failed";
            }

            return status;
        }

        public void CopyFile(string sourceFile, string destFile)
        {
            try
            {
                System.IO.File.Copy(sourceFile, destFile, true);
            }
            catch(FileNotFoundException e)
            {
                label1.Text = e.ToString();
            }
        }

        public void DeleteOlder( string path )
        {
            DirectoryInfo di = new DirectoryInfo(path);
            if (di.CreationTime < DateTime.Now.AddDays(-2)) di.Delete();
        }

        private void Execute_Click(object sender, EventArgs e)
        {
            string fileName = @"ML00000000";
            string sourcePath = @"C:\Users\Cojman\Documents\KONAMI\Pro Evolution Soccer 2016\save";
            string targetPath = @"F:\pes16\";
            string path = targetPath + DateTime.Now.ToString("yyyy-MM-dd-HHmm");

            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
            string destFile = System.IO.Path.Combine(path, fileName);

            tbPath.Text = path;
            if (CreateDir(path) > 0)
            {
                CopyFile(sourceFile, destFile);
                label1.Visible = true;
            }
            btOpenFolder.Visible = true;
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            string path = @"F:\pes16\";
            DeleteOlder(path);
        }

        private void btOpenFolder_Click(object sender, EventArgs e)
        {
            string path = tbPath.Text;
            System.Diagnostics.Process.Start(path);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(1);
        }

        private void cleanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Action<Control.ControlCollection> func = null;
            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else
                        func(control.Controls);
            };
            func(Controls);

            label1.Text = null;
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
