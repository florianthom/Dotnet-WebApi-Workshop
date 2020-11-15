namespace homepageBackend.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class Projects
        {
            public const string GetAll = Base + "/projects";

            public const string Get = Base + "/projects/{projectId}";
            
            public const string Update = Base + "/projects/{projectId}";
            
            public const string Delete = Base + "/projects/{projectId}";

            public const string Create = Base + "/projects";
        }
    }
}