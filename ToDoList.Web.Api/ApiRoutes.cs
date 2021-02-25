
namespace ToDoList.Web.Api
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class NoteTask
        {
            public const string GetAll = Base + "/Tasks";
            public const string Create = Base + "/Tasks";
            public const string Get = Base + "/Task/{taskId}";
            public const string Complete = Base + "/Task/{taskId}";
            public const string Delete = Base + "/Task/{taskId}";

        }

        public static class Account
        {
            public const string Register = Base + "/identity/account/register";
            public const string Login = Base + "/identity/account/login";
            public const string LogOut = Base + "/identity/account/logout";
        }
    }
}
