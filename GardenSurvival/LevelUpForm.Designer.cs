partial class LevelUpForm
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
         this.Label = new System.Windows.Forms.Label();
         this.SuspendLayout();
         // 
         // Label
         // 
         this.Label.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
         this.Label.ForeColor = System.Drawing.Color.White;
         this.Label.Location = new System.Drawing.Point(20, 20);
         this.Label.Name = "Label";
         this.Label.Size = new System.Drawing.Size(360, 60);
         this.Label.TabIndex = 0;
         this.Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // LevelUpForm
         // 
         this.ClientSize = new System.Drawing.Size(403, 254);
         this.Controls.Add(this.Label);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "LevelUpForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Повышение уровня";
         this.TopMost = true;
         this.ResumeLayout(false);

   }

   private System.Windows.Forms.Label Label;

   #endregion
}