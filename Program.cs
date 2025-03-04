public class Program
{
    public static void Test5x5()
    {
        var solvedBoard = new []
        {
            new [] {'A', 'B', 'C', 'D', 'E'},
            new [] {'F', 'G', 'H', 'I', 'J'},
            new [] {'K', 'L', 'M', 'N', 'O'},
            new [] {'P', 'Q', 'R', 'S', 'T'},
            new [] {'U', 'V', 'W', 'X', 'Y'},
        };

        var mixedUpBoard = new []
        {
            new [] {'A', 'B', 'C', 'D', 'E'},
            new [] {'F', 'G', 'H', 'I', 'J'},
            new [] {'K', 'L', 'M', 'N', 'O'},
            new [] {'P', 'Q', 'R', 'S', 'T'},
            new [] {'U', 'V', 'W', 'X', 'Y'},
        };

        IBoardInterface<char> boardInterface = new BoardInterface<char>(mixedUpBoard, solvedBoard);

        boardInterface.Move(Direction.Vertical, 0, -1);
        boardInterface.Move(Direction.Horizontal, 1, -2);
        boardInterface.Move(Direction.Vertical, 2, -3);
        boardInterface.Move(Direction.Vertical, 3, -4);
        boardInterface.Move(Direction.Horizontal, 4, -3);
        boardInterface.Move(Direction.Vertical, 2, -4);
        boardInterface.Move(Direction.Horizontal, 4, -3);
        boardInterface.Move(Direction.Horizontal, 1, -2);
        boardInterface.Move(Direction.Vertical, 4, -2);
        boardInterface.Move(Direction.Horizontal, 3, -4);
        boardInterface.Move(Direction.Vertical, 2, -7);

        BoardSolver<char> boardSolver = new BoardSolver<char>(boardInterface);

        boardSolver.GetSolution();

        
    }

    public static void Test4x4()
    {
        var solvedBoard =  new []
        {
            new [] {'A', 'B', 'C', 'D'},
            new [] {'E', 'F', 'G', 'H'},
            new [] {'I', 'J', 'K', 'L'},
            new [] {'M', 'N', 'O', 'P'},
        };

        var mixedUpBoard = new []
        {
            new [] {'A', 'B', 'C', 'D'},
            new [] {'E', 'F', 'G', 'H'},
            new [] {'I', 'J', 'K', 'L'},
            new [] {'M', 'N', 'O', 'P'},
        };

        IBoardInterface<char> boardInterface = new BoardInterface<char>(mixedUpBoard, solvedBoard);

        boardInterface.Move(Direction.Vertical, 0, -1);
        boardInterface.Move(Direction.Horizontal, 1, -2);
        boardInterface.Move(Direction.Vertical, 2, -2);
        boardInterface.Move(Direction.Vertical, 3, -1);
        boardInterface.Move(Direction.Horizontal, 4, -3);
        boardInterface.Move(Direction.Vertical, 2, -4);
        boardInterface.Move(Direction.Horizontal, 4, -3);
        boardInterface.Move(Direction.Horizontal, 1, -2);
        boardInterface.Move(Direction.Vertical, 4, -2);
        boardInterface.Move(Direction.Horizontal, 3, -4);
        boardInterface.Move(Direction.Vertical, 2, -7);

        BoardSolver<char> boardSolver = new BoardSolver<char>(boardInterface);

        boardSolver.GetSolution();

    }
    public static void Main()
    {
        Test4x4();
        Test5x5();
    }
}