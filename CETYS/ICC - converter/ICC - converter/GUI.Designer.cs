namespace ICC___converter
{
    partial class GUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI));
            this.header = new System.Windows.Forms.Label();
            this.input_gui = new System.Windows.Forms.TextBox();
            this.result_gui = new System.Windows.Forms.TextBox();
            this.convertir_gui = new System.Windows.Forms.Button();
            this.progress_gui = new System.Windows.Forms.ProgressBar();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.file_gui = new System.Windows.Forms.Button();
            this.type_gui = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // header
            // 
            this.header.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.header.AutoSize = true;
            this.header.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.header.Location = new System.Drawing.Point(12, 9);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(261, 31);
            this.header.TabIndex = 3;
            this.header.Text = "Convertidor de Base";
            // 
            // input_gui
            // 
            this.input_gui.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.input_gui.Location = new System.Drawing.Point(18, 43);
            this.input_gui.Multiline = true;
            this.input_gui.Name = "input_gui";
            this.input_gui.Size = new System.Drawing.Size(700, 125);
            this.input_gui.TabIndex = 4;
            // 
            // result_gui
            // 
            this.result_gui.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.result_gui.Location = new System.Drawing.Point(18, 174);
            this.result_gui.Multiline = true;
            this.result_gui.Name = "result_gui";
            this.result_gui.Size = new System.Drawing.Size(700, 125);
            this.result_gui.TabIndex = 5;
            // 
            // convertir_gui
            // 
            this.convertir_gui.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.convertir_gui.Location = new System.Drawing.Point(618, 306);
            this.convertir_gui.Name = "convertir_gui";
            this.convertir_gui.Size = new System.Drawing.Size(100, 35);
            this.convertir_gui.TabIndex = 6;
            this.convertir_gui.Text = "Convertir";
            this.convertir_gui.UseVisualStyleBackColor = true;
            // 
            // progress_gui
            // 
            this.progress_gui.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progress_gui.Location = new System.Drawing.Point(18, 306);
            this.progress_gui.Name = "progress_gui";
            this.progress_gui.Size = new System.Drawing.Size(392, 35);
            this.progress_gui.TabIndex = 7;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // file_gui
            // 
            this.file_gui.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.file_gui.Location = new System.Drawing.Point(643, 9);
            this.file_gui.Name = "file_gui";
            this.file_gui.Size = new System.Drawing.Size(75, 28);
            this.file_gui.TabIndex = 8;
            this.file_gui.Text = "Archivo...";
            this.file_gui.UseVisualStyleBackColor = true;
            // 
            // type_gui
            // 
            this.type_gui.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.type_gui.FormattingEnabled = true;
            this.type_gui.Items.AddRange(new object[] {
            "Binary",
            "Decimal",
            "Hexadecimal"});
            this.type_gui.Location = new System.Drawing.Point(416, 312);
            this.type_gui.Name = "type_gui";
            this.type_gui.Size = new System.Drawing.Size(196, 24);
            this.type_gui.TabIndex = 9;
            // 
            // GUI
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(732, 353);
            this.Controls.Add(this.type_gui);
            this.Controls.Add(this.file_gui);
            this.Controls.Add(this.progress_gui);
            this.Controls.Add(this.convertir_gui);
            this.Controls.Add(this.result_gui);
            this.Controls.Add(this.input_gui);
            this.Controls.Add(this.header);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "GUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Convertidor de Base";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label header;
        private System.Windows.Forms.TextBox input_gui;
        private System.Windows.Forms.TextBox result_gui;
        private System.Windows.Forms.Button convertir_gui;
        private System.Windows.Forms.ProgressBar progress_gui;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button file_gui;
        private System.Windows.Forms.ComboBox type_gui;
    }
}

