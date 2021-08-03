namespace GroceryStoreAPI.Contracts.v1
{
    public class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Customers
        {
            public const string GetAll = Base + "/Customers";
            public const string Update = Base + "/Customers/{customerId}";
            public const string Delete = Base + "/Customers/{customerId}";
            public const string Create = Base + "/Customers";
            public const string Get = Base + "/Customers/{customerId}";
        }
    }
}
