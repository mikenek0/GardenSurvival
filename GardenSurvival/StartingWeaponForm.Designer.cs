namespace GardenSurvival
{
   partial class StartingWeaponForm
   {
      private System.ComponentModel.IContainer components = null;

      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      private void InitializeComponent()
      {
         this.lblTitle = new System.Windows.Forms.Label();
         this.btnGarlic = new System.Windows.Forms.Button();
         this.btnWhip = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // lblTitle
         // 
         this.lblTitle.AutoSize = true;
         this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
         this.lblTitle.ForeColor = System.Drawing.Color.White;
         this.lblTitle.Location = new System.Drawing.Point(33, 53);
         this.lblTitle.Name = "lblTitle";
         this.lblTitle.Size = new System.Drawing.Size(430, 38);
         this.lblTitle.TabIndex = 0;
         this.lblTitle.Text = "Выберите начальное оружие";
         this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // btnGarlic
         // 
         this.btnGarlic.BackColor = System.Drawing.Color.SlateBlue;
         this.btnGarlic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.btnGarlic.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
         this.btnGarlic.ForeColor = System.Drawing.Color.White;
         this.btnGarlic.Location = new System.Drawing.Point(159, 148);
         this.btnGarlic.Name = "btnGarlic";
         this.btnGarlic.Size = new System.Drawing.Size(180, 53);
         this.btnGarlic.TabIndex = 1;
         this.btnGarlic.Text = "Реппелент";
         this.btnGarlic.UseVisualStyleBackColor = false;
         this.btnGarlic.Click += new System.EventHandler(this.btnGarlic_Click);
         // 
         // btnWhip
         // 
         this.btnWhip.BackColor = System.Drawing.Color.DarkSlateGray;
         this.btnWhip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.btnWhip.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
         this.btnWhip.ForeColor = System.Drawing.Color.White;
         this.btnWhip.Location = new System.Drawing.Point(159, 219);
         this.btnWhip.Name = "btnWhip";
         this.btnWhip.Size = new System.Drawing.Size(180, 53);
         this.btnWhip.TabIndex = 2;
         this.btnWhip.Text = "Мухобойка";
         this.btnWhip.UseVisualStyleBackColor = false;
         this.btnWhip.Click += new System.EventHandler(this.btnWhip_Click);
         // 
         // StartingWeaponForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.BackColor = System.Drawing.Color.Black;
         this.ClientSize = new System.Drawing.Size(498, 358);
         this.Controls.Add(this.btnWhip);
         this.Controls.Add(this.btnGarlic);
         this.Controls.Add(this.lblTitle);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
         this.Name = "StartingWeaponForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Load += new System.EventHandler(this.StartingWeaponForm_Load);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label lblTitle;
      private System.Windows.Forms.Button btnGarlic;
      private System.Windows.Forms.Button btnWhip;
   }
}