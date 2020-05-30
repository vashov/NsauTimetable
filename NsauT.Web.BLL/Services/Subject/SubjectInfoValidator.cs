using NsauT.Web.BLL.Services.Subject.DTO;
using System.Collections.Generic;

namespace NsauT.Web.BLL.Services.Subject
{
    public class SubjectInfoValidator
    {
        public static List<(string Key, string Message)> Validate(SubjectInfoDto info)
        {
            var errors = new List<(string, string)>();

            string errorMessage;

            if (string.IsNullOrWhiteSpace(info.Title))
            {
                errorMessage = "Не указано название предмета";
                var pair = (nameof(info.Title), errorMessage);
                errors.Add(pair);
            }

            return errors;
        }
    }
}
