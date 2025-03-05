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

    public static void TestCodeWars()
    {
        var solvedBoard = new []
        {
            new [] {'A', 'B', 'C', 'D', 'E', 'F'},
            new [] {'G', 'H', 'I', 'J', 'K', 'L'},
            new [] {'M', 'N', 'O', 'P', 'Q', 'R'},
            new [] {'S', 'T', 'U', 'V', 'W', 'X'},
            new [] {'Y', 'Z', '0', '1', '2', '3'},
            new [] {'4', '5', '6', '7', '8', '9'},
        };

        var mixedUpBoard = new []
        {
            new [] {'W', 'C', 'M', 'D', 'J', '0'},
            new [] {'O', 'R', 'F', 'B', 'A', '1'},
            new [] {'K', 'N', 'G', 'L', 'Y', '2'},
            new [] {'P', 'H', 'V', 'S', 'E', '3'},
            new [] {'T', 'X', 'Q', 'U', 'I', '4'},
            new [] {'Z', '5', '6', '7', '8', '9'},
        };

        var mixedUpBoard2 = new []
        {
            new [] {'W', 'C', 'M', 'D', 'J', '0'},
            new [] {'O', 'R', 'F', 'B', 'A', '1'},
            new [] {'K', 'N', 'G', 'L', 'Y', '2'},
            new [] {'P', 'H', 'V', 'S', 'E', '3'},
            new [] {'T', 'X', 'Q', 'U', 'I', '4'},
            new [] {'Z', '5', '6', '7', '8', '9'},
        };

        IBoardInterface<char> boardInterface = new BoardInterface<char>(mixedUpBoard, solvedBoard);
        BoardSolver<char> solver = new BoardSolver<char>(boardInterface);
        var solution = solver.GetSolution();
        if (boardInterface.IsSolved())
            Console.WriteLine("SOLVED!");

        IBoardInterface<char> boardInterfaceTest = new BoardInterface<char>(mixedUpBoard2, solvedBoard);
        BoardSolver<char> solverTest = new BoardSolver<char>(boardInterfaceTest);
        solverTest.ApplySolution(solution);
        if (boardInterfaceTest.IsSolved())
        {
            Console.WriteLine("SOLUTION TESTED!");
        }
        else
        {
            Console.WriteLine($"Final board:\n {boardInterfaceTest}");
        }
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
        TestCodeWars();
        //TestRandom();
    }

    private static void TestRandom()
    {
        Random r = new Random(1337);
        for (int run = 0; run < 900; run++)
        {
            var sizeX = r.Next(2, 55);
            var sizeY = r.Next(2, 55);

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

            if (r.Next(10) == 9)
            {
                Console.WriteLine("Feeding Unsolvable.");
                solved[sizeY - 1][sizeX - 1] = 0;
                solved[0][0] = 0;
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
                Console.WriteLine($"ERROR ON RUN {run}: {e.Message}.");
            }

        }
    }
}