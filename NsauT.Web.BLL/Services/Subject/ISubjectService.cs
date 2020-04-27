using NsauT.Web.BLL.Services.Subject.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace NsauT.Web.BLL.Services.Subject
{
    public interface ISubjectService
    {
        SubjectDto GetSubject(int subjectId);
        ServiceResult UpdateApprovedStatus(int subjectId);
    }
}
