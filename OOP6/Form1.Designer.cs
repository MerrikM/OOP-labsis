namespace OOP6
{
    partial class Form
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_red = new System.Windows.Forms.Button();
            this.btn_green = new System.Windows.Forms.Button();
            this.btn_blu = new System.Windows.Forms.Button();
            this.btn_yellow = new System.Windows.Forms.Button();
            this.btn_black = new System.Windows.Forms.Button();
            this.btn_darkblu = new System.Windows.Forms.Button();
            this.btn_manycolor = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.picturbx = new System.Windows.Forms.PictureBox();
            this.btn_clr = new System.Windows.Forms.Button();
            this.btn_tri = new System.Windows.Forms.Button();
            this.btn_sqr = new System.Windows.Forms.Button();
            this.btn_crcle = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picturbx)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.btn_red);
            this.flowLayoutPanel2.Controls.Add(this.btn_green);
            this.flowLayoutPanel2.Controls.Add(this.btn_blu);
            this.flowLayoutPanel2.Controls.Add(this.btn_yellow);
            this.flowLayoutPanel2.Controls.Add(this.btn_black);
            this.flowLayoutPanel2.Controls.Add(this.btn_darkblu);
            this.flowLayoutPanel2.Controls.Add(this.btn_manycolor);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(533, 23);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(331, 63);
            this.flowLayoutPanel2.TabIndex = 7;
            // 
            // btn_red
            // 
            this.btn_red.BackColor = System.Drawing.Color.Red;
            this.btn_red.Location = new System.Drawing.Point(3, 3);
            this.btn_red.Name = "btn_red";
            this.btn_red.Size = new System.Drawing.Size(75, 23);
            this.btn_red.TabIndex = 0;
            this.btn_red.UseVisualStyleBackColor = false;
            this.btn_red.Click += new System.EventHandler(this.btn_red_Click);
            // 
            // btn_green
            // 
            this.btn_green.BackColor = System.Drawing.Color.Lime;
            this.btn_green.Location = new System.Drawing.Point(84, 3);
            this.btn_green.Name = "btn_green";
            this.btn_green.Size = new System.Drawing.Size(75, 23);
            this.btn_green.TabIndex = 2;
            this.btn_green.UseVisualStyleBackColor = false;
            this.btn_green.Click += new System.EventHandler(this.btn_red_Click);
            // 
            // btn_blu
            // 
            this.btn_blu.BackColor = System.Drawing.Color.Cyan;
            this.btn_blu.Location = new System.Drawing.Point(165, 3);
            this.btn_blu.Name = "btn_blu";
            this.btn_blu.Size = new System.Drawing.Size(75, 23);
            this.btn_blu.TabIndex = 1;
            this.btn_blu.UseVisualStyleBackColor = false;
            this.btn_blu.Click += new System.EventHandler(this.btn_red_Click);
            // 
            // btn_yellow
            // 
            this.btn_yellow.BackColor = System.Drawing.Color.Yellow;
            this.btn_yellow.Location = new System.Drawing.Point(246, 3);
            this.btn_yellow.Name = "btn_yellow";
            this.btn_yellow.Size = new System.Drawing.Size(75, 23);
            this.btn_yellow.TabIndex = 3;
            this.btn_yellow.UseVisualStyleBackColor = false;
            this.btn_yellow.Click += new System.EventHandler(this.btn_red_Click);
            // 
            // btn_black
            // 
            this.btn_black.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_black.Location = new System.Drawing.Point(3, 32);
            this.btn_black.Name = "btn_black";
            this.btn_black.Size = new System.Drawing.Size(75, 23);
            this.btn_black.TabIndex = 5;
            this.btn_black.UseVisualStyleBackColor = false;
            this.btn_black.Click += new System.EventHandler(this.btn_red_Click);
            // 
            // btn_darkblu
            // 
            this.btn_darkblu.BackColor = System.Drawing.Color.Blue;
            this.btn_darkblu.Location = new System.Drawing.Point(84, 32);
            this.btn_darkblu.Name = "btn_darkblu";
            this.btn_darkblu.Size = new System.Drawing.Size(75, 23);
            this.btn_darkblu.TabIndex = 6;
            this.btn_darkblu.UseVisualStyleBackColor = false;
            this.btn_darkblu.Click += new System.EventHandler(this.btn_red_Click);
            // 
            // btn_manycolor
            // 
            this.btn_manycolor.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.btn_manycolor.Location = new System.Drawing.Point(165, 32);
            this.btn_manycolor.Name = "btn_manycolor";
            this.btn_manycolor.Size = new System.Drawing.Size(156, 23);
            this.btn_manycolor.TabIndex = 4;
            this.btn_manycolor.Text = "***";
            this.btn_manycolor.UseVisualStyleBackColor = true;
            this.btn_manycolor.Click += new System.EventHandler(this.btn_manycolor_Click);
            // 
            // colorDialog1
            // 
            this.colorDialog1.FullOpen = true;
            // 
            // picturbx
            // 
            this.picturbx.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picturbx.Location = new System.Drawing.Point(1, 92);
            this.picturbx.Name = "picturbx";
            this.picturbx.Size = new System.Drawing.Size(1602, 755);
            this.picturbx.TabIndex = 8;
            this.picturbx.TabStop = false;
            this.picturbx.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.picturbx.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.picturbx.Resize += new System.EventHandler(this.picturbx_Resize);
            // 
            // btn_clr
            // 
            this.btn_clr.Location = new System.Drawing.Point(285, 3);
            this.btn_clr.Name = "btn_clr";
            this.btn_clr.Size = new System.Drawing.Size(202, 54);
            this.btn_clr.TabIndex = 2;
            this.btn_clr.Text = "Clear All";
            this.btn_clr.UseVisualStyleBackColor = true;
            this.btn_clr.Click += new System.EventHandler(this.btn_clr_Click);
            // 
            // btn_tri
            // 
            this.btn_tri.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_tri.Location = new System.Drawing.Point(189, 3);
            this.btn_tri.Name = "btn_tri";
            this.btn_tri.Size = new System.Drawing.Size(90, 54);
            this.btn_tri.TabIndex = 5;
            this.btn_tri.Text = "Triangle";
            this.btn_tri.UseVisualStyleBackColor = true;
            this.btn_tri.Click += new System.EventHandler(this.btn_tri_Click);
            // 
            // btn_sqr
            // 
            this.btn_sqr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_sqr.Location = new System.Drawing.Point(98, 3);
            this.btn_sqr.Name = "btn_sqr";
            this.btn_sqr.Size = new System.Drawing.Size(85, 54);
            this.btn_sqr.TabIndex = 4;
            this.btn_sqr.Text = "Square";
            this.btn_sqr.UseVisualStyleBackColor = true;
            this.btn_sqr.Click += new System.EventHandler(this.btn_sqr_Click);
            // 
            // btn_crcle
            // 
            this.btn_crcle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_crcle.Location = new System.Drawing.Point(3, 3);
            this.btn_crcle.Name = "btn_crcle";
            this.btn_crcle.Size = new System.Drawing.Size(89, 54);
            this.btn_crcle.TabIndex = 3;
            this.btn_crcle.Text = "Circle";
            this.btn_crcle.UseVisualStyleBackColor = true;
            this.btn_crcle.Click += new System.EventHandler(this.btn_crcle_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.flowLayoutPanel1.Controls.Add(this.btn_crcle);
            this.flowLayoutPanel1.Controls.Add(this.btn_sqr);
            this.flowLayoutPanel1.Controls.Add(this.btn_tri);
            this.flowLayoutPanel1.Controls.Add(this.btn_clr);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(25, 23);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(502, 63);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1602, 845);
            this.Controls.Add(this.picturbx);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "Form";
            this.Text = "Moi_Paint";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form_KeyUp);
            this.flowLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picturbx)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button btn_red;
        private System.Windows.Forms.Button btn_green;
        private System.Windows.Forms.Button btn_blu;
        private System.Windows.Forms.Button btn_yellow;
        private System.Windows.Forms.Button btn_manycolor;
        private System.Windows.Forms.Button btn_black;
        private System.Windows.Forms.Button btn_darkblu;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.PictureBox picturbx;
        private System.Windows.Forms.Button btn_clr;
        private System.Windows.Forms.Button btn_tri;
        private System.Windows.Forms.Button btn_sqr;
        private System.Windows.Forms.Button btn_crcle;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}

