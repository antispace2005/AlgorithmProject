namespace Algorithms;

public static class Task2
{
    private const char NUL = '\0';

    public static (bool possible, (int row, int col)[] dominoes) TileDominoes(
        int n,
        (int row, int col) missing1,
        (int row, int col) missing2)
    {
        // n x n board initialized with '\0' (NUL) by default
        char[,] board = new char[n, n];

        // Mark missing positions
        board[missing1.row, missing1.col] = 'X';
        board[missing2.row, missing2.col] = 'X';

        int elementCount = n * n;
        for (int i = 0; i < elementCount; i++)
        {
            int x = i / n;
            int y = i % n;
        }

        return (false, []);
    }

    private static bool FindSolution(char[,] board, int lastPlacedIndex, int n)
    {
        return false;
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
    public static (bool possible, (int row, int col)[] dominoes) tileDominoes(
        int n,
        (int row, int col) missing1,
        (int row, int col) missing2)
        => TileDominoes(n, missing1, missing2);
}
