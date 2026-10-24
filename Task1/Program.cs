Console.WriteLine("Task 1");

public partial class Program
{
    public static (bool possible, int[] flips) RemoveCoins(int n, char[] coins)
    {
        // Selection priority:
        // 1. Maximizes net 'H' gains after neighbor flips.
        // 2. Tie-breaker: Closest to an edge or empty slot ('\0').
        // Up to n iterations; coins contain 'H', 'T', or '\0' (cleared).
                
        coins = coins.ToArray(); //copy
        
        int[] flips = new int[n];
        for (int mainLoop = 0; mainLoop < n; mainLoop++)
        {
            var best = (Index: -1, Value: -3, edgeDistance: int.MaxValue);
            int currentEdge = -1;

            // --- Forward Loop (Left to Right) ---
            int tempValue,tempEdgeDistance;
            for (int i = 0; i < n; i++)
            {
                switch (coins[i]) {
                    case '\0':
                        currentEdge = i;
                        continue;

                    case 'T':
                        continue;

                    case 'H':
                        tempValue = 0;
                        tempEdgeDistance = Math.Abs(currentEdge - i) - 1;
                        if (i != 0)
                        {
                            switch (coins[i - 1])
                            {
                                case 'H':
                                    tempValue--;
                                    break;
                                case 'T':
                                    tempValue++;
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (i < n - 1)
                        {
                            switch (coins[i + 1])
                            {
                                case 'H':
                                    tempValue--;
                                    break;
                                case 'T':
                                    tempValue++;
                                    break;
                                default:
                                    break;
                            }
                        }

                        if (tempValue > best.Value || (tempValue == best.Value && tempEdgeDistance < best.edgeDistance))
                        {
                            best = (i, tempValue, tempEdgeDistance);
                        }
                        break;

                    default:
                        break;
                }
            }

            // --- Backward Loop (Right to Left) ---
            currentEdge = n;
            for (int i = n - 1; i >= 0; i--)
            {
                switch (coins[i]) {
                    case '\0':
                        currentEdge = i;
                        continue;

                    case 'T':
                        continue;

                    case 'H':
                        tempValue = 0;
                        tempEdgeDistance = Math.Abs(currentEdge - i) - 1;
                        if (i != 0)
                        {
                            switch (coins[i - 1])
                            {
                                case 'H':
                                    tempValue--;
                                    break;
                                case 'T':
                                    tempValue++;
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (i < n - 1)
                        {
                            switch (coins[i + 1])
                            {
                                case 'H':
                                    tempValue--;
                                    break;
                                case 'T':
                                    tempValue++;
                                    break;
                                default:
                                    break;
                            }
                        }

                        if (tempValue > best.Value || (tempValue == best.Value && tempEdgeDistance < best.edgeDistance))
                        {
                            best = (i, tempValue, tempEdgeDistance);
                        }
                        break;

                    default:
                        break;
                }
            }

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
