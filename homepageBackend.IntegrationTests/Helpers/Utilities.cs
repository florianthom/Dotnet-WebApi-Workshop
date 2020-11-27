using System.Collections.Generic;
using homepageBackend.Data;
using homepageBackend.Domain;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace homepageBackend.IntegrationTests.Helpers
{
    public static class Utilities
    {
        public static void InitializeDbForTests(DataContext db)
        {
            db.Projects.AddRange(GetSeedingProjects());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(DataContext db)
        {
            db.Projects.RemoveRange(db.Projects);
            InitializeDbForTests(db);
        }

        public static List<Project> GetSeedingProjects()
        {
            return new List<Project>()
            {
                new Project() {Name = "TestProject1"},
                new Project() {Name = "TestProject2"},
            };
        }
    }
}