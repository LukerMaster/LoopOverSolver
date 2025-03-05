public class BoardSolver<T>(IBoardInterface<T> board)
{
    private List<string> solution = new List<string>();

    /// <summary>
    /// Algorithm used to swap squares on last row needs to be done in pairs and run interchangably up/down.
    /// Doing it only once leaves the field in incorrect state.
    /// </summary>
    private bool parityError = false;
    private void MoveOutOfTheAxis(T square)
    {
        var current = board.GetPositionOf(square);
        var desired = board.GetDesiredPositionOf(square);

        while (current.x == desired.x || current.y == desired.y)
        {
            // If on the same column, first move out of the way (to the side)
            if (current.x == desired.x)
            {
                if (current.y == desired.y) return;
                else
                {
                    board.Move(Direction.Horizontal, current.y, 1);
                    current = board.GetPositionOf(square);
                }
            }
            // If on the same row, first move out of the way (down, then left, then put the original column back)
            if (current.y == desired.y)
            {
                if (current.x == desired.x) return;
                else
                {
                    board.Move(Direction.Vertical, current.x, 1);
                    board.Move(Direction.Horizontal, current.y + 1, 1);
                    board.Move(Direction.Vertical, current.x, -1);
                    current = board.GetPositionOf(square);
                }
            }
        }
    }
    private void SolveAllButLastRow() // As far as I know it's a "belt" method.
    {
        for (int i = 0; i < board.GetTotalSquareCount() - board.GetSizeX() /* stop at second to last row*/; i++)
        {
            var square = board.GetSquareByOrder(i);

            MoveOutOfTheAxis(square);

            var current = board.GetPositionOf(square);
            var distance = board.GetDistanceToDesiredOf(square);
            var desired = board.GetDesiredPositionOf(square);

            // At this point, the element is not aligned with any already-solved axis, so we can proceed with standard algorithm
            board.Move(Direction.Vertical, desired.x, -distance.y);
            board.Move(Direction.Horizontal, current.y, distance.x);
            board.Move(Direction.Vertical, desired.x, distance.y);

            if (board.GetDistanceToDesiredOf(square) != (0, 0))
            {
                Console.WriteLine($"Warning: '{square}' Square ended up on wrong place.");
            }
        }
    }

    private void SwapOnLastRow(int positionXOnLastRowOfSquare1, int positionXOnLastRowOfSquare2)
    {
        int verticalDirection = parityError ? -1 : 1;

        positionXOnLastRowOfSquare1 = positionXOnLastRowOfSquare1 % board.GetSizeX();
        positionXOnLastRowOfSquare2 = positionXOnLastRowOfSquare2 % board.GetSizeX();

        var symbol1 = board.GetSquareOn(positionXOnLastRowOfSquare1, board.GetSizeY() - 1);
        var symbol2 = board.GetSquareOn(positionXOnLastRowOfSquare2, board.GetSizeY() - 1);
        
        var focusedSquarePosition = board.GetPositionOf(symbol1);
        var desiredX = board.GetSizeX() - 1; // We will be doing the swap on last vertical column.
        var distance = desiredX - focusedSquarePosition.x;

        board.Move(Direction.Horizontal, board.GetSizeY() - 1, distance); // Move the desired square to last column
        board.Move(Direction.Vertical, board.GetSizeX() - 1, -verticalDirection); // Swap it with whatever was on the top of first row

        focusedSquarePosition = board.GetPositionOf(symbol2);
        distance = desiredX - focusedSquarePosition.x;

        board.Move(Direction.Horizontal, board.GetSizeY() - 1, distance); // Move the second square to last row
        board.Move(Direction.Vertical, board.GetSizeX() - 1, verticalDirection); // Swap it back.

        board.Move(Direction.Horizontal, board.GetSizeY() - 1, -distance); // Go back
        board.Move(Direction.Vertical, board.GetSizeX() - 1, -verticalDirection); // Put the original square (borrowed from the first row) back to it's place.

        parityError = !parityError;
    }

    private void SwapOnLastColumn(int positionYOnLastRowOfSquare1, int positionYOnLastRowOfSquare2)
    {
        int verticalDirection = parityError ? -1 : 1;

        positionYOnLastRowOfSquare1 = positionYOnLastRowOfSquare1 % board.GetSizeY();
        positionYOnLastRowOfSquare2 = positionYOnLastRowOfSquare2 % board.GetSizeY();

        var symbol1 = board.GetSquareOn(board.GetSizeX() - 1, positionYOnLastRowOfSquare1);
        var symbol2 = board.GetSquareOn(board.GetSizeX() - 1, positionYOnLastRowOfSquare2);
        
        var focusedSquarePosition = board.GetPositionOf(symbol1);
        var desiredY = board.GetSizeY() - 1; // We will be doing the swap on last row.
        var distance = desiredY - focusedSquarePosition.y;

        board.Move(Direction.Vertical, board.GetSizeX() - 1, distance); // Move the desired square to last column
        board.Move(Direction.Horizontal, board.GetSizeY() - 1, -verticalDirection); // Swap it with whatever was on the top of first row

        focusedSquarePosition = board.GetPositionOf(symbol2);
        distance = desiredY - focusedSquarePosition.y;

        board.Move(Direction.Vertical, board.GetSizeX() - 1, distance); // Move the second square to last row
        board.Move(Direction.Horizontal, board.GetSizeY() - 1, verticalDirection); // Swap it back.

        board.Move(Direction.Vertical, board.GetSizeX() - 1, -distance); // Go back
        board.Move(Direction.Horizontal, board.GetSizeY() - 1, -verticalDirection); // Put the original square (borrowed from the first row) back to it's place.

        parityError = !parityError;
    }

    private void TrySwappingOutOfPlaceSquaresOnLastRow()
    {
        for (int i = 0; i < board.GetSizeX(); i++)
        {
            if (board.GetDistanceToDesiredOf(board.GetSquareOn(i, board.GetSizeY() - 1)) != (0, 0)) // if this square is out of place
            {
                for (int j = i + 1; j < board.GetSizeX(); j++)
                {
                    if (board.GetSquareOn(j, board.GetSizeY() - 1)!.Equals(board.GetSquareThatShouldBeOn(i, board.GetSizeY() - 1)))
                    {
                        SwapOnLastRow(i, j);
                        MoveFirstSquareOnLastRowToItsPosition();
                        break;
                    }
                }
            }
        }
    }

    private void TrySwappingOutOfPlaceSquaresOnLastColumn()
    {
        for (int i = 0; i < board.GetSizeY(); i++)
        {
            if (board.GetDistanceToDesiredOf(board.GetSquareOn(board.GetSizeX() - 1, i)) != (0, 0)) // if this square is out of place
            {
                for (int j = i + 1; j < board.GetSizeY(); j++)
                {
                    if (board.GetSquareOn(board.GetSizeX() - 1, j)!.Equals(board.GetSquareThatShouldBeOn(board.GetSizeX() - 1, i)))
                    {
                        SwapOnLastColumn(i, j);
                        MoveFirstSquareOnLastColumnToItsPosition();
                        break;
                    }
                }
            }
        }
    }

    private void MoveFirstSquareOnLastRowToItsPosition()
    {
        var first = board.GetSquareByOrder(board.GetTotalSquareCount() - board.GetSizeX()); // First square on last row
        var distance = board.GetDistanceToDesiredOf(first);
        board.Move(Direction.Horizontal, board.GetSizeY() - 1, distance.x); // Move the board so the first square is in correct position.
    }
    private void MoveFirstSquareOnLastColumnToItsPosition()
    {
        var first = board.GetSquareByOrder(board.GetSizeX() - 1); // First square on last column
        var distance = board.GetDistanceToDesiredOf(first);
        board.Move(Direction.Vertical, board.GetSizeX() - 1, distance.y); // Move the board so the first square is in correct position.
    }
    private void ReshuffleLastRowWithBubbleSort()
    {
        for (int i = 0; i < board.GetSizeX() - 1; i++)
        {
            SwapOnLastRow(0, 1);
            MoveFirstSquareOnLastRowToItsPosition();
            
        }
    }
    private void ReshuffleLastColumnWithBubbleSort()
    {
        for (int i = 0; i < board.GetSizeY() - 1; i++)
        {
            SwapOnLastColumn(0, 1);
            MoveFirstSquareOnLastColumnToItsPosition();
        }
    }
    private void SolveLastRow()
    {
        MoveFirstSquareOnLastRowToItsPosition();

        if (!board.IsRowSolved(board.GetSizeY() - 1))
        {
            TrySwappingOutOfPlaceSquaresOnLastRow();
            MoveFirstSquareOnLastRowToItsPosition();
        }
        if (parityError)
        {
            ReshuffleLastRowWithBubbleSort();
        }
        if (!board.IsColumnSolved(board.GetSizeX() - 1))
        {
            TrySwappingOutOfPlaceSquaresOnLastColumn();
            MoveFirstSquareOnLastColumnToItsPosition();
        }
        if (!board.IsSolved() && board.GetSizeY() % 2 == 0)
        {
            ReshuffleLastColumnWithBubbleSort();
        }
        MoveFirstSquareOnLastRowToItsPosition();
    }

    private void WriteDownMovesEvent(object? sender, OnMoveEventArgs e)
    {
        var directionStr = string.Empty;
        if (e.Direction == Direction.Horizontal)
        {
            if (e.Amount > 0)
            {
                directionStr = "L";
            }
            else
            {
                directionStr = "R";
            }
        }
        if (e.Direction == Direction.Vertical)
        {
            if (e.Amount > 0)
            {
                directionStr = "D";
            }
            else
            {
                directionStr = "U";
            }
        }
        for (int i = 0; i < e.Amount; i++)
        {
            solution.Add(directionStr + e.Column);
        }
        
    }

    public List<string>? GetSolution()
    {
        board.OnMove += WriteDownMovesEvent;
        SolveAllButLastRow(); // 3% of difficulty
        SolveLastRow(); // 97% of difficulty
        if (!board.IsSolved())
        {
            return null;
        }
        return solution;
    }
}
