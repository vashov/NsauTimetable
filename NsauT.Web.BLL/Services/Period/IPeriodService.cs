using NsauT.Web.BLL.Services.Period.DTO;

namespace NsauT.Web.BLL.Services.Period
{
    public interface IPeriodService
    {
        PeriodDto GetPeriod(int id);

        /// <summary>Add or update period and return subject Id</summary>
        ServiceResult AddOrUpdatePeriod(PeriodDto periodDto, int? periodId);

        /// <summary>
        /// Approve period, then update approved status for day-subject-timetable and return subject Id
        /// </summary>
        ServiceResult ApprovePeriod(int periodId);

        /// <summary>
        /// Delete period, then update approved status for day-subject-timetable and return subject Id
        /// </summary>
        ServiceResult DeletePeriod(int id);
    }
}
