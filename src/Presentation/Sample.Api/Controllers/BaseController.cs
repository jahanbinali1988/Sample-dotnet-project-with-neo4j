using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Sample.Api.Extensions;
using Sample.Api.Models;
using Sample.Application.Contract.Exceptions;
using Sample.SharedKernel.Application;
using Sample.SharedKernel.SeedWork;
using Humanizer;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Sample.Api.Controllers
{
    public class BaseController : ControllerBase, IActionFilter
    {
        private HttpStatusCode _responseHttpStatusCode = HttpStatusCode.OK;
        private ApiResponseMeta _meta;
        private readonly ILogger _logger;

        public BaseController(ILogger logger)
        {
            _logger = logger;
        }


        [NonAction]
        public virtual void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = StatusCode((int)HttpStatusCode.BadRequest, CreateApiResponse(null, new ApiResponseMeta()
                {
                    Error = ModelState.ToList().Select(x => new
                    {
                        PropertyName = x.Key?.Camelize(),
                        Errors = x.Value.Errors.Select(y => new
                        {
                            Message = y.ErrorMessage,
                            Type = "RequestSchemaValidation"
                        })
                    }),
                    DisplayMessage = ErrorCode.RequestSchemaValidation.GetDisplayMessage(),
                    ErrorCode = (int)ErrorCode.RequestSchemaValidation,
                    Message = "Validation error"
                }));
                return;
            }
        }

        [NonAction]
        public virtual void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                if (context.Exception is BusinessRuleValidationException businessRuleValidationException)
                {
                    context.Result = StatusCode((int)HttpStatusCode.BadRequest, CreateApiResponse(null, new ApiResponseMeta
                    {
                        Error = businessRuleValidationException.Properties.Select(x => new
                        {
                            PropertyName = x?.Camelize(),
                            Errors = new[] { new
                            {
                                businessRuleValidationException.Message,
                                businessRuleValidationException.ErrorType
                            } }
                        }),
                        DisplayMessage = ErrorCode.BusinessRuleValidation.GetDisplayMessage(),
                        ErrorCode = (int)ErrorCode.BusinessRuleValidation,
                        Message = "Validation error"
                    }));
                    context.ExceptionHandled = true;
                    return;
                }

                if (context.Exception is EntityNotFoundException entityNotFoundException)
                {
                    context.Result = StatusCode((int)HttpStatusCode.NotFound, CreateApiResponse(null, new ApiResponseMeta()
                    {

                        ErrorCode = (int)ErrorCode.NotFound,
                        Message = entityNotFoundException.Message,
                    }));
                    context.ExceptionHandled = true;
                    return;
                }

                if (context.Exception is ArgumentException argumentException && argumentException.Source == "Ardalis.GuardClauses")
                {
                    context.Result = StatusCode((int)HttpStatusCode.BadRequest, CreateApiResponse(null, new ApiResponseMeta()
                    {
                        Error = ModelState.ToList().Select(x => new
                        {
                            PropertyName = argumentException.ParamName?.Camelize(),
                            Errors = new[]
                            {
                                new
                                {
                                    Message = argumentException.Message,
                                    Type = "RequestSchemaValidation"
                                }
                            }
                        }),
                        DisplayMessage = ErrorCode.RequestSchemaValidation.GetDisplayMessage(),
                        ErrorCode = (int)ErrorCode.BadRequest,
                        Message = "Validation error"
                    }));
                    context.ExceptionHandled = true;
                    return;
                }

                _logger.LogCritical(context.Exception, context.Exception.Message);
                //default behavior
                context.Result = StatusCode(500, new ApiResponse()
                {
                    Meta = new ApiResponseMeta()
                    {
#if DEBUG
                        Error = context.Exception,
#endif
                        ErrorCode = (int)ErrorCode.InternalServerError,
                        DisplayMessage = ErrorCode.InternalServerError.GetDisplayMessage(),
                        Message = context.Exception.Message,
                    }
                });
                context.ExceptionHandled = true;
                return;
            }


            if (context.Result is NoContentResult)
            {
                context.Result = StatusCode((int)HttpStatusCode.NoContent);
                return;
            }

            if (context.Result is ObjectResult response)
            {
                if (response?.Value == null)
                {
                    context.Result = StatusCode((int)_responseHttpStatusCode, CreateApiResponse(null));
                    return;
                }
                else
                {
                    context.Result = StatusCode((int)_responseHttpStatusCode, CreateApiResponse(response.Value, _meta));
                    return;
                }
            }
        }

        [NonAction]
        public ActionResult<T> Created<T>(T value) where T : ViewModelBase
        {
            var route = Request.Path;
            _responseHttpStatusCode = HttpStatusCode.Created;

            string id = value.Id.ToString();
            _meta = new ApiResponseMeta()
            {
                Location = $"{route}/{id}"
            };

            return value;
        }

        [NonAction]
        public ActionResult<T> Get<T>(T value) where T : ViewModelBase
        {
            _responseHttpStatusCode = HttpStatusCode.OK;

            _meta = new ApiResponseMeta()
            {
                Location = Request.Path
            };

            return value;
        }

        [NonAction]
        public ActionResult<IEnumerable<TViewModel>> List<T, TViewModel>(Pagination<T> value) where TViewModel : ViewModelBase, new() where T : new()
        {
            _meta = new ApiResponseMeta()
            {
                TotalCount = value.TotalItems
            };

            List<KeyValuePair<string, string>> param = new List<KeyValuePair<string, string>>();
            int count = 20;
            if (Request.Query.TryGetValue("count", out var countStr))
            {
                count = int.Parse(countStr);
            }

            if (value.Items is null || count != value.Items.Count)
                return value.Items.Adapt<List<TViewModel>>();

            param.Add(!Request.Query.TryGetValue("offset", out var offsetStr)
                ? new KeyValuePair<string, string>("offset", "0")
                : new KeyValuePair<string, string>("offset", (int.Parse(offsetStr) + count).ToString()));

            param.Add(new KeyValuePair<string, string>("count", count.ToString()));

            foreach (var queryParam in Request.Query)
            {
                if (queryParam.Key.Equals("offset", StringComparison.OrdinalIgnoreCase) || queryParam.Key.Equals("offset", StringComparison.OrdinalIgnoreCase))
                    continue;

                param.Add(new KeyValuePair<string, string>(queryParam.Key, queryParam.Value.ToString()));
            }

            _meta.NextUrl = Request.Path.ToString().Split('?').FirstOrDefault() + "?" + String.Join('&', param.Select(x => $"{x.Key}={x.Value}"));

            return value.Items.Adapt<List<TViewModel>>();
        }

        private ApiResponse CreateApiResponse(object data, ApiResponseMeta meta = null)
        {
            meta ??= new ApiResponseMeta();

            return new ApiResponse()
            {
                Data = data,
                Meta = meta
            };
        }

        [NonAction]
        public ActionResult<T> BadRequest<T>(T errorModel, ErrorCode code = ErrorCode.BadRequest)
        {
            _meta = new ApiResponseMeta()
            {
                Error = errorModel,
                ErrorCode = (int)code,
                DisplayMessage = code.GetDisplayMessage(),
            };

            _responseHttpStatusCode = HttpStatusCode.BadRequest;

            return null;
        }
    }
}
