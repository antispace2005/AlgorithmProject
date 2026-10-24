namespace Algorithms;

public static class Task2
{
    private const char NUL = '\0';

    public static (bool possible, char[,] board) TileDominoes(
        int n,
        (int row, int col) missing1,
        (int row, int col) missing2)
    {
        // n x n board initialized with '\0' (NUL) by default
        char[,] board = new char[n, n];

        // Mark missing positions
        board[missing1.row, missing1.col] = 'X';
        board[missing2.row, missing2.col] = 'X';

        var (possible, resultBoard) = FindSolution(board, (char)('1' - 1), n);
        if (!possible || resultBoard == null)
        {
            return (false, new char[0, 0]);
        }

        return (true, resultBoard);
    }

    private static (bool possible, char[,]? board) FindSolution(char[,] board, char lastValue, int n)
    {
        // Find the next empty index in order
        int nextIndex = -1;
        int totalElements = n * n;
        for (int i = 0; i < totalElements; i++)
        {
            int r = i / n;
            int c = i % n;
            if (board[r, c] == NUL)
            {
                nextIndex = i;
                break;
            }
        }

        // All slots filled successfully
        if (nextIndex == -1)
        {
            return (true, board);
        }

        int x = nextIndex / n;
        int y = nextIndex % n;
        char nextValue = (char)(lastValue + 1);

        char[,]? hBoard = Move(board, x, y, isHorizontal: true, nextValue, n);
        char[,]? vBoard = Move(board, x, y, isHorizontal: false, nextValue, n);

        // Both placements invalid
        if (hBoard == null && vBoard == null)
        {
            return (false, null);
        }

        // Try horizontal placement first
        if (hBoard != null)
        {
            var (possible, resultBoard) = FindSolution(hBoard, nextValue, n);
            if (possible)
            {
                return (true, resultBoard);
            }
        }

        // If horizontal placement fails, try vertical placement
        if (vBoard != null)
        {
            var (possible, resultBoard) = FindSolution(vBoard, nextValue, n);
            if (possible)
            {
                return (true, resultBoard);
            }
        }

        return (false, null);
    }

    /// <summary>
    /// Places a 2x1 domino on the board.
    /// If horizontal: places at (x, y) and (x, y + 1).
    /// If vertical: places at (x, y) and (x + 1, y).
    /// Returns a new copy of the board with the domino placed, or null if invalid.
    /// </summary>
    private static char[,]? Move(char[,] board, int x, int y, bool isHorizontal, char value, int n)
    {
        if (x < 0 || x >= n || y < 0 || y >= n)
            return null;

        if (isHorizontal)
        {
            if (y + 1 >= n) return null;
            if (board[x, y] != NUL || board[x, y + 1] != NUL) return null;

            char[,] newBoard = (char[,])board.Clone();
            newBoard[x, y] = value;
            newBoard[x, y + 1] = value;
            return newBoard;
        }
        else
        {
            if (x + 1 >= n) return null;
            if (board[x, y] != NUL || board[x + 1, y] != NUL) return null;

            char[,] newBoard = (char[,])board.Clone();
            newBoard[x, y] = value;
            newBoard[x + 1, y] = value;
            return newBoard;
        }
    }

    // Lowercase alias matching the specification
    public static (bool possible, char[,] board) tileDominoes(
        int n,
        (int row, int col) missing1,
        (int row, int col) missing2)
        => TileDominoes(n, missing1, missing2);
}
