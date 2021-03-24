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
    }
}

