using System;
using System.Collections.Generic;

namespace Dicebag
{
    /// <summary>
    /// Simple too for rolling dice expressions such as 1d20, 4d6+2 etc
    /// </summary>
    /// <remarks>
    /// Dice expression BNF:
    ///            [roll] ::= [expression]((+|-)[expression])*
    ///      [expression] ::= [number]|[dice-expression]
    /// [dice-expression] ::= [number]?d[number]
    ///
    /// Any whitespace is ignored
    /// </remarks>
    public static class DiceRoller
    {
        /// <summary>
        /// Holds the result of a roll.
        /// </summary>
        public class Result
        {
            /// <summary>
            /// The total sum of the roll
            /// </summary>
            public int Total { get; private set; }

            /// <summary>
            /// The total modifier that was applied to the roll(s)
            /// </summary>
            public int Modifier { get; private set; }

            /// <summary>
            /// The results of the individual rolls.
            /// The key is the die type, "d20", "d6", etc
            /// The value is a list of the results of every roll with that die
            /// </summary>
            public Dictionary<string, List<int>> Rolls { get; private set; }
        }

        public static Result Roll(string diceExpression)
        {
            if(string.IsNullOrWhiteSpace(diceExpression))
            {
                throw new Exception("[ERROR]: The roll expression is empty.");
            }

            diceExpression = diceExpression.Trim();
            int offset = 0;

            void Panic(string errorMessage)
            {
                string errorPosition = new string(' ', offset) + "^";
                throw new Exception(string.Format("[ERROR]: ({0}) {1}\n\t{2}\n\t{3}", offset, errorMessage, diceExpression, errorPosition));
            }



            return null;
        }
    }
}

