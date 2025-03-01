﻿using AngleSharp.Text;
using System.Text.RegularExpressions;
using System.Collections.Concurrent;

namespace fmtest.calculations
{
    public class AbilityCalculators
    {
        private readonly DealFactorCalculator _dealFactorCalculator;
        private readonly WonderkidCalculator _wonderkidCalculator;
        public AbilityCalculators(DealFactorCalculator dealFactorCalculator, WonderkidCalculator wonderkidCalculator)
        {
            _dealFactorCalculator = dealFactorCalculator;
            _wonderkidCalculator = wonderkidCalculator;
        }

        private readonly ConcurrentDictionary<string, int> _transferValueCache = new ConcurrentDictionary<string, int>();

        public int CalculateBpdOnDefend(PlayerAttributes pa)
        {
            var ability = 0;

            // Weights are hypothetical and can be adjusted based on your game analysis
            int weightHeading = 4;
            int weightMarking = 4;
            int weightTackling = 4;
            int weightPositioning = 4;
            int weightStrength = 3;
            int weightJumpingReach = 3;
            int weightPace = 3;
            int weightAcceleration = 3;
            int weightAgility = 3;
            int weightStamina = 3;
            int weightWorkRate = 3;
            int weightDetermination = 3;
            int weightBravery = 3;
            int weightConcentration = 3;
            int weightDecisions = 3;
            int weightAnticipation = 3;

            int weightPassing = 2;
            int weightComposure = 2;
            int weightBalance = 2;
            int weightAggression = 2;
            int weightLeadership = 2;

            int weightFirstTouch = 1;
            int weightTechnique = 1;
            int weightVision = 1;

            // Calculate ability score
            ability += pa.Hea * weightHeading;
            ability += pa.Mar * weightMarking;
            ability += pa.Tck * weightTackling;
            ability += pa.Pos * weightPositioning;
            ability += pa.Str * weightStrength;
            ability += pa.Jum * weightJumpingReach;
            ability += pa.Pac * weightPace;
            ability += pa.Acc * weightAcceleration;
            ability += pa.Agi * weightAgility;
            ability += pa.Sta * weightStamina;
            ability += pa.Wor * weightWorkRate;
            ability += pa.Det * weightDetermination;
            ability += pa.Bra * weightBravery;
            ability += pa.Cnt * weightConcentration;
            ability += pa.Dec * weightDecisions;
            ability += pa.Ant * weightAnticipation;

            ability += pa.Pas * weightPassing;
            ability += pa.Cmp * weightComposure;
            ability += pa.Bal * weightBalance;
            ability += pa.Agg * weightAggression;
            //ability += pa.Lea * weightLeadership;

            ability += pa.Fir * weightFirstTouch;
            ability += pa.Tec * weightTechnique;
            ability += pa.Vis * weightVision;

            string heightString = Regex.Match(pa.Height, @"\d+").Value;
            int height;
            if (int.TryParse(heightString, out height))
            {
                int heightScore = GetHeightScore(height);
                ability += heightScore;
            }

            return ability;

            //// Normalize the ability score to a 0-100 range
            //int maxScore = 20 * (weightHeading + weightMarking + weightTackling + weightPositioning +
            //    weightStrength + weightJumpingReach + weightPace + weightAcceleration + weightAgility +
            //    weightStamina + weightWorkRate + weightDetermination + weightBravery + weightConcentration +
            //    weightDecisions + weightAnticipation + weightPassing + weightComposure + weightBalance +
            //    weightAggression + weightLeadership + weightFirstTouch + weightTechnique + weightVision) +
            //    GetMaxHeightScore();

            //int normalizedAbility = (int)((double)ability / maxScore * 100);

            //return normalizedAbility;
        }


        public int CalculateSegundoVolanteOnSupport(PlayerAttributes pa)
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

        public int CalculateAdvancedForward(PlayerAttributes pa)
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
            ability += pa.Ant * weightTier1; // Anticipation
            ability += pa.Acc * weightTier1; // Acceleration
            ability += pa.Pac * weightTier1; // Pace
            ability += pa.Wor * weightTier1; // Work Rate
            ability += pa.Agi * weightTier1; // Agility

            // Tier 2
            ability += pa.Dri * weightTier2; // Dribbling
            ability += pa.Fir * weightTier2; // First Touch
            ability += pa.Tec * weightTier2; // Technique
            ability += pa.Bal * weightTier2; // Balance
            ability += pa.Sta * weightTier2; // Stamina
            ability += pa.Str * weightTier2; // Strength

            // Tier 3
            ability += pa.Fin * weightTier3; // Finishing
            ability += pa.Cmp * weightTier3; // Composure
            ability += pa.Dec * weightTier3; // Decisions
            ability += pa.Vis * weightTier3; // Vision
            ability += pa.Lon * weightTier3; // Long Shots
            //ability += pa.Pen * weightTier3; // Penalty Taking

            return ability;
            // Calculate the maximum possible score based on the weights and number of attributes
            //int maxScore = (6 * weightTier1) + (6 * weightTier2) + (6 * weightTier3);

            //// Normalize the ability score to a 0-100 range
            //int normalizedAbility = (int)((double)ability / maxScore * 100);

            //return normalizedAbility;
        }

        public int CalculateInsideForward(PlayerAttributes pa)
        {
            int ability = 0;

            // Assign weights for key (green), desirable (blue), and additional (yellow) attributes.
            int weightRequired = 5;
            int weightKey = 3;
            int weightDesirable = 2;
            int weightAdditional = 1;

            // Key Attributes (Green)
            ability += pa.Dri * weightKey; // Dribbling
            ability += pa.Fin * weightKey; // Finishing
            ability += pa.Fir * weightKey; // First Touch
            ability += pa.Pas * weightKey; // Passing
            ability += pa.Tec * weightKey; // Technique
            ability += pa.Acc * weightRequired; // Acceleration
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
            ability += pa.Pac * weightRequired; // Pace
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

        public int CalculateWingBackAttacking(PlayerAttributes pa)
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

        public int CalculateSweeperKeeper(PlayerAttributes pa)
        {
            var ability = 0;

            // Weights for key attributes
            int weightKey = 3;

            // Weights for desirable attributes
            int weightDesirable = 2;

            // Key attributes for a sweeper keeper
            ability += pa.Ref * weightKey; // Reflexes
            ability += pa.OneVOne * weightKey; // One On Ones
            ability += pa.TRO * weightKey; // Rushing Out
            ability += pa.Pas * weightKey; // Passing
            ability += pa.Vis * weightKey; // Vision
            ability += pa.Ant * weightKey; // Anticipation
            ability += pa.Cmd * weightKey; // Communication
            ability += pa.Kic * weightKey; // Kicking
            ability += pa.Thr * weightKey; // Throwing

            // Desirable attributes for a sweeper keeper
            ability += pa.Acc * weightDesirable; // Acceleration
            ability += pa.Agi * weightDesirable; // Agility
            ability += pa.Jum * weightDesirable; // Jumping Reach
            ability += pa.Dec * weightDesirable; // Decisions
            ability += pa.Cnt * weightDesirable; // Concentration
            ability += pa.Cmp * weightDesirable; // Composure
            ability += pa.Fir * weightDesirable; // First Touch
            ability += pa.Tec * weightDesirable; // Technique

            // Normalize the ability score to a 0-100 range
            int maxScore = (9 * weightKey) + (8 * weightDesirable);
            int normalizedAbility = (int)((double)ability / maxScore * 100);

            return normalizedAbility;
        }

        public int CalculateShotStopper(PlayerAttributes pa)
        {
            var ability = 0;

            // Weights for key attributes
            int weightKey = 3;

            // Weights for desirable attributes
            int weightDesirable = 2;

            // Key attributes for a shot stopper
            ability += pa.Ref * weightKey; // Reflexes
            ability += pa.Han * weightKey; // Handling
            ability += pa.OneVOne * weightKey; // One On Ones
            ability += pa.Aer * weightKey; // Aerial Ability
            ability += pa.Cmd * weightKey; // Communication
            ability += pa.Ant * weightKey; // Anticipation
            ability += pa.Pos * weightKey; // Positioning

            // Desirable attributes for a shot stopper
            ability += pa.Jum * weightDesirable; // Jumping Reach
            ability += pa.TRO * weightDesirable; // Rushing Out
            ability += pa.Kic * weightDesirable; // Kicking
            ability += pa.Thr * weightDesirable; // Throwing
            ability += pa.Cnt * weightDesirable; // Concentration
            ability += pa.Dec * weightDesirable; // Decisions
            ability += pa.Wor * weightDesirable; // Work Rate

            // Normalize the ability score to a 0-100 range
            int maxScore = (7 * weightKey) + (7 * weightDesirable);
            int normalizedAbility = (int)((double)ability / maxScore * 100);

            return normalizedAbility;
        }

        public int CalculateDeepLyingPlaymaker(PlayerAttributes pa)
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

        public int CalculateAmAbility(PlayerAttributes pa)
        {
            var ability = 0;

            // Weights for key attributes
            int weightTechnique = 5;
            int weightPassing = 5;
            int weightVision = 5;
            int weightCreativity = 5;
            int weightOffTheBall = 4;
            int weightDribbling = 4;
            int weightFirstTouch = 4;
            int weightLongShots = 3;
            int weightFinishing = 3;
            int weightComposure = 3;
            int weightDecisions = 3;
            int weightWorkRate = 3;
            int weightStamina = 3;
            int weightAgility = 3;
            int weightBalance = 3;
            int weightAcceleration = 2;
            int weightPace = 2;
            int weightStrength = 1;
            int weightBravery = 1;
            int weightDetermination = 2;
            int weightFlair = 2;
            int weightAnticipation = 2;
            int weightConcentration = 2;
            int weightLeadership = 1;

            // Calculate ability score
            ability += pa.Tec * weightTechnique;
            ability += pa.Pas * weightPassing;
            ability += pa.Vis * weightVision;
            ability += pa.Fla * weightCreativity;
            ability += pa.OtB * weightOffTheBall;
            ability += pa.Dri * weightDribbling;
            ability += pa.Fir * weightFirstTouch;
            ability += pa.Lon * weightLongShots;
            ability += pa.Fin * weightFinishing;
            ability += pa.Cmp * weightComposure;
            ability += pa.Dec * weightDecisions;
            ability += pa.Wor * weightWorkRate;
            ability += pa.Sta * weightStamina;
            ability += pa.Agi * weightAgility;
            ability += pa.Bal * weightBalance;
            ability += pa.Acc * weightAcceleration;
            ability += pa.Pac * weightPace;
            ability += pa.Str * weightStrength;
            ability += pa.Bra * weightBravery;
            ability += pa.Det * weightDetermination;
            ability += pa.Fla * weightFlair;
            ability += pa.Ant * weightAnticipation;
            ability += pa.Cnt * weightConcentration;
            //ability += pa.Lea * weightLeadership;

            return ability;

            //// Normalize the ability score to a 0-100 range
            //int maxScore = 20 * (weightTechnique + weightPassing + weightVision + weightCreativity +
            //    weightOffTheBall + weightDribbling + weightFirstTouch + weightLongShots + weightFinishing +
            //    weightComposure + weightDecisions + weightWorkRate + weightStamina + weightAgility +
            //    weightBalance + weightAcceleration + weightPace + weightStrength + weightBravery +
            //    weightDetermination + weightFlair + weightAnticipation + weightConcentration + weightLeadership);

            //int normalizedAbility = (int)((double)ability / maxScore * 100);

            //return normalizedAbility;
        }

        public int CalculateDefensiveMidfielder(PlayerAttributes pa)
        {
            var ability = 0;

            // Weights for key attributes
            int weightTackling = 5;
            int weightMarking = 5;
            int weightPositioning = 5;
            int weightStrength = 4;
            int weightStamina = 4;
            int weightWorkRate = 4;
            int weightPassing = 3;
            int weightDecisions = 3;
            int weightAnticipation = 3;
            int weightTeamwork = 3;
            int weightAgility = 2;
            int weightBalance = 2;
            int weightConcentration = 2;
            int weightBravery = 2;
            int weightComposure = 2;
            int weightDetermination = 2;
            int weightVision = 1;
            int weightAcceleration = 1;
            int weightPace = 1;
            int weightFirstTouch = 1;
            int weightHeading = 1;

            // Calculate ability score
            ability += pa.Tck * weightTackling;
            ability += pa.Mar * weightMarking;
            ability += pa.Pos * weightPositioning;
            ability += pa.Str * weightStrength;
            ability += pa.Sta * weightStamina;
            ability += pa.Wor * weightWorkRate;
            ability += pa.Pas * weightPassing;
            ability += pa.Dec * weightDecisions;
            ability += pa.Ant * weightAnticipation;
            ability += pa.Tea * weightTeamwork;
            ability += pa.Agi * weightAgility;
            ability += pa.Bal * weightBalance;
            ability += pa.Cnt * weightConcentration;
            ability += pa.Bra * weightBravery;
            ability += pa.Cmp * weightComposure;
            ability += pa.Det * weightDetermination;
            ability += pa.Vis * weightVision;
            ability += pa.Acc * weightAcceleration;
            ability += pa.Pac * weightPace;
            ability += pa.Fir * weightFirstTouch;
            ability += pa.Hea * weightHeading;

            // Calculate height score
            string heightString = Regex.Match(pa.Height, @"\d+").Value;
            int height;
            if (int.TryParse(heightString, out height))
            {
                int heightScore = GetHeightScore(height);
                ability += heightScore;
            }

            return ability;

            //// Normalize the ability score to a 0-100 range
            //int maxScore = 20 * (weightTackling + weightMarking + weightPositioning + weightStrength +
            //    weightStamina + weightWorkRate + weightPassing + weightDecisions + weightAnticipation +
            //    weightTeamwork + weightAgility + weightBalance + weightConcentration + weightBravery +
            //    weightComposure + weightDetermination + weightVision + weightAcceleration + weightPace +
            //    weightFirstTouch + weightHeading) + GetMaxHeightScoreForDefensiveMidfielder();

            //int normalizedAbility = (int)((double)ability / maxScore * 100);

            //return normalizedAbility;
        }


        public int CalculateWonderkidPotential(PlayerAttributes pa)
        {
           return _wonderkidCalculator.CalculateWonderkidPotential(pa);
        }

        public int CalculateDealFactor(PlayerAttributes pa, PlayerScores roleScores)
        {
            //TODO: FIX: its shit.
            //return _dealFactorCalculator.CalculateDealFactor(pa, roleScores);
            return 0;
        }


        private int GetHeightScore(int height)
        {
            int minDesirableHeight = 185; // Minimum desirable height for a defender in centimeters
            int optimalHeight = 190; // Optimal height for a defender in centimeters
            int maxDesirableHeight = 200; // Maximum desirable height for a defender in centimeters

            if (height >= minDesirableHeight && height <= maxDesirableHeight)
            {
                if (height >= optimalHeight)
                {
                    return 100; // Maximum score for defenders taller than or equal to the optimal height
                }
                else
                {
                    int heightDifference = optimalHeight - height;
                    int scoreReduction = heightDifference * 2; // Reduce score by 2 for each centimeter below the optimal height
                    return Math.Max(0, 100 - scoreReduction);
                }
            }
            else if (height < minDesirableHeight)
            {
                int heightDifference = minDesirableHeight - height;
                int scoreReduction = heightDifference * 5; // Reduce score by 5 for each centimeter below the minimum desirable height
                return Math.Max(0, 100 - scoreReduction);
            }
            else // height > maxDesirableHeight
            {
                return 100; // Maximum score for exceptionally tall defenders
            }
        }
    }
}