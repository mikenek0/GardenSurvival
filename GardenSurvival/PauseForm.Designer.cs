using System.Drawing;

partial class PauseForm
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
    
   }

   #region Windows Form Designer generated code

   /// <summary>
   /// Требуемый метод для поддержки конструктора — не изменяйте 
   /// содержимое этого метода с помощью редактора кода.
   /// </summary>
   private void InitializeComponent()
   {
         this.ContinueButton = new System.Windows.Forms.Button();
         this.ExitButton = new System.Windows.Forms.Button();
         this.Label = new System.Windows.Forms.Label();
         this.MainMenuButton = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // ContinueButton
         // 
         this.ContinueButton.BackColor = System.Drawing.Color.MediumBlue;
         this.ContinueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.ContinueButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
         this.ContinueButton.ForeColor = System.Drawing.Color.White;
         this.ContinueButton.Location = new System.Drawing.Point(61, 89);
         this.ContinueButton.Name = "ContinueButton";
         this.ContinueButton.Size = new System.Drawing.Size(238, 60);
         this.ContinueButton.TabIndex = 1;
         this.ContinueButton.Text = "Продолжить";
         this.ContinueButton.UseVisualStyleBackColor = false;
         this.ContinueButton.Click += new System.EventHandler(this.ContinueButton_Click);
         // 
         // ExitButton
         // 
         this.ExitButton.BackColor = System.Drawing.Color.Maroon;
         this.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.ExitButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
         this.ExitButton.ForeColor = System.Drawing.Color.White;
         this.ExitButton.Location = new System.Drawing.Point(61, 221);
         this.ExitButton.Name = "ExitButton";
         this.ExitButton.Size = new System.Drawing.Size(238, 60);
         this.ExitButton.TabIndex = 2;
         this.ExitButton.Text = "Выйти из игры";
         this.ExitButton.UseVisualStyleBackColor = false;
         this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
         // 
         // Label
         // 
         this.Label.Font = new System.Drawing.Font("Microsoft YaHei UI", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.Label.ForeColor = System.Drawing.SystemColors.ControlLightLight;
         this.Label.Location = new System.Drawing.Point(35, 34);
         this.Label.Name = "Label";
         this.Label.Size = new System.Drawing.Size(285, 27);
         this.Label.TabIndex = 0;
         this.Label.Text = "Игра приостановлена";
         this.Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // MainMenuButton
         // 
         this.MainMenuButton.BackColor = System.Drawing.Color.DarkSlateBlue;
         this.MainMenuButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.MainMenuButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
         this.MainMenuButton.ForeColor = System.Drawing.Color.White;
         this.MainMenuButton.Location = new System.Drawing.Point(61, 155);
         this.MainMenuButton.Name = "MainMenuButton";
         this.MainMenuButton.Size = new System.Drawing.Size(238, 60);
         this.MainMenuButton.TabIndex = 3;
         this.MainMenuButton.Text = "Выйти в главное меню";
         this.MainMenuButton.UseVisualStyleBackColor = false;
         this.MainMenuButton.Click += new System.EventHandler(this.MainMenuButton_Click);
         // 
         // PauseForm
         // 
         this.ClientSize = new System.Drawing.Size(362, 323);
         this.Controls.Add(this.MainMenuButton);
         this.Controls.Add(this.Label);
         this.Controls.Add(this.ContinueButton);
         this.Controls.Add(this.ExitButton);
         this.Name = "PauseForm";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Пауза";
         this.TopMost = true;
         this.ResumeLayout(false);

   }

   private System.Windows.Forms.Button ContinueButton;
   private System.Windows.Forms.Button ExitButton;
   private System.Windows.Forms.Label Label;

   #endregion

   private System.Windows.Forms.Button MainMenuButton;
}