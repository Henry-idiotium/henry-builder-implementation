using System.Collections.Generic;

namespace AcademiaManagement
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Client.GetClient(new List<Academia>()).MainMenu();
        }
    }
}