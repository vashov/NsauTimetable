using AutoMapper;
using NsauT.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            CreateMap<NsauT.Web.BLL.Services.Timetable.DTO.SubjectInfoDto,
                NsauT.Web.Areas.Manage.Models.TimetableController.SubjectModel>();

            CreateMap<NsauT.Web.BLL.Services.Subject.DTO.SubjectDto,
                NsauT.Web.Areas.Manage.Models.SubjectController.SubjectViewModel>();

            CreateMap<NsauT.Web.BLL.Services.Subject.DTO.SchoolDayDto,
                NsauT.Web.Areas.Manage.Models.SubjectController.SchoolDayModel>();

            CreateMap<NsauT.Web.BLL.Services.Subject.DTO.PeriodDto,
                NsauT.Web.Areas.Manage.Models.SubjectController.PeriodModel>();

            CreateMap<DayOfWeek, string>()
                .ConvertUsing(typeof(DayOfWeekToStringConverter));

            CreateMap<PeriodOption, string>()
                .ConvertUsing(typeof(PeriodOptionToStringConverter));

            CreateMap<PeriodNumber, string>()
                .ConvertUsing(typeof(PeriodNumberToStringConverter));

            CreateMap<NsauT.Web.BLL.Services.Period.DTO.PeriodDto,
                NsauT.Web.Areas.Manage.Models.PeriodController.PeriodBindingModel>();

            CreateMap<NsauT.Web.Areas.Manage.Models.PeriodController.PeriodBindingModel,
                NsauT.Web.BLL.Services.Period.DTO.PeriodDto>();

            CreateMap<string, PeriodNumber>()
                .ConvertUsing(typeof(StringToPeriodNumberConverter));

            CreateMap<string, PeriodOption>()
                .ConvertUsing(typeof(StringToPeriodOptionConverter));
        }
    }
}
