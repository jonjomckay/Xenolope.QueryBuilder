using System;
using Xunit;

namespace Xenolope.QueryBuilder
{
    public class Test
    {
        [Fact]
        public void TestSelectWithEverything()
        {
            var expected = "SELECT id, name, created_at FROM users u WHERE u.id = :id AND u.name = 'Cooter Berger' OR u.name = 'Lance Drake Mandrell' GROUP BY u.id, u.name ORDER BY u.name ASC LIMIT 500";

            var query = new SelectQuery()
                .Select("id", "name", "created_at")
                .From("users", "u")
                .Where("u.id = :id")
                .AndWhere("u.name", "=", new StringLiteral("Cooter Berger"))
                .OrWhere("u.name", "=", new StringLiteral("Lance Drake Mandrell"))
                .GroupBy("u.id", "u.name")
                .OrderBy("u.name", OrderDirection.Ascending)
                .Limit(500);

            Assert.Equal(expected, query.ToString());
        }

        [Fact]
        public void TestSelectWithNoFrom()
        {
            Assert.Throws<InvalidOperationException>(() =>
                {
                    new SelectQuery()
                        .Select("id")
                        .Limit(100)
                        .ToString();
                });
        }

        [Fact]
        public void TestSelectWithSingleWhere()
        {
            var expected = "SELECT id, name FROM users u WHERE u.id = :id";

            var query = new SelectQuery()
                .Select("id", "name")
                .From("users", "u")
                .Where("u.id = :id");

            Assert.Equal(expected, query.ToString());
        }

        [Fact]
        public void TestSelectWithAndWhereNumber()
        {
            var expected = "SELECT id, name FROM users u WHERE u.id = 123 AND u.name = 'Cooter Berger'";

            var query = new SelectQuery()
                .Select("id", "name")
                .From("users", "u")
                .Where("u.id", "=", 123)
                .AndWhere("u.name", "=", new StringLiteral("Cooter Berger"));

            Assert.Equal(expected, query.ToString());
        }

        [Fact]
        public void TestSelectWithAndWhereString()
        {
            var expected = "SELECT id, name FROM users u WHERE u.id = :id AND u.name = 'Cooter Berger'";

            var query = new SelectQuery()
                .Select("id", "name")
                .From("users", "u")
                .Where("u.id", "=", ":id")
                .AndWhere("u.name", "=", new StringLiteral("Cooter Berger"));

            Assert.Equal(expected, query.ToString());
        }
    }
}

