namespace AcademiaManagement
{
    using System;
    using System.Globalization;

    internal class Director
    {
        // Gets or sets the Builder.
        public IBuilder Builder { get; set; }

        /// <summary>
        /// The student entity fabrication process.
        /// </summary>
        public void MakeStudent(string[] buildFields)
        {
            // Set academia position to be a student.
            Builder.SetPosition(AcademicPosition.Student);

            /*  Input each item in string array argument variable in to each of the 
             *  Builder implementors' methods chronologically.
             */
            Builder.SetID(buildFields[0]);
            Builder.SetName(buildFields[1]);
            Builder.SetDateOfBirth(DateTime.Parse(buildFields[2],
                                                  CultureInfo.CreateSpecificCulture("de-DE")));
            Builder.SetEmail(buildFields[3]);
            Builder.SetAddress(buildFields[4]);
            Builder.SetDivision(buildFields[5]);
        }

        /// <summary>
        /// The lecturer entity fabrication process.
        /// </summary>
        public void MakeLecturer(string[] buildFields)
        {
            // Set academia position to be a lecturer.
            Builder.SetPosition(AcademicPosition.Lecturer);

            /*  Input each item in string array argument variable in to each of the 
             *  Builder implementors' methods chronologically.
             */
            Builder.SetID(buildFields[0]);
            Builder.SetName(buildFields[1]);
            Builder.SetDateOfBirth(DateTime.Parse(buildFields[2],
                                                  CultureInfo.CreateSpecificCulture("de-DE")));
            Builder.SetEmail(buildFields[3]);
            Builder.SetAddress(buildFields[4]);
            Builder.SetDivision(buildFields[5]);
        }
    }
}
