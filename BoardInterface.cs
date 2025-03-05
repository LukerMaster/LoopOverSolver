
public class BoardInterface<T> : IBoardInterface<T> where T : notnull, IEquatable<T>
{
    private T[][] _board;
    private T[][] _target;

    
    public event EventHandler<OnMoveEventArgs>? OnMove;
    private (int x, int y) GetPositionOf(T square, T[][] sourceBoard)
    {
        for (int y = 0; y < GetSizeY(); y++)
        {
            for (int x = 0; x < GetSizeX(); x++)
            {
                if (square.Equals(sourceBoard[y][x])) return (x, y);
            }
        }
        throw new Exception("Symbol not found!");
    }

    public override string ToString()
    {
        var str = string.Empty;
        for (int y = 0; y < GetSizeY(); y++)
        {
            str += "| " + string.Join(" | ", _board[y]) + " |" + '\n';
        }
        return str;
    }

    public BoardInterface(T[][] currentBoard, T[][] solvedBoard)
    {
        _board = currentBoard;
        _target = solvedBoard;
    }

    public (int x, int y) GetDesiredPositionOf(T square)
    {
        return GetPositionOf(square, _target);
    }

    public (int x, int y) GetDistanceToDesiredOf(T square)
    {
        var desired = GetPositionOf(square, _target);
        var current = GetPositionOf(square, _board);
        return (desired.x - current.x, desired.y - current.y);
    }

    public (int x, int y) GetPositionOf(T square)
    {
        return GetPositionOf(square, _board);
    }

    public void Move(Direction direction, int column, int by)
    {
        if (by == 0) return;

        if (direction is Direction.Horizontal)
        {
            by = by % GetSizeX();
            column = column % GetSizeY();

            if (by < 0) by = GetSizeX() + by; // Moving right is effectively moving left by "negated" amount.
         
            T[] buffer = new T[by];
            Array.Copy(_board[column], GetSizeX() - by, buffer, 0, by);
            Array.Copy(_board[column], 0, _board[column], by, GetSizeX() - by);
            Array.Copy(buffer, _board[column], by);
        }
        if (direction is Direction.Vertical)
        {
            by = by % GetSizeY();
            column = column % GetSizeX();

            if (by < 0) by = GetSizeY() + by; // Moving down is effectively moving up by "negated" amount.

            T[] buffer = new T[by];
            for (int i = 0; i < by; i++)
            {
                Array.Copy(_board[i+GetSizeY()-by], column, buffer, i, 1);
            }
            for (int i = GetSizeY() - by - 1; i >= 0; i--)
            {
                Array.Copy(_board[i], column, _board[i + by], column, 1);
            }
            for (int i = 0; i < by; i++)
            {
                Array.Copy(buffer, i, _board[i], column, 1);
            }
        }
        OnMove?.Invoke(this, new OnMoveEventArgs() { Amount = by, Column = column, Direction = direction});
    }

    public int GetSizeX()
    {
        return _board[0].Length;
    }

    public int GetSizeY()
    {
        return _board.Length;
    }

    public int GetIndexOf(T square)
    {
        var position = GetPositionOf(square, _target);
        return position.y * GetSizeX() + position.x;
    }

    public int GetTotalSquareCount()
    {
        return GetSizeY() * GetSizeX();
    }

    public T GetSquareByOrder(int order)
    {
        return _target[order / GetSizeX()][order % GetSizeX()];
    }

    public bool IsRowSolved(int row)
    {
        for (int i = 0; i < GetSizeX(); i++)
        {
            if (!_board[row][i].Equals(_target[row][i]))
                return false;
        }
        return true;
    }

    public T GetSquareOn(int x, int y)
    {
        return _board[y][x];
    }

    public T GetSquareThatShouldBeOn(int x, int y)
    {
        return _target[y][x];
    }

    public bool IsSolved()
    {
        for (int i = 0; i < GetSizeY(); i++)
        {
            if (!IsRowSolved(i)) return false;
        }
        return true;
    }
}