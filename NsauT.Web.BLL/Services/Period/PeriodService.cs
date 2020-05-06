using Microsoft.EntityFrameworkCore;
using NsauT.Web.BLL.Infrastructure;
using NsauT.Web.BLL.Services.Period.DTO;
using NsauT.Web.BLL.Services.SchoolDay;
using NsauT.Web.DAL.DataStore;
using NsauT.Web.DAL.Models;
using System.Linq;

namespace NsauT.Web.BLL.Services.Period
{
    public class PeriodService : IPeriodService
    {
        private ApplicationContext Context { get; }
        //private IApproverFacade Approver { get; }
        private ISchoolDayService SchoolDayService { get; }

        public PeriodService(ApplicationContext context, ISchoolDayService schoolDayService/*, IApproverFacade approver*/)
        {
            Context = context;
            SchoolDayService = schoolDayService;
            //Approver = approver;
        }

        public PeriodDto GetPeriod(int id)
        {
           PeriodDto period = Context.Periods
                .AsNoTracking()
                .Include(p => p.SchoolDay)
                .Select(p => new PeriodDto
                {
                    Id = p.Id,
                    Number = p.Number,
                    Cabinet = p.Cabinet,
                    Subgroup = p.Subgroup,
                    IsLecture = p.IsLecture,
                    Option = p.Option,
                    OptionDate = p.OptionDate,
                    OptionCabinet = p.OptionCabinet,
                    SchoolDayId = p.SchoolDay.Id,
                    IsApproved = p.IsApproved
                })
                .SingleOrDefault(t => t.Id == id);

            return period;
        }

        public ServiceResult AddOrUpdatePeriod(PeriodDto periodDto, int? periodId)
        {
            var errors = PeriodValidator.Validate(periodDto);
            if (errors.Any())
            {
                var result = new ServiceResult(Result.Error);
                result.Errors.AddRange(errors);
                return result;
            }

            ServiceResult operationResult;
            if (periodId != null)
            {
                operationResult = UpdatePeriod(periodDto, periodId.Value);
            }
            else
            {
                operationResult = AddPeriod(periodDto);
            }

            if (operationResult.Result != Result.OK)
            {
                return operationResult;
            }

            int subjectId = Context.SchoolDays
                .Include(d => d.Subject)
                .Where(d => d.Id == periodDto.SchoolDayId)
                .Select(d => d.Subject.Id)
                .Single();

            //Approver.CascadeUpdateApprovedDay(periodDto.SchoolDayId);
            SchoolDayService.UpdateApprovedStatus(periodDto.SchoolDayId);

            return new ServiceResult(Result.OK, subjectId);
        }

        private ServiceResult UpdatePeriod(PeriodDto periodDto, int periodId)
        {
            PeriodEntity periodEntity = Context.Periods
                .SingleOrDefault(d => d.Id == periodId);

            if (periodEntity == null)
            {
                return new ServiceResult(Result.NotFound);
            }

            periodEntity.Number = periodDto.Number;
            periodEntity.Cabinet = periodDto.Cabinet;
            periodEntity.Subgroup = periodDto.Subgroup;
            periodEntity.IsLecture = periodDto.IsLecture;
            periodEntity.Option = periodDto.Option;
            periodEntity.OptionDate = periodDto.OptionDate;
            periodEntity.OptionCabinet = periodDto.OptionCabinet;
            periodEntity.IsApproved = true;

            Context.SaveChanges();

            return new ServiceResult(Result.OK, periodId);
        }

        private ServiceResult AddPeriod(PeriodDto periodDto)
        {
            SchoolDayEntity day = Context.SchoolDays
                .Include(d => d.Periods)
                .SingleOrDefault(d => d.Id == periodDto.SchoolDayId);

            if (day == null)
            {
                return new ServiceResult(Result.NotFound);
            }

            var periodEntity = new PeriodEntity();

            periodEntity.Number = periodDto.Number;
            periodEntity.Cabinet = periodDto.Cabinet;
            periodEntity.Subgroup = periodDto.Subgroup;
            periodEntity.IsLecture = periodDto.IsLecture;
            periodEntity.Option = periodDto.Option;
            periodEntity.OptionDate = periodDto.OptionDate;
            periodEntity.OptionCabinet = periodDto.OptionCabinet;
            periodEntity.IsApproved = true;

            day.Periods.Add(periodEntity);

            Context.SaveChanges();

            return new ServiceResult(Result.OK, periodEntity.Id);
        }

        public ServiceResult ApprovePeriod(int periodId)
        {
            PeriodEntity period = Context.Periods
                .Include(p => p.SchoolDay)
                .ThenInclude(d => d.Subject)
                .SingleOrDefault(p => p.Id == periodId);

            if (period == null)
            {
                return new ServiceResult(Result.NotFound);
            }

            int subjectId = period.SchoolDay.Subject.Id;
            int dayId = period.SchoolDay.Id;

            period.IsApproved = true;
            Context.SaveChanges();

            //Approver.CascadeUpdateApprovedDay(dayId);
            SchoolDayService.UpdateApprovedStatus(dayId);

            return new ServiceResult(Result.OK, subjectId);
        }

        public ServiceResult DeletePeriod(int id)
        {
            PeriodEntity period = Context.Periods
                .Include(p => p.SchoolDay)
                .ThenInclude(d => d.Subject)
                .FirstOrDefault(p => p.Id == id);
            if (period == null)
            {
                return new ServiceResult(Result.NotFound);
            }

            int subjectId = period.SchoolDay.Subject.Id;
            int dayId = period.SchoolDay.Id;

            Context.Periods.Remove(period);
            Context.SaveChanges();

            //Approver.CascadeUpdateApprovedDay(dayId);
            SchoolDayService.UpdateApprovedStatus(dayId);

            return new ServiceResult(Result.OK, subjectId);
        }
    }
}
