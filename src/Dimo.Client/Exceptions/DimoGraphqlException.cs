using System;

namespace Dimo.Client.Exceptions
{
    public class DimoGraphqlException : Exception
    {
        public string ErrorMessage { get; }
        
        public string[] Errors { get; }

        public DimoGraphqlException(string errorMessage): base(errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        
        public DimoGraphqlException(string errorMessage, string[] errors): base(errorMessage)
        {
            ErrorMessage = errorMessage;
            Errors = errors;
        }
    }
}