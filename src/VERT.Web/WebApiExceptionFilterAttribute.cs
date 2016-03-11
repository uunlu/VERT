using FluentValidation;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;

namespace VERT.Web
{
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            TryCaptureValidationException(actionExecutedContext);
        }

        public override Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            OnException(actionExecutedContext);
            return Task.FromResult(0);
        }

        private void TryCaptureValidationException(HttpActionExecutedContext actionExecutedContext)
        {
            var validationException = actionExecutedContext.Exception as ValidationException;
            if (validationException == null)
                return;

            var content = new
            {
                errors = validationException
                    .Errors
                    .Select(x => new
                    {
                        errorMessage = x.ErrorMessage,
                        propertyName = x.PropertyName,
                        attemptedValue = x.AttemptedValue
                    })
            };
            string contentJson = JsonConvert.SerializeObject(content);
            throw new HttpResponseException(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(contentJson, Encoding.UTF8, "application/json")
            });
        }
    }
}
