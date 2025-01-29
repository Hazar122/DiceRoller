using System.Text.RegularExpressions;

namespace DiceRoller
{
    public partial class DiceRoller
    {
        [GeneratedRegex("[^0-9d+-]")]
        private static partial Regex diceFormat();

        private static readonly Random randomInt = new();

        public static (int, int, int) ParseDiceInput(string input)
        {
            string inputLower = input.ToLower();
            string formattedString = diceFormat().Replace(inputLower, string.Empty);

            // Check for format XdZ or XdZ(+/-)Y
            Match match = Regex.Match(formattedString, @"^(\d+)d(\d+)([+-]\d+)?$");

            if (!match.Success)
                throw new FormatException("Invalid dice format");

            int amount = int.Parse(match.Groups[1].Value);
            int sides = int.Parse(match.Groups[2].Value);
            int modifier = match.Groups[3].Success ? int.Parse(match.Groups[3].Value) : 0;

            return (amount, sides, modifier);
        }

        public static int RollDice(int amount, int sides)
        {
            int total = 0;
            for (int i = 0; i < amount; i++)
            {
                total += randomInt.Next(1, sides + 1);
            }
            return total;
        }
        public static int RollDiceWithModifier(int amount, int sides, int modifier)
        {
            return RollDice(amount, sides) + modifier;
        }
        public static int RollDiceWithModifier((int amount, int sides, int modifier) rollData)
        {
            return RollDiceWithModifier(rollData.amount, rollData.sides, rollData.modifier);
        }

        public static int GetAverageRoll(int amount, int sides)
        {
            return amount * (sides + 1) / 2;
        }
        public static int GetAverageRollWithModifier(int amount, int sides, int modifier)
        {
            return GetAverageRoll(amount, sides) + modifier;
        }
        public static int GetAverageRollWithModifier((int amount, int sides, int modifier) rollData)
        {
            return GetAverageRollWithModifier(rollData.amount, rollData.sides, rollData.modifier);
        }

        public static int RollDiceWithAdvantage(int amount, int sides)
        {
            int roll1 = RollDice(amount, sides);
            int roll2 = RollDice(amount, sides);
            return Math.Max(roll1, roll2);
        }
        public static int RollDiceWithAdvantageWithModifier(int amount, int sides, int modifier)
        {
            return RollDiceWithAdvantage(amount, sides) + modifier;
        }
        public static int RollDiceWithAdvantageWithModifier((int amount, int sides, int modifier) rollData)
        {
            return RollDiceWithAdvantageWithModifier(rollData.amount, rollData.sides, rollData.modifier);
        }

        public static int RollDiceWithDisadvantage(int amount, int sides)
        {
            int roll1 = RollDice(amount, sides);
            int roll2 = RollDice(amount, sides);
            return Math.Min(roll1, roll2);
        }
        public static int RollDiceWithDisadvantageWithModifier(int amount, int sides, int modifier)
        {
            return RollDiceWithDisadvantage(amount, sides) + modifier;
        }
        public static int RollDiceWithDisadvantageWithModifier((int amount, int sides, int modifier) rollData)
        {
            return RollDiceWithDisadvantageWithModifier(rollData.amount, rollData.sides, rollData.modifier);
        }
    }
}
