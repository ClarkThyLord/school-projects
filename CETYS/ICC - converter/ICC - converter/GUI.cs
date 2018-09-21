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
            if (this.input_gui.Text.Length == 0 || this.from_gui.SelectedIndex == -1 || this.to_gui.SelectedIndex == -1 || this.from_gui.SelectedIndex == this.to_gui.SelectedIndex)
            {
                this.output_gui.Text = this.input_gui.Text;
            } else
            {
                string result = ICC___converter.scripts.base_converter.run(this.input_gui.Text, this.from_gui.Items[this.from_gui.SelectedIndex].ToString(), this.to_gui.Items[this.to_gui.SelectedIndex].ToString());

                if (result == "")
                {
                    MessageBox.Show(string.Format("¡Error de análisis al convertir {0} a {1}!", this.from_gui.Text, this.to_gui.Text), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    this.stats_gui.Text = "Tamaño original: 0 | Tamaño convertido: 0 | Diferencia: 0 : 0%";
                }
                else
                {
                    this.output_gui.Text = result;
                }
            }

            stats();
        }

        private void stats()
        {
            double diffrence = this.input_gui.Text.Length - this.output_gui.Text.Length;

            string diffrence_sign = "";
            if (diffrence > 0)
            {
                diffrence_sign = "-";
            }
            else if (diffrence < 0)
            {
                diffrence_sign = "+";
            }

            this.stats_gui.Text = String.Format("Tamaño original: {0} | Tamaño convertido: {1} | Diferencia: {2} : {3}%", this.input_gui.Text.Length, this.output_gui.Text.Length, diffrence_sign + diffrence.ToString(), diffrence_sign + Math.Round((diffrence / (this.input_gui.Text.Length != 0 ? this.input_gui.Text.Length + this.output_gui.Text.Length : 1)) * 100, 2).ToString());
        }

        private void input_change(object sender, EventArgs e)
        {
            if (((TextBox)sender).ContainsFocus)
            {
                convert();
            }
        }

        private void file_save(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.file_save_dialog.ShowDialog() == DialogResult.OK)
            {
                if (this.file_save_dialog.FileName != "")
                {
                    StreamWriter fs = new StreamWriter(this.file_save_dialog.OpenFile());
                    
                    fs.WriteLine(this.output_gui.Text);

                    fs.Dispose();
                    fs.Close();
                } else
                {
                    MessageBox.Show("¡Necesitas dar un nombre de archivo válido!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void file_open(string content)
        {
            this.input_gui.Text = content;

            convert();
        }

        private void file_chooser(object sender, EventArgs e)
        {
            if (this.file_open_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.file_name_gui.Text = System.IO.Path.GetFullPath(this.file_open_dialog.FileName);
                
                StringBuilder content = new StringBuilder();
                if (this.file_open_dialog.FilterIndex == 1)
                {
                    int progress = 0;
                    byte[] file_bytes = File.ReadAllBytes(System.IO.Path.GetFullPath(this.file_open_dialog.FileName)); 
                    foreach (byte file_byte in file_bytes)
                    {
                        content.Append(Convert.ToString(file_byte, 2).PadLeft(8, '0'));

                        if (progress > 1000000)
                        {
                            MessageBox.Show("¡El archivo es demasiado grande!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        progress += 1;
                        this.progress_gui.Value = (int)Math.Round((double)((progress / file_bytes.Length) * 100), 0);
                    }
                } else
                {
                    content.Insert(0, File.ReadAllText(System.IO.Path.GetFullPath(this.file_open_dialog.FileName)));
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

                    if (progress > 1000000)
                    {
                        MessageBox.Show("¡El archivo es demasiado grande!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

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
            if (((ComboBox)sender).ContainsFocus)
            {
                convert();
            }
        }

        private void convert_switch(object sender, EventArgs e)
        {
            int temp_index = this.from_gui.SelectedIndex;
            this.from_gui.SelectedIndex = this.to_gui.SelectedIndex;
            this.to_gui.SelectedIndex = temp_index;

            string temp_input = string.Copy(this.input_gui.Text);
            this.input_gui.Text = this.output_gui.Text;
            this.output_gui.Text = temp_input;

            stats();
        }

        private void convert_to(object sender, EventArgs e)
        {
            if (((ComboBox)sender).ContainsFocus)
            {
                convert();
            }
        }

        private void covert_manual(object sender, EventArgs e)
        {
            if (((Button)sender).ContainsFocus)
            {
                convert();
            }
        }
    }
}
