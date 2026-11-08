# Advent of Code 2025

Solutions for [Advent of Code 2025](https://adventofcode.com/2025) written in C# on .NET 10.

## Project structure

* `Helpers` – shared class library referenced by all day projects
* `Day01` … `Day12` – one console app per day

Each day project contains:

* `Program.cs` – solution code
* `input/input.txt` – puzzle input (not committed, see below)
* `output/` – directory for any generated output

## Puzzle inputs

Advent of Code asks participants not to publish their puzzle inputs, so the
`input/` directories are git-ignored and not part of this repository.
To run a solution, save your own input as `Day##/input/input.txt`.

## Running

```bash
# build everything
dotnet build

# run a specific day
dotnet run --project Day01
```

Target framework: `net10.0`.
