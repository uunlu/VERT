using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marten;
using FluentValidation;
using VERT.Core;
using VERT.Core.Users;
using Dapper;

namespace VERT.Services.Features.Users
{
    public class SearchUsers
    {
        public class Request : BaseRequest<Response>
        {
            public string SearchTerm { get; set; }
        }

        public class Response : BaseResponse
        {
            public IEnumerable<Item> Items { get; set; } = Enumerable.Empty<Item>();

            public class Item
            {
                public Guid UserId { get; set; }
                public string Name { get; set; }
                public string Email { get; set; }
            }
        }

        public class Handler : BaseHandler<Request, Response>
        {
            public override async Task<Response> Handle(Request request)
            {
                // Showcase: Querying Postgres data (stored as jsonb documents using Marten) by using Dapper. Also, searching with ILIKE on a duplicated and indexed column.
                string sql = $@"
SELECT
    u.id                    AS UserId,
    u.data->>'Name'         AS Name,
    u.data->>'Email'        AS Email
FROM mt_doc_user u
WHERE u.name ILIKE '%{request.SearchTerm}%'
ORDER BY u.name ASC
";
                var items = await Session.Connection.QueryAsync<Response.Item>(sql);
                return new Response { Items = items };
            }
        }
    }
}
