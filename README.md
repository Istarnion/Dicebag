Dicebag
=======

Dicebag is a simple dice expression evaluator. See `Dicebag/DiceRoller.cs` for details on the accepted grammar, but in essence, it handles expressions on the form `1d20+4`.

Syntax errors (including empty or null input) are reported in the Message of thrown exceptions.

No dependencies.

## Requirements
[.NET 7.0](https://dotnet.microsoft.com/download/dotnet/7.0)

## Usage
`DiceRoller.Result result = DiceRoller.Roll(<dice expression>);`
The `DiceRoller.Result` class contains the total number rolled, the sum of all modifiers, as well as a record of all dice throws made.

## NOTE
The API is not yet stable, and the code is not yet optimized and may very well be very slow. Also note that the is not yet any max numbers set for the grammar. The implementation uses 32-bit signed integers and does not guard against overflow. This means the API can be vulnerable to DoS attacks where a simple input causes a overwhelming amount of rolls, and allocation of enough memory to store all the results.

