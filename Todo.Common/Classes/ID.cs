using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Common.Classes
{
    public sealed class ID : IEquatable<ID>
    {
        public static readonly ID Empty = new ID(null);

        private static readonly char[] chars =
        [
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 
            'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
        ];

        private static readonly byte charCount = (byte)chars.Length;

        private static UInt128 count = 0;

        private readonly string key;

        private static string GetNewKey()
        {
            UInt128 current = ID.count;
            var builder = new StringBuilder();

            while (current > 0)
            {
                current--;
                int index = (int)(current % ID.charCount);
                builder.Append(ID.chars[index]);
                current /= ID.charCount;
            }

            // Reverse the string
            for (int i = 0, j = builder.Length - 1; i < j; i++, j--)
            {
                (builder[i], builder[j]) = (builder[j], builder[i]);
            }

            return builder.ToString();

        }

        // Private constructor for ID.Empty with a dummy argument
        private ID(object? _)
        {
            this.key = string.Empty;
        }

        public ID()
        {
            ID.count++;

            this.key = ID.GetNewKey();
        }

        public override string ToString() =>
            this.key;

        public bool Equals(string? str)
        {
            if (str is null)
                return false;

            return (this.key == str);
        }

        public override bool Equals(object? obj)
        {
            if (obj is ID id)
                return this.Equals(id);

            return false;
        }

        public bool Equals(ID? other)
        {
            if (other is null)
                return false;

            return (this.key == other.key);
        }

        public static bool operator ==(ID? left, ID? right)
        {
            if (left is null)
                return (right is null);

            return left.Equals(right);
        }

        public static bool operator !=(ID? left, ID? right) =>
            (!(left == right));

        public override int GetHashCode() =>
            this.key.GetHashCode();
    }
}
