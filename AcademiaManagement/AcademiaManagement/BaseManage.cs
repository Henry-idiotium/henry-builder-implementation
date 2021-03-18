namespace AcademiaManagement
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    internal class BaseManage
    {
        // A list that contain all academia entities.
        protected List<Academia> AcademiaList;

        /// <summary>
        /// Take an input argument of AcademicPosition enumerable data type, then inform
        /// the fields needed to be provided to fabricate an acdemia object. Then add it
        /// into the current academia list.
        /// </summary>
        private void Add(AcademicPosition position)
        {
            Console.Clear();

            Console.WriteLine("Please input the following fields:\n" +
                              "=============================\n");

            // Input for each corresponding fields.
            var divisionType = position == AcademicPosition.Student ? "CLASS" : "DEPARTMENT";
            string[] fieldNames = {
                "ID: ",
                "NAME: ",
                "DATE OF BIRTH: ",
                "EMAIL: ",
                "ADDRESS: ",
                $"{divisionType}: "};
            var fieldsToAdd = new List<string>();
            for (int i = 0; i < 6; i++)
            {
                Console.Write(fieldNames[i]);
                fieldsToAdd.Add(Console.ReadLine());
            }

            // Validate ID and Email of the inputted academia information. An error
            // message will be displayed, if the ID and Email fields are null or match
            // other entities.
            string[,] fieldIndication =
            {
                { "0", "ID"    },
                { "3", "Email" }
            };
            for (int i = 0; i < fieldIndication.Length / 2; i++)
            {
                var foo1 = fieldsToAdd[int.Parse(fieldIndication[i, 0])];
                var foo2 = AcademiaList.Where(a => a.AcademiaID == foo1);

                if (Validation.FieldIsNullOrWhiteSpace(foo1, fieldIndication[i, 1])
                    || Validation.IsDuplicate(foo2, fieldIndication[i, 1]))
                    return;
            }

            // If any input fields is empty then it will assign with redacted text.
            for (int i = 0; i < 6; i++)
                fieldsToAdd[i] = fieldsToAdd[i].All(char.IsWhiteSpace) ?
                    "[...]" : fieldsToAdd[i];

            // Preparing for the creation step.
            var _director = new Director();
            var _builder = new AcademiaBuilder();
            _director.Builder = _builder;

            // Depend on the position parameter, the corresponding academia representation
            // (or entity) fabrication process will be initiated.
            var cases = new Dictionary<Func<AcademicPosition, bool>, Action>
            {
                { p => p == AcademicPosition.Student,
                            () => _director.MakeStudent(fieldsToAdd.ToArray())  },
                { p => p == AcademicPosition.Lecturer,
                            () => _director.MakeLecturer(fieldsToAdd.ToArray()) },
            };
            cases.First(c => c.Key(position)).Value();

            // Add the created entity to the global list of academias.
            AcademiaList.Add(_builder.GetAcademia());

            // Display successful Message.
            Console.WriteLine("\n=============================\n" +
                              $"Add {position} with ID - {fieldsToAdd[0]} was successfully!");
            Console.ReadLine();
        }

        /// <summary>
        /// Take an input argument of AcademicPosition enumerable data type; then, it
        /// will display and remove the correspond academia entity ID after inform and
        /// request to provide the ID keyword.
        /// </summary>
        private void Delete(AcademicPosition position)
        {
            Console.Clear();

            // Check if list is null and display error message.
            if (Validation.ListIsNull(AcademiaList, position)) return;

            // Input the Academia ID to find the academia entity to delete.
            Console.Write("Delete {0} with ID: ", position.ToString());
            var itemToDelete = AcademiaList.SingleOrDefault(a => a.AcademiaID == Console.ReadLine()
                                                                 && a.AcademiaPosition == position);

            // Check if the input is null. If so then return back to menu, otherwise,
            // proceed to remove it.
            if (Validation.AcademiaIsNull(itemToDelete, itemToDelete.AcademiaID)
                || Validation.FieldIsNullOrWhiteSpace(itemToDelete.AcademiaID, "ID")) return;
            AcademiaList.Remove(itemToDelete);

            // Display successful message.
            Console.WriteLine(itemToDelete.Display() +
                              "\n\n=============================\n" +
                              "Delete successfully!",
                              $"Delete {position} with ID - {itemToDelete.AcademiaID} was successfully!");
            Console.ReadLine();
        }

        /// <summary>
        /// Take an input argument of AcademicPosition enumerable data type, then take
        /// new data to reassign to the updating academia entity.
        /// </summary>
        private void Update(AcademicPosition position)
        {
            Console.Clear();

            // Check if list is null and display error message.
            if (Validation.ListIsNull(AcademiaList, position)) return;

            // Input the Academia ID to find the academia entity to update.
            Console.Write("Update {0} with ID: ", position);
            var inputField = Console.ReadLine();
            var itemToUpdate = AcademiaList.SingleOrDefault(a => a.AcademiaID == inputField
                                                                 && a.AcademiaPosition == position);

            // Check if the input is null. If so then return back to menu, otherwise,
            // proceed to display it.
            if (Validation.AcademiaIsNull(itemToUpdate, inputField)
                || Validation.FieldIsNullOrWhiteSpace(itemToUpdate.AcademiaID, "ID"))
                return;

            Console.WriteLine("=============================\n" +
                              itemToUpdate.Display() +
                              "\n\n=============================\n" +
                              "Note: Press enter to not update that field\n" +
                              "Fields:\n");

            // Input for each corresponding fields.
            var divisionType = position == AcademicPosition.Student ? "CLASS" : "DEPARTMENT";
            string[] fieldNames = {
                "ID: ",
                "NAME: ",
                "DATE OF BIRTH: ",
                "EMAIL: ",
                "ADDRESS: ",
                $"{divisionType}: "};
            var fieldsToUpdate = new List<string>();
            for (int i = 0; i < 6; i++)
            {
                Console.Write(fieldNames[i]);
                fieldsToUpdate.Add(Console.ReadLine());
            }

            // Validate ID and Email of the inputted academia information. An error message
            // will be displayed, if the ID and Email fields are null or match other entities.
            var bar = new int[] { 0, 3 };
            for (int i = 0; i < bar.Length; i++)
            {
                var foo = AcademiaList.Where(a => a.AcademiaID == fieldsToUpdate[bar[i]]);
                if (Validation.IsDuplicate(foo, fieldNames[bar[i]])) return;
            }

            // Depend on each inputted new data, the academia entity to update will be
            // reassigned new data if it is not null or contains only whitespace.
            var mapFields = new Dictionary<Func<int, bool>, Action>
            {
                { x => x == 0, () => itemToUpdate.AcademiaID          = fieldsToUpdate[0] },
                { x => x == 1, () => itemToUpdate.AcademiaName        = fieldsToUpdate[1] },
                { x => x == 2, () => itemToUpdate.AcademiaDateOfBirth = DateTime.Parse(fieldsToUpdate[2],
                                                                                       CultureInfo.CreateSpecificCulture("de-DE"))},
                { x => x == 3, () => itemToUpdate.AcademiaEmail       = fieldsToUpdate[3] },
                { x => x == 4, () => itemToUpdate.AcademiaAddress     = fieldsToUpdate[4] },
                { x => x == 5, () => itemToUpdate.AcademiaDivision    = fieldsToUpdate[5] }
            };
            for (int i = 0; i < fieldsToUpdate.Count; i++)
                if (!string.IsNullOrWhiteSpace(fieldsToUpdate[i]))
                    mapFields.First(x => x.Key(i)).Value();

            // Display successfull message.
            Console.WriteLine(
                "\n=============================\n" +
                $"Update {position} with with ID - {itemToUpdate.AcademiaID} successfully!");
            Console.ReadLine();
        }

        /// <summary>
        /// Take an input argument of AcademicPosition enumerable data type; then, display
        /// full information of a specific academia entity after request and take in the
        /// academia name partially or fully.
        /// </summary>
        private void Search(AcademicPosition position)
        {
            Console.Clear();

            // Check if list is null and display error message.
            if (Validation.ListIsNull(AcademiaList, position)) return;

            // Input Academia name to search.
            Console.Write("Enter the {0} name: ", position);

            // Get a list of results.
            var resultList = AcademiaList.Where(a => a.AcademiaName.Contains(Console.ReadLine())
                                                     && a.AcademiaPosition == position)
                                         .ToArray();

            Console.Write("\n=============================" +
                          "\nRESULT: ");

            // check if there are any results, if so then display brief information of each
            // result; otherwise, display an error.
            if (!resultList.Any())
            {
                Console.Write("found no results!");
                Console.ReadLine();
                return;
            }
            else
            {
                Console.WriteLine("found {0} results.\n", resultList.Count());
                for (int i = 0; i < resultList.Length; i++)
                {
                    Console.WriteLine(
                        $"{i}. " +
                        $"ID: {resultList[i].AcademiaID} " +
                        $"| NAME: {resultList[i].AcademiaName}\n");
                }
            }

            // Choose a desire result. Then, validate choice input, if it is null, then
            // display an error. Otherwise, it display full information of the chose
            // academia entity.
            Console.Write("=============================\n" +
                          "Please choose one by it index: ");
            var choice = Console.ReadLine();

            if (Validation.ChoiceIsValid(choice, 1, resultList.Length)) return;
            Console.WriteLine($"\nThe {position} full information: ");
            Console.WriteLine(resultList[int.Parse(choice)].Display());

            Console.ReadLine();
        }

        /// <summary>
        /// Take an input argument of AcademicPosition enumerable data type; then display
        /// all academia entities of the corresponding inputted AcademicPosition.
        /// </summary>
        private void ViewAll(AcademicPosition position)
        {
            Console.Clear();

            // Check if list is null and display error message.
            if (Validation.ListIsNull(AcademiaList, position)) return;

            // Get and display a list of academia entities that have the correspond
            // position.
            var resultList = AcademiaList.Where(a => a.AcademiaPosition == position)
                                         .ToList();
            Console.WriteLine($"LIST OF {position.ToString()}S :\n" +
                              "======================");
            foreach (var a in resultList) Console.WriteLine(a.Display());
            Console.ReadLine();
        }

        /// <summary>
        /// Display all operation options and prompt to choose one.
        /// </summary>
        public void OptionDisplay(AcademicPosition position)
        {
            Console.Clear();
            Console.WriteLine("=========== STUDENT MANAGE ===========\n" +
                              "1.\tView all {0}s\n" +
                              "2.\tSearch {0}\n" +
                              "3.\tAdd new {0}\n" +
                              "4.\tDelete {0}s\n" +
                              "5.\tUpdate {0}\n" +
                              "6.\tBack to main menu\n" +
                              "======================================\n",
                              position);

            var initialChoice = Console.ReadLine();

            // Validate choice
            if (Validation.ChoiceIsValid(initialChoice, 1, 6))
                Client.GetClient(AcademiaList).MainMenu();

            var choice = int.Parse(initialChoice);

            var cases = new Dictionary<Func<int, bool>, Action>
                {
                    { x => x == 1, () => ViewAll(position)                         },
                    { x => x == 2, () => Search(position)                          },
                    { x => x == 3, () => Add(position)                             },
                    { x => x == 4, () => Delete(position)                          },
                    { x => x == 5, () => Update(position)                          },
                    { x => x == 6, () => Client.GetClient(AcademiaList).MainMenu() }
                };
            cases.First(c => c.Key(choice)).Value();
        }
    }

    internal class LecturerManage : BaseManage
    {
        /// <summary>
        /// Prevents a default instance of the LecturerManage class from being created.
        /// It provides the action that can be to modify or view data.
        /// </summary>
        private LecturerManage(List<Academia> academias)
        {
            AcademiaList = academias;
            var position = AcademicPosition.Lecturer;

            while (true) OptionDisplay(position);
        }

        /// <summary>
        /// Return a new LecturerManage instance internally without changing anything else.
        /// </summary>
        public static LecturerManage GetStudentManage(List<Academia> academias)
            => new LecturerManage(academias);
    }

    internal class StudentManage : BaseManage
    {
        /// <summary>
        /// Prevents a default instance of the StudentManage class from being created.
        /// It provides the action that can be to modify or view data.
        /// </summary>
        private StudentManage(List<Academia> academias)
        {
            AcademiaList = academias;
            var position = AcademicPosition.Student;

            while (true) OptionDisplay(position);
        }

        /// <summary>
        /// Return a new StudentManage instance internally without changing anything else.
        /// </summary>
        public static StudentManage GetStudentManage(List<Academia> academias)
            => new StudentManage(academias);
    }
}