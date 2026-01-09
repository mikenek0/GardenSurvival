using System;
using System.Windows.Forms;

namespace GardenSurvival
{
   internal static class Program
   {
      [STAThread]
      static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);

         while (true) // цикл для возврата в главное меню
         {
            using (var mainMenu = new MainMenuForm())
            {
               var result = mainMenu.ShowDialog();

               if (result == DialogResult.OK && mainMenu.ShouldStartGame)
               {
                  using (var gameForm = new Form1())
                  {
                     var gameResult = gameForm.ShowDialog();

                     if (gameResult == DialogResult.Abort)
                     {
                        continue;
                     }
                     else
                     {
                        break;
                     }
                  }
               }
               else
               {
                  break;
               }
            }
         }
      }
   }
}