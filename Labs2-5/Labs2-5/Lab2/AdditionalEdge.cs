using System.Linq;

namespace Labs_DM.Lab2
{
    public record AdditionalEdge(string Path, int Length)
    {
        public bool Intersect(string other)
        {
            string combination = $"{Path.First()}" + $"{Path.Last()}" + $"{other.First()}" + $"{other.Last()}";

            return string.Join("", combination.OrderBy(x => x)) == string.Join("", combination.Distinct().OrderBy(x => x));
        }

        public bool IsSame(string other)
        {
            return string.Join("", Path.OrderBy(x => x)) == string.Join("", other.OrderBy(x => x));
        }
    }
}
