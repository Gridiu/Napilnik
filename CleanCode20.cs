class Player
{
    public string Name { get; private set; }
    public int Age { get; private set; }
}

class PlayerMovement
{
    public float MovementDirectionX { get; private set; }
    public float MovementDirectionY { get; private set; }
    public float MovementSpeed { get; private set; }

    public void Move()
    {
        //Do move
    }
}

class PlayerShooting
{
    public float WeaponCooldown { get; private set; }
    public int WeaponDamage { get; private set; }

    public void Attack()
    {
        //attack
    }

    public bool IsReloading()
    {
        throw new NotImplementedException();
    }
}