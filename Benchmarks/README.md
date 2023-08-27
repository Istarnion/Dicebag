// * Summary *

BenchmarkDotNet v0.13.7, macOS Ventura 13.4.1 (c) (22F770820d) [Darwin 22.5.0]
Apple M2 Max, 1 CPU, 12 logical and 12 physical cores
.NET SDK 7.0.305
  [Host]     : .NET 7.0.8 (7.0.823.31807), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 7.0.8 (7.0.823.31807), Arm64 RyuJIT AdvSIMD


|                         Method |         Mean |       Error |    StdDev |     Gen0 |     Gen1 |     Gen2 | Allocated |
|------------------------------- |-------------:|------------:|----------:|---------:|---------:|---------:|----------:|
|         Roll_SingleDigitNumber |     547.5 ns |     2.10 ns |   1.97 ns |   0.0296 |        - |        - |     248 B |
|          Roll_MultiDigitNumber |     554.1 ns |     2.54 ns |   2.37 ns |   0.0296 |        - |        - |     248 B |
|                Roll_SimpleRoll |     613.0 ns |     2.12 ns |   1.98 ns |   0.0610 |        - |        - |     512 B |
|    Roll_SimpleRollWithModifier |     624.5 ns |     2.11 ns |   1.97 ns |   0.0658 |        - |        - |     552 B |
|          Roll_WhitespaceStress |     680.2 ns |     1.20 ns |   1.06 ns |   0.0782 |        - |        - |     656 B |
|              Roll_CompoundRoll |     670.1 ns |     2.28 ns |   2.13 ns |   0.0772 |        - |        - |     648 B |
|              Roll_MultipleDice |     810.6 ns |     0.97 ns |   0.81 ns |   0.1001 |        - |        - |     840 B |
|         Roll_LargeNumberOfDice |   1,728.1 ns |     2.07 ns |   1.94 ns |   0.1755 |        - |        - |    1472 B |
|            Roll_LargeDiceFaces |     748.9 ns |     0.96 ns |   0.80 ns |   0.0887 |        - |        - |     744 B |
|  Roll_CompoundLargeExpressions |   2,197.0 ns |     3.20 ns |   2.83 ns |   0.2747 |        - |        - |    2304 B |
|          Roll_InsaneExpression | 543,154.4 ns | 1,055.66 ns | 987.47 ns | 221.6797 | 221.6797 | 221.6797 | 1389134 B |
| Roll_MassiveCompoundExpression | 169,149.8 ns |   535.60 ns | 501.00 ns |  43.9453 |  10.9863 |        - |  367872 B |

// * Hints *
Outliers
  DiceRollerBenchmarks.Roll_WhitespaceStress: Default          -> 1 outlier  was  removed (690.36 ns)
  DiceRollerBenchmarks.Roll_CompoundRoll: Default              -> 2 outliers were detected (667.18 ns, 669.11 ns)
  DiceRollerBenchmarks.Roll_MultipleDice: Default              -> 2 outliers were removed, 4 outliers were detected (810.53 ns, 811.39 ns, 814.27 ns, 814.58 ns)
  DiceRollerBenchmarks.Roll_LargeDiceFaces: Default            -> 2 outliers were removed, 4 outliers were detected (749.15 ns, 749.25 ns, 754.86 ns, 761.57 ns)
  DiceRollerBenchmarks.Roll_CompoundLargeExpressions: Default  -> 1 outlier  was  removed (2.21 us)
  DiceRollerBenchmarks.Roll_MassiveCompoundExpression: Default -> 2 outliers were detected (167.88 us, 168.09 us)

// * Legends *
  Mean      : Arithmetic mean of all measurements
  Error     : Half of 99.9% confidence interval
  StdDev    : Standard deviation of all measurements
  Gen0      : GC Generation 0 collects per 1000 operations
  Gen1      : GC Generation 1 collects per 1000 operations
  Gen2      : GC Generation 2 collects per 1000 operations
  Allocated : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)
  1 ns      : 1 Nanosecond (0.000000001 sec)

// * Diagnostic Output - MemoryDiagnoser *


// ***** BenchmarkRunner: End *****
Run time: 00:03:29 (209.82 sec), executed benchmarks: 12

Global total time: 00:03:31 (211.92 sec), executed benchmarks: 12
// * Artifacts cleanup *
Artifacts cleanup is finished
BenchmarkDotNet.Reports.Summary