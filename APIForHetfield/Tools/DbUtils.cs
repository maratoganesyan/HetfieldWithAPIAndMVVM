using APIForHetfield.Models;

namespace APIForHetfield.Tools
{
    public static class DbUtils
    {
        public static Db db;

        static DbUtils()
        {
            try
            {
                db = new Db();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static class Roles
        {
            public const string Admin = "Администратор";

            public const string Director = "Директор";

            public const string Client = "Клиент";

            public const string SalesManager = "Менеджер по объявлениям";
        }

        public static class CarStatuses
        {
            public const int InProcessing = 1;

            public const int Exposed = 2;

            public const int Saled = 3;
        }

        public static class OrderStatuses
        {
            public const int Deleted = 1;

            public const int Finished = 2;

            public const int InProcessing = 3;
        }
    }
}
