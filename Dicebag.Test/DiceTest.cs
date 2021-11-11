using NUnit.Framework;

namespace Dicebag.Test
{
    public class DiceTests
    {
        [Test]
        public void SingleDigitNumber()
        {
            string expression = "1";
            DiceRoller.Result result = DiceRoller.Roll(expression);
            Assert.AreEqual(1, result.Total);
            Assert.AreEqual(1, result.Modifier);
            Assert.AreEqual(0, result.Rolls.Count);
        }

        [Test]
        public void MultiDigitNumber()
        {
            string expression = "2895";
            DiceRoller.Result result = DiceRoller.Roll(expression);
            Assert.AreEqual(2895, result.Total);
            Assert.AreEqual(2895, result.Modifier);
            Assert.AreEqual(0, result.Rolls.Count);
        }

        [Test]
        public void SimpleRoll()
        {
            string expression = "1d20";
            DiceRoller.Result result = DiceRoller.Roll(expression);
            Assert.Less(0, result.Total);
            Assert.Greater(20, result.Total);
            Assert.AreEqual(0, result.Modifier);
            Assert.AreEqual(1, result.Rolls.Count);
        }

        [Test]
        public void SimpleRollShorthand()
        {
            string expression = "d20";
            DiceRoller.Result result = DiceRoller.Roll(expression);
            Assert.Less(0, result.Total);
            Assert.Greater(20, result.Total);
            Assert.AreEqual(0, result.Modifier);
            Assert.AreEqual(1, result.Rolls.Count);
        }

        [Test]
        public void SimpleRollUppercase()
        {
            string expression = "1D20";
            DiceRoller.Result result = DiceRoller.Roll(expression);
            Assert.Less(0, result.Total);
            Assert.Greater(20, result.Total);
            Assert.AreEqual(0, result.Modifier);
            Assert.AreEqual(1, result.Rolls.Count);
        }

        [Test]
        public void SimpleRollShorthandUppercase()
        {
            string expression = "D20";
            DiceRoller.Result result = DiceRoller.Roll(expression);
            Assert.Less(0, result.Total);
            Assert.Greater(20, result.Total);
            Assert.AreEqual(0, result.Modifier);
            Assert.AreEqual(1, result.Rolls.Count);
        }


        [Test]
        public void RollWithModifier()
        {
            string expression = "1d20+5";
            DiceRoller.Result result = DiceRoller.Roll(expression);
            Assert.Less(5, result.Total);
            Assert.Greater(25, result.Total);
            Assert.AreEqual(5, result.Modifier);
            Assert.AreEqual(1, result.Rolls.Count);
        }

        [Test]
        public void WhitespaceStress()
        {
            string expression = "   1     d      20 +    5      ";
            DiceRoller.Result result = DiceRoller.Roll(expression);
            Assert.Less(5, result.Total);
            Assert.Greater(25, result.Total);
            Assert.AreEqual(5, result.Modifier);
            Assert.AreEqual(1, result.Rolls.Count);
        }

        [Test]
        public void CompondRoll()
        {
            string expression = "1d20+2d6";
            DiceRoller.Result result = DiceRoller.Roll(expression);
            Assert.Less(1, result.Total);
            Assert.Greater(27, result.Total);
            Assert.AreEqual(0, result.Modifier);
            Assert.AreEqual(2, result.Rolls.Count);
        }

        [Test]
        public void CompondRollWithShorthands()
        {
            string expression = "d12+d8";
            DiceRoller.Result result = DiceRoller.Roll(expression);
            Assert.Less(1, result.Total);
            Assert.Greater(21, result.Total);
            Assert.AreEqual(0, result.Modifier);
            Assert.AreEqual(2, result.Rolls.Count);
        }

        [Test]
        public void EmptyInput()
        {
            string expression = "";
            try
            {
                DiceRoller.Result result = DiceRoller.Roll(expression);
                Assert.Fail();
            }
            catch (DiceRollException)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test]
        public void NullInput()
        {
            string expression = null;
            try
            {
                DiceRoller.Result result = DiceRoller.Roll(expression);
                Assert.Fail();
            }
            catch (DiceRollException)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test]
        public void BadInput()
        {
            string expression = "This is not a dice roll expression";
            try
            {
                DiceRoller.Result result = DiceRoller.Roll(expression);
                Assert.Fail();
            }
            catch (DiceRollException)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test]
        public void ChainExpression()
        {
            // NOTE(istarnion): This is sort of an unintended feature.
            // discuss with users and see if this is actually something
            // we should reject as invalid syntax.
            string expression = "1d1 - 1d1d100d100d100";
            DiceRoller.Result result = DiceRoller.Roll(expression);
            Assert.Positive(result.Total);
        }

        [Test]
        public void NegativeExpression()
        {
            string expression = "-1d20";
            try
            {
                DiceRoller.Result result = DiceRoller.Roll(expression);
                Assert.Negative(result.Total);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Test]
        public void ExplicitPositiveExpression()
        {
            string expression = "+1d20";
            try
            {
                DiceRoller.Result result = DiceRoller.Roll(expression);
                Assert.Positive(result.Total);
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
}
