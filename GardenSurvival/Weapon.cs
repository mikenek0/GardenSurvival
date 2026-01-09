using System;
using System.Collections.Generic;
using System.Windows.Forms;

public class Weapon
{
   public int Id { get; set; }
   public int Level { get; set; } = 1;
   public float Cooldown { get; set; } = 0f;
   public float BaseCooldown { get; set; } = 1f; 
   public int Damage { get; set; } = 10;
   public bool Active { get; set; } = true;

   public void Update(float deltaTime)
   {
      if (Cooldown > 0) Cooldown -= deltaTime;
   }

   public void StartAttack()
   {
      if (Cooldown <= 0)
      {
         Cooldown = BaseCooldown; 
      }
   }

   public bool CanAttack() => Cooldown <= 0;
}

public class GarlicWeapon : Weapon
{
   public float Range { get; set; }

   private static readonly Dictionary<int, (int damage, float cooldown, float range)> Levels = new()
    {
        { 1, (5, 0.1f, 70f) },
        { 2, (15, 0.1f, 70f) },
        { 3, (15, 0.1f, 85f) },
        { 4, (30, 0.1f, 85f) },
        { 5, (40, 0.1f, 110f) },
        { 6, (40, 0.1f, 150f) }
       
    };

   public GarlicWeapon()
   {
      Id = 1;
      Level = 1;
      UpdateStats(); 
   }

   private void UpdateStats()
   {
      if (Levels.TryGetValue(Level, out var stats))
      {
         Damage = stats.damage;
         BaseCooldown = stats.cooldown; 
         Range = stats.range;
         
      }
   }
   public void LevelUp()
   {
      Level++;
      UpdateStats();
   }
}

public class WhipWeapon : Weapon
{
   public float Range { get; set; }
   public float AnimationTime { get; set; } = 0f;
   public float ArcAngle { get; } = 30f;
   public float PlayerAngle { get; set; } = 0f; 

   private static readonly Dictionary<int, (int damage, float cooldown, float range)> Levels = new()
    {
        { 1, (50, 0.8f, 150f) },
        { 2, (60, 0.7f, 170f) },
        { 3, (60, 0.7f, 170f) },
        { 4, (80, 0.7f, 170f) },
        { 5, (80, 0.5f, 170f) },
        { 6, (100, 0.5f, 200f) }
    };

   public WhipWeapon()
   {
      Id = 2;
      Level = 1;
      UpdateStats();
   }

   private void UpdateStats()
   {
      if (Levels.TryGetValue(Level, out var stats))
      {
         Damage = stats.damage;
         BaseCooldown = stats.cooldown;
         Range = stats.range;
      }
   }
   public bool IsInAttackArc(float enemyX, float enemyY, float playerX, float playerY, float attackAngle)
   {
      float dx = enemyX - playerX;
      float dy = enemyY - playerY;
      float distance = (float)Math.Sqrt(dx * dx + dy * dy);

      if (distance > Range) return false;

      float angleToEnemy = (float)(Math.Atan2(dy, dx) * 180.0 / Math.PI);
      if (angleToEnemy < 0) angleToEnemy += 360f;

      float normAttack = attackAngle % 360f;
      if (normAttack < 0) normAttack += 360f;

      float diff = Math.Abs(angleToEnemy - normAttack);
      if (diff > 180f) diff = 360f - diff;

      return diff <= ArcAngle / 2f;
   }

   private float _lastValidAngle = 0f;
   public void LevelUp() => UpdateStats();
   public void UpdateDirection(float playerX, float playerY, float prevPlayerX, float prevPlayerY)
   {
      if (playerX != prevPlayerX || playerY != prevPlayerY)
      {
         float dx = playerX - prevPlayerX;
         float dy = playerY - prevPlayerY;

         if (Math.Abs(dx) > Math.Abs(dy))
            _lastValidAngle = dx > 0 ? 0f : 180f; 
         else
            _lastValidAngle = dy > 0 ? 90f : -90f; 
      }
      
   }
   public float GetLastAttackAngle() => _lastValidAngle;
   public void TriggerAttack(float angle)
   {
      if (CanAttack())
      {
         StartAttack();
         PlayerAngle = angle;        
         _lastValidAngle = angle;    
         AnimationTime = 0.25f;
      }
   }

   public void UpdateAnimation(float deltaTime)
   {
      if (AnimationTime > 0)
      {
         AnimationTime -= deltaTime;
         if (AnimationTime < 0) AnimationTime = 0;
      }
   }
}

