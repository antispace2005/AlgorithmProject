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

    // Lowercase alias matching the specification
    public static (bool possible, (int row, int col)[] dominoes) tileDominoes(
        int n,
        (int row, int col) missing1,
        (int row, int col) missing2)
        => TileDominoes(n, missing1, missing2);
}
