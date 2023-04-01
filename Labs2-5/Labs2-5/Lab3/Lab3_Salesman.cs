using Labs_DM.Shared;

namespace Labs_DM.Lab3
{
    public class Lab3_Salesman
    {
        public static void Lab3_Execute()
        {
            int[][] array = FileHelper.ReadFromFile("Lab3/Files/l3-1.txt", true);
            char[] headers = StringHelper.GetNames(array.Length);

            GraphWithSalesman graph = new GraphWithSalesman();  
            graph.FillFromMatrix(headers, array);

            graph.FindSalesmanPath('F');
        }
    }
}
