using System.Windows.Forms;

namespace GardenSurvival
{
   partial class Form1
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

      private void InitializeComponent()
      {
         this.SuspendLayout();

         // Form1
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
         this.Name = "Form1";
         this.Text = "Garden Survival";
         this.ResumeLayout(false);
      }
   }
}