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
    }
}
