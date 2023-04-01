using System.Linq;

namespace Labs_DM.Shared
{
    public static class StringHelper
    {
        public static char[] GetNames(int count)
        {
            return Enumerable.Range('A', count).Select(x => (char)x).ToArray();
        }
    } 
}
