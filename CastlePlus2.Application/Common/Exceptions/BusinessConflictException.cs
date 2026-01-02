using System;

namespace CastlePlus2.Application.Common.Exceptions
{
    public class BusinessConflictException : Exception
    {
        public BusinessConflictException(string message) : base(message)
        {
        }
    }
}