partial class GameOverForm
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

   #region Windows Form Designer generated code

   /// <summary>
   /// Требуемый метод для поддержки конструктора — не изменяйте 
   /// содержимое этого метода с помощью редактора кода.
   /// </summary>
   private void InitializeComponent()
   {
         this.RestartButton = new System.Windows.Forms.Button();
         this.ExitButton = new System.Windows.Forms.Button();
         this.Label = new System.Windows.Forms.Label();
         this.StatsLabel = new System.Windows.Forms.Label();
         this.MainMenuButton = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // RestartButton
         // 
         this.RestartButton.BackColor = System.Drawing.Color.MediumBlue;
         this.RestartButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.RestartButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
         this.RestartButton.ForeColor = System.Drawing.Color.White;
         this.RestartButton.Location = new System.Drawing.Point(63, 94);
         this.RestartButton.Name = "RestartButton";
         this.RestartButton.Size = new System.Drawing.Size(238, 60);
         this.RestartButton.TabIndex = 2;
         this.RestartButton.Text = "Новая игра";
         this.RestartButton.UseVisualStyleBackColor = false;
         this.RestartButton.Click += new System.EventHandler(this.RestartButton_Click);
         // 
         // ExitButton
         // 
         this.ExitButton.BackColor = System.Drawing.Color.Maroon;
         this.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.ExitButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
         this.ExitButton.ForeColor = System.Drawing.Color.White;
         this.ExitButton.Location = new System.Drawing.Point(63, 226);
         this.ExitButton.Name = "ExitButton";
         this.ExitButton.Size = new System.Drawing.Size(238, 60);
         this.ExitButton.TabIndex = 3;
         this.ExitButton.Text = "Выйти из игры";
         this.ExitButton.UseVisualStyleBackColor = false;
         this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
         // 
         // Label
         // 
         this.Label.Font = new System.Drawing.Font("Microsoft YaHei UI", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.Label.ForeColor = System.Drawing.Color.Red;
         this.Label.Location = new System.Drawing.Point(63, 30);
         this.Label.Name = "Label";
         this.Label.Size = new System.Drawing.Size(238, 30);
         this.Label.TabIndex = 0;
         this.Label.Text = "Game Over!";
         this.Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         this.Label.Click += new System.EventHandler(this.Label_Click);
         // 
         // StatsLabel
         // 
         this.StatsLabel.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
         this.StatsLabel.ForeColor = System.Drawing.Color.White;
         this.StatsLabel.Location = new System.Drawing.Point(63, 61);
         this.StatsLabel.Name = "StatsLabel";
         this.StatsLabel.Size = new System.Drawing.Size(238, 30);
         this.StatsLabel.TabIndex = 1;
         this.StatsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         // 
         // MainMenuButton
         // 
         this.MainMenuButton.BackColor = System.Drawing.Color.SlateBlue;
         this.MainMenuButton.Cursor = System.Windows.Forms.Cursors.Default;
         this.MainMenuButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
         this.MainMenuButton.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
         this.MainMenuButton.ForeColor = System.Drawing.Color.White;
         this.MainMenuButton.Location = new System.Drawing.Point(63, 160);
         this.MainMenuButton.Name = "MainMenuButton";
         this.MainMenuButton.Size = new System.Drawing.Size(238, 60);
         this.MainMenuButton.TabIndex = 4;
         this.MainMenuButton.Text = "Выйти в главное меню";
         this.MainMenuButton.UseVisualStyleBackColor = false;
         this.MainMenuButton.Click += new System.EventHandler(this.MainMenuButton_Click);
         // 
         // GameOverForm
         // 
         this.ClientSize = new System.Drawing.Size(362, 323);
         this.Controls.Add(this.MainMenuButton);
         this.Controls.Add(this.Label);
         this.Controls.Add(this.StatsLabel);
         this.Controls.Add(this.RestartButton);
         this.Controls.Add(this.ExitButton);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "GameOverForm";
         this.ShowInTaskbar = false;
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Game Over";
         this.TopMost = true;
         this.ResumeLayout(false);

   }

   private System.Windows.Forms.Button RestartButton;
   private System.Windows.Forms.Button ExitButton;
   private System.Windows.Forms.Label Label;
   private System.Windows.Forms.Label StatsLabel; // новое поле

   #endregion

   private System.Windows.Forms.Button MainMenuButton;
}