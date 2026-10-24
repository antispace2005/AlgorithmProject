using System.Diagnostics;
using Algorithms;

namespace TestSuit;

public class Task2Tests
{
    private static void PrintBoard(char[,] board, int n)
    {
        if (board == null || board.GetLength(0) == 0)
        {
            TestContext.Out.WriteLine("  [No Solution / Board Empty]");
            return;
        }

        string horizontalBorder = "  +" + string.Join("+", Enumerable.Repeat("---", n)) + "+";
        TestContext.Out.WriteLine(horizontalBorder);

        for (int r = 0; r < n; r++)
        {
            var rowCells = new List<string>();
            for (int c = 0; c < n; c++)
            {
                char ch = board[r, c];
                string display = ch == '\0' ? " " : ch.ToString();
                rowCells.Add($" {display} ");
            }
            TestContext.Out.WriteLine("  |" + string.Join("|", rowCells) + "|");
            TestContext.Out.WriteLine(horizontalBorder);
        }
    }

    [Test]
    public void Test_ImpossibleCase_5x5()
    {
        int n = 5;
        (int row, int col) missing1 = (0, 0);
        (int row, int col) missing2 = (1, 1);

        TestContext.Out.WriteLine($"=== Test 1: Impossible Case ({n}x{n}) ===");
        TestContext.Out.WriteLine($"Missing 1: {missing1}, Missing 2: {missing2}");
        TestContext.Out.WriteLine($"Reason: Total remaining squares ({n * n - 2} = 23) is odd, cannot be tiled by 2x1 dominoes.");

        var sw = Stopwatch.StartNew();
        var (possible, board) = Task2.TileDominoes(n, missing1, missing2);
        sw.Stop();

        TestContext.Out.WriteLine($"Result Possible: {possible}");
        TestContext.Out.WriteLine($"Time elapsed: {sw.ElapsedMilliseconds} ms ({sw.Elapsed.TotalMicroseconds:F1} µs)");
        PrintBoard(board, n);

        Assert.That(possible, Is.False);
        Assert.That(board.GetLength(0), Is.EqualTo(0));
    }

    [Test]
    public void Test_SolvableCase_4x4()
    {
        int n = 4;
        (int row, int col) missing1 = (0, 0);
        (int row, int col) missing2 = (0, 1);

        TestContext.Out.WriteLine($"=== Test 2: Solvable Case ({n}x{n}) ===");
        TestContext.Out.WriteLine($"Missing 1: {missing1}, Missing 2: {missing2}");

        var sw = Stopwatch.StartNew();
        var (possible, board) = Task2.TileDominoes(n, missing1, missing2);
        sw.Stop();

        TestContext.Out.WriteLine($"Result Possible: {possible}");
        TestContext.Out.WriteLine($"Time elapsed: {sw.ElapsedMilliseconds} ms ({sw.Elapsed.TotalMicroseconds:F1} µs)");
        PrintBoard(board, n);

        Assert.That(possible, Is.True);
        Assert.That(board.GetLength(0), Is.EqualTo(n));
    }

    [Test]
    public void Test_WorstCase_MutilatedChessboard_8x8()
    {
        int n = 8;
        // Two opposite corners: (0,0) and (7,7) have the same color ((0+0)%2 == 0 and (7+7)%2 == 0)
        // 62 squares (even), but color mismatch forces backtracking to exhaust all permutations.
        (int row, int col) missing1 = (0, 0);
        (int row, int col) missing2 = (7, 7);

        TestContext.Out.WriteLine($"=== Test 3: Worst-Case Backtracking ({n}x{n} Mutilated Chessboard) ===");
        TestContext.Out.WriteLine($"Missing 1: {missing1}, Missing 2: {missing2}");
        TestContext.Out.WriteLine("Reason: Both missing cells share the same color. Even cell count (62) forces exhaustive search.");

        var sw = Stopwatch.StartNew();
        var (possible, board) = Task2.TileDominoes(n, missing1, missing2);
        sw.Stop();

        TestContext.Out.WriteLine($"Result Possible: {possible}");
        TestContext.Out.WriteLine($"Time elapsed: {sw.ElapsedMilliseconds} ms ({sw.Elapsed.TotalMicroseconds:F1} µs)");
        PrintBoard(board, n);

        Assert.That(possible, Is.False);
        Assert.That(board.GetLength(0), Is.EqualTo(0));
    }

    [Test]
    public void Test_SolvableCase_8x8_Timing()
    {
        int n = 8;
        (int row, int col) missing1 = (0, 0);
        (int row, int col) missing2 = (0, 1);

        TestContext.Out.WriteLine($"=== Test 4: Solvable Large Board ({n}x{n}) ===");
        TestContext.Out.WriteLine($"Missing 1: {missing1}, Missing 2: {missing2}");

        var sw = Stopwatch.StartNew();
        var (possible, board) = Task2.TileDominoes(n, missing1, missing2);
        sw.Stop();

        TestContext.Out.WriteLine($"Result Possible: {possible}");
        TestContext.Out.WriteLine($"Time elapsed: {sw.ElapsedMilliseconds} ms ({sw.Elapsed.TotalMicroseconds:F1} µs)");
        PrintBoard(board, n);

        Assert.That(possible, Is.True);
        Assert.That(board.GetLength(0), Is.EqualTo(n));
    }
}
