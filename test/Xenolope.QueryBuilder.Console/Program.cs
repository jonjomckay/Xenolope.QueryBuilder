using System;

namespace Xenolope.QueryBuilder
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var query = new SelectQuery()
                .Select("id", "name", "created_at")
                .From("users", "u")
                .Where("u.id = :id")
                .AndWhere("u.name", "=", new StringLiteral("Yo Mamma"))
                .OrWhere("u.name", "=", new StringLiteral("Lance Drake Mandrell"))
                .GroupBy("u.id", "u.name")
                .OrderBy("u.name", OrderDirection.Ascending)
                .Limit(500);

            System.Console.WriteLine(query);
        }
    }
}
