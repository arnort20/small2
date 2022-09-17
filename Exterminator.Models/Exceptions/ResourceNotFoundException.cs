//TODO create a new exception class named ResourceNotFoundException that inherits from Exception
using System;
using Exterminator.Models;

namespace Exterminator.Models.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message) : base(message)
        {
            //TODO: Implement the constructor

        }
    }
}
