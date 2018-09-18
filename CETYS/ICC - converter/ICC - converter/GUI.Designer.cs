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
            this.output_gui = new System.Windows.Forms.TextBox();
            this.convert_gui = new System.Windows.Forms.Button();
            this.progress_gui = new System.Windows.Forms.ProgressBar();
            this.file_dialog = new System.Windows.Forms.OpenFileDialog();
            this.file_gui = new System.Windows.Forms.Button();
            this.to_gui = new System.Windows.Forms.ComboBox();
            this.file_name_gui = new System.Windows.Forms.Label();
            this.table_middle_layout = new System.Windows.Forms.TableLayoutPanel();
            this.switch_gui = new System.Windows.Forms.Button();
            this.from_gui = new System.Windows.Forms.ComboBox();
            this.table_top_layout = new System.Windows.Forms.TableLayoutPanel();
            this.stats_gui = new System.Windows.Forms.Label();
            this.table_middle_layout.SuspendLayout();
            this.table_top_layout.SuspendLayout();
            this.SuspendLayout();
            // 
            // header
            // 
            this.header.AutoSize = true;
            this.header.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.header.ForeColor = System.Drawing.SystemColors.ControlText;
            this.header.Location = new System.Drawing.Point(3, 0);
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
            this.input_gui.Location = new System.Drawing.Point(3, 3);
            this.input_gui.MaxLength = 1000000;
            this.input_gui.Multiline = true;
            this.input_gui.Name = "input_gui";
            this.input_gui.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.input_gui.Size = new System.Drawing.Size(706, 129);
            this.input_gui.TabIndex = 4;
            this.input_gui.TextChanged += new System.EventHandler(this.input_change);
            // 
            // output_gui
            // 
            this.output_gui.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.output_gui.Location = new System.Drawing.Point(3, 138);
            this.output_gui.MaxLength = 1000000;
            this.output_gui.Multiline = true;
            this.output_gui.Name = "output_gui";
            this.output_gui.ReadOnly = true;
            this.output_gui.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.output_gui.Size = new System.Drawing.Size(706, 129);
            this.output_gui.TabIndex = 5;
            // 
            // convert_gui
            // 
            this.convert_gui.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.convert_gui.Location = new System.Drawing.Point(618, 320);
            this.convert_gui.MinimumSize = new System.Drawing.Size(0, 24);
            this.convert_gui.Name = "convert_gui";
            this.convert_gui.Size = new System.Drawing.Size(106, 25);
            this.convert_gui.TabIndex = 6;
            this.convert_gui.Text = "Convertir";
            this.convert_gui.UseVisualStyleBackColor = true;
            this.convert_gui.Click += new System.EventHandler(this.covert_manual);
            // 
            // progress_gui
            // 
            this.progress_gui.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progress_gui.Location = new System.Drawing.Point(12, 319);
            this.progress_gui.MinimumSize = new System.Drawing.Size(0, 24);
            this.progress_gui.Name = "progress_gui";
            this.progress_gui.Size = new System.Drawing.Size(286, 24);
            this.progress_gui.TabIndex = 7;
            // 
            // file_dialog
            // 
            this.file_dialog.FileName = "file_dialog";
            // 
            // file_gui
            // 
            this.file_gui.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.file_gui.Location = new System.Drawing.Point(538, 3);
            this.file_gui.Name = "file_gui";
            this.file_gui.Size = new System.Drawing.Size(171, 28);
            this.file_gui.TabIndex = 8;
            this.file_gui.Text = "Abrir documento...";
            this.file_gui.UseVisualStyleBackColor = true;
            this.file_gui.Click += new System.EventHandler(this.file_chooser);
            // 
            // to_gui
            // 
            this.to_gui.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.to_gui.FormattingEnabled = true;
            this.to_gui.Items.AddRange(new object[] {
            "Binary",
            "Decimal",
            "Hexadecimal"});
            this.to_gui.Location = new System.Drawing.Point(487, 319);
            this.to_gui.Name = "to_gui";
            this.to_gui.Size = new System.Drawing.Size(125, 24);
            this.to_gui.TabIndex = 9;
            this.to_gui.Text = "Convertir a...";
            this.to_gui.SelectedIndexChanged += new System.EventHandler(this.convert_to);
            // 
            // file_name_gui
            // 
            this.file_name_gui.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.file_name_gui.AutoSize = true;
            this.file_name_gui.BackColor = System.Drawing.SystemColors.Control;
            this.file_name_gui.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.file_name_gui.ForeColor = System.Drawing.SystemColors.ControlText;
            this.file_name_gui.Location = new System.Drawing.Point(270, 0);
            this.file_name_gui.MinimumSize = new System.Drawing.Size(100, 28);
            this.file_name_gui.Name = "file_name_gui";
            this.file_name_gui.Size = new System.Drawing.Size(262, 34);
            this.file_name_gui.TabIndex = 10;
            this.file_name_gui.Text = "(arrastrar y soltar un archivo)";
            this.file_name_gui.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // table_middle_layout
            // 
            this.table_middle_layout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.table_middle_layout.AutoSize = true;
            this.table_middle_layout.BackColor = System.Drawing.SystemColors.Control;
            this.table_middle_layout.ColumnCount = 1;
            this.table_middle_layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.table_middle_layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.table_middle_layout.Controls.Add(this.input_gui, 0, 0);
            this.table_middle_layout.Controls.Add(this.output_gui, 0, 1);
            this.table_middle_layout.Location = new System.Drawing.Point(12, 43);
            this.table_middle_layout.Name = "table_middle_layout";
            this.table_middle_layout.RowCount = 2;
            this.table_middle_layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.table_middle_layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.table_middle_layout.Size = new System.Drawing.Size(712, 270);
            this.table_middle_layout.TabIndex = 11;
            // 
            // switch_gui
            // 
            this.switch_gui.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.switch_gui.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.switch_gui.Location = new System.Drawing.Point(435, 311);
            this.switch_gui.Name = "switch_gui";
            this.switch_gui.Size = new System.Drawing.Size(46, 38);
            this.switch_gui.TabIndex = 12;
            this.switch_gui.Text = "⮂";
            this.switch_gui.UseVisualStyleBackColor = true;
            this.switch_gui.Click += new System.EventHandler(this.convert_switch);
            // 
            // from_gui
            // 
            this.from_gui.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.from_gui.FormattingEnabled = true;
            this.from_gui.Items.AddRange(new object[] {
            "Binary",
            "Decimal",
            "Hexadecimal"});
            this.from_gui.Location = new System.Drawing.Point(304, 320);
            this.from_gui.Name = "from_gui";
            this.from_gui.Size = new System.Drawing.Size(125, 24);
            this.from_gui.TabIndex = 13;
            this.from_gui.Text = "Convertir de...";
            this.from_gui.SelectedIndexChanged += new System.EventHandler(this.convert_from);
            // 
            // table_top_layout
            // 
            this.table_top_layout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.table_top_layout.AutoSize = true;
            this.table_top_layout.BackColor = System.Drawing.SystemColors.Control;
            this.table_top_layout.ColumnCount = 3;
            this.table_top_layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.table_top_layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table_top_layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.table_top_layout.Controls.Add(this.header, 0, 0);
            this.table_top_layout.Controls.Add(this.file_gui, 2, 0);
            this.table_top_layout.Controls.Add(this.file_name_gui, 1, 0);
            this.table_top_layout.Location = new System.Drawing.Point(12, 9);
            this.table_top_layout.Name = "table_top_layout";
            this.table_top_layout.RowCount = 1;
            this.table_top_layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table_top_layout.Size = new System.Drawing.Size(712, 34);
            this.table_top_layout.TabIndex = 14;
            // 
            // stats_gui
            // 
            this.stats_gui.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stats_gui.AutoSize = true;
            this.stats_gui.ForeColor = System.Drawing.SystemColors.ControlText;
            this.stats_gui.Location = new System.Drawing.Point(9, 352);
            this.stats_gui.Name = "stats_gui";
            this.stats_gui.Size = new System.Drawing.Size(613, 17);
            this.stats_gui.TabIndex = 15;
            this.stats_gui.Text = "Tamaño original: 12345 | Tamaño convertido: 12345 | Diferencia: 12345 | Diferenci" +
    "a %: 12345%";
            // 
            // GUI
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(732, 378);
            this.Controls.Add(this.stats_gui);
            this.Controls.Add(this.table_top_layout);
            this.Controls.Add(this.from_gui);
            this.Controls.Add(this.switch_gui);
            this.Controls.Add(this.table_middle_layout);
            this.Controls.Add(this.to_gui);
            this.Controls.Add(this.progress_gui);
            this.Controls.Add(this.convert_gui);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(750, 425);
            this.Name = "GUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Convertidor de Base";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.file_drag_drop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.file_drag_enter);
            this.table_middle_layout.ResumeLayout(false);
            this.table_middle_layout.PerformLayout();
            this.table_top_layout.ResumeLayout(false);
            this.table_top_layout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label header;
        private System.Windows.Forms.TextBox input_gui;
        private System.Windows.Forms.TextBox output_gui;
        private System.Windows.Forms.Button convert_gui;
        private System.Windows.Forms.ProgressBar progress_gui;
        private System.Windows.Forms.OpenFileDialog file_dialog;
        private System.Windows.Forms.Button file_gui;
        private System.Windows.Forms.ComboBox to_gui;
        private System.Windows.Forms.Label file_name_gui;
        private System.Windows.Forms.TableLayoutPanel table_middle_layout;
        private System.Windows.Forms.Button switch_gui;
        private System.Windows.Forms.ComboBox from_gui;
        private System.Windows.Forms.TableLayoutPanel table_top_layout;
        private System.Windows.Forms.Label stats_gui;
    }
}

