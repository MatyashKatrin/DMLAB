using Labs_DM.Shared;

namespace Labs_DM.Lab2
{
    public class Lab2_Postman
    {
        public static void Lab2_Execute()
        {
            int[][] array = FileHelper.ReadFromFile("Lab2/Files/l2-1.txt", true);
            char[] headers = StringHelper.GetNames(array.Length);

            GraphWithPostman graph = new GraphWithPostman();
            graph.FillFromMatrix(headers, array);

            graph.FindPostmanWay();
        }
    }
}
