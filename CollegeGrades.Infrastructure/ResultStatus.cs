using CollegeGrades.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace CollegeGrades.Infrastructure
{
    public class ResultStatus : List<string>, IResultStatus
    {
        private ResultStatus(IEnumerable<string> errors) : base(errors)
        {
        }

        private ResultStatus(params string[] errors) : base(errors)
        {
        }

        public ResultStatus()
        {
        }

        public bool Succeeded { get { return Count == 0; } }

        public static IResultStatus Create(params string[] errors)
        {
            return new ResultStatus(errors);
        }

        public static IResultStatus Create(IEnumerable<string> errors)
        {
            return new ResultStatus(errors);
        }

        private static Lazy<IResultStatus> success = new Lazy<IResultStatus>(() => new ResultStatus());

        public static IResultStatus Success
        {
            get
            {
                return success.Value;
            }
        }

        /// <summary>
        /// Converts the value of the current <see cref="IResultStatus"/> object to its equivalent string representation.
        /// </summary>
        /// <returns>A string representation of the current <see cref="IResultStatus"/> object.</returns>
        /// <remarks>
        /// If the operation was successful the ToString() will return "Succeeded" otherwise it returned
        /// "Failed : " followed by a comma delimited list of errors, if any.
        /// </remarks>
        public override string ToString()
        {
            return Succeeded ?
                   "Succeeded" :
                   string.Format("{0} : {1}", "Failed", string.Join(",", this));
        }
    }
}