using System.Diagnostics;
using Algorithms;

namespace TestSuit;

public class Task3Tests
{
    private static void ValidateMoves(int n, List<int> moves)
    {
        // Initial state: all n switches are ON (true)
        bool[] state = new bool[n + 1];
        for (int i = 1; i <= n; i++)
        {
            state[i] = true;
        }

        for (int step = 0; step < moves.Count; step++)
        {
            int m = moves[step];
            Assert.That(m, Is.InRange(1, n), $"Move {step + 1}: Switch {m} out of range [1, {n}]");

            if (m == n)
            {
                // Rightmost switch (Switch n) can always be toggled
                state[n] = !state[n];
            }
            else
            {
                // Any other switch m can be toggled iff immediate right neighbor (m + 1) is ON
                // and all switches farther to its right (m + 2 .. n) are OFF.
                Assert.That(state[m + 1], Is.True,
                    $"Illegal Move at step {step + 1} (toggle S{m}): immediate right neighbor S{m + 1} is OFF, but must be ON.");

                for (int j = m + 2; j <= n; j++)
                {
                    Assert.That(state[j], Is.False,
                        $"Illegal Move at step {step + 1} (toggle S{m}): switch S{j} (farther to its right) is ON, but must be OFF.");
                }

                state[m] = !state[m];
            }
        }

        // Final state: all switches must be OFF (false)
        for (int i = 1; i <= n; i++)
        {
            Assert.That(state[i], Is.False, $"Switch S{i} is still ON at the end of the sequence!");
        }
    }

    private static void PrintSwitchesProgression(int n, List<int> moves)
    {
        // Initial state: all n switches are ON (1)
        bool[] state = new bool[n + 1];
        for (int i = 1; i <= n; i++) state[i] = true;

        string switchHeader = string.Join("  ", Enumerable.Range(1, n).Select(i => $"S{i}"));
        TestContext.Out.WriteLine($"                    Switches : [ {switchHeader} ]");
        TestContext.Out.WriteLine($"Initial State                : [ {FormatSwitchState(state, n)} ]");

        for (int step = 0; step < moves.Count; step++)
        {
            int m = moves[step];
            state[m] = !state[m];
            string action = state[m] ? "Turn ON " : "Turn OFF";
            TestContext.Out.WriteLine($"Step {step + 1,2}: {action} S{m,-2} -> State : [ {FormatSwitchState(state, n)} ]");
        }
        TestContext.Out.WriteLine();
    }

    private static string FormatSwitchState(bool[] state, int n)
    {
        return string.Join("   ", Enumerable.Range(1, n).Select(i => state[i] ? "1" : "0"));
    }

    private static string FormatMoves(List<int> moves)
    {
        if (moves.Count <= 50)
        {
            return $"[{string.Join(", ", moves)}]";
        }

        // For very large sequences (e.g. n=20, n=21 with >1M moves), format head and tail
        return $"[{string.Join(", ", moves.Take(25))}, ... <{moves.Count - 50:N0} moves> ..., {string.Join(", ", moves.TakeLast(25))}]";
    }

    [TestCase(1, 1)]
    [TestCase(2, 2)]
    [TestCase(3, 5)]
    [TestCase(4, 10)]
    public void Test_SmallCases(int n, int expectedMinMoves)
    {
        TestContext.Out.WriteLine($"=== Test Case: n = {n} ===");
        var sw = Stopwatch.StartNew();
        var (minMoves, moves) = Task3.turnOffSwitches(n);
        sw.Stop();

        TestContext.Out.WriteLine($"Reported minMoves : {minMoves:N0}");
        TestContext.Out.WriteLine($"Actual moves count: {moves.Count:N0}");
        TestContext.Out.WriteLine($"Moves sequence    : {FormatMoves(moves)}");
        TestContext.Out.WriteLine($"Time elapsed      : {sw.ElapsedMilliseconds} ms ({sw.Elapsed.TotalMicroseconds:F1} µs)");

        Assert.That(minMoves, Is.EqualTo(expectedMinMoves));
        Assert.That(moves.Count, Is.EqualTo(expectedMinMoves));
        ValidateMoves(n, moves);
    }

    [Test]
    public void Test_StepByStepProgression_n5()
    {
        int n = 5;
        int expectedMinMoves = (int)(((1L << 6) - 1) / 3); // 21

        TestContext.Out.WriteLine($"=== Test Case: n = {n} (Step-by-Step Progression) ===");
        var sw = Stopwatch.StartNew();
        var (minMoves, moves) = Task3.turnOffSwitches(n);
        sw.Stop();

        TestContext.Out.WriteLine($"Expected minMoves : {expectedMinMoves}");
        TestContext.Out.WriteLine($"Reported minMoves : {minMoves}");
        TestContext.Out.WriteLine($"Moves sequence    : {FormatMoves(moves)}");
        TestContext.Out.WriteLine($"Time elapsed      : {sw.ElapsedMilliseconds} ms ({sw.Elapsed.TotalMicroseconds:F1} µs)");
        TestContext.Out.WriteLine();

        PrintSwitchesProgression(n, moves);

        Assert.That(minMoves, Is.EqualTo(expectedMinMoves));
        Assert.That(moves.Count, Is.EqualTo(expectedMinMoves));
        ValidateMoves(n, moves);
    }

    [Test]
    public void Test_StepByStepProgression_n6()
    {
        int n = 6;
        int expectedMinMoves = (int)(((1L << 7) - 2) / 3); // 42

        TestContext.Out.WriteLine($"=== Test Case: n = {n} (Step-by-Step Progression) ===");
        var sw = Stopwatch.StartNew();
        var (minMoves, moves) = Task3.turnOffSwitches(n);
        sw.Stop();

        TestContext.Out.WriteLine($"Expected minMoves : {expectedMinMoves}");
        TestContext.Out.WriteLine($"Reported minMoves : {minMoves}");
        TestContext.Out.WriteLine($"Moves sequence    : {FormatMoves(moves)}");
        TestContext.Out.WriteLine($"Time elapsed      : {sw.ElapsedMilliseconds} ms ({sw.Elapsed.TotalMicroseconds:F1} µs)");
        TestContext.Out.WriteLine();

        PrintSwitchesProgression(n, moves);

        Assert.That(minMoves, Is.EqualTo(expectedMinMoves));
        Assert.That(moves.Count, Is.EqualTo(expectedMinMoves));
        ValidateMoves(n, moves);
    }

    [Test]
    public void Test_LargeCase_n20()
    {
        int n = 20;
        int expectedMinMoves = (int)(((1L << 21) - 2) / 3); // 699,050

        TestContext.Out.WriteLine($"=== Test Case: n = {n} (Large Case) ===");
        var sw = Stopwatch.StartNew();
        var (minMoves, moves) = Task3.turnOffSwitches(n);
        sw.Stop();

        TestContext.Out.WriteLine($"Expected minMoves : {expectedMinMoves:N0}");
        TestContext.Out.WriteLine($"Reported minMoves : {minMoves:N0}");
        TestContext.Out.WriteLine($"Actual moves count: {moves.Count:N0}");
        TestContext.Out.WriteLine($"Moves preview     : {FormatMoves(moves)}");
        TestContext.Out.WriteLine($"Generation Time   : {sw.ElapsedMilliseconds} ms ({sw.Elapsed.TotalMicroseconds:F1} µs)");

        Assert.That(minMoves, Is.EqualTo(expectedMinMoves));
        Assert.That(moves.Count, Is.EqualTo(expectedMinMoves));

        var valSw = Stopwatch.StartNew();
        ValidateMoves(n, moves);
        valSw.Stop();
        TestContext.Out.WriteLine($"Validation Time   : {valSw.ElapsedMilliseconds} ms ({valSw.Elapsed.TotalMicroseconds:F1} µs)");
    }

    [Test]
    public void Test_LargeCase_n21()
    {
        int n = 21;
        int expectedMinMoves = (int)(((1L << 22) - 1) / 3); // 1,398,101

        TestContext.Out.WriteLine($"=== Test Case: n = {n} (Large Case) ===");
        var sw = Stopwatch.StartNew();
        var (minMoves, moves) = Task3.turnOffSwitches(n);
        sw.Stop();

        TestContext.Out.WriteLine($"Expected minMoves : {expectedMinMoves:N0}");
        TestContext.Out.WriteLine($"Reported minMoves : {minMoves:N0}");
        TestContext.Out.WriteLine($"Actual moves count: {moves.Count:N0}");
        TestContext.Out.WriteLine($"Moves preview     : {FormatMoves(moves)}");
        TestContext.Out.WriteLine($"Generation Time   : {sw.ElapsedMilliseconds} ms ({sw.Elapsed.TotalMicroseconds:F1} µs)");

        Assert.That(minMoves, Is.EqualTo(expectedMinMoves));
        Assert.That(moves.Count, Is.EqualTo(expectedMinMoves));

        var valSw = Stopwatch.StartNew();
        ValidateMoves(n, moves);
        valSw.Stop();
        TestContext.Out.WriteLine($"Validation Time   : {valSw.ElapsedMilliseconds} ms ({valSw.Elapsed.TotalMicroseconds:F1} µs)");
    }
}
