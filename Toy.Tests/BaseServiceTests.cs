using System.IO;

namespace Toy.Tests
{
    public class BaseServiceTests
    {
        public void GenerateFile(string[] lines, string filePath)
        {
            using (StreamWriter file = new StreamWriter(filePath))
            {
                foreach (string line in lines)
                {
                    file.WriteLine(line);
                }
            }
        }
    }
}