using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
//using WMPLib;

public partial class GameOverForm : Form
{
   public bool ShouldRestart { get; private set; } = false;
   public bool MainMenu { get; private set; } = false;
   public int PlayerLevel { get; set; } = 1;
   //private WindowsMediaPlayer _buttonSound;
   public GameOverForm()
   {
      InitializeComponent();
      
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.BackColor = Color.Black;
      this.Opacity = 0.85f;
      this.TopMost = true;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.ControlBox = false;
      this.Text = "";
      //_buttonSound = new WindowsMediaPlayer();
      //_buttonSound.URL = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "button.mp3");
      //_buttonSound.settings.volume = 40;
   }

   private void RestartButton_Click(object sender, EventArgs e)
   {
      //_buttonSound.controls.stop();
      //_buttonSound.controls.play();
      ShouldRestart = true;
      this.DialogResult = DialogResult.OK;
      this.Close();
   }

   private void ExitButton_Click(object sender, EventArgs e)
   {
      //_buttonSound.controls.stop();
      //_buttonSound.controls.play();
      this.DialogResult = DialogResult.Cancel;
      this.Close();
   }
   public void UpdateStats(int level, int MaxRecordLevel)
   {
      if (this.StatsLabel != null)
      {
         this.StatsLabel.Text = $"Уровень: {level}. Рекорд: {MaxRecordLevel}!";
      }
   }

   private void Label_Click(object sender, EventArgs e)
   {

   }

   private void MainMenuButton_Click(object sender, EventArgs e)
   {
      //_buttonSound.controls.stop();
      //_buttonSound.controls.play();
      MainMenu = true;
      this.DialogResult = DialogResult.OK;
      this.Close();
   }
}