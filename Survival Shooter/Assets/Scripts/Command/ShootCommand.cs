public class ShootCommand : Command
{
    PlayerShooting PShooting;
    public ShootCommand(PlayerShooting _pShooting)
    {
        PShooting = _pShooting;
    }

    public override void Execute()
    {
        // nembak
        PShooting.Shoot();
    }

    public override void UnExecute()
    {
        
    }
}
