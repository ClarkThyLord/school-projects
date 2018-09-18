using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace ICC___converter
{
    public partial class GUI : Form
    {
        public GUI()
        {
            InitializeComponent();
        }

        private void input(object sender, EventArgs e)
        {

        }

        private void file_open()
        {

        }

        private void file_chooser(object sender, EventArgs e)
        {
            if (this.file_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(this.file_dialog.FileName);
                
                this.file_name_gui.Text = sr.ReadToEnd();

                sr.Close();
            }
        }

        private void file_drag_enter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void file_drag_drop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files) {
                this.file_name_gui.Text = file.ToString();
            }
        }
    }
}
