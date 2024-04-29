﻿namespace fmtest
{
    /// <summary>
    /// Interface for ability calculators.
    /// </summary>
    interface IAbilityCalculators
    {
        /// <summary>
        /// Calculates the ability score for a Ball-Playing Defender on Defend duty.
        /// </summary>
        /// <param name="pa">The player attributes.</param>
        /// <returns>The ability score.</returns>
        int CalculateBpdOnDefend(PlayerAttributes pa);

        /// <summary>
        /// Calculates the ability score for a Segundo Volante on Support duty.
        /// </summary>
        /// <param name="pa">The player attributes.</param>
        /// <returns>The ability score.</returns>
        int CalculateSegundoVolanteOnSupport(PlayerAttributes pa);

        /// <summary>
        /// Calculates the ability score for an Advanced Forward.
        /// </summary>
        /// <param name="pa">The player attributes.</param>
        /// <returns>The ability score.</returns>
        int CalculateAdvancedForward(PlayerAttributes pa);

        /// <summary>
        /// Calculates the ability score for an Inside Forward.
        /// </summary>
        /// <param name="pa">The player attributes.</param>
        /// <returns>The ability score.</returns>
        int CalculateInsideForward(PlayerAttributes pa);

        /// <summary>
        /// Calculates the ability score for a Wing Back (Attacking).
        /// </summary>
        /// <param name="pa">The player attributes.</param>
        /// <returns>The ability score.</returns>
        int CalculateWingBackAttacking(PlayerAttributes pa);

        /// <summary>
        /// Calculates the ability score for a Sweeper Keeper.
        /// </summary>
        /// <param name="pa">The player attributes.</param>
        /// <returns>The ability score.</returns>
        int CalculateSweeperKeeper(PlayerAttributes pa);

        /// <summary>
        /// Calculates the ability score for a Deep-Lying Playmaker.
        /// </summary>
        /// <param name="pa">The player attributes.</param>
        /// <returns>The ability score.</returns>
        int CalculateDeepLyingPlaymaker(PlayerAttributes pa);

        /// <summary>
        /// Calculates the ability score for a Defensive Midfielder.
        /// </summary>
        /// <param name="pa">The player attributes.</param>
        /// <returns>The ability score.</returns>
        int CalculateDefensiveMidfielder(PlayerAttributes pa);

        /// <summary>
        /// Calculates the potential ability score for a wonderkid.
        /// </summary>
        /// <param name="pa">The player attributes.</param>
        /// <returns>The potential ability score.</returns>
        int CalculateWonderkidPotential(PlayerAttributes pa);
    }

    class AbilityCalculators
    {
        public async Task<int> CalculateBpdOnDefend(PlayerAttributes pa)
        {
            var ability = 0;

            // Weights are hypothetical and can be adjusted based on your game analysis
            int weightHeading = 3;
            int weightMarking = 3;
            int weightPassing = 2;
            int weightTackling = 3;
            int weightComposure = 2;
            int weightPositioning = 3;
            int weightJumpingReach = 2;
            int weightStrength = 2;
            int weightAcceleration = 2;
            int weightAgility = 2;
            int weightBalance = 2;

            // Desirable traits, weighted less as they are not key but still important.
            int weightFirstTouch = 1;
            int weightTechnique = 1;
            int weightAggression = 1;
            int weightAnticipation = 2;
            int weightBravery = 2;
            int weightConcentration = 2;
            int weightDecisions = 2;
            int weightVision = 1;
            int weightPace = 1;
            int weightStamina = 2;
            int weightWorkRate = 2;
            int weightDetermination = 2;
            int weightLeadership = 1;

            // Calculate ability score
            ability += pa.Hea * weightHeading;
            ability += pa.Mar * weightMarking;
            ability += pa.Pas * weightPassing;
            ability += pa.Tck * weightTackling;
            ability += pa.Cmp * weightComposure;
            ability += pa.Pos * weightPositioning;
            ability += pa.Jum * weightJumpingReach;
            ability += pa.Str * weightStrength;
            ability += pa.Acc * weightAcceleration;
            ability += pa.Agi * weightAgility;
            ability += pa.Bal * weightBalance;

            // Add desirable traits
            ability += pa.Fir * weightFirstTouch;
            ability += pa.Tec * weightTechnique;
            ability += pa.Agg * weightAggression;
            ability += pa.Ant * weightAnticipation;
            ability += pa.Bra * weightBravery;
            ability += pa.Cnt * weightConcentration;
            ability += pa.Dec * weightDecisions;
            ability += pa.Vis * weightVision;
            ability += pa.Pac * weightPace;
            ability += pa.Sta * weightStamina;
            ability += pa.Wor * weightWorkRate;
            ability += pa.Det * weightDetermination;
            ability += pa.Lea * weightLeadership;

            // Normalize the ability score to a 0-100 range
            int maxScore = 20 * (weightHeading + weightMarking + weightPassing + weightTackling +
                                 weightComposure + weightPositioning + weightJumpingReach + weightStrength +
                                 weightAcceleration + weightAgility + weightBalance + weightFirstTouch +
                                 weightTechnique + weightAggression + weightAnticipation + weightBravery +
                                 weightConcentration + weightDecisions + weightVision + weightPace +
                                 weightStamina + weightWorkRate + weightDetermination + weightLeadership);
            int normalizedAbility = (int)((double)ability / maxScore * 100);

            return normalizedAbility;
        }

        public async Task<int> CalculateSegundoVolanteOnSupport(PlayerAttributes pa)
        {
            var ability = 0;

            // Weights are hypothetical and should be adjusted based on the importance in your specific context
            int weightMarking = 2;
            int weightPassing = 3;
            int weightTackling = 2;
            int weightOffTheBall = 3; // Important for movement in support positions.
            int weightPositioning = 2; // Critical for both defensive and offensive phases.
            int weightWorkRate = 3; // A high work rate is essential for a box-to-box midfielder.
            int weightPace = 2; // Useful for quick transitions.
            int weightStamina = 3; // Stamina is crucial for a player who covers much of the field.

            // Desirable traits, weighted less as they support the main role but are not the key focus.
            int weightFinishing = 1;
            int weightFirstTouch = 2;
            int weightLongShots = 1;
            int weightAnticipation = 2;
            int weightComposure = 2;
            int weightConcentration = 2; // Important to maintain focus throughout the match.
            int weightDecisions = 2; // Important for selecting the right plays.
            int weightAcceleration = 2; // Beneficial for quick bursts of speed.
            int weightBalance = 1;
            int weightStrength = 1; // While important, it's less critical compared to other attributes for this role.

            // Calculate ability score
            ability += pa.Mar * weightMarking;
            ability += pa.Pas * weightPassing;
            ability += pa.Tck * weightTackling;
            ability += pa.OtB * weightOffTheBall; // Assuming 'Otb' stands for Off the Ball.
            ability += pa.Pos * weightPositioning;
            ability += pa.Wor * weightWorkRate; // Assuming 'Wor' stands for Work Rate.
            ability += pa.Pac * weightPace;
            ability += pa.Sta * weightStamina; // Assuming 'Sta' stands for Stamina.

            // Add desirable traits
            ability += pa.Fin * weightFinishing; // Assuming 'Fin' stands for Finishing.
            ability += pa.Fir * weightFirstTouch; // Assuming 'Fir' stands for First Touch.
            ability += pa.Lon * weightLongShots; // Assuming 'Lng' stands for Long Shots.
            ability += pa.Ant * weightAnticipation;
            ability += pa.Cmp * weightComposure; // Assuming 'Com' stands for Composure.
            ability += pa.Cnt * weightConcentration; // Assuming 'Cnt' stands for Concentration.
            ability += pa.Dec * weightDecisions;
            ability += pa.Acc * weightAcceleration; // Assuming 'Acc' stands for Acceleration.
            ability += pa.Bal * weightBalance; // Assuming 'Bal' stands for Balance.
            ability += pa.Str * weightStrength; // Assuming 'Str' stands for Strength.

            return ability;
        }

        public async Task<int> CalculateAdvancedForward(PlayerAttributes pa)
        {
            var ability = 0;

            // Assign weights by tiers - these are arbitrary and should be adjusted based on detailed role analysis.
            // Tier 1 attributes are most important, so they have the highest weight.
            int weightTier1 = 3;
            // Tier 2 attributes are important but less so than Tier 1, so they have a medium weight.
            int weightTier2 = 2;
            // Tier 3 attributes are the least important for the primary role but still relevant.
            int weightTier3 = 1;

            // Tier 1
            ability += pa.OtB * weightTier1; // Off The Ball
            ability += pa.Cmp * weightTier1; // Composure
            ability += pa.Acc * weightTier1; // Acceleration
            ability += pa.Pac * weightTier1; // Pace

            // Tier 2
            ability += pa.Dri * weightTier2; // Dribbling
            ability += pa.Ant * weightTier2; // Anticipation
            ability += pa.Fir * weightTier2; // First Touch

            // Tier 3
            ability += pa.Fin * weightTier3; // Finishing
            ability += pa.Agi * weightTier3; // Agility
            ability += pa.Tec * weightTier3; // Technique
            ability += pa.Dec * weightTier3; // Decisions

            return ability;
        }

        public async Task<int> CalculateInsideForward(PlayerAttributes pa)
        {
            int ability = 0;

            // Assign weights for key (green), desirable (blue), and additional (yellow) attributes.
            int weightKey = 3;
            int weightDesirable = 2;
            int weightAdditional = 1;

            // Key Attributes (Green)
            ability += pa.Dri * weightKey; // Dribbling
            ability += pa.Fin * weightKey; // Finishing
            ability += pa.Fir * weightKey; // First Touch
            ability += pa.Pas * weightKey; // Passing
            ability += pa.Tec * weightKey; // Technique
            ability += pa.Acc * weightKey; // Acceleration
            ability += pa.Agi * weightKey; // Agility
            ability += pa.OtB * weightKey; // Off the Ball
            ability += pa.Ant * weightKey; // Anticipation

            // Desirable Attributes (Blue)
            ability += pa.Cro * weightDesirable; // Crossing
            ability += pa.Lon * weightDesirable; // Long Shots
            ability += pa.Cmp * weightDesirable; // Composure
            ability += pa.Dec * weightDesirable; // Decisions
            ability += pa.Vis * weightDesirable; // Vision
            ability += pa.Wor * weightDesirable; // Work Rate
            ability += pa.Pac * weightDesirable; // Pace
            ability += pa.Sta * weightDesirable; // Stamina
            ability += pa.Bal * weightDesirable; // Balance
            ability += pa.Fla * weightDesirable; // Flair

            // Additional Attributes (Yellow)
            ability += pa.Str * weightAdditional; // Strength
            ability += pa.Jum * weightAdditional; // Jumping Reach
            //ability += pa. * weightAdditional; // Natural Fitness
            ability += pa.Agg * weightAdditional; // Aggression
            ability += pa.Bra * weightAdditional; // Bravery

            // Normalize the ability score to a 0-100 range
            int maxScore = (10 * weightKey) + (10 * weightDesirable) + (5 * weightAdditional);
            int normalizedAbility = (int)((double)ability / maxScore * 100);

            return normalizedAbility;
        }

        public async Task<int> CalculateWingBackAttacking(PlayerAttributes pa)
        {
            int ability = 0;

            // Weights for key attributes, higher because they are essential for the role.
            int weightKey = 3;

            // Weights for desirable attributes, lower but still significant.
            int weightDesirable = 2;

            // Weights for additional attributes, adding more depth to the evaluation.
            int weightAdditional = 1;

            // Key attributes
            ability += pa.Mar * weightKey; // Marking
            ability += pa.Tck * weightKey; // Tackling
            ability += pa.Ant * weightKey; // Anticipation
            ability += pa.Pos * weightKey; // Positioning
            ability += pa.Tea * weightKey; // Teamwork
            ability += pa.Wor * weightKey; // Work Rate
            ability += pa.Acc * weightKey; // Acceleration
            ability += pa.Sta * weightKey; // Stamina
            ability += pa.Cro * weightKey; // Crossing
            ability += pa.Dri * weightKey; // Dribbling

            // Desirable attributes
            ability += pa.Fir * weightDesirable; // First Touch
            ability += pa.Pas * weightDesirable; // Passing
            ability += pa.Tec * weightDesirable; // Technique
            ability += pa.Cnt * weightDesirable; // Concentration
            ability += pa.Dec * weightDesirable; // Decisions
            ability += pa.OtB * weightDesirable; // Off the Ball
            ability += pa.Agi * weightDesirable; // Agility
            ability += pa.Pac * weightDesirable; // Pace
            ability += pa.Str * weightDesirable; // Strength
            ability += pa.Bal * weightDesirable; // Balance

            // Additional attributes
            ability += pa.Jum * weightAdditional; // Jumping Reach
            ability += pa.Hea * weightAdditional; // Heading
            ability += pa.Lon * weightAdditional; // Long Shots
            ability += pa.Vis * weightAdditional; // Vision
            //ability += pa.Pen * weightAdditional; // Penalty Taking

            // Normalize the ability score to a 0-100 range
            int maxScore = (10 * weightKey) + (10 * weightDesirable) + (5 * weightAdditional);
            int normalizedAbility = (int)((double)ability / maxScore * 100);

            return normalizedAbility;
        }

        public async Task<int> CalculateSweeperKeeper(PlayerAttributes pa)
        {
            var ability = 0;

            // Weights for key attributes, higher because they are essential for the role.
            int weightKey = 3;

            // Weights for desirable attributes, lower but still significant.
            int weightDesirable = 2;

            // Key attributes for a sweeper keeper
            ability += pa.Ref * weightKey; // Reflexes
            ability += pa.OneVOne * weightKey; // One On Ones
            ability += pa.TRO * weightKey; // Rushing Out
            ability += pa.Pas * weightKey; // Passing
            ability += pa.Ant * weightKey; // Anticipation
            ability += pa.Cmd * weightKey; // Communication
            ability += pa.Kic * weightKey; // Kicking

            // Desirable attributes for a sweeper keeper
            ability += pa.Acc * weightDesirable; // Acceleration
            ability += pa.Agi * weightDesirable; // Agility
            ability += pa.Jum * weightDesirable; // Jumping Reach
            ability += pa.Thr * weightDesirable; // Throwing

            // These attributes could be considered either desirable or additional, with lower weights
            int weightAdditional = 1;
            ability += pa.Tec * weightAdditional; // Technique
            ability += pa.Fir * weightAdditional; // First Touch
            ability += pa.Cnt * weightAdditional; // Concentration
            ability += pa.Dec * weightAdditional; // Decisions
            ability += pa.Pos * weightAdditional; // Positioning
            ability += pa.Wor * weightAdditional; // Work Rate

            return ability;
        }

        public async Task<int> CalculateDeepLyingPlaymaker(PlayerAttributes pa)
        {
            var ability = 0;

            // Weights for key attributes, higher because they are essential for the role.
            int weightKey = 3;

            // Weights for desirable attributes, lower but still significant.
            int weightDesirable = 2;

            // Key attributes for a deep lying playmaker
            ability += pa.Pas * weightKey; // Passing
            ability += pa.Dec * weightKey; // Decisions
            ability += pa.Pos * weightKey; // Positioning
            ability += pa.Vis * weightKey; // Vision
            ability += pa.Tec * weightKey; // Technique
            ability += pa.Fir * weightKey; // First Touch
            ability += pa.Ant * weightKey; // Anticipation

            // Desirable attributes for a deep lying playmaker
            ability += pa.Tea * weightDesirable; // Teamwork
            ability += pa.Wor * weightDesirable; // Work Rate
            ability += pa.Cmp * weightDesirable; // Composure
            ability += pa.Cnt * weightDesirable; // Concentration
            ability += pa.Agi * weightDesirable; // Agility
            ability += pa.Bal * weightDesirable; // Balance
            ability += pa.Sta * weightDesirable; // Stamina

            // These attributes could be considered either desirable or additional, with lower weights
            int weightAdditional = 1;
            ability += pa.Lon * weightAdditional; // Long Shots
            ability += pa.Mar * weightAdditional; // Marking
            ability += pa.Tck * weightAdditional; // Tackling
            ability += pa.Str * weightAdditional; // Strength

            return ability;
        }

        public async Task<int> CalculateDefensiveMidfielder(PlayerAttributes pa)
        {
            var ability = 0;

            // Weights for key attributes, higher because they are essential for the role.
            int weightKey = 3;

            // Key attributes for a defensive midfielder
            ability += pa.Mar * weightKey; // Marking
            ability += pa.Tck * weightKey; // Tackling
            ability += pa.Str * weightKey; // Strength
            ability += pa.Pos * weightKey; // Positioning

            // Secondary attributes for a defensive midfielder
            int weightSecondary = 2;
            ability += pa.Wor * weightSecondary; // Work Rate
            ability += pa.Sta * weightSecondary; // Stamina
            ability += pa.Agi * weightSecondary; // Agility
            ability += pa.Bal * weightSecondary; // Balance

            // Additional attributes that could benefit a defensive midfielder
            int weightAdditional = 1;
            ability += pa.Pas * weightAdditional; // Passing
            ability += pa.Dec * weightAdditional; // Decisions
            ability += pa.Ant * weightAdditional; // Anticipation
            ability += pa.Tea * weightAdditional; // Teamwork

            return ability;
        }

        public async Task<int> CalculateWonderkidPotential(PlayerAttributes pa)
        {
            int ageMultiplier = CalculateAgeMultiplier(pa.Age);

            int potential = 0;

            // Define weights for each attribute based on the player's position
            int worWeight = GetAttributeWeight(pa.Position, "Wor");
            int detWeight = GetAttributeWeight(pa.Position, "Det");
            int staWeight = GetAttributeWeight(pa.Position, "Sta");
            int pacWeight = GetAttributeWeight(pa.Position, "Pac");
            int accWeight = GetAttributeWeight(pa.Position, "Acc");
            int tecWeight = GetAttributeWeight(pa.Position, "Tec");
            int decWeight = GetAttributeWeight(pa.Position, "Dec");
            int antWeight = GetAttributeWeight(pa.Position, "Ant");
            int flaWeight = GetAttributeWeight(pa.Position, "Fla");
            int natWeight = GetAttributeWeight(pa.Position, "Nat");

            // Calculate the weighted sum of attributes
            potential += pa.Wor * worWeight;
            potential += pa.Det * detWeight;
            potential += pa.Sta * staWeight;
            potential += pa.Pac * pacWeight;
            potential += pa.Acc * accWeight;
            potential += pa.Tec * tecWeight;
            potential += pa.Dec * decWeight;
            potential += pa.Ant * antWeight;
            potential += pa.Fla * flaWeight;
            //potential += pa.Nat * natWeight;

            // Apply the age multiplier
            potential *= ageMultiplier;

            // Normalize the potential score to a range of 0-100
            int maxPotential = (20 * (worWeight + detWeight + staWeight + pacWeight + accWeight +
                                       tecWeight + decWeight + antWeight + flaWeight + natWeight)) * 5;
            int normalizedPotential = (int)((double)potential / maxPotential * 100);

            return normalizedPotential;
        }

        private int CalculateAgeMultiplier(int age)
        {
            int maxAge = 21;
            int minMultiplier = 1;
            int maxMultiplier = 5;

            if (age <= 15)
            {
                return maxMultiplier;
            }
            else if (age >= maxAge)
            {
                return minMultiplier;
            }
            else
            {
                double multiplierDeclineRate = (double)(maxMultiplier - minMultiplier) / (maxAge - 15);
                int ageDifference = age - 15;
                int ageMultiplier = maxMultiplier - (int)(multiplierDeclineRate * ageDifference);
                return ageMultiplier;
            }
        }

        private int GetAttributeWeight(string position, string attribute)
        {
            // Define the attribute weights for each position
            // You can adjust these weights based on your game's requirements
            switch (position)
            {
                case "S":
                case "AM":
                case "AM (R)":
                case "AM (L)":
                    switch (attribute)
                    {
                        case "Wor": return 3;
                        case "Det": return 2;
                        case "Sta": return 2;
                        case "Pac": return 3;
                        case "Acc": return 3;
                        case "Tec": return 3;
                        case "Dec": return 2;
                        case "Ant": return 2;
                        case "Fla": return 2;
                        case "Nat": return 1;
                        default: return 1;
                    }
                case "M":
                case "M (R)":
                case "M (L)":
                case "DM":
                    switch (attribute)
                    {
                        case "Wor": return 3;
                        case "Det": return 2;
                        case "Sta": return 3;
                        case "Pac": return 2;
                        case "Acc": return 2;
                        case "Tec": return 3;
                        case "Dec": return 3;
                        case "Ant": return 2;
                        case "Fla": return 2;
                        case "Nat": return 1;
                        default: return 1;
                    }
                case "D":
                case "D (C)":
                case "D (R)":
                case "D (L)":
                    switch (attribute)
                    {
                        case "Wor": return 2;
                        case "Det": return 3;
                        case "Sta": return 2;
                        case "Pac": return 2;
                        case "Acc": return 1;
                        case "Tec": return 2;
                        case "Dec": return 3;
                        case "Ant": return 3;
                        case "Fla": return 1;
                        case "Nat": return 1;
                        default: return 1;
                    }
                default:
                    return 1;
            }
        }
    }
}