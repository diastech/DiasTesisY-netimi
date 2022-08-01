using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Standard;
using DiasShared.InterfacesAbstracts.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.BaseDto;
using System;
using System.Collections.Generic;

namespace DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Test.Shared.Custom
{
    public class CustomPeriodicTicketDto : PeriodicTicketDto, IBaseDevelopmentCustomDto
    {
        public int DayNumber { get; set; }
        public List<bool> Weeks { get; set; }
        public int WeekPeriod { get; set; }
        //her ayda bir opsiyon1
        public int MonthADayOptionOne { get; set; }

        //şununcu günü
        public int MonthDay { get; set; }

        //her ayda bir opsiyon2
        public int MonthADayOptionsTwo { get; set; }

        //1.
        public int WeekNumberinMonth { get; set; }

        //.günü
        public string WeekDayinMonth { get; set; }
        
        //Her yıl .ayının opsiyon1
        public string YearMonthOptionOne { get; set; }

        //.günü
        public int YearMonthDay { get; set; }

        //Her yıl .ayının opsiyon2
        public string YearMonthOptionTwo { get; set; }

        //.
        public int YearMonthWeekNumber { get; set; }

        public string YearMonthWeekName { get; set; }

        public int Option { get; set; }
       
       
        public bool isTime { get; set; }
        public int TicketUserId { get; set; }

        public DateTime DayStartedTime { get; set; }
        public DateTime DayEndedTime { get; set; }
    }
}
