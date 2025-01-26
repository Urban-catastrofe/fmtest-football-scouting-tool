using System.Collections.Concurrent;

namespace fmtest.calculations
{
    public class DealFactorCalculator
    {
        private readonly ConcurrentDictionary<string, int> _transferValueCache = new();

        public int CalculateDealFactor(PlayerAttributes player, PlayerScores scores)
        {
            if (player == null) return 0;

            // Get the highest ability score
            int highestAbilityScore = new[]
            {
                scores.AdvancedForwardScore,
                scores.AttackingMidfielderScore,
                scores.BpdDefendScore,
                scores.InsideForwardScore,
                scores.SegundoVolanteScore,
                scores.WingBackAttacking,
                scores.SweeperKeeper,
                scores.ShotStopper,
                scores.DeepLyingPlaymaker,
                scores.DefensiveMidfielder
            }.Max();

            return CalculateDealFactor(
                highestAbilityScore,
                player.TransferValue,
                player.MinFeeRls,
                player.MinFeeRlsToForeignClubs);
        }

        public int CalculateDealFactor(int abilityScore, string transferValueString, string minFeeRlsString, string minFeeRlsToForeignClubsString)
        {
            int transferValue = ParseTransferValue(transferValueString);
            int minFeeRls = ParseTransferValue(minFeeRlsString);
            int minFeeRlsToForeignClubs = ParseTransferValue(minFeeRlsToForeignClubsString);

            if (transferValue == 0)
                return 0;

            double abilityWeight = 0.7;
            double transferValueWeight = 0.3;

            double normalizedAbilityScore = (double)abilityScore / 100;
            double normalizedTransferValue = Math.Log10(transferValue) / 8;

            double dealFactor = (abilityWeight * normalizedAbilityScore) + 
                              (transferValueWeight * (1 - normalizedTransferValue));

            // Check release clauses
            if (minFeeRls > 0 || minFeeRlsToForeignClubs > 0)
            {
                int minFee = GetLowestReleaseClause(minFeeRls, minFeeRlsToForeignClubs);
                if (minFee > 0 && minFee < transferValue)
                {
                    double minFeeWeight = 0.2;
                    double normalizedMinFee = Math.Log10(minFee) / 8;
                    dealFactor += minFeeWeight * (1 - normalizedMinFee);
                }
            }

            return (int)(dealFactor * 100);
        }

        private int GetLowestReleaseClause(int domestic, int foreign)
        {
            if (domestic == 0) return foreign;
            if (foreign == 0) return domestic;
            return Math.Min(domestic, foreign);
        }

        private int ParseTransferValue(string transferValueString)
        {
            if (string.IsNullOrWhiteSpace(transferValueString))
                return 0;

            return _transferValueCache.GetOrAdd(transferValueString, key =>
            {
                try
                {
                    // Remove transfer listed indicator and clean up the string
                    key = key.Replace("(Transfer Listed)", "")
                           .Replace("€", "")
                           .Replace(",", "")
                           .Trim();

                    if (string.IsNullOrWhiteSpace(key) || key == "N/A" || key == "-")
                        return 0;

                    // Handle ranges by taking the lower value
                    if (key.Contains(" - "))
                    {
                        key = key.Split(" - ")[0].Trim();
                    }

                    // Handle decimal values
                    if (key.Contains("."))
                    {
                        var parts = key.Split('.');
                        if (key.EndsWith("M", StringComparison.OrdinalIgnoreCase))
                        {
                            var number = double.Parse(parts[0]);
                            var fraction = parts.Length > 1 ? 
                                double.Parse("0." + parts[1].TrimEnd('M', 'm')) : 0;
                            return (int)((number + fraction) * 1_000_000);
                        }
                    }

                    // Handle K and M values
                    if (key.EndsWith("K", StringComparison.OrdinalIgnoreCase))
                    {
                        return (int)(double.Parse(key.TrimEnd('K', 'k')) * 1_000);
                    }
                    if (key.EndsWith("M", StringComparison.OrdinalIgnoreCase))
                    {
                        return (int)(double.Parse(key.TrimEnd('M', 'm')) * 1_000_000);
                    }

                    return int.Parse(key);
                }
                catch
                {
                    return 0;
                }
            });
        }
    }

    public static class DealFactorExtensions
    {
        private static readonly Lazy<DealFactorCalculator> _calculator =
            new Lazy<DealFactorCalculator>(() => new DealFactorCalculator(), 
                LazyThreadSafetyMode.ExecutionAndPublication);

        public static int CalculateDealFactor(this PlayerAttributes player, PlayerScores scores)
        {
            return _calculator.Value.CalculateDealFactor(player, scores);
        }
    }
}
