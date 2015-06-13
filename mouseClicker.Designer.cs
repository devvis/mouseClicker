namespace mouseClicker
{
    partial class mainWnd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainWnd));
            this.mouzh4ckz = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mouzh4ckz
            // 
            this.mouzh4ckz.AutoSize = true;
            this.mouzh4ckz.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mouzh4ckz.Location = new System.Drawing.Point(2, 9);
            this.mouzh4ckz.Name = "mouzh4ckz";
            this.mouzh4ckz.Size = new System.Drawing.Size(133, 14);
            this.mouzh4ckz.TabIndex = 0;
            this.mouzh4ckz.Text = "Press F1 for help.";
            // 
            // mainWnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(139, 31);
            this.Controls.Add(this.mouzh4ckz);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "mainWnd";
            this.Opacity = 0.8D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mouse Clicker v0.4";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mouzh4ckz;
    }
}

