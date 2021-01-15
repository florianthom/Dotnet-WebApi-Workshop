using System.Collections.Generic;

namespace dotnet5BackendProject.Contracts.V1.Responses
{
    public class ErrorResponse
    {
        public ErrorResponse(){}

        public ErrorResponse(ErrorModel error)
        {
            Errors.Add(error);
        }
        
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}