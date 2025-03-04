
using System.Drawing;

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
        //Test4x4();
        //Test5x5();
        TestRandom();
    }

    private static void TestRandom()
    {
        Random r = new Random(1337);
        for (int run = 0; run < 300; run++)
        {
            var size = r.Next(2, 60);

            var board = new int[size][];
            for (int i = 0; i < size; i++)
            {
                board[i] = new int[size];
            }
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    board[y][x] = (y * size) + x;
                }
            }

            var solved = new int[size][];
            for (int i = 0; i < size; i++)
            {
                solved[i] = new int[size];
            }
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    solved[y][x] = (y * size) + x;
                }
            }

            IBoardInterface<int> boardInterface = new BoardInterface<int>(board, solved);

            for (int i = 0; i < r.Next(120); i++)
            {
                var direction = r.Next(2) % 2 == 0 ? Direction.Horizontal : Direction.Vertical;
                boardInterface.Move(direction, r.Next(boardInterface.GetSize() - 1), r.Next(10));
            }

            BoardSolver<int> solver = new BoardSolver<int>(boardInterface);

            try {
                solver.GetSolution();
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"SUCCESS ON RUN {run}!");
            }
            catch (Exception e)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Fail on run {run}: {e}. Board:\n {boardInterface}");
            }

        }
    }
}