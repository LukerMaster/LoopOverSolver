using System.Numerics;

public class Program
{
    public enum Direction
    {
      Vertical,
      Horizontal
    }
     public static void Move(char[][] board, Direction direction, int column, int by = 1)
     {
       by = by % board.Length;
       column = column % board.Length;
       if (by == 0) return;

       if (direction is Direction.Horizontal)
       {
         if (by < 0) by = board.Length + by; // Moving right is effectively moving left by "negated" amount.
         
         char[] buffer = new char[by];
         Array.Copy(board[column], board.Length - by, buffer, 0, by);
         Array.Copy(board[column], 0, board[column], by, board.Length - by);
         Array.Copy(buffer, board[column], by);
         
       }
       if (direction is Direction.Vertical)
       {
          by = -by; // Necessary to make UP direction be negative (to align with debug representations).
          if (by < 0) by = board.Length + by; // Moving up is effectively moving down by "negated" amount.

          char[] buffer = new char[by];
          for (int i = 0; i < by; i++)
          {
          Array.Copy(board[i], column, buffer, i, 1);
          }
          for (int i = by; i < board.Length; i++)
          {
          Array.Copy(board[i], column, board[i-by], column, 1);
          }
          for (int i = 0; i < buffer.Length; i++)
          {
          Array.Copy(buffer, i, board[board.Length - by + i], column, 1);
          }
       }
     } 

/// <summary>
/// Makes a flattened list of which elements should be focused on ordering first.
/// </summary>
/// <param name="solvedBoard"></param>
/// <returns></returns>
     public static List<char> CreateOrderList(char[][] solvedBoard)
     {
       return solvedBoard.SelectMany(x => x).ToList();
     }
     public static (int x, int y) GetCoordsOf(char letter, char[][] board)
     {
      for (int y = 0; y < board.Length; y++)
      {
        for (int x = 0; x < board.Length; x++)
        {
          if (letter == board[y][x]) return (x, y);
        }
      }
      throw new Exception("Letter not found!");
     }
     public static (int x, int y) GetDesiredPositionOf(char letter, List<char> orderList, char[][] board)
     {
       var idx = orderList.IndexOf(letter);
       return (idx % board.Length, idx / board.Length);
     }

     public static (int x, int y) GetDistanceToDesired(char letter, List<char> orderList, char[][] board)
     {
      var desired = GetDesiredPositionOf(letter, orderList, board);
      var actual = GetCoordsOf(letter, board);
      return (desired.x - actual.x, desired.y - actual.y);
     }

     public static void FillMiddleRows(char[][] board, List<char> orderList)
     {
      for (int i = 0; i < orderList.Count - board.Length /* stop at second to last row*/; i++)
      {
        var current = GetCoordsOf(orderList[i], board);
        var distance = GetDistanceToDesired(orderList[i], orderList, board);
        var desired = GetDesiredPositionOf(orderList[i], orderList, board);
        
        // If on the same row, first move out of the way
        if (current.x == desired.x)
        {
          if (current.y == desired.y) continue;
          else
          {
            Move(board, Direction.Horizontal, current.y, 1);
            current = GetCoordsOf(orderList[i], board);
            distance = GetDistanceToDesired(orderList[i], orderList, board);
            desired = GetDesiredPositionOf(orderList[i], orderList, board);
          }
        }
        // If on the same column, first move out of the way
        if (current.y == desired.y)
        {
          if (current.x == desired.x) continue;
          else
          {
            Move(board, Direction.Vertical, current.x, 1);
            Move(board, Direction.Horizontal, current.y + 1, 1);
            Move(board, Direction.Vertical, current.x, -1);
            current = GetCoordsOf(orderList[i], board);
            distance = GetDistanceToDesired(orderList[i], orderList, board);
            desired = GetDesiredPositionOf(orderList[i], orderList, board);
          }
        }
        // At this point, the element is not aligned with any already-solved axis, so we can proceed with standard algorithm

        Move(board, Direction.Vertical, desired.x, -distance.y);
        Move(board, Direction.Horizontal, current.y, distance.x);
        Move(board, Direction.Vertical, desired.x, distance.y);
        
      }
     }

#region LastRow

     public static bool IsRowContinous(char[][] board, int rowIdx, List<char> orderList)
     {
        return false;
     }
#endregion
     public static void FillLastRow(char[][] board, List<char> orderList)
     {
        // Move first to be at its correct position.
        var first = orderList[orderList.Count - board.Length];
        var firstPos = GetCoordsOf(first, board);
        var firstDistance = GetDistanceToDesired(first, orderList, board);
        Move(board, Direction.Horizontal, firstPos.y, firstDistance.x);


        for (int i = orderList.Count - board.Length + 1; i < orderList.Count; i++)
        {
          var current = GetCoordsOf(orderList[i], board);
          var distance = GetDistanceToDesired(orderList[i], orderList, board);
          if (distance.x == 0) continue; // Already in correct place

          Move(board, Direction.Horizontal, current.y, board.Length - current.x);
          Move(board, Direction.Vertical, board.Length, 1);
        }
     }
     public static List<string> Solve(char[][] mixedUpBoard, char[][] solvedBoard)
     {
       var orderList = CreateOrderList(solvedBoard);
       FillMiddleRows(mixedUpBoard, orderList);

       return new List<string>();
     }


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

        Move(mixedUpBoard, Direction.Vertical, 0, -1);
        Move(mixedUpBoard, Direction.Horizontal, 1, -2);
        Move(mixedUpBoard, Direction.Vertical, 2, -3);
        Move(mixedUpBoard, Direction.Vertical, 3, -4);
        Move(mixedUpBoard, Direction.Horizontal, 4, -3);
        Move(mixedUpBoard, Direction.Vertical, 2, -4);
        Move(mixedUpBoard, Direction.Horizontal, 4, -3);
        Move(mixedUpBoard, Direction.Horizontal, 1, -2);
        Move(mixedUpBoard, Direction.Vertical, 4, -2);
        Move(mixedUpBoard, Direction.Horizontal, 3, -4);
        Move(mixedUpBoard, Direction.Vertical, 2, -7);

        Solve(mixedUpBoard, solvedBoard);
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

        Move(mixedUpBoard, Direction.Vertical, 0, -1);
        Move(mixedUpBoard, Direction.Horizontal, 1, -2);
        Move(mixedUpBoard, Direction.Vertical, 2, -2);
        Move(mixedUpBoard, Direction.Vertical, 3, -1);
        Move(mixedUpBoard, Direction.Horizontal, 4, -3);
        Move(mixedUpBoard, Direction.Vertical, 2, -4);
        Move(mixedUpBoard, Direction.Horizontal, 4, -3);
        Move(mixedUpBoard, Direction.Horizontal, 1, -2);
        Move(mixedUpBoard, Direction.Vertical, 4, -2);
        Move(mixedUpBoard, Direction.Horizontal, 3, -4);
        Move(mixedUpBoard, Direction.Vertical, 2, -7);

        Solve(mixedUpBoard, solvedBoard);
    }
    public static void Main()
    {
        Test4x4();
        Test5x5();
    }
}

