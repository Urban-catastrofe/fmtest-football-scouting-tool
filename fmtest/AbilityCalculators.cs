using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmtest
{
    class AbilityCalculators
    {
        public int CalculateBpdOnDefend(PlayerAttributes pa)
        {
            var ability = 0;

            // Weights are hypothetical and can be adjusted based on your game analysis
            int weightHeading = 2;
            int weightMarking = 3;
            int weightPassing = 1;
            int weightTackling = 3;
            int weightComposure = 2;
            int weightPositioning = 3;
            int weightJumpingReach = 2;
            int weightStrength = 2;

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

            // Calculate ability score
            ability += pa.Hea * weightHeading;
            ability += pa.Mar * weightMarking;
            ability += pa.Pas * weightPassing;
            ability += pa.Tck * weightTackling;
            ability += pa.Cmp * weightComposure;
            ability += pa.Pos * weightPositioning;
            ability += pa.Jum * weightJumpingReach;
            ability += pa.Str * weightStrength;

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

            return ability;
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

        public int CalculateInsideForward(PlayerAttributes pa)
        {
            var ability = 0;

            // Assign weights for key (green) and desirable (blue) attributes.
            // Key attributes are given a higher weight.
            int weightKey = 3;
            int weightDesirable = 2;

            // Key Attributes (Green)
            ability += pa.Dri * weightKey; // Dribbling
            ability += pa.Fin * weightKey; // Finishing
            ability += pa.Fir * weightKey; // First Touch
            ability += pa.Pas * weightKey; // Passing
            ability += pa.Tec * weightKey; // Technique
            ability += pa.Acc * weightKey; // Acceleration
            ability += pa.Agi * weightKey; // Agility

            // Desirable Attributes (Blue)
            ability += pa.Cro * weightDesirable; // Crossing
            ability += pa.Lon * weightDesirable; // Long Shots
            ability += pa.OtB * weightDesirable; // Off the Ball
            ability += pa.Cmp * weightDesirable; // Composure
            ability += pa.Dec * weightDesirable; // Decisions
            ability += pa.Vis * weightDesirable; // Vision
            ability += pa.Wor * weightDesirable; // Work Rate
            ability += pa.Pac * weightDesirable; // Pace
            ability += pa.Sta * weightDesirable; // Stamina

            return ability;
        }

        public int CalculateWingBackAttacking(PlayerAttributes pa)
        {
            var ability = 0;

            // Weights for key attributes, higher because they are essential for the role.
            int weightKey = 3;

            // Weights for desirable attributes, lower but still significant.
            int weightDesirable = 2;

            // Key attributes
            ability += pa.Mar * weightKey; // Marking
            ability += pa.Tck * weightKey; // Tackling
            ability += pa.Ant * weightKey; // Anticipation
            ability += pa.Pos * weightKey; // Positioning
            ability += pa.Tea * weightKey; // Teamwork
            ability += pa.Wor * weightKey; // Work Rate
            ability += pa.Acc * weightKey; // Acceleration
            ability += pa.Sta * weightKey; // Stamina

            // Desirable attributes
            ability += pa.Cro * weightDesirable; // Crossing
            ability += pa.Dri * weightDesirable; // Dribbling
            ability += pa.Fir * weightDesirable; // First Touch
            ability += pa.Pas * weightDesirable; // Passing
            ability += pa.Tec * weightDesirable; // Technique
            ability += pa.Cnt * weightDesirable; // Concentration
            ability += pa.Dec * weightDesirable; // Decisions
            ability += pa.OtB * weightDesirable; // Off the Ball
            ability += pa.Agi * weightDesirable; // Agility
            ability += pa.Pac * weightDesirable; // Pace

            return ability;
        }

        public int CalculateSweeperKeeper(PlayerAttributes pa)
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

        public int CalculateWonderkidPotential(PlayerAttributes pa)
        {
            var potential = 0;
            int ageMultiplierBase = 15; // Base age for maximum potential multiplier
            int maxMultiplier = 5; // Maximum multiplier for a 15-year-old player
            int minMultiplier = 1; // Minimum multiplier for older players

            int ageMultiplier = pa.Age > ageMultiplierBase ? minMultiplier : maxMultiplier - (pa.Age - 15);

            potential += (pa.Wor + pa.Det + pa.Sta + pa.Pac + pa.Acc) * ageMultiplier;

            return potential;
        }

    }
}
