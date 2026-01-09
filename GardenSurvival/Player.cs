using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

public class Player
{
   public float X { get; set; }
   public float Y { get; set; }
   public float Speed { get; set; } = 150f;
   public float Health { get; set; } = 60f;
   public float MaxHealth { get; set; } = 60f;
   public int Level { get; set; } = 1;
   public int Exp { get; set; } = 0;
   public int ExpToNextLevel { get; set; } = 7;
   public float HitboxRadius { get; set; } = 28f;
   public bool IsDamaged { get; private set; }
   private float _damageTimer = 0f;

   public List<Weapon> Weapons { get; set; } = new();

   private static readonly string RecordFile = Path.Combine(Application.StartupPath, "record.txt");
   public static int MaxRecordLevel { get; private set; } = 0;

   static Player()
   {
      LoadRecord();
   }

   public static void LoadRecord()
   {
      if (File.Exists(RecordFile))
      {
         string content = File.ReadAllText(RecordFile).Trim();
         if (int.TryParse(content, out int level))
            MaxRecordLevel = level;
      }
   }

   public static void SaveRecord(int currentLevel)
   {
      if (currentLevel > MaxRecordLevel)
      {
         MaxRecordLevel = currentLevel;
        
         File.WriteAllText(RecordFile, MaxRecordLevel.ToString());
     
      }
   }

   public bool TakeDamage(float damage)
   {
      Health -= damage;

      IsDamaged = true;
      _damageTimer = 0.3f; 

      return Health <= 0;
   }
   public void TickDamage(float deltaTime)
   {
      if (IsDamaged)
      {
         _damageTimer -= deltaTime;
         if (_damageTimer <= 0f)
         {
            _damageTimer = 0f;
            IsDamaged = false;
         }
      }
   }

   public bool AddExp(int amount)
   {
      Exp += amount;
      if (Exp >= ExpToNextLevel)
      {
         LevelUp();
         return true; 
      }
      return false;
   }

   private void LevelUp()
   {
      Level++;
      Exp -= ExpToNextLevel;
      ExpToNextLevel = (int)(ExpToNextLevel * 1.7f);
      Health = Math.Min(Health * 1.5f, MaxHealth);
   }

}