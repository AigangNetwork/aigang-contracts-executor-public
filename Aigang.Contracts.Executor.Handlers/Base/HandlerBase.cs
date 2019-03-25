using System;
using System.Threading.Tasks;
using Aigang.Contracts.Executor.Contracts;
using Aigang.Contracts.Executor.Contracts.Errors;
using Aigang.Contracts.Executor.Domain;
using Aigang.Contracts.Executor.Domain.Errors;
using log4net;
using Newtonsoft.Json;

namespace Aigang.Contracts.Executor.Handlers.Base
{
    public abstract class HandlerBase<TRequest, TResponse> where TRequest : BaseRequest where TResponse : BaseResponse, new()
    {
        public readonly ILog Logger;

        protected HandlerBase(ILog logger)
        {
            Logger = logger;
        }

        public async Task<TResponse> HandleAsync(TRequest request)
        {         
            TResponse response;

            try
            {
                Logger.Info(request.GetType() + " Request received");

                ValidationFailure failure = await ValidateRequestWrapperAsync(request);
 
                if (!string.IsNullOrEmpty(failure.Reason))
                {
                    response = FormatResponseFromValidationFailures(failure);
                }
                else
                {
                    response = await HandleCoreAsync(request);
                }
            }
            catch (Exception ex)
            {
                response = FormatGeneralError(ex);
                Logger.Error($"Error in handler: {ex.Message} \r\n Request: {JsonConvert.SerializeObject(request)} \r\n Response: {JsonConvert.SerializeObject(response)}", ex);
            }

            return response;
        }

        private TResponse FormatGeneralError(Exception ex)
        {
            var response = new TResponse();

            response.Error = new InternalServerErrorResponse(ex.ToString());

            return response;
        }

        private TResponse FormatResponseFromValidationFailures(ValidationFailure failure)
        {
            return new TResponse
            {
                Error = new ErrorResponse(failure.Reason, failure.Message)
            };
        }

        private async Task<ValidationFailure> ValidateRequestWrapperAsync(TRequest request)
        {
            if (request == null)
            {
                return new ValidationFailure
                {
                    Reason = ErrorReasons.EmptyRequest
                };
            }
            

            return await ValidateRequestAsync(request);
        }
        
        protected abstract Task<ValidationFailure> ValidateRequestAsync(TRequest request);

        protected abstract Task<TResponse> HandleCoreAsync(TRequest request);      
    }
}