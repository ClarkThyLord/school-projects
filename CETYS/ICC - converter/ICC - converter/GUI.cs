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
        
        private void convert()
        {
            if (this.from_gui.SelectedText == this.to_gui.SelectedText)
            {
                this.output_gui.Text = this.input_gui.Text;
            } else
            {
                ICC___converter.scripts.base_converter.run(this.input_gui.Text, this.from_gui.SelectedText, this.to_gui.SelectedText);
            }

            double diffrence = this.input_gui.Text.Length - this.output_gui.Text.Length;

            string diffrence_sign = "";
            if (diffrence > 0)
            {
                diffrence_sign = "-";
            } else if (diffrence < 0)
            {
                diffrence_sign = "+";
            }

            this.stats_gui.Text = String.Format("Tamaño original: {0} | Tamaño convertido: {1} | Diferencia: {2} : {3}%", this.input_gui.Text.Length, this.output_gui.Text.Length, diffrence_sign + diffrence.ToString(), diffrence_sign + ((diffrence / (this.input_gui.Text.Length != 0 ? this.input_gui.Text.Length + this.output_gui.Text.Length : 1)) * 100).ToString());
        }

        private void input_change(object sender, EventArgs e)
        {
            convert();
        }

        private void file_open(string content)
        {
            this.input_gui.Text = content;

            convert();
        }

        private void file_chooser(object sender, EventArgs e)
        {
            if (this.file_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.file_name_gui.Text = System.IO.Path.GetFullPath(this.file_dialog.FileName);
                
                StringBuilder content = new StringBuilder();
                byte[] file_bytes = File.ReadAllBytes(System.IO.Path.GetFullPath(this.file_dialog.FileName));

                int progress = 0;
                foreach (byte file_byte in file_bytes)
                {
                    content.Append(Convert.ToString(file_byte, 2).PadLeft(8, '0'));

                    progress += 1;
                    this.progress_gui.Value = (int)Math.Round((double)((progress / file_bytes.Length) * 100), 0);
                }

                this.input_gui.Text = content.ToString();
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
            if (files.Length > 0) {
                string file_path = files[0].ToString();
                this.file_name_gui.Text = file_path;
                
                StringBuilder content = new StringBuilder();
                byte[] file_bytes = File.ReadAllBytes(file_path);

                int progress = 0;
                foreach (byte file_byte in file_bytes)
                {
                    content.Append(Convert.ToString(file_byte, 2).PadLeft(8, '0'));

                    progress += 1;
                    this.progress_gui.Value = (int)Math.Round((double)((progress / file_bytes.Length) * 100), 0);
                }

                this.input_gui.Text = content.ToString();
            } else
            {
                MessageBox.Show("¡El archivo no puede ser cargado!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void convert_from(object sender, EventArgs e)
        {
            convert();
        }

        private void convert_switch(object sender, EventArgs e)
        {
            int temp = (int)this.from_gui.SelectedIndex;
            this.from_gui.SelectedIndex = this.to_gui.SelectedIndex;
            this.to_gui.SelectedIndex = temp;

            convert();
        }

        private void convert_to(object sender, EventArgs e)
        {
            convert();
        }

        private void covert_manual(object sender, EventArgs e)
        {
            convert();
        }
    }
}
