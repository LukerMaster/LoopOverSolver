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

    public static void TestMoving()
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

        IBoardInterface<char> board = new BoardInterface<char>(mixedUpBoard, solvedBoard);

        board.Move(Direction.Vertical, 0, 1);
        board.Move(Direction.Vertical, 1, 2);
        board.Move(Direction.Vertical, 2, 3);
        board.Move(Direction.Vertical, 3, 4);
        board.Move(Direction.Vertical, 4, 5);

        Console.WriteLine(board);

    }
    public static void Main()
    {
        //Test4x4();
        //Test5x5();
        //TestMoving();
        TestRandom();
    }

    private static void TestRandom()
    {
        Random r = new Random(1337);
        for (int run = 0; run < 400; run++)
        {
            var sizeX = r.Next(2, 25);
            var sizeY = r.Next(2, 25);

            var board = new int[sizeY][];
            for (int i = 0; i < sizeY; i++)
            {
                board[i] = new int[sizeX];
            }
            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    board[y][x] = (y * sizeX) + x;
                }
            }

            var solved = new int[sizeY][];
            for (int i = 0; i < sizeY; i++)
            {
                solved[i] = new int[sizeX];
            }
            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    solved[y][x] = (y * sizeX) + x;
                }
            }


            IBoardInterface<int> boardInterface = new BoardInterface<int>(board, solved);

            for (int i = 0; i < r.Next(120); i++)
            {
                var direction = r.Next(2) % 2 == 0 ? Direction.Horizontal : Direction.Vertical;
                boardInterface.Move(direction, r.Next(boardInterface.GetSizeX() - 1), r.Next(10));
            }

            BoardSolver<int> solver = new BoardSolver<int>(boardInterface);

            try {
                var solution = solver.GetSolution();
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Green;
                if (!boardInterface.IsSolved())
                    Console.WriteLine($"FAILED RUN {run}. BOARD {boardInterface.GetSizeX() % 2 == 0} X {boardInterface.GetSizeY() % 2 == 0}.");
            }
            catch (Exception e)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERROR ON RUN {run}: {e}. Board:\n {boardInterface}");
            }

        }
    }
}