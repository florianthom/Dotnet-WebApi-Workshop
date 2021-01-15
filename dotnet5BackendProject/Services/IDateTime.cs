using System;

namespace dotnet5BackendProject.Services
{
    public interface IDateTime
    {
        public DateTime Now { get; }
    }
}