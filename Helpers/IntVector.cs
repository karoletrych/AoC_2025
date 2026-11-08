using System.Diagnostics;

namespace Helpers;

[DebuggerDisplay( "{X},{Y},{Z}")]
public readonly record struct IntVector : IEquatable<IntVector>
{
    public readonly long X;
    public readonly long Y;
    public readonly long Z;

    public IntVector()
    {
        
    }

    public override string ToString()
    {
        return $"{X},{Y},{Z}";
    }

    public readonly bool Equals(IntVector other)
    {
        return X == other.X && Y == other.Y && Z == other.Z;
    }

    public readonly override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }

    public IntVector(string s, string s1, string s2)
    {
        X = long.Parse(s);
        Y = long.Parse(s1);
        Z = long.Parse(s2);
    }

    public double Distance(IntVector other)
    {
        var dx = X - other.X;
        var dy = Y - other.Y;
        var dz = Z - other.Z;
        checked
        {
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }
    }
}