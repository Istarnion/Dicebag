using BenchmarkDotNet.Running;
using Benchmarks;

var summary = BenchmarkRunner.Run<DiceRollerBenchmarks>();
Console.WriteLine(summary);