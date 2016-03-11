using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using VERT.Core.Users;
using VERT.Core;

namespace VERT.Testing.UnitTests
{
    public class UserTests
    {
        [Fact]
        public void register()
        {
            var user = User.Register("John", "john@haselt.com", "yeah!%");

            user.Name.ShouldBe("John");
            user.Email.ShouldBe("john@haselt.com");
            user.IsPasswordValid("yeah!%").ShouldBeTrue();
        }

        [Fact]
        public void change_password_when_old_is_correct()
        {
            var user = User.Register("John", "john@haselt.com", "yeah!%");
            user.ChangePassword("yeah!%", "nanananana");
            user.IsPasswordValid("nanananana").ShouldBeTrue();
        }

        [Fact]
        public void cannot_change_password_when_old_is_incorrect()
        {
            var user = User.Register("John", "john@haselt.com", "yeah!%");

            Assert.Throws<CoreException>(() =>
            {
                user.ChangePassword("inc0rect!@", "nanananana");
            });
        }
    }
}
