using Labs_DM.Shared;

namespace Labs_DM.Lab4
{
    public class Lab4_Chanels
    {
        public static void Lab4_Execute()
        {
            int[][] array = FileHelper.ReadFromFile("Lab4/Files/l4-2.txt", true);
            char[] headers = StringHelper.GetNames(array.Length);

            GraphWithChannels graph = new GraphWithChannels();
            graph.FillFromMatrix(headers, array);

            graph.FordFulkerson();
        }
    }
}
