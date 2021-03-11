namespace AcademiaManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal interface IBuilder
    {
        /// <summary>
        /// Set the position of the academia entity.
        /// </summary>
        public void SetPosition(AcademicPosition position);

        /// <summary>
        /// Set the ID of the academia entity.
        /// </summary>
        public void SetID(string id);

        /// <summary>
        /// Set the name of the academia entity.
        /// </summary>
        public void SetName(string name);

        /// <summary>
        /// Set the date of birth of the academia entity.
        /// </summary>
        public void SetDateOfBirth(DateTime dateOfBirth);

        /// <summary>
        /// Set the email of the academia entity.
        /// </summary>
        public void SetEmail(string email);

        /// <summary>
        /// Set the address of the academia entity.
        /// </summary>
        public void SetAddress(string address);

        /// <summary>
        /// Set the division of the academia entity.
        /// </summary>
        public void SetDivision(string division);
    }

    internal class AcademiaBuilder : IBuilder
    {
        // Declare an academia instance variable.
        private Academia _academia;

        /// <summary>
        /// Each time a new instance is initialize, the Reset() function will be called.
        /// </summary>
        public AcademiaBuilder() => Reset();

        /// <summary>
        /// Initializes a new Academia instance, then assign it to academia instance
        /// variable.
        /// </summary>
        private void Reset() => _academia = new Academia();

        /// <summary>
        /// Take an input argument of AcademicPosition enumerable data type, then
        /// assign it to the academia object's position property.
        /// </summary>
        public void SetPosition(AcademicPosition position)
            => _academia.AcademiaPosition = position;

        /// <summary>
        /// Take an input argument of string data type, then assign it to academia
        /// object's ID property.
        /// </summary>
        public void SetID(string id)
            => _academia.AcademiaID = id;

        /// <summary>
        /// Take an input argument of string data type, then assign it to the academia
        /// object's name property.
        /// </summary>
        public void SetName(string name)
            => _academia.AcademiaName = name;

        /// <summary>
        /// Take an input argument of DateTime data type, then assign it to the
        /// academia object's date of birth property.
        /// </summary>
        public void SetDateOfBirth(DateTime dateOfBirth)
            => _academia.AcademiaDateOfBirth = dateOfBirth;

        /// <summary>
        /// Take an input argument of string data type, then assign it to the academia
        /// object's email property.
        /// </summary>
        public void SetEmail(string email)
            => _academia.AcademiaEmail = email;

        /// <summary>
        /// Take an input argument of string data type, then assign it to the
        /// academia object's address property.
        /// </summary>
        public void SetAddress(string address)
            => _academia.AcademiaAddress = address;

        /// <summary>
        /// Take an input argument of string data type, then assign it to the academia
        /// object's division property.
        /// </summary>
        public void SetDivision(string division)
            => _academia.AcademiaDivision = division;

        /// <summary>
        /// Call the Reset() function to initilize new academia instance, then return
        /// the current academia instance.
        /// </summary>
        public Academia GetAcademia()
        {
            Academia a = _academia;
            Reset();
            return a;
        }
    }

    internal class Academia
    {
        // Gets or sets the AcademiaPosition.
        public AcademicPosition AcademiaPosition { get; set; }

        // Gets or sets the AcademiaID.
        public string AcademiaID { get; set; }

        // Gets or sets the AcademiaName.
        public string AcademiaName { get; set; }

        // Gets or sets the AcademiaDateOfBirth.
        public DateTime AcademiaDateOfBirth { get; set; }

        // Gets or sets the AcademiaEmail.
        public string AcademiaEmail { get; set; }

        // Gets or sets the AcademiaAddress.
        public string AcademiaAddress { get; set; }

        // Gets or sets the AcademiaDivision.
        public string AcademiaDivision { get; set; }

        /// <summary>
        /// Return a string of indication for each information fields of a particular
        /// academia entity.
        /// </summary>
        public string Display()
        {
            string division = "DIVISION";
            var divisionType = new Dictionary<Func<AcademicPosition, bool>, Action>
            {
                { p => p == AcademicPosition.Student,  () => division = "CLASS"      },
                { p => p == AcademicPosition.Lecturer, () => division = "DEPARTMENT" }
            };
            divisionType.First(c => c.Key(AcademiaPosition)).Value();

            return
                "\nPOSITION: " + AcademiaPosition +
                " | ID: " + AcademiaID +
                " | NAME: " + AcademiaName +
                " | DATE OF BIRTH: " + AcademiaDateOfBirth.ToString("d-MM-yyyy") +
                " | EMAIL: " + AcademiaEmail +
                " | ADDRESS: " + AcademiaAddress +
                " | " + division + ": " + AcademiaDivision;
        }
    }

    public enum AcademicPosition
    {
        Student,
        Lecturer
    }
}