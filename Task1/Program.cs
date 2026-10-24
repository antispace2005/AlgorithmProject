Console.WriteLine("Task 1");

public partial class Program
{
    public static (bool possible, int[] flips) RemoveCoins(int n, char[] coins)
    {
        // Priority is which results the most heads up after the flip then which one is closest to an edge
        // Maximum number of loops is n, assuming coins contains T, H and '\0' as empty slot
        
        coins = coins.ToArray(); // copy
        int[] flips = new int[n];

        for (int mainLoop = 0; mainLoop < n; mainLoop++)
        {
            var best = (Index: -1, Value: -3, EdgeDistance: int.MaxValue);

            // Reusable pass function for both forward (left-to-right) and backward (right-to-left)
            void ScanPass(bool forward)
            {
                int currentEdge = forward ? -1 : n;
                int start = forward ? 0 : n - 1;
                int end = forward ? n : -1;
                int step = forward ? 1 : -1;

                for (int i = start; i != end; i += step)
                {
                    switch (coins[i])
                    {
                        case '\0':
                            currentEdge = i;
                            continue;

                        case 'T':
                            continue;

                        case 'H':
                            int tempValue = 0;
                            int tempEdgeDistance = Math.Abs(currentEdge - i) - 1;

                            if (i != 0)
                            {
                                switch (coins[i - 1])
                                {
                                    case 'H': tempValue--; break;
                                    case 'T': tempValue++; break;
                                }
                            }
                            if (i < n - 1)
                            {
                                switch (coins[i + 1])
                                {
                                    case 'H': tempValue--; break;
                                    case 'T': tempValue++; break;
                                }
                            }

                            if (tempValue > best.Value || (tempValue == best.Value && tempEdgeDistance < best.EdgeDistance))
                            {
                                best = (i, tempValue, tempEdgeDistance);
                            }
                            break;
                    }
                }
            }

            // --- Forward Pass (Left to Right) ---
            ScanPass(forward: true);

            // --- Backward Pass (Right to Left) ---
            ScanPass(forward: false);

            // If no 'H' was found to remove, check if all coins were cleared
            if (best.Index == -1)
            {
                bool allCleared = coins.All(c => c == '\0');
                if (allCleared)
                {
                    return (true, flips.Take(mainLoop).ToArray());
                }
                return (false, []);
            }

            // Record the chosen index
            flips[mainLoop] = best.Index;

            // Flip adjacent neighbors
            if (best.Index > 0)
            {
                if (coins[best.Index - 1] == 'H') coins[best.Index - 1] = 'T';
                else if (coins[best.Index - 1] == 'T') coins[best.Index - 1] = 'H';
            }
            if (best.Index < n - 1)
            {
                if (coins[best.Index + 1] == 'H') coins[best.Index + 1] = 'T';
                else if (coins[best.Index + 1] == 'T') coins[best.Index + 1] = 'H';
            }

            // Clear the removed coin
            coins[best.Index] = '\0';
        }

        // Check if the board is completely cleared after n iterations
        if (coins.All(c => c == '\0'))
        {
            return (true, flips);
        }

        return (false, []);
    }
}
