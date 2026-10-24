using Algorithms;

namespace TestSuit;

public class Task1Tests
{
    [SetUp]
    public void Setup()
    {
    }

    private static void PrintBoardProgression(char[] initialCoins, int[] flips)
    {
        char[] state = initialCoins.ToArray();
        TestContext.Out.WriteLine($"Initial : [{FormatBoard(state)}]");

        for (int step = 0; step < flips.Length; step++)
        {
            int idx = flips[step];

            // Flip neighbors
            if (idx > 0)
            {
                if (state[idx - 1] == 'H') state[idx - 1] = 'T';
                else if (state[idx - 1] == 'T') state[idx - 1] = 'H';
            }
            if (idx < state.Length - 1)
            {
                if (state[idx + 1] == 'H') state[idx + 1] = 'T';
                else if (state[idx + 1] == 'T') state[idx + 1] = 'H';
            }

            // Remove selected coin
            state[idx] = '\0';

            TestContext.Out.WriteLine($"Step {step + 1}  : [{FormatBoard(state)}]");
        }
        TestContext.Out.WriteLine();
    }

    private static string FormatBoard(char[] coins)
    {
        // Renders '\0' as empty space/blank like [, T]
        return string.Join(", ", coins.Select(c => c == '\0' ? "" : c.ToString()));
    }

    [Test]
    public void Test_CoinsIsEmpty()
    {
        int n = 0;
        char[] coins = [];

        var (possible, flips) = Task1.RemoveCoins(n, coins);

        TestContext.Out.WriteLine("--- Test: Empty Coins ---");
        TestContext.Out.WriteLine($"Possible: {possible}");
        PrintBoardProgression(coins, flips);

        Assert.That(possible, Is.True);
        Assert.That(flips, Is.Empty);
    }

    [Test]
    public void Test_CoinsWontBeSolved()
    {
        int n = 3;
        char[] coins = ['T', 'T', 'T'];

        var (possible, flips) = Task1.RemoveCoins(n, coins);

        TestContext.Out.WriteLine("--- Test: All Tails (Unsolvable) ---");
        TestContext.Out.WriteLine($"Possible: {possible}");
        PrintBoardProgression(coins, flips);

        Assert.That(possible, Is.False);
        Assert.That(flips, Is.Empty);
    }

    [Test]
    public void Test_SolvableCase_AlgorithmSucceeds()
    {
        int n = 2;
        char[] coins = ['H', 'T'];

        var (possible, flips) = Task1.RemoveCoins(n, coins);

        TestContext.Out.WriteLine("--- Test: Solvable (Algorithm Succeeds) ---");
        TestContext.Out.WriteLine($"Possible: {possible}");
        PrintBoardProgression(coins, flips);

        Assert.That(possible, Is.True);
        Assert.That(flips.Length, Is.EqualTo(n));
    }

    [Test]
    public void Test_SolvableCase_WhereGreedyAlgorithmFails()
    {
        // Board: ['H', 'H', 'T', 'T', 'H', 'T']
        // A global solver can clear this board, but greedy local choices
        // leave stranded 'T' coins and return false.
        int n = 7;
        char[] coins = ['H', 'T', 'T', 'H', 'T', 'T', 'H'];

        var (possible, flips) = Task1.RemoveCoins(n, coins);

        TestContext.Out.WriteLine("--- Test: Solvable (Greedy Fails) ---");
        TestContext.Out.WriteLine($"Possible: {possible}");
        PrintBoardProgression(coins, flips);

        Assert.That(possible, Is.False);
    }
}
