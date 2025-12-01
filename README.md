# Advent of Code 2025

Solutions for Advent of Code 2025 using net10.0.

## Project Structure

* Helpers - Shared class library referenced by all day projects
* Day01 through Day12 - Individual console applications for each day

Each day project contains:
* Program.cs - Main entry point and solution code
* input/input.txt - Puzzle input file
* output/ - Directory for output files
* Reference to Helpers project

## Running Solutions

To run a specific day's solution:
    dotnet run --project Day01

To build all projects:
    dotnet build

## File Organization

Input files should be placed in Day##/input/input.txt for each day.
