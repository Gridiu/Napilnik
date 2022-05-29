﻿using System;

namespace WeaponTask
{
    public class Weapon
    {
        private readonly int _damage;
        private int _bullets;

        public Weapon(int damage, int bullets)
        {
            if (damage <= 0)
                throw new ArgumentOutOfRangeException(nameof(damage));

            if (bullets < 0)
                throw new ArgumentOutOfRangeException(nameof(bullets));

            _damage = damage;
            _bullets = bullets;
        }

        public bool HaveBulletToFire => _bullets >= 0;

        public void Fire(Player player)
        {
            player.TakeDamage(_damage);

            _bullets--;
        }
    }

    public class Player
    {
        public Player(int health)
        {
            if (health <= 0)
                throw new ArgumentOutOfRangeException(nameof(health));

            Health = health;
        }

        public int Health { get; private set; }

        public void TakeDamage(int damage)
        {
            Health -= damage;

            if (Health < 0)
                Health = 0;
        }
    }

    public class Bot
    {
        private readonly Weapon _weapon;

        public Bot(Weapon weapon)
        {
            _weapon = new Weapon();
        }

        public void OnSeePlayer(Player player)
        {
            if (player.Health > 0)
                if (_weapon.HaveBulletToFire)
                    _weapon.Fire(player);
        }
    }
}