using System.IO.Compression;

public class BoardSolver<T>(IBoardInterface<T> board)
{

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
        for (int i = 0; i < board.GetTotalSquareCount() - board.GetSize() /* stop at second to last row*/; i++)
        {
            var square = board.GetSquareByOrder(i);
            var stateOfTheBoard = board.ToString();

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
                throw new Exception($"Square {square} ended up not on its place! Board before: \n{stateOfTheBoard}\n\n Board after:\n{board.ToString()}");
            }
        }
    }

    private void SwapOnLastRow(int positionXOnLastRowOfSquare1, int positionXOnLastRowOfSquare2)
    {
        positionXOnLastRowOfSquare1 = positionXOnLastRowOfSquare1 % board.GetSize();
        positionXOnLastRowOfSquare2 = positionXOnLastRowOfSquare2 % board.GetSize();

        var symbol1 = board.GetSquareOn(positionXOnLastRowOfSquare1, board.GetSize() - 1);
        var symbol2 = board.GetSquareOn(positionXOnLastRowOfSquare2, board.GetSize() - 1);
        
        var focusedSquarePosition = board.GetPositionOf(symbol1);
        var desiredX = board.GetSize() - 1; // We will be doing the swap on last vertical column.
        var distance = desiredX - focusedSquarePosition.x;

        board.Move(Direction.Horizontal, board.GetSize() - 1, distance); // Move the desired square to last column
        board.Move(Direction.Vertical, board.GetSize() - 1, -1); // Swap it with whatever was on the top of first row

        focusedSquarePosition = board.GetPositionOf(symbol2);
        distance = desiredX - focusedSquarePosition.x;

        board.Move(Direction.Horizontal, board.GetSize() - 1, distance); // Move the second square to last row
        board.Move(Direction.Vertical, board.GetSize() - 1, 1); // Swap it back.

        board.Move(Direction.Horizontal, board.GetSize() - 1, -distance); // Go back
        board.Move(Direction.Vertical, board.GetSize() - 1, -1); // Put the original square (borrowed from the first row) back to it's place.

        // At this point, the board may be in completely incorrect state,
        // as this function requires to be called twice to preserve correct order on last column.
    }

    private bool TrySwappingOutOfPlaceSquares()
    {
        for (int i = 0; i < board.GetSize(); i++)
        {
            if (board.GetDistanceToDesiredOf(board.GetSquareOn(i, board.GetSize() - 1)) != (0, 0)) // if this square is out of place
            {
                for (int j = i + 1; j < board.GetSize(); j++)
                {
                    if (board.GetSquareOn(j, board.GetSize() - 1)!.Equals(board.GetSquareThatShouldBeOn(i, board.GetSize() - 1)))
                    {
                        SwapOnLastRow(i, j);
                        return true;
                    }
                }
            }
        }
        return false;
    }
    private void SolveLastRow()
    {
        int pairIdx = 0;

        bool isInInvalidState = false; // Swapping needs to be done twice everytime to preserve correctness of the last row.

        var first = board.GetSquareByOrder(board.GetTotalSquareCount() - board.GetSize()); // First square on last row
        var distance = board.GetDistanceToDesiredOf(first);
        board.Move(Direction.Horizontal, board.GetSize() - 1, distance.x); // Move the board so the first square is in correct position.

        while (!board.IsRowSolved(board.GetSize() - 1))
        {
            if (TrySwappingOutOfPlaceSquares()) isInInvalidState = !isInInvalidState;

            pairIdx++;

            first = board.GetSquareByOrder(board.GetTotalSquareCount() - board.GetSize()); // First square on last row
            distance = board.GetDistanceToDesiredOf(first);
            board.Move(Direction.Horizontal, board.GetSize() - 1, distance.x); // Move the board so the first square is in correct position.

            if (pairIdx > board.GetSize() * 100)
            {
                throw new Exception("Cannot solve board.");
            }
        }
    }

    public string[] GetSolution()
    {
        SolveAllButLastRow();
        SolveLastRow();
        return Array.Empty<string>();
    }
}
