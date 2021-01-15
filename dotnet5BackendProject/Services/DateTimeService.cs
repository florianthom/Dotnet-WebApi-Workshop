using System;

namespace dotnet5BackendProject.Services
{
    public class DateTimeService : IDateTime
    {
        DateTime IDateTime.Now => DateTime.Now;
    }
}