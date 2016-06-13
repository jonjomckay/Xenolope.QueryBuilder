using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Xenolope.QueryBuilder
{
    public class SelectQuery
    {
        private Table table;

        private string[] columns;

        private string firstWhere;
        private List<string> andWheres = new List<string>();
        private List<string> orWheres = new List<string>();

        private OrderBy orderBy;
        private string[] groupBys;

        private int? limit;

        public SelectQuery AndWhere(string condition)
        {
            andWheres.Add(condition);

            return this;
        }

        public SelectQuery AndWhere(string column, string comparison, StringLiteral value)
        {
            return AndWhere(column, comparison, value.ToString());
        }

        public SelectQuery AndWhere(string column, string comparison, string value)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(column);
            stringBuilder.Append(" ");
            stringBuilder.Append(comparison);
            stringBuilder.Append(" ");
            stringBuilder.Append(value);

            andWheres.Add(stringBuilder.ToString());

            return this;
        }

        public SelectQuery From(string name, string alias = "t0")
        {
            this.table = new Table()
            {
                Name = name,
                Alias = alias
            };

            return this;
        }

        public SelectQuery GroupBy(params string[] groupBys)
        {
            this.groupBys = groupBys;

            return this;
        }

        public SelectQuery Limit(int limit)
        {
            this.limit = limit;

            return this;
        }

        public SelectQuery OrderBy(string column, OrderDirection direction)
        {
            this.orderBy = new OrderBy()
            {
                Column = column,
                Direction = direction
            };

            return this;
        }

        public SelectQuery Select(params string[] columns)
        {
            this.columns = columns;

            return this;
        }

        public SelectQuery OrWhere(string condition)
        {
            orWheres.Add(condition);

            return this;
        }

        public SelectQuery OrWhere(string column, string comparison, StringLiteral value)
        {
            return OrWhere(column, comparison, value.ToString());
        }

        public SelectQuery OrWhere(string column, string comparison, string value)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(column);
            stringBuilder.Append(" ");
            stringBuilder.Append(comparison);
            stringBuilder.Append(" ");
            stringBuilder.Append(value);

            orWheres.Add(stringBuilder.ToString());

            return this;
        }

        public SelectQuery Where(string condition)
        {
            this.firstWhere = condition;

            return this;
        }

        public SelectQuery Where(string column, string comparison, object value)
        {
            return Where(column, comparison, value.ToString());
        }

        public SelectQuery Where(string column, string comparison, string value)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(column);
            stringBuilder.Append(" ");
            stringBuilder.Append(comparison);
            stringBuilder.Append(" ");
            stringBuilder.Append(value);

            this.firstWhere = stringBuilder.ToString();

            return this;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            if (this.table == null)
            {
                throw new InvalidOperationException("A FROM table was not specified");
            }

            if (this.columns == null)
            {
                throw new InvalidOperationException("No columns to select were specified");
            }

            stringBuilder.Append("SELECT");
            stringBuilder.Append(" ");
            stringBuilder.Append(string.Join(", ", this.columns));
            stringBuilder.Append(" ");
            stringBuilder.Append("FROM");
            stringBuilder.Append(" ");
            stringBuilder.Append(this.table.Name);
            stringBuilder.Append(" ");
            stringBuilder.Append(this.table.Alias);

            if (this.firstWhere != null)
            {
                stringBuilder.Append(" ");
                stringBuilder.Append("WHERE");
                stringBuilder.Append(" ");

                stringBuilder.Append(this.firstWhere);
            }

            if (this.andWheres.Any())
            {
                if (this.firstWhere == null)
                {
                    throw new InvalidOperationException("An initial WHERE is needed");
                }

                stringBuilder.Append(" AND ");
                stringBuilder.Append(string.Join(" AND ", this.andWheres));
            }

            if (this.orWheres.Any())
            {
                if (this.firstWhere == null)
                {
                    throw new InvalidOperationException("An initial WHERE is needed");
                }

                stringBuilder.Append(" OR ");
                stringBuilder.Append(string.Join(" OR ", this.orWheres));
            }

            if (this.groupBys != null)
            {
                stringBuilder.Append(" ");
                stringBuilder.Append("GROUP BY");
                stringBuilder.Append(" ");

                stringBuilder.Append(string.Join(", ", this.groupBys));
            }

            if (this.orderBy != null)
            {
                stringBuilder.Append(" ");
                stringBuilder.Append("ORDER BY");
                stringBuilder.Append(" ");
                stringBuilder.Append(this.orderBy.Column);
                stringBuilder.Append(" ");

                switch (this.orderBy.Direction)
                {
                    case OrderDirection.Ascending:
                        stringBuilder.Append("ASC");
                        break;
                    case OrderDirection.Descending:
                        stringBuilder.Append("DESC");
                        break;
                }
            }

            if (this.limit != null)
            {
                stringBuilder.Append(" ");
                stringBuilder.Append("LIMIT");
                stringBuilder.Append(" ");
                stringBuilder.Append(this.limit);
            }

            return stringBuilder.ToString();
        }
    }
}

