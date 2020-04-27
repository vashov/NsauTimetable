using System;
using System.Collections.Generic;
using System.Text;

namespace NsauT.Web.BLL.Infrastructure
{
    public interface IApproverFacade
    {
        void CascadeUpdateApprovedDay(int dayId);

        void CascadeUpdateApprovedSubject(int subjectId);
    }
}
