using System;

namespace CollegeGrades.Core.Exceptions
{
    public class InvalidInputException : Exception
    {
        public string Field { get; set; }

        public InvalidInputException()
        {
        }

        public InvalidInputException(string message)
            : base(message)
        {
            Field = "";
        }

        public InvalidInputException(string field, string message)
            : base(message)
        {
            Field = field;
        }

        public InvalidInputException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}