using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
//using WMPLib;

public partial class LevelUpForm : Form
{
   public string SelectedOption { get; private set; } = "continue";
   //private WindowsMediaPlayer _buttonSound;
   public LevelUpForm(int newLevel, List<string> options)
   {
      InitializeComponent();

      this.Label.Text = $"Уровень {newLevel}!\nВыберите улучшение:";
      this.Label.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.BackColor = Color.Black;
      this.TopMost = true;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.ControlBox = false;
      this.Text = "";
      //_buttonSound = new WindowsMediaPlayer();
      //_buttonSound.URL = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "button.mp3");
      //_buttonSound.settings.volume = 40;
      int yPos = 100;
      foreach (var option in options)
      {
         var button = new Button
         {
            Text = option,
            Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204))),
            Location = new Point(50, yPos),
            Size = new Size(300, 40),
            BackColor = Color.FromArgb(60, 120, 255),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
         };

         var opt = option; 
         button.Click += (s, e) =>
         {
            //_buttonSound.controls.stop();
            //_buttonSound.controls.play();
            SelectedOption = opt;
            this.DialogResult = DialogResult.OK;
            this.Close();
         };

         this.Controls.Add(button);
         yPos += 50;
      }
   }
}