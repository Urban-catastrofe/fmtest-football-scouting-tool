namespace fmtest.calculations
{
    public class WonderkidCalculator
    {
        private const int MIN_AGE = 15;
        private const int MAX_AGE = 23;
        private const int MAX_SCORE = 100;

        public int CalculateWonderkidPotential(PlayerAttributes player)
        {
            if (player == null || player.Age < MIN_AGE || player.Age > MAX_AGE)
                return 0;

            var baseScore = CalculateBaseScore(player);
            var multipliers = CalculateMultipliers(player);
            var positionBonus = CalculatePositionSpecificBonus(player);

            double finalScore = baseScore * multipliers * positionBonus;
            return (int)Math.Min(MAX_SCORE, finalScore);
        }

        private double CalculateBaseScore(PlayerAttributes player)
        {
            var technicalScore = CalculateTechnicalScore(player);
            var mentalScore = CalculateMentalScore(player);
            var physicalScore = CalculatePhysicalScore(player);

            return (technicalScore * 0.35 + mentalScore * 0.4 + physicalScore * 0.25) * 20;
        }

        private double CalculateTechnicalScore(PlayerAttributes p)
        {
            string position = NormalizePosition(p.Position);
            double totalScore = 0;
            int attributeCount = 5;

            switch (position)
            {
                case "GK":
                    totalScore = p.Han * 2.0 + p.Aer * 1.5 + p.Ref * 2.0 +
                                p.OneVOne * 1.5 + p.Kic * 1.0;
                    break;

                case "DEF":
                    totalScore = p.Tck * 2.0 + p.Mar * 2.0 + p.Pos * 1.5 +
                                p.Hea * 1.5 + p.Tec * 1.0;
                    break;

                case "MID":
                    totalScore = p.Pas * 2.0 + p.Tec * 2.0 + p.Fir * 1.5 +
                                p.Vis * 1.5 + p.Dec * 1.0;
                    break;

                case "ATT":
                    totalScore = p.Fin * 2.0 + p.Tec * 1.5 + p.Dri * 1.5 +
                                p.Fir * 1.5 + p.Cmp * 1.5;
                    break;
            }

            return totalScore / (attributeCount * 20.0);
        }

        private double CalculateMentalScore(PlayerAttributes p)
        {
            // Core mental attributes weighted sum
            double totalScore = p.Det * 2.0 + p.Wor * 2.0 + p.Dec * 1.5 +
                               p.Ant * 1.5 + p.Cmp * 1.5 + p.OtB * 1.5 + p.Fla * 1.0;

            const int attributeCount = 7;
            return totalScore / (attributeCount * 20.0);
        }

        private double CalculatePhysicalScore(PlayerAttributes p)
        {
            double totalScore = p.Pac * 1.5 + p.Acc * 1.5 + p.Sta * 1.5 +
                               p.Agi * 1.0 + p.Bal * 1.0 + p.Str * 1.0;

            const int attributeCount = 6;
            return totalScore / (attributeCount * 20.0);
        }

        private double CalculateMultipliers(PlayerAttributes player)
        {
            return CalculateAgeMultiplier(player.Age) *
                   CalculatePersonalityMultiplier(player.Personality) *
                   CalculatePhysicalPotentialMultiplier(player) *
                   CalculateContractMultiplier(player);
        }

        private double CalculateAgeMultiplier(int age)
        {
            if (age < MIN_AGE || age > MAX_AGE)
                return 0.5;

            return 2.0 - (Math.Pow(age - MIN_AGE, 1.5) / Math.Pow(MAX_AGE - MIN_AGE, 1.5));
        }

        private double CalculatePersonalityMultiplier(string personality)
        {
            return personality switch
            {
                "Model Citizen" => 1.4,
                "Perfectionist" or "Model Professional" => 1.3,
                "Professional" or "Resolute" => 1.2,
                "Ambitious" or "Determined" or "Driven" => 1.15,
                "Fairly Professional" or "Spirited" => 1.1,
                "Fairly Ambitious" => 1.05,
                "Temperamental" => 0.9,
                "Low Determination" => 0.8,
                "Unambitious" or "Low Professionalism" => 0.7,
                _ => 1.0
            };
        }

        private double CalculatePhysicalPotentialMultiplier(PlayerAttributes player)
        {
            double multiplier = 1.0;

            if (!string.IsNullOrEmpty(player.Height))
            {
                int heightCm = ParseHeight(player.Height);
                string position = NormalizePosition(player.Position);

                multiplier *= position switch
                {
                    "GK" => heightCm >= 188 ? 1.2 : heightCm >= 185 ? 1.1 : 1.0,
                    "DEF" => heightCm >= 185 ? 1.15 : heightCm >= 180 ? 1.1 : 1.0,
                    "ATT" => heightCm >= 180 ? 1.1 : 1.0,
                    _ => heightCm >= 175 ? 1.05 : 1.0
                };
            }

            // Two-footedness bonus
            if (player.LeftFoot == "Very Strong" && player.RightFoot == "Very Strong")
                multiplier *= 1.15;
            else if ((player.LeftFoot == "Strong" && player.RightFoot == "Strong") ||
                     (player.LeftFoot == "Very Strong" && player.RightFoot == "Strong") ||
                     (player.LeftFoot == "Strong" && player.RightFoot == "Very Strong"))
                multiplier *= 1.1;

            return multiplier;
        }

        private double CalculateContractMultiplier(PlayerAttributes player)
        {
            double multiplier = 1.0;

            if (!string.IsNullOrEmpty(player.MinFeeRls) || !string.IsNullOrEmpty(player.MinFeeRlsToForeignClubs))
                multiplier *= 0.95;

            if (player.MediaHandling == "Natural" || player.MediaHandling == "Good")
                multiplier *= 1.05;

            return multiplier;
        }

        private double CalculatePositionSpecificBonus(PlayerAttributes player)
        {
            string position = NormalizePosition(player.Position);

            return position switch
            {
                "GK" => IsEliteGoalkeeperProspect(player) ? 1.2 : 1.0,
                "DEF" => IsEliteDefenderProspect(player) ? 1.15 : 1.0,
                "MID" => IsEliteMidfielderProspect(player) ? 1.15 : 1.0,
                "ATT" => IsEliteAttackerProspect(player) ? 1.2 : 1.0,
                _ => 1.0
            };
        }

        private bool IsEliteGoalkeeperProspect(PlayerAttributes p) =>
            (p.Ref + p.OneVOne + p.Aer + p.Han + p.Cmd) / 5.0 >= 14 &&
            p.Det >= 14 && p.Wor >= 14 && p.Pos >= 14;

        private bool IsEliteDefenderProspect(PlayerAttributes p) =>
            (p.Tck + p.Mar + p.Pos + p.Str + p.Dec) / 5.0 >= 14 &&
            p.Det >= 14 && p.Wor >= 14;

        private bool IsEliteMidfielderProspect(PlayerAttributes p) =>
            (p.Pas + p.Tec + p.Dec + p.Vis + p.Sta) / 5.0 >= 14 &&
            p.Det >= 14 && p.Wor >= 14;

        private bool IsEliteAttackerProspect(PlayerAttributes p) =>
            (p.Fin + p.Dri + p.Tec + p.OtB + p.Cmp) / 5.0 >= 14 &&
            p.Det >= 14 && p.Acc >= 14;

        private string NormalizePosition(string position)
        {
            if (string.IsNullOrEmpty(position))
                return "OTHER";

            position = position.ToUpper().Trim();

            if (position.StartsWith("GK"))
                return "GK";
            if (position.StartsWith("D") || position.Contains("WB"))
                return "DEF";
            if (position.StartsWith("M") || position.StartsWith("DM"))
                return "MID";
            if (position.StartsWith("AM") || position.StartsWith("S"))
                return "ATT";

            return "OTHER";
        }

        private int ParseHeight(string height)
        {
            try
            {
                return int.Parse(height.Split(' ')[0]);
            }
            catch
            {
                return 180;
            }
        }

    }

    public static class WonderkidCalculatorExtensions
    {
        private static readonly Lazy<WonderkidCalculator> _calculator =
            new Lazy<WonderkidCalculator>(() => new WonderkidCalculator(), LazyThreadSafetyMode.ExecutionAndPublication);

        public static int CalculateWonderkidPotential(this PlayerAttributes player)
        {
            return _calculator.Value.CalculateWonderkidPotential(player);
        }
    }

}
