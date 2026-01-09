using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
//using WMPLib;

public partial class MainMenuForm : Form
{
   //private WindowsMediaPlayer _buttonSound;
   //private WindowsMediaPlayer _menuMusic;

   public bool ShouldStartGame { get; private set; } = false;
   public MainMenuForm()
   {
      InitializeComponent();
      this.WindowState = FormWindowState.Maximized;
      this.BackColor = Color.Black;
      this.TopMost = true;

      //_buttonSound = new WindowsMediaPlayer();
      //_buttonSound.URL = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "button.mp3");
      //_buttonSound.settings.volume = 40;
      //_buttonSound.controls.stop();
      //_menuMusic = new WindowsMediaPlayer();
      //_menuMusic.URL = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "main_menu.mp3");
      //_menuMusic.settings.volume = 60;
      //_menuMusic.settings.setMode("loop", true); 

      //_menuMusic.controls.play();
   }

   private void MainMenuForm_Load(object sender, EventArgs e)
   {
      CenterUIElements();
   }


   private void CenterUIElements()
   {
      int centerX = this.ClientSize.Width / 2;
      int centerY = this.ClientSize.Height / 2;

      this.TitleLabel.Location = new Point(centerX - 200, centerY - 150);
      this.TitleLabel.Size = new Size(400, 80);

      this.StartGameButton.Location = new Point(centerX - 100, centerY - 30);
      this.StartGameButton.Size = new Size(200, 50);

      this.ExitButton.Location = new Point(centerX - 100, centerY + 50);
      this.ExitButton.Size = new Size(200, 50);
   }

   private void StartGameButton_Click(object sender, EventArgs e)
   {
      //_buttonSound.controls.stop();
      //_buttonSound.controls.play();
      //_menuMusic.controls.stop();

      ShouldStartGame = true;
      this.DialogResult = DialogResult.OK;
      this.Close();
   }

   private void ExitButton_Click(object sender, EventArgs e)
   {
      //_buttonSound.controls.stop();
      //_buttonSound.controls.play();
      //_menuMusic.controls.stop();

      this.DialogResult = DialogResult.Cancel;
      this.Close();
   }

   protected override void OnFormClosing(FormClosingEventArgs e)
   {
      base.OnFormClosing(e);
      //_menuMusic.controls.stop();
      //_menuMusic.close();
   }
   
}