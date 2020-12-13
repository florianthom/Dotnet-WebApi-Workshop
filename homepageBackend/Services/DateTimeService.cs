using System;

namespace homepageBackend.Services
{
    public class DateTimeService : IDateTime
    {
        DateTime IDateTime.Now => DateTime.Now;
    }
}