using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WMPLib;

public partial class PauseForm : Form
{
   public bool MainMenu { get; private set; } = false;
   private WindowsMediaPlayer _buttonSound;
   public PauseForm()
   {
      InitializeComponent();

      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.BackColor = Color.Black;
      this.Opacity = 1.00f;
      this.TopMost = true;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.ControlBox = false;
      this.Text = "";
      _buttonSound = new WindowsMediaPlayer();
      _buttonSound.URL = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "button.mp3");
      _buttonSound.settings.volume = 40;
   }

   private void ContinueButton_Click(object sender, EventArgs e)
   {
      _buttonSound.controls.stop();
      _buttonSound.controls.play();
      this.DialogResult = DialogResult.OK;
      this.Hide(); 
      this.Close(); 
   }

   private void ExitButton_Click(object sender, EventArgs e)
   {
      _buttonSound.controls.stop();
      _buttonSound.controls.play();
      this.DialogResult = DialogResult.Cancel;
      this.Close();
   }

   private void MainMenuButton_Click(object sender, EventArgs e)
   {
      _buttonSound.controls.stop();
      _buttonSound.controls.play();
      MainMenu = true;
      this.DialogResult = DialogResult.OK;
      this.Close();
   }
}