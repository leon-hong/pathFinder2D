
namespace pathFinder2D
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPathFind = new System.Windows.Forms.Button();
            this.pnlMap = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btnPathFind
            // 
            this.btnPathFind.Location = new System.Drawing.Point(545, 233);
            this.btnPathFind.Name = "btnPathFind";
            this.btnPathFind.Size = new System.Drawing.Size(107, 38);
            this.btnPathFind.TabIndex = 0;
            this.btnPathFind.Text = "길찾기";
            this.btnPathFind.UseVisualStyleBackColor = true;
            this.btnPathFind.Click += new System.EventHandler(this.btnPathFind_Click);
            // 
            // pnlMap
            // 
            this.pnlMap.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlMap.Location = new System.Drawing.Point(15, 14);
            this.pnlMap.Name = "pnlMap";
            this.pnlMap.Size = new System.Drawing.Size(512, 512);
            this.pnlMap.TabIndex = 1;
            this.pnlMap.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMap_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 538);
            this.Controls.Add(this.pnlMap);
            this.Controls.Add(this.btnPathFind);
            this.Name = "Form1";
            this.Text = "2차원에서 길찾기";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPathFind;
        private System.Windows.Forms.Panel pnlMap;
    }
}

