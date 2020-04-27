using System.Collections.Generic;

namespace NsauT.Web.BLL.Services
{
    public enum Result
    {
        Error = 0,
        OK = 1,
        NotFound = 2
    }

    public struct ServiceResult
    {
        public int Id { get; set; }
        public Result Result { get; set; }
        public List<(string Key, string Message)> Errors { get; }

        public ServiceResult(Result result, int id)
        {
            Result = result;
            Id = id;
            Errors = new List<(string, string)>();
        }

        public ServiceResult(Result result)
        {
            Result = result;
            Id = default;
            Errors = new List<(string, string)>();
        }
    }
}
