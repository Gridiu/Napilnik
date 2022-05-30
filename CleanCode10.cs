class Weapon
{
    private const int BulletsPerShot = 1;

    private int _bullets;

    public bool CanShoot() => _bullets > 0;

    public void Shoot() => _bullets -= BulletsPerShot;
}