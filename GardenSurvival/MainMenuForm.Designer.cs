partial class MainMenuForm
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
      this.StartGameButton = new System.Windows.Forms.Button();
     
      this.ExitButton = new System.Windows.Forms.Button();
      this.TitleLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // TitleLabel
      // 
      this.TitleLabel.Font = new System.Drawing.Font("Arial", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.TitleLabel.ForeColor = System.Drawing.Color.White;
      this.TitleLabel.Location = new System.Drawing.Point(0, 100);
      this.TitleLabel.Size = new System.Drawing.Size(800, 80);
      this.TitleLabel.Text = "Garden Survival";
      this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // StartGameButton
      // 
      this.StartGameButton.Location = new System.Drawing.Point(0, 0); 
      this.StartGameButton.Size = new System.Drawing.Size(200, 50);
      this.StartGameButton.Text = "Начать игру";
      this.StartGameButton.UseVisualStyleBackColor = true;
      this.StartGameButton.BackColor = System.Drawing.Color.MediumBlue;
      this.StartGameButton.ForeColor = System.Drawing.Color.White;
      this.StartGameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.StartGameButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.StartGameButton.Click += new System.EventHandler(this.StartGameButton_Click);
      // 
      // ExitButton
      // 
      this.ExitButton.Location = new System.Drawing.Point(0, 0); 
      this.ExitButton.Size = new System.Drawing.Size(200, 50);
      this.ExitButton.Text = "Выйти из игры";
      this.ExitButton.UseVisualStyleBackColor = true;
      this.ExitButton.BackColor = System.Drawing.Color.Maroon;
      this.ExitButton.ForeColor = System.Drawing.Color.White;
      this.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.ExitButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
      // 
      // MainMenuForm
      // 
      this.ClientSize = new System.Drawing.Size(800, 600); 
      this.Controls.Add(this.TitleLabel);
      this.Controls.Add(this.StartGameButton);
      this.Controls.Add(this.ExitButton);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "MainMenuForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "Garden Survival - Главное меню";
      this.TopMost = true;
      this.Load += new System.EventHandler(this.MainMenuForm_Load);
      this.ResumeLayout(false);
   }

   private System.Windows.Forms.Button StartGameButton;
   private System.Windows.Forms.Button SettingsButton;
   private System.Windows.Forms.Button AboutButton;
   private System.Windows.Forms.Button ExitButton;
   private System.Windows.Forms.Label TitleLabel;

   #endregion
}