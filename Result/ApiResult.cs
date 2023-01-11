using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaylocityDemo.Result
{
    public class ApiResult
    {
        public ApiResult()
        {
            Errors = new List<string>();
            StatusCode = -1;
            Type = "SUCCESS";
        }

        public object Data { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public List<string> Errors { get; set; }

        public ApiResult WithData(object data)
        {
            return WithData(data, StatusCodes.Status200OK);
        }

        public ApiResult WithData(object data, int statusCode)
        {
            Data = data;
            StatusCode = statusCode;
            return this;
        }

        public ApiResult WithErrors(string message)
        {
            return WithErrors(message, StatusCodes.Status400BadRequest);
        }

        public ApiResult WithErrors(string message, int statusCode)
        {
            return WithErrors(message, statusCode);
        }

        public ApiResult WithErrors(ModelStateDictionary modelState)
        {
            return WithErrors(modelState, "An error occurred", StatusCodes.Status400BadRequest);
        }

        public ApiResult WithErrors(ModelStateDictionary modelState, string message, int statusCode)
        {
            if (modelState != null)
            {
                foreach (var kv in modelState)
                    foreach (var error in kv.Value.Errors)
                        if (Errors.FindIndex(k => k.Equals(error.ErrorMessage)) == -1)
                            Errors.Add(error.ErrorMessage);
            }

            Message = message;
            StatusCode = statusCode;
            Type = "ERROR";
            return this;
        }

        public JsonResult ToJsonResult()
        {
            Dictionary<string, object> obj = new Dictionary<string, object>();

            if (!string.IsNullOrWhiteSpace(Message))
                obj.Add("message", Message);
            if (Errors.Any())
                obj.Add("errors", Errors);
            if (Data != null)
                obj.Add("data", Data);

            JsonResult result = new JsonResult(obj);
            result.StatusCode = StatusCode;

            return result;
        }
    }
}
