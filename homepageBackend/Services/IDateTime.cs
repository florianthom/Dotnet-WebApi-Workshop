using System;

namespace homepageBackend.Services
{
    public interface IDateTime
    {
        public DateTime Now { get; }
    }
}