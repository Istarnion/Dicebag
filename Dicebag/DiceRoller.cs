using System;
using System.Collections.Generic;

namespace Dicebag
{
    public class DiceRollException : Exception
    {
        public string Expression { get; private set; }
        public int Offset { get; private set; }

        public DiceRollException(string message, string expression, int offset) : base(message)
        {
            Expression = expression ?? "";
            Offset = offset;
        }
    }

    /// <summary>
    /// Simple tool for rolling dice expressions such as 1d20, 4d6+2 etc
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
            public int Total { get; set; }

            /// <summary>
            /// The total modifier that was applied to the roll(s)
            /// </summary>
            public int Modifier { get; set; }

            /// <summary>
            /// The results of the individual rolls.
            /// The key is the die type, "d20", "d6", etc
            /// The value is a list of the results of every roll with that die
            /// </summary>
            public Dictionary<string, List<int>> Rolls { get; set; }
        }

        private class ExpressionParseResult
        {
            public int Constant;
            public int NumDice;
            public int DiceFaces;
        }

        public static Result Roll(string diceExpression)
        {
            if (string.IsNullOrWhiteSpace(diceExpression))
            {
                throw new DiceRollException("[ERROR]: The roll expression is empty.", diceExpression, 0);
            }

            diceExpression = diceExpression.Trim();
            int offset = 0;

            void Panic(string errorMessage)
            {
                throw new DiceRollException(errorMessage, diceExpression, offset);
            }

            char Peek()
            {
                return diceExpression[offset];
            }

            char Pop()
            {
                char result = diceExpression[offset];
                ++offset;
                return result;
            }

            bool EOF()
            {
                return offset >= diceExpression.Length;
            }

            void GobbleWhitespace()
            {
                while (!EOF() && char.IsWhiteSpace(Peek()))
                {
                    ++offset;
                }
            }

            bool Expect(char c)
            {
                GobbleWhitespace();
                if (diceExpression[offset] == c)
                {
                    ++offset;
                    return true;
                }
                else
                {
                    return false;
                }
            }


            int ConsumeNumber()
            {
                int result = 0;
                GobbleWhitespace();
                while (!EOF() && char.IsDigit(Peek()))
                {
                    char c = Pop();
                    int i = c - '0';
                    result = result * 10 + i;
                }

                return result;
            }

            int ExpectNonZeroNumber()
            {
                int result = ConsumeNumber();

                if (result <= 0)
                {
                    Panic("Expected a non-zero number.");
                }

                return result;
            }

            // Can be just [number] or [number]?d[number]
            ExpressionParseResult ConsumeExpression()
            {
                var result = new ExpressionParseResult();

                GobbleWhitespace();
                if (char.IsDigit(Peek()))
                {
                    int number = ConsumeNumber();
                    GobbleWhitespace();

                    if (!EOF() && Peek() == 'd')
                    {
                        ++offset;
                        int number2 = ExpectNonZeroNumber();

                        result.NumDice = number;
                        result.DiceFaces = number2;
                    }
                    else
                    {
                        result.Constant = number;
                    }
                }
                else if (Expect('d'))
                {
                    int number2 = ExpectNonZeroNumber();

                    result.NumDice = 1;
                    result.DiceFaces = number2;
                }
                else
                {
                    Panic("Expected dice expression.");
                }

                return result;
            }

            var rng = new Random();
            var result = new Result();
            result.Rolls = new Dictionary<string, List<int>>();

            int RollDice(int numFaces)
            {
                int result = 1 + rng.Next(numFaces);
                return result;
            }

            // This is for iterations of the do..while loop other than the first one.
            // It gets set to 1 or -1 depending on the connection operator being + or -,
            int sign = 1;

            do
            {
                var expressionResult = ConsumeExpression();

                if (expressionResult.NumDice > 0)
                {
                    string dieKey = "d" + expressionResult.DiceFaces;
                    var results = new List<int>(expressionResult.NumDice);
                    for (int i = 0; i < expressionResult.NumDice; ++i)
                    {
                        int roll = RollDice(expressionResult.DiceFaces);
                        results.Add(roll);
                        result.Total += roll * sign;
                    }

                    if (result.Rolls.ContainsKey(dieKey))
                    {
                        result.Rolls[dieKey].AddRange(results);
                    }
                    else
                    {
                        result.Rolls[dieKey] = results;
                    }
                }
                else
                {
                    result.Modifier += expressionResult.Constant * sign;
                }

                GobbleWhitespace();
                if (!EOF())
                {
                    if (Peek() == '+')
                    {
                        ++offset;
                        sign = 1;
                    }
                    else if (Peek() == '-')
                    {
                        ++offset;
                        sign = -1;
                    }
                }
            }
            while (!EOF());

            result.Total += result.Modifier;

            return result;
        }
    }
}

