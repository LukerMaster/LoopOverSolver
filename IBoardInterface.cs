public interface IBoardInterface<T>
{
    public void Move(Direction direction, int column, int by);
    public (int x, int y) GetPositionOf(T square);
    public (int x, int y) GetDesiredPositionOf(T square);
    public (int x, int y) GetDistanceToDesiredOf(T square);
    public int GetSize();
    public int GetTotalSquareCount();
    public int GetIndexOf(T square);
    public T GetSquareByOrder(int index);
}