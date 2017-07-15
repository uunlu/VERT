using Marten;
using VERT.Core.Contacts;
using VERT.Core.Meetings;
using VERT.Core.Users;

namespace VERT.Infrastructure
{
    public class MartenMappings : MartenRegistry
    {
        public MartenMappings()
        {
            For<User>()
                .Duplicate(x => x.Name);

            For<Meeting>();

            For<Contact>();
        }
    }
}
