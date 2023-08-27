using NUnit.Framework;

namespace Dicebag.Test;

public class DiceTests
{
    private static void AssertRange(int total, int low, int high)
    {
        Assert.LessOrEqual(low, total);
        Assert.GreaterOrEqual(high, total);
    }
        
    [Test]
    public void SingleDigitNumber()
    {
        const string expression = "1";
        var result = DiceRoller.Roll(expression);
        Assert.AreEqual(1, result.Total);
        Assert.AreEqual(1, result.Modifier);
        Assert.AreEqual(0, result.Rolls.Count);
    }

    [Test]
    public void MultiDigitNumber()
    {
        const string expression = "2895";
        var result = DiceRoller.Roll(expression);
        Assert.AreEqual(2895, result.Total);
        Assert.AreEqual(2895, result.Modifier);
        Assert.AreEqual(0, result.Rolls.Count);
    }

    [Test]
    public void SimpleRoll()
    {
        const string expression = "1d20";
        var result = DiceRoller.Roll(expression);
        AssertRange(result.Total, 1, 20);
        Assert.AreEqual(0, result.Modifier);
        Assert.AreEqual(1, result.Rolls.Count);
    }

    [Test]
    public void SimpleRollShorthand()
    {
        const string expression = "d20";
        var result = DiceRoller.Roll(expression);
        AssertRange(result.Total, 1, 20);
        Assert.AreEqual(0, result.Modifier);
        Assert.AreEqual(1, result.Rolls.Count);
    }

    [Test]
    public void SimpleRollUppercase()
    {
        const string expression = "1D20";
        var result = DiceRoller.Roll(expression);
        AssertRange(result.Total, 1, 20);
        Assert.AreEqual(0, result.Modifier);
        Assert.AreEqual(1, result.Rolls.Count);
    }

    [Test]
    public void SimpleRollShorthandUppercase()
    {
        const string expression = "D20";
        var result = DiceRoller.Roll(expression);
        AssertRange(result.Total, 1, 20);
        Assert.AreEqual(0, result.Modifier);
        Assert.AreEqual(1, result.Rolls.Count);
    }


    [Test]
    public void RollWithModifier()
    {
        const string expression = "1d20+5";
        var result = DiceRoller.Roll(expression);
        AssertRange(result.Total, 6, 25);
        Assert.AreEqual(5, result.Modifier);
        Assert.AreEqual(1, result.Rolls.Count);
    }

    [Test]
    public void WhitespaceStress()
    {
        const string expression = "   1     d      20 +    5      ";
        var result = DiceRoller.Roll(expression);
        AssertRange(result.Total, 6, 25);
        Assert.AreEqual(5, result.Modifier);
        Assert.AreEqual(1, result.Rolls.Count);
    }

    [Test]
    public void CompoundRoll()
    {
        const string expression = "1d20+2d6";
        var result = DiceRoller.Roll(expression);
        AssertRange(result.Total, 3, 32);
        Assert.AreEqual(0, result.Modifier);
        Assert.AreEqual(2, result.Rolls.Count);
    }

    [Test]
    public void CompoundRollWithShorthands()
    {
        const string expression = "d12+d8";
        var result = DiceRoller.Roll(expression);
        AssertRange(result.Total, 2, 20);
        Assert.AreEqual(0, result.Modifier);
        Assert.AreEqual(2, result.Rolls.Count);
    }

    [Test]
    public void EmptyInput()
    {
        const string expression = "";
        try
        {
            var result = DiceRoller.Roll(expression);
            Assert.Fail();
        }
        catch (DiceRollException)
        {
            Assert.Pass();
        }
    }

    [Test]
    public void NullInput()
    {
        const string expression = null;
        try
        {
            var result = DiceRoller.Roll(expression);
            Assert.Fail();
        }
        catch (DiceRollException)
        {
            Assert.Pass();
        }
    }

    [Test]
    public void BadInput()
    {
        const string expression = "This is not a dice roll expression";
        try
        {
            var result = DiceRoller.Roll(expression);
            Assert.Fail();
        }
        catch (DiceRollException)
        {
            Assert.Pass();
        }
    }

    [Test]
    public void ChainExpression()
    {
        // NOTE(istarnion): This is sort of an unintended feature.
        // discuss with users and see if this is actually something
        // we should reject as invalid syntax.
        const string expression = "1d1 - 1d1d100d100d100";
        var result = DiceRoller.Roll(expression);
        Assert.Positive(result.Total);
    }

    [Test]
    public void NegativeExpression()
    {
        const string expression = "-1d20";
        try
        {
            var result = DiceRoller.Roll(expression);
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
        const string expression = "+1d20";
        try
        {
            var result = DiceRoller.Roll(expression);
            Assert.Positive(result.Total);
        }
        catch
        {
            Assert.Fail();
        }
    }
}