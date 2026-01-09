using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
//using WMPLib;

namespace GardenSurvival
{
   public partial class StartingWeaponForm : Form
   {
      public string SelectedWeapon { get; private set; } = "Garlic"; 
      //private WindowsMediaPlayer _buttonSound;
      public StartingWeaponForm()
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

      private void btnGarlic_Click(object sender, EventArgs e)
      {
         //_buttonSound.controls.stop();
         //_buttonSound.controls.play();
         SelectedWeapon = "Garlic";
         this.DialogResult = DialogResult.OK;
         this.Close();
      }

      private void btnWhip_Click(object sender, EventArgs e)
      {
         //_buttonSound.controls.stop();
         //_buttonSound.controls.play();
         SelectedWeapon = "Whip";
         this.DialogResult = DialogResult.OK;
         this.Close();
      }

      private void StartingWeaponForm_Load(object sender, EventArgs e)
      {
         this.StartPosition = FormStartPosition.CenterScreen;
      }
   }
}