namespace NsauT.Web.BLL.Services.SchoolDay
{
    public interface ISchoolDayService
    {
        /// <summary>Delete day and return Id of subject</summary>
        ServiceResult DeleteDay(int dayId);

        /// <summary>Update approve status of day and return Id of subject</summary>
        ServiceResult UpdateApprovedStatus(int dayId);

        /// <summary>Check is day approved</summary>
        bool IsDayApproved(int dayId);
    }
}
