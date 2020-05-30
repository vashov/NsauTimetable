using NsauT.Web.BLL.Services.Subject.DTO;

namespace NsauT.Web.BLL.Services.Subject
{
    public interface ISubjectService
    {
        SubjectDto GetSubject(int subjectId);
        ServiceResult UpdateApprovedStatus(int subjectId);

        /// <summary>Approve subject info and if OK, return subjectId</summary>
        ServiceResult ApproveSubjectInfo(int subjectInfoId);
        bool IsSubjectApproved(int subjectId);
        SubjectInfoDto GetSubjectInfo(int subjectInfoId);

        /// <summary>Update and validate subject info. If OK, return subjectId</summary>
        ServiceResult UpdateSubjectInfo(SubjectInfoDto subjectInfoDto, int subjectInfoId);
    }
}
