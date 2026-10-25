namespace Algorithms;

public static class Task3
{
    /// <summary>
    /// Turns off n security switches using the minimum number of moves.
    /// Rules:
    /// 1. The rightmost switch (Switch n) may be toggled at any time.
    /// 2. Any other switch m (1 <= m < n) may only be toggled when its immediate
    ///    right neighbor (Switch m + 1) is ON and all switches farther to its right
    ///    (Switches m + 2 .. n) are OFF.
    /// </summary>
    public static (int minMoves, List<int> toggles) turnOffSwitches(int n)
    {
        if (n <= 0) return (0, []);

        // Minimum moves: (2^(n+1) - 1) / 3 for odd n, (2^(n+1) - 2) / 3 for even n
        int expectedMoves = (int)(((1L << (n + 1)) - (n % 2 == 1 ? 1 : 2)) / 3);
        List<int> moves = new List<int>(Math.Min(expectedMoves, 2_000_000));

        TurnOff(n, n, moves);
        return (moves.Count, moves);
    }

    private static void TurnOff(int k, int n, List<int> moves)
    {
        if (k <= 0) return;

        if (k == 1)
        {
            moves.Add(n); // Rightmost switch
            return;
        }

        if (k == 2)
        {
            moves.Add(n - 1);
            moves.Add(n);
            return;
        }

        TurnOff(k - 2, n, moves);
        moves.Add(n - k + 1);
        TurnOn(k - 2, n, moves);
        TurnOff(k - 1, n, moves);
    }

    private static void TurnOn(int k, int n, List<int> moves)
    {
        if (k <= 0) return;

        if (k == 1)
        {
            moves.Add(n);
            return;
        }

        if (k == 2)
        {
            moves.Add(n);
            moves.Add(n - 1);
            return;
        }

        TurnOn(k - 1, n, moves);
        TurnOff(k - 2, n, moves);
        moves.Add(n - k + 1);
        TurnOn(k - 2, n, moves);
    }
}
