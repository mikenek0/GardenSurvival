using System;
using System.Collections.Generic;

public class Enemy
{
   public float X { get; set; }
   public float Y { get; set; }
   public float Speed { get; set; } = 1f;
   public float Health { get; set; } = 1f;
   public bool Active { get; set; } = false;
   public int Id { get; set; }
   public float HitboxRadius { get; set; } = 21f;

   public void MoveTowards(float targetX, float targetY)
   {
      var dx = targetX - X;
      var dy = targetY - Y;
      var dist = (float)Math.Sqrt(dx * dx + dy * dy);

      if (dist > 1f)
      {
         dx /= dist;
         dy /= dist;
         X += (float)(dx * Speed);
         Y += (float)(dy * Speed);
      }
   }
   public bool IsDead() => Health <= 0;

   public void AvoidOtherEnemies(List<Enemy> allEnemies, float deltaTime)
   {
      foreach (var other in allEnemies)
      {
         if (other == this || !other.Active) continue;

         float dx = other.X - X;
         float dy = other.Y - Y;
         float dist = (float)Math.Sqrt(dx * dx + dy * dy);

         if (dist < HitboxRadius * 2)
         {
            float repel = 50f * deltaTime; 
            if (dist > 1f)
            {
               dx /= dist;
               dy /= dist;
               X -= dx * repel;
               Y -= dy * repel;
            }
         }
      }
   }
   public void CollideWithPlayer(float playerX, float playerY, float playerHitboxRadius, float deltaTime)
   {
      float dx = X - playerX;
      float dy = Y - playerY;
      float dist = (float)Math.Sqrt(dx * dx + dy * dy);

      float minDist = HitboxRadius + playerHitboxRadius;

      if (dist < minDist)
      {
         if (dist > 1f)
         {
            dx /= dist;
            dy /= dist;

            float overlap = minDist - dist;
            X += dx * overlap * 1.5f; 
            Y += dy * overlap * 1.5f;

         }
      }
   }
}