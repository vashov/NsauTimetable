using NsauTimetable.Client.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NsauTimetable.Client.DAL.DataStore
{
    public class UnitOfWork
    {
        public UnitOfWork(IRepository<TimetableEntity> timetableRep)
        {
            TimetableRepository = timetableRep;
        }

        IRepository<TimetableEntity> TimetableRepository { get; set; }
    }
}
