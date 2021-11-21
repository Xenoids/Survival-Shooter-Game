public class MoveCommand : Command
{
    PlayerMovement pMovement;
    float h,v;

    public MoveCommand(PlayerMovement _pmovement, float _h, float _v)
    {
        this.pMovement = _pmovement;
        this.h = _h;
        this.v = _v;
    }

    // Trigger pMovement
    public override void Execute()
    {
        pMovement.Movement(h,v);
        // animasi
        pMovement.Animating(h,v);
    }

    public override void UnExecute()
    {
        // invers arah dari pMovement
        pMovement.Movement(-h,-v);

        // animasi
        pMovement.Animating(h,v);
    }
}