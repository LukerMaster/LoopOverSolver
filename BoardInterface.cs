
public class BoardInterface<T> : IBoardInterface<T> where T : notnull, IEquatable<T>
{
    private T[][] _board;
    private T[][] _target;
    private (int x, int y) GetPositionOf(T square, T[][] sourceBoard)
    {
        for (int y = 0; y < sourceBoard.Length; y++)
        {
            for (int x = 0; x < sourceBoard.Length; x++)
            {
                if (square.Equals(sourceBoard[y][x])) return (x, y);
            }
        }
        throw new Exception("Symbol not found!");
    }

    public override string ToString()
    {
        var str = string.Empty;
        for (int y = 0; y < GetSize(); y++)
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
        by = by % _board.Length;
        column = column % _board.Length;
        if (by == 0) return;

        if (direction is Direction.Horizontal)
        {
            if (by < 0) by = _board.Length + by; // Moving right is effectively moving left by "negated" amount.
         
            T[] buffer = new T[by];
            Array.Copy(_board[column], _board.Length - by, buffer, 0, by);
            Array.Copy(_board[column], 0, _board[column], by, _board.Length - by);
            Array.Copy(buffer, _board[column], by);
        }
        if (direction is Direction.Vertical)
        {
            by = -by; // Necessary to make UP direction be negative (to align with debug representations).
            if (by < 0) by = _board.Length + by; // Moving up is effectively moving down by "negated" amount.

            T[] buffer = new T[by];
            for (int i = 0; i < by; i++)
            {
                Array.Copy(_board[i], column, buffer, i, 1);
            }
            for (int i = by; i < _board.Length; i++)
            {
                Array.Copy(_board[i], column, _board[i-by], column, 1);
            }
            for (int i = 0; i < buffer.Length; i++)
            {
                Array.Copy(buffer, i, _board[_board.Length - by + i], column, 1);
            }
        }
    }

    public int GetSize()
    {
        return _board.Length;
    }

    public int GetIndexOf(T square)
    {
        var position = GetPositionOf(square, _target);
        return position.y * GetSize() + position.x;
    }

    public int GetTotalSquareCount()
    {
        return GetSize() * GetSize();
    }

    public T GetSquareByOrder(int order)
    {
        return _target[order / GetSize()][order % GetSize()];
    }

    public bool IsRowSolved(int row)
    {
        for (int i = 0; i < GetSize(); i++)
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
}