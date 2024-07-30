using System;

namespace Dimo.Client.Core.Exceptions
{
    public class DimoException : Exception
    {
        public string ErrorMessage { get; set; }
        
        public DimoException(string errorMessage) : base(errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}