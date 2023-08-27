using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet.Attributes;
using Dicebag;

namespace Benchmarks;

[MemoryDiagnoser]
[SuppressMessage("Performance", "CA1822:Mark members as static")]
public class DiceRollerBenchmarks
{
    [Benchmark]
    public void Roll_SingleDigitNumber() => DiceRoller.Roll("1");

    [Benchmark]
    public void Roll_MultiDigitNumber() => DiceRoller.Roll("2895");

    [Benchmark]
    public void Roll_SimpleRoll() => DiceRoller.Roll("1d20");

    [Benchmark]
    public void Roll_SimpleRollWithModifier() => DiceRoller.Roll("1d20+5");

    [Benchmark]
    public void Roll_WhitespaceStress() => DiceRoller.Roll("   1     d      20 +    5      ");

    [Benchmark]
    public void Roll_CompoundRoll() => DiceRoller.Roll("1d20+2d6");

    [Benchmark]
    public void Roll_MultipleDice() => DiceRoller.Roll("10d6+5d8+2d12");

    [Benchmark]
    public void Roll_LargeNumberOfDice() => DiceRoller.Roll("100d6+50d8+25d12");

    [Benchmark]
    public void Roll_LargeDiceFaces() => DiceRoller.Roll("10d1000+5d5000");

    [Benchmark]
    public void Roll_CompoundLargeExpressions() => DiceRoller.Roll("100d1000+50d5000+25d10000+10d6+5d8+2d12");

    private static readonly string InsaneExpression = string.Concat(Enumerable.Repeat("100d1000+", 1000));
    [Benchmark]
    public void Roll_InsaneExpression() => DiceRoller.Roll(InsaneExpression);

    private const string BaseExpression = "100d1000+50d5000+25d10000+10d6+5d8+2d12+";
    private static readonly string MassiveExpression = string.Concat(Enumerable.Repeat(BaseExpression, 100));
    [Benchmark]
    public void Roll_MassiveCompoundExpression()
    {
        DiceRoller.Roll(MassiveExpression);
    }
}