using NsauT.Client.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NsauT.Client.DAL.DataStore
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
