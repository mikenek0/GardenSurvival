using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

//using System.IO;
//using System.Media;
//using System.Threading.Tasks;
//using WMPLib; 
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace GardenSurvival
{
   public partial class Form1 : Form
   {
      [DllImport("GameRender.dll", CallingConvention = CallingConvention.Cdecl)]
      public static extern void InitTileMap(int width, int height);

      [DllImport("GameRender.dll", CallingConvention = CallingConvention.Cdecl)]
      public static extern void FreeTileMap();
      [DllImport("GameRender.dll", CallingConvention = CallingConvention.Cdecl)]
      public static extern bool InitOpenGL(IntPtr hwnd);

      [DllImport("GameRender.dll", CallingConvention = CallingConvention.Cdecl)]
      public static extern void ResizeOpenGL(int width, int height);


      [DllImport("GameRender.dll", CallingConvention = CallingConvention.Cdecl)]
      public static extern void ShutdownOpenGL();

      [StructLayout(LayoutKind.Sequential)]
      public struct GameObject
      {
         public float x;
         public float y;
         public int Id;
      }
      [StructLayout(LayoutKind.Sequential)]
      public struct WeaponObject
      {
         public float x;
         public float y;
         public float radius;
         public int type;        
         public float angle;    
         public float life;      
      }

      [StructLayout(LayoutKind.Sequential)]
      public struct ExpOrbObject
      {
         public float x;
         public float y;
         public int value;
      }

      [StructLayout(LayoutKind.Sequential)]
      public struct TextObject
      {
         public float x;
         public float y;
         [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
         public string text;
         public int fontSize;
         public float r, g, b;
      }

      [DllImport("GameRender.dll", CallingConvention = CallingConvention.Cdecl)]
      public static extern void RenderFrame(
          float playerX, float playerY,bool IsDamaged,
          [In] GameObject[] enemies, int enemyCount,
          [In] WeaponObject[] weapons, int weaponCount,
          [In] ExpOrbObject[] orbs, int orbCount,
          [In] TextObject[] texts, int textCount, 
          int screenWidth, int screenHeight);
      private DateTime _gameStartTime = DateTime.Now;
      
      private Player _player = new();
      private List<Enemy> _activeEnemies = new();
      private Random _random = new();
    
      private const int EnemiesPerSpawn = 2;
      private int _currentWave = 1;
      private float _waveTimer = WaveInterval;
      private const float WaveInterval = 20f; 

      private Timer _gameTimer;
      private DateTime _lastUpdate = DateTime.Now;

      private Panel _healthBarBackground; 
      private Panel _healthBarFill;      
      private float _prevPlayerX;
      private float _prevPlayerY;

      private Panel _expBarBackground; 
      private Panel _expBarFill;      
      private int _enemiesKilled = 0; 
      //private WindowsMediaPlayer _damageSound;
      //private WindowsMediaPlayer _deathSound;
      //private WindowsMediaPlayer _enemyDeathSound;
      //private WindowsMediaPlayer _levelUpSound;
      //private WindowsMediaPlayer _buttonSound;
      //private WindowsMediaPlayer _musicPlayer;
      //private string[] _gameTracks = { "Game1.mp3", "Game2.mp3", "Game3.mp3" };
      //private Random _musicRandom = new Random();
      //private bool _deathSoundPlayed = false;
      public class ExpOrb
      {
         public float X { get; set; }
         public float Y { get; set; }
         public float Radius { get; set; } = 8f; 
         public int Value { get; set; } = 1; 
      }

      private List<ExpOrb> _expOrbs = new();
      private Dictionary<Keys, bool> _keyStates = new Dictionary<Keys, bool>
        {
            { Keys.W, false },
            { Keys.A, false },
            { Keys.S, false },
            { Keys.D, false },
            { Keys.Up, false },
            { Keys.Down, false },
            { Keys.Left, false },
            { Keys.Right, false }
        };
      private bool _isPaused = false;
      
      private void Form1_Load(object sender, EventArgs e)
      {
         ShowStartingWeaponSelection();
      }
      public Form1()
      {
         _gameStartTime = DateTime.Now;
         InitializeComponent();
         this.Load += Form1_Load;
         this.FormBorderStyle = FormBorderStyle.None;
         this.WindowState = FormWindowState.Normal;
         this.Size = Screen.PrimaryScreen.Bounds.Size;
         this.Location = Point.Empty; 
         this.Text = "Garden Survival";

         _keyStates[Keys.W] = false;
         _keyStates[Keys.A] = false;
         _keyStates[Keys.S] = false;
         _keyStates[Keys.D] = false;
         _keyStates[Keys.Left] = false;
         _keyStates[Keys.Right] = false;
         _keyStates[Keys.Up] = false;
         _keyStates[Keys.Down] = false;
         _keyStates[Keys.Escape] = false;
         _keyStates[Keys.R] = false;

         if (!InitOpenGL(this.Handle))
         {
            MessageBox.Show("Ошибка инициализации OpenGL!");
            this.Close();
            return;
         }
         ResizeOpenGL(this.ClientSize.Width, this.ClientSize.Height);
         InitializeUI();
         _gameTimer = new Timer
         {
            Interval = 16 
         };
         _gameTimer.Tick += GameTimer_Tick;
         _gameTimer.Start();

         this.KeyPreview = true;
         this.KeyDown += Form1_KeyDown;
         this.KeyUp += Form1_KeyUp;

         _player.X = 50 * 64 / 2f;
         _player.Y = 50*64 / 2f;
         _prevPlayerX = _player.X;
         _prevPlayerY = _player.Y;
         InitTileMap(50, 50);
         //_damageSound = new WindowsMediaPlayer();
         //_damageSound.URL = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "player_damage.mp3");
         //_damageSound.settings.volume = 70;

         //_deathSound = new WindowsMediaPlayer();
         //_deathSound.URL = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "player_death.mp3");
         //_deathSound.settings.volume = 70;

         //_enemyDeathSound = new WindowsMediaPlayer();
         //_enemyDeathSound.URL = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "enemy_death.mp3");
         //_enemyDeathSound.settings.volume = 70;

         //_levelUpSound = new WindowsMediaPlayer();
         //_levelUpSound.URL = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lvl_up.mp3");
         //_levelUpSound.settings.volume = 70;

         //_buttonSound = new WindowsMediaPlayer();
         //_buttonSound.URL = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "button.mp3");
         //_buttonSound.settings.volume = 40;

         //_musicPlayer = new WindowsMediaPlayer();
         //_musicPlayer.settings.volume = 45;
         //_musicPlayer.settings.setMode("loop", false); 
         //_musicPlayer.PlayStateChange += MusicPlayer_PlayStateChange;

         //PlayRandomGameTrack();
      }
      //private void PlayRandomGameTrack()
      //{
      //   _musicPlayer.controls.stop();

      //   System.Threading.Tasks.Task.Delay(50).ContinueWith(_ =>
      //   {
      //      int index = _musicRandom.Next(_gameTracks.Length);
      //      string trackPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _gameTracks[index]);

      //      if (!File.Exists(trackPath))
      //      {
      //         Console.WriteLine($"Файл не найден: {trackPath}");
      //         return;
      //      }

      //      _musicPlayer.URL = trackPath;
      //      _musicPlayer.controls.play();
      //   }, TaskScheduler.FromCurrentSynchronizationContext());
      //}

      //private void MusicPlayer_PlayStateChange(int NewState)
      //{
      //   if (NewState == (int)WMPPlayState.wmppsMediaEnded)
      //      PlayRandomGameTrack(); 
      //}
      private void ShowStartingWeaponSelection()
      {
         _gameTimer.Stop();
         _isPaused = true;

         //_musicPlayer.controls.pause();
         //_damageSound.controls.stop();
         //_deathSound.controls.stop();
         //_enemyDeathSound.controls.stop();
         //_levelUpSound.controls.stop();

         var originalBackColor = this.BackColor;
         this.BackColor = Color.Black;
         this.Visible = true;
         _healthBarBackground.Visible = false;
         _expBarBackground.Visible = false;

         using (var weaponForm = new StartingWeaponForm())
         {
            var result = weaponForm.ShowDialog(this);

            if (result == DialogResult.OK)
            {
               _player.Weapons.Clear();
               if (weaponForm.SelectedWeapon == "Whip")
                  _player.Weapons.Add(new WhipWeapon());
               else
                  _player.Weapons.Add(new GarlicWeapon());
            }
         }

         //_musicPlayer.controls.play();

         this.BackColor = originalBackColor;
         _healthBarBackground.Visible = true;
         _expBarBackground.Visible = true;
         _lastUpdate = DateTime.Now;
         _gameTimer.Start();
         _isPaused = false;
      }

      private void InitializeUI()
      {
         _healthBarBackground = new Panel
         {
            Size = new Size(200, 20),
            Location = new Point(10, 10),
            BackColor = Color.Gray,
            BorderStyle = BorderStyle.FixedSingle
         };

         _healthBarFill = new Panel
         {
            Size = new Size((int)(_healthBarBackground.Width * (_player.Health / _player.MaxHealth)), _healthBarBackground.Height),
            Location = Point.Empty,
            BackColor = Color.DarkGreen,
            BorderStyle = BorderStyle.FixedSingle
         };

         _healthBarBackground.Controls.Add(_healthBarFill);
         this.Controls.Add(_healthBarBackground);
         _healthBarBackground.BringToFront();
         _healthBarFill.BringToFront();

         _expBarBackground = new Panel
         {
            Size = new Size(200, 20),
            Location = new Point(10, 40),
            BackColor = Color.Gray,
            BorderStyle = BorderStyle.FixedSingle
         };

         _expBarFill = new Panel
         {
            Size = new Size((int)(_expBarBackground.Width * (_player.Exp / (float)_player.ExpToNextLevel)), _expBarBackground.Height),
            Location = Point.Empty,
            BackColor = Color.Blue
         };

         _expBarBackground.Controls.Add(_expBarFill);
         this.Controls.Add(_expBarBackground);
         _expBarBackground.BringToFront();
         _expBarFill.BringToFront();

      }
      private void UpdateUI()
      {
         float healthPercent = _player.Health / _player.MaxHealth;
         _healthBarFill.Width = (int)(_healthBarBackground.Width * healthPercent);

         if (healthPercent > 0.6f)
            _healthBarFill.BackColor = Color.DarkGreen;
         else if (healthPercent > 0.3f)
            _healthBarFill.BackColor = Color.Orange;
         else
            _healthBarFill.BackColor = Color.Red;

         float expPercent = _player.Exp / (float)_player.ExpToNextLevel;
         _expBarFill.Width = (int)(_expBarBackground.Width * expPercent);

      }
      private void GameTimer_Tick(object sender, EventArgs e)
      {
         if (_isPaused) return;
         var now = DateTime.Now;
         float deltaTime = (float)(now - _lastUpdate).TotalSeconds;
         _lastUpdate = now;
         deltaTime = Math.Min(deltaTime, 0.1f);

         const int TILE_SIZE = 64;
         const int MAP_WIDTH = 50;   
         const int MAP_HEIGHT = 50;  

         float mapWidthPx = MAP_WIDTH * TILE_SIZE;
         float mapHeightPx = MAP_HEIGHT * TILE_SIZE;

         float moveDistance = _player.Speed * deltaTime;
         if (_keyStates[Keys.W] || _keyStates[Keys.Up]) _player.Y -= moveDistance;
         if (_keyStates[Keys.S] || _keyStates[Keys.Down]) _player.Y += moveDistance;
         if (_keyStates[Keys.A] || _keyStates[Keys.Left]) _player.X -= moveDistance;
         if (_keyStates[Keys.D] || _keyStates[Keys.Right]) _player.X += moveDistance;

         _player.X = Math.Max(_player.HitboxRadius, Math.Min(_player.X, mapWidthPx - _player.HitboxRadius));
         _player.Y = Math.Max(_player.HitboxRadius, Math.Min(_player.Y, mapHeightPx - _player.HitboxRadius));

         var whipWeapons = _player.Weapons.OfType<WhipWeapon>().ToList();
         foreach (var whip_w in whipWeapons)
            whip_w.UpdateDirection(_player.X, _player.Y, _prevPlayerX, _prevPlayerY);
        
         _prevPlayerX = _player.X;
         _prevPlayerY = _player.Y;

         // обновление оружия
         foreach (var weapon in _player.Weapons.ToList())
         {
            weapon.Update(deltaTime);
            if (weapon is GarlicWeapon garlic_w)
            {
               bool attacked = false;
               foreach (var enemy in _activeEnemies)
               {
                  float dx = enemy.X - _player.X;
                  float dy = enemy.Y - _player.Y;
                  float dist = (float)Math.Sqrt(dx * dx + dy * dy);
                  if (dist < garlic_w.Range + enemy.HitboxRadius)
                  {
                     if (garlic_w.CanAttack())
                     {
                        enemy.Health -= garlic_w.Damage;
                        attacked = true;
                     }
                  }
               }
               if (attacked) garlic_w.StartAttack();
            }
            else if (weapon is WhipWeapon whip_w)
            {
               whip_w.UpdateAnimation(deltaTime);
               if (whip_w.CanAttack())
               {
                  float playerAngle = whip_w.GetLastAttackAngle();
                  whip_w.TriggerAttack(playerAngle); 

                  foreach (var enemy in _activeEnemies)
                  {
                     if (whip_w.IsInAttackArc(enemy.X, enemy.Y, _player.X, _player.Y, playerAngle))
                     {
                        enemy.Health -= whip_w.Damage;
                     }
                  }
               }
            }
            
         }
         // сбор опыта
         for (int i = _expOrbs.Count - 1; i >= 0; i--)
         {
            var orb = _expOrbs[i];
            float dx = orb.X - _player.X;
            float dy = orb.Y - _player.Y;
            float dist = (float)Math.Sqrt(dx * dx + dy * dy);

            if (dist < _player.HitboxRadius + orb.Radius)
            {
               if (_player.AddExp(orb.Value)) 
               {
                  ShowLevelUpForm(_player.Level); 
               }
               _expOrbs.RemoveAt(i);
            }
         }
         // спавн врагов
         _waveTimer += deltaTime;
         if (_waveTimer >= WaveInterval)
         {
            _waveTimer = 0;
            _currentWave++;

            int enemiesToSpawn = EnemiesPerSpawn + _currentWave * 2;
            int enemyHealth = 40 + _currentWave * 20;

            float baseSpawnRadius = Math.Min(this.ClientSize.Width, this.ClientSize.Height) * 0.65f;
            var _rng= new Random();
            for (int i = 0; i < enemiesToSpawn; i++)
            {
               float angle = (float)(_random.NextDouble() * 2 * Math.PI);
               float spawnRadius = baseSpawnRadius * (1.0f + (float)_random.NextDouble() * 0.2f);

               float spawnX = _player.X + (float)Math.Cos(angle) * spawnRadius;
               float spawnY = _player.Y + (float)Math.Sin(angle) * spawnRadius;
               //добавление врага
               var enemy = new Enemy
               {
                  X = spawnX,
                  Y = spawnY,
                  Health = enemyHealth,
                  Active = true,
                  Id = _rng.Next(0, 4) 
               };

               _activeEnemies.Add(enemy);
            }
         }
         //обновление врагов
         for (int i = 0; i < _activeEnemies.Count; i++)
         {
            var enemy = _activeEnemies[i];
            if (!enemy.Active) continue;

            enemy.MoveTowards(_player.X, _player.Y);
            enemy.AvoidOtherEnemies(_activeEnemies, deltaTime);

            float dx = enemy.X - _player.X;
            float dy = enemy.Y - _player.Y;
            float dist = (float)Math.Sqrt(dx * dx + dy * dy);

            if (dist < _player.HitboxRadius + enemy.HitboxRadius)
            {
               bool wasAlive = _player.Health > 0;

               if (_player.TakeDamage(20 * deltaTime))
               {
                  //if (!_deathSoundPlayed)
                  //{
                  //   _deathSound.controls.stop();
                  //   _deathSound.controls.play();
                  //   _deathSoundPlayed = true;
                  //}

                  HandleGameOver();
               }
               //else if (wasAlive && _player.IsDamaged)
               //{
               //   _damageSound.controls.stop();
               //   _damageSound.controls.play();
               //}
            }
            enemy.CollideWithPlayer(_player.X, _player.Y, _player.HitboxRadius, deltaTime);
            if (enemy.Health <= 0)
            {
               enemy.Active = false;
               _enemiesKilled++;

               //_enemyDeathSound.controls.stop();
               //_enemyDeathSound.controls.play();

               _expOrbs.Add(new ExpOrb
               {
                  X = enemy.X,
                  Y = enemy.Y,
                  Value = 1
               });

               _activeEnemies.RemoveAt(i);
            }
            _player.TickDamage(deltaTime);
         }
         var weaponList = new List<WeaponObject>();

         var garlic = _player.Weapons.OfType<GarlicWeapon>().FirstOrDefault();
         if (garlic != null)
         {
            weaponList.Add(new WeaponObject
            {
               x = _player.X,
               y = _player.Y,
               radius = garlic.Range,
               type = 1 
            });
         }
         var whip = _player.Weapons.OfType<WhipWeapon>().FirstOrDefault();
         if (whip != null && whip.AnimationTime > 0)
         {
            weaponList.Add(new WeaponObject
            {
               x = _player.X,
               y = _player.Y,
               radius = whip.Range,
               type = 2,
               angle = whip.PlayerAngle, 
               life = whip.AnimationTime
                 
            });
         }
         
         WeaponObject[] weaponArray = weaponList.ToArray();
         int enemyCount = Math.Min(_activeEnemies.Count, 1000);
         GameObject[] enemyArray = new GameObject[enemyCount];
         for (int i = 0; i < enemyCount; i++)
         {
            enemyArray[i] = new GameObject { x = _activeEnemies[i].X, y = _activeEnemies[i].Y, Id=_activeEnemies[i].Id };
         }

         var textList = new List<TextObject>();

         var elapsed = DateTime.Now - _gameStartTime;
         var timeText = new TextObject
         {
            x = this.ClientSize.Width / 2f,
            y = 30f,
            text = $"{(int)elapsed.TotalMinutes}:{elapsed.Seconds:D2}",
            fontSize = 30,
            r = 1.0f,
            g = 1.0f,
            b = 1.0f 
         };
         textList.Add(timeText);

         var levelText = new TextObject
         {
            x = 215f,
            y = 55f,
            text = $"Lvl {_player.Level}",
            fontSize = 16,
            r = 1.0f,
            g = 1.0f,
            b = 0.0f 
         };

         textList.Add(levelText);
         var enemiesKilledText = new TextObject
         {
            x = 10f,
            y = 80f, 
            text = $"Killed: {_enemiesKilled}",
            fontSize = 16,
            r = 1.0f,
            g = 0.0f,
            b = 0.3f 
         };
         textList.Add(enemiesKilledText);

         TextObject[] textArray = textList.ToArray();

         UpdateUI();
         ExpOrbObject[] orbArray = _expOrbs.Select(o => new ExpOrbObject { x = o.X, y = o.Y, value = o.Value }).ToArray();
         RenderFrame(
            _player.X, _player.Y, _player.IsDamaged,
            enemyArray, enemyCount,
            weaponArray, weaponArray.Length,
            orbArray, orbArray.Length,
            textArray, textArray.Length,
            this.ClientSize.Width,  
            this.ClientSize.Height
         ); 

      }
      private void ShowLevelUpForm(int level)
      {
         _isPaused = true;
         _gameTimer.Stop();

         foreach (var key in _keyStates.Keys.ToList())
            _keyStates[key] = false;
        
         var options = new List<string>();
         var garlic = _player.Weapons.OfType<GarlicWeapon>().FirstOrDefault();
         if (garlic != null)
            options.Add($"Реппелент: Уровень {garlic.Level + 1}");
         
         if (garlic == null)
            options.Add("Новое оружие: Реппелент");
         
         var whip = _player.Weapons.OfType<WhipWeapon>().FirstOrDefault();
         if (whip != null)
            options.Add($"Мухобойка: Уровень {whip.Level + 1}");

         if (whip == null)
            options.Add("Новое оружие: Мухобойка");

         if (options.Count == 0)
            options.Add("Продолжить");

         //_levelUpSound.controls.stop();
         //_levelUpSound.controls.play();
         var levelUpForm = new LevelUpForm(level, options);
         levelUpForm.ShowDialog(this);

         if (levelUpForm.SelectedOption.StartsWith("Реппелент:"))
            garlic?.LevelUp();
         else if (levelUpForm.SelectedOption.StartsWith("Мухобойка:"))
            whip?.LevelUp();
         else if (levelUpForm.SelectedOption == "Новое оружие: Мухобойка")
            _player.Weapons.Add(new WhipWeapon());
         else if (levelUpForm.SelectedOption == "Новое оружие: Реппелент")
            _player.Weapons.Add(new GarlicWeapon());

         _isPaused = false;
         _lastUpdate = DateTime.Now;
         _gameTimer.Start();
      }
      private void HandleGameOver()
      {
         //_musicPlayer.controls.stop();
         Player.SaveRecord(_player.Level);

         _isPaused = true;
         _gameTimer.Stop();

         _healthBarBackground.Visible = false;
        
         _expBarBackground.Visible = false;
 
         var originalBackColor = this.BackColor;
         this.BackColor = Color.Black;

         var gameOverForm = new GameOverForm();
         gameOverForm.UpdateStats(_player.Level, Player.MaxRecordLevel);
         var result = gameOverForm.ShowDialog();

         this.BackColor = originalBackColor;

         _healthBarBackground.Visible = true;
         
         _expBarBackground.Visible = true;
        
         if (gameOverForm.ShouldRestart)
         {
            //_musicPlayer.controls.stop();
            _gameTimer.Stop();
            ResetGame();

            _isPaused = false;
            _lastUpdate = DateTime.Now;
            _gameTimer.Start();
         }
         else if (gameOverForm.MainMenu)
         {
            //_musicPlayer.controls.stop();
            this.DialogResult = DialogResult.Abort;
            this.Close();
         }
         else
         {
            this.Close();
         }

         gameOverForm.Dispose();
        
      }
      private void ResetGame()
      {
         _gameStartTime = DateTime.Now;
         //_deathSoundPlayed = false;
         //_musicPlayer.controls.stop();
         _waveTimer = WaveInterval;
         _currentWave = 1;
         _enemiesKilled = 0;

         _player = new Player
         {
            X = this.ClientSize.Width / 2f,
            Y = this.ClientSize.Height / 2f
         };

         _activeEnemies.Clear();
         _expOrbs.Clear();

         foreach (var key in _keyStates.Keys.ToList())
             _keyStates[key] = false;
         ShowStartingWeaponSelection();
         UpdateUI();
        
      }
      private void Form1_KeyDown(object sender, KeyEventArgs e)
      {
         if (_keyStates.ContainsKey(e.KeyCode))
            _keyStates[e.KeyCode] = true;

         if (e.KeyCode == Keys.Escape)
         {
            _keyStates[Keys.Escape] = true;
            PauseGame();
         }
      }

      private void Form1_KeyUp(object sender, KeyEventArgs e)
      {
         if (_keyStates.ContainsKey(e.KeyCode))
            _keyStates[e.KeyCode] = false;
      }
      private void PauseGame()
      {
         _isPaused = true;
         _gameTimer.Stop();
         var pauseForm = new PauseForm();
         pauseForm.FormClosed += (s, e) =>
         {
            if (pauseForm.DialogResult == DialogResult.Cancel)
            {
               this.BeginInvoke(new Action(() => this.Close()));
            }
            else if (pauseForm.MainMenu)
            {
               //_musicPlayer.controls.stop();
               this.DialogResult = DialogResult.Abort;
               this.Close();
            }
            else
            {
               _isPaused = false;
               _lastUpdate = DateTime.Now;
               _gameTimer.Start();
            }
            pauseForm.Dispose();
         };

         pauseForm.Show(this);
      }

      protected override void OnPaint(PaintEventArgs e)
      {

      }

      protected override void OnFormClosing(FormClosingEventArgs e)
      {
         _gameTimer?.Stop();
         ShutdownOpenGL();
         base.OnFormClosing(e);
      }

      protected override void OnResize(EventArgs e)
      {
         base.OnResize(e);
         ResizeOpenGL(this.ClientSize.Width, this.ClientSize.Height);
      }
   }
}