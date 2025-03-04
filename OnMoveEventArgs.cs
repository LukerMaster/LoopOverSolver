public class OnMoveEventArgs : EventArgs
{
    public int Amount {get; set;}
    public int Column {get; set;}
    public Direction Direction {get; set;}
}