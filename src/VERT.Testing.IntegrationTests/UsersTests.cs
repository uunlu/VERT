using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using MediatR;
using VERT.Services.Features.Users;
using FluentValidation;

namespace VERT.Testing.IntegrationTests
{
    public class UsersTests : BootstrapperFixture
    {
        [Fact]
        public void retrieve_user_after_register()
        {
            using (var container = boostrapper.RootContainer.GetNestedContainer())
            {
                var mediator = container.GetInstance<IMediator>();

                var registerUserCommand = new RegisterUser.Request
                {
                    Email = "pece.deteto@haselt.com",
                    Name = "Pece Deteto",
                    Password = "asdfghj123!@",
                    PasswordConfirmation = "asdfghj123!@"
                };
                var registerUserResponse = mediator.Send(registerUserCommand).Result;
                var queryUserDetails = new QueryUserDetails.Request
                {
                    UserId = registerUserResponse.UserId
                };

                // Act
                var userDetailsResponse = mediator.Send(queryUserDetails).Result;

                // Assert
                userDetailsResponse.UserId.ShouldBe(registerUserResponse.UserId);
                userDetailsResponse.Email.ShouldBe(registerUserCommand.Email);
                userDetailsResponse.Name.ShouldBe(registerUserCommand.Name);
            }
        }

        [Fact]
        public void throw_validation_error_when_no_name_and_email_are_sent_uppon_registration()
        {
            using (var container = boostrapper.RootContainer.GetNestedContainer())
            {
                var mediator = container.GetInstance<IMediator>();

                var request = new RegisterUser.Request
                {
                    Email = "",   // empty, on purpose
                    Name = "",    // empty, on purpose
                    Password = "asdfghj123!@",
                    PasswordConfirmation = "asdfghj123!@"
                };

                try
                {
                    var response = mediator.Send(request).Result;
                }
                catch (AggregateException ex)
                {
                    ex.InnerException.ShouldBeOfType<ValidationException>();
                    (ex.InnerException as ValidationException).Errors.Count().ShouldBe(2);  // email and name are required
                }
            }
        }
    }
}
