public class BoardSolver<T>(IBoardInterface<T> board)
{
    private void SolveAllButLastRow()
    {
        for (int i = 0; i < board.GetTotalSquareCount() - board.GetSize() /* stop at second to last row*/; i++)
        {
            var square = board.GetSquareByOrder(i);

            var current = board.GetPositionOf(square);
            var distance = board.GetDistanceToDesiredOf(square);
            var desired = board.GetDesiredPositionOf(square);
        
            // If on the same row, first move out of the way
            if (current.x == desired.x)
            {
                if (current.y == desired.y) continue;
                else
                {
                    board.Move(Direction.Horizontal, current.y, 1);
                    current = board.GetPositionOf(square);
                    distance = board.GetDistanceToDesiredOf(square);
                    desired = board.GetDesiredPositionOf(square);
                }
            }
            // If on the same column, first move out of the way
            if (current.y == desired.y)
            {
                if (current.x == desired.x) continue;
                else
                {
                    board.Move(Direction.Vertical, current.x, 1);
                    board.Move(Direction.Horizontal, current.y + 1, 1);
                    board.Move(Direction.Vertical, current.x, -1);
                    current = board.GetPositionOf(square);
                    distance = board.GetDistanceToDesiredOf(square);
                    desired = board.GetDesiredPositionOf(square);
                }
            }

            // At this point, the element is not aligned with any already-solved axis, so we can proceed with standard algorithm
            board.Move(Direction.Vertical, desired.x, -distance.y);
            board.Move(Direction.Horizontal, current.y, distance.x);
            board.Move(Direction.Vertical, desired.x, distance.y);
        }
    }

    public string[] GetSolution()
    {
        SolveAllButLastRow();
        return Array.Empty<string>();
    }
}
