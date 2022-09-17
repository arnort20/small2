// TODO create a new exception class named ModelFormatException that inherits from Exception
using System;
using Exterminator.Models;

namespace Exterminator.Models.Exceptions
{
    public class ModelFormatException : Exception
    {
        public ModelFormatException(string message) : base(message)
        {

        
        }
    }
}