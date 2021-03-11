namespace AcademiaManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Client
    {
        // A list that contain all academia entities.
        private List<Academia> _academias;

        /// <summary>
        /// Prevents a default instance of the Client class from being created.
        /// </summary>
        private Client(List<Academia> academias) => _academias = academias;

        /// <summary>
        /// Return a new Client instance internally without changing anything else.
        /// </summary>
        public static Client GetClient(List<Academia> academias)
            => new Client(academias);

        /// <summary>
        /// Inform the options to manage entities.
        /// </summary>
        public void MainMenu()
        {
            Console.Clear();
            Console.WriteLine(
                "============ MAIN MENU ============\n" +
                "1.\tManage Students\n" +
                "2.\tManage Lecturers\n" +
                "3.\tExit\n" +
                "===================================\n");
            int choice = int.Parse(Console.ReadLine());

            // Depend on the choice, the corresponding management option will be called;
            // otherwise, the choice for exit will stop the program.
            var cases = new Dictionary<Func<int, bool>, Action>
            {
                { x => x == 1, () => StudentManage.GetStudentManage(_academias)  },
                { x => x == 2, () => LecturerManage.GetStudentManage(_academias) },
                { x => x == 3, () => Environment.Exit(0)                         }
            };
            cases.First(c => c.Key(choice)).Value();
        }
    }
}