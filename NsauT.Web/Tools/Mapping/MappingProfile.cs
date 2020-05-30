using AutoMapper;
using System;

namespace NsauT.Web.Tools.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NsauT.Web.Models.AccountController.SignInUserViewModel,
                NsauT.Web.BLL.Services.User.DTO.SignInUserDto>();

            CreateMap<NsauT.Web.BLL.Services.Timetable.DTO.TimetableInfoDto,
                NsauT.Web.Areas.Manage.Models.TimetableController.TimetableInfoModel>();

            CreateMap<NsauT.Web.BLL.Services.Timetable.DTO.TimetableModelDto,
                NsauT.Web.Areas.Manage.Models.TimetableController.TimetableViewModel>();

            CreateMap<NsauT.Web.BLL.Services.Timetable.DTO.SubjectShortDto,
                NsauT.Web.Areas.Manage.Models.TimetableController.SubjectModel>();

            CreateMap<NsauT.Web.BLL.Services.Subject.DTO.SubjectDto,
                NsauT.Web.Areas.Manage.Models.SubjectController.SubjectViewModel>();

            CreateMap<NsauT.Web.BLL.Services.Subject.DTO.SchoolDayDto,
                NsauT.Web.Areas.Manage.Models.SubjectController.SchoolDayModel>();

            CreateMap<NsauT.Web.BLL.Services.Subject.DTO.PeriodDto,
                NsauT.Web.Areas.Manage.Models.SubjectController.PeriodModel>();

            CreateMap<DayOfWeek, string>()
                .ConvertUsing(typeof(DayOfWeekToStringConverter));

            CreateMap<NsauT.Shared.Enums.PeriodOption, string>()
                .ConvertUsing(typeof(PeriodOptionToStringConverter));

            CreateMap<NsauT.Shared.Enums.PeriodNumber, string>()
                .ConvertUsing(typeof(PeriodNumberToStringConverter));

            CreateMap<NsauT.Web.BLL.Services.Period.DTO.PeriodDto,
                NsauT.Web.Areas.Manage.Models.PeriodController.PeriodBindingModel>();

            CreateMap<NsauT.Web.Areas.Manage.Models.PeriodController.PeriodBindingModel,
                NsauT.Web.BLL.Services.Period.DTO.PeriodDto>();

            CreateMap<string, NsauT.Shared.Enums.PeriodNumber>()
                .ConvertUsing(typeof(StringToPeriodNumberConverter));

            CreateMap<string, NsauT.Shared.Enums.PeriodOption>()
                .ConvertUsing(typeof(StringToPeriodOptionConverter));

            CreateMap<NsauT.Web.BLL.Services.Subject.DTO.SubjectInfoDto,
               NsauT.Web.Areas.Manage.Models.SubjectController.SubjectInfoBindingModel>();

            CreateMap<NsauT.Web.Areas.Manage.Models.SubjectController.SubjectInfoBindingModel,
                NsauT.Web.BLL.Services.Subject.DTO.SubjectInfoDto>();

            #region api-dto mapping
            CreateMap<NsauT.Web.BLL.Services.Timetable.DTO.ApprovedTimetableDto, 
                NsauT.Shared.Models.ApprovedTimetableKey>();

            CreateMap<NsauT.Web.BLL.Services.Timetable.DTO.TimetableApi.PeriodApiDto,
                NsauT.Shared.Models.TimetableInfo.PeriodModel>();

            CreateMap<NsauT.Web.BLL.Services.Timetable.DTO.TimetableApi.SchoolDayApiDto,
                NsauT.Shared.Models.TimetableInfo.SchoolDayModel>();

            CreateMap<NsauT.Web.BLL.Services.Timetable.DTO.TimetableApi.SubjectApiDto,
                NsauT.Shared.Models.TimetableInfo.SubjectModel>();

            CreateMap<NsauT.Web.BLL.Services.Timetable.DTO.TimetableApi.TimetableApiDto,
                NsauT.Shared.Models.TimetableInfo.TimetableModel>();
            #endregion
        }
    }
}
