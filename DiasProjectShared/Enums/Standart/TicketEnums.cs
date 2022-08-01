using System.ComponentModel.DataAnnotations;

namespace DiasShared.Enums.Standart
{
    public class TicketEnums
    {
        #region Core

        /// <summary>
        /// TicketPriorityDto ile bağlantılıdır
        /// Burada yapılacak herhangi bir değişiklik veritabanına yansıtılmalıdır(id dahil)
        /// Name -> Dto daki Name, Enum Adı -> Dto daki NormalizedName, 
        /// tamsayı ->  Dto daki Id
        /// </summary>
        public enum PriorityEnum
        {
            [Display(Name = "Kritik")]
            CRITICAL = 2,

            [Display(Name = "Yüksek Öncelikli")]
            HIGHPRIORITY = 3,

            [Display(Name = "Normal")]
            NORMAL = 4,

            [Display(Name = "Düşük Öncelikli")]
            LOWPRIORITY = 5
        }

        /// <summary>
        /// TicketStateDto ile bağlantılıdır
        /// Burada yapılacak herhangi bir değişiklik veritabanına yansıtılmalıdır(id dahil)
        /// Name -> Dto daki Name, Enum Adı -> Dto daki NormalizedName, 
        /// tamsayı ->  Dto daki Id
        /// </summary>
        public enum TicketStatusEnum
        {
            [Display(Name = "Yeni")]
            NEW = 1,

            [Display(Name = "Atandı")]
            ASSIGNED = 2,

            [Display(Name = "Üzerinde Çalışıyor")]
            WORKINGON = 3,

            [Display(Name = "Çözümlendi")]
            SOLVED = 4,

            [Display(Name = "Kapatıldı")] 
            CLOSED = 5,

            [Display(Name = "Askıya Alndı")]
            SUSPENDED = 6,

            [Display(Name = "Beklemede")]
            WAITING = 7,

            [Display(Name = "İptal Edildi")]
            REJECTED = 8
        }

        /// <summary>
        /// TicketStateRoleDto ile bağlantılıdır
        /// Burada yapılacak herhangi bir değişiklik veritabanına yansıtılmalıdır(id dahil)
        /// Name -> Dto daki Name, Enum Adı -> Dto daki NormalizedName, 
        /// tamsayı ->  Dto daki Id
        /// </summary>
        public enum TicketStateRoleEnum
        {
            [Display(Name = "İş Emri Bildiren Anonim Kişi")]
            ANONYMOUSTICKETREPORTER = 1,

            [Display(Name = "İş Emri Bildiren Kullanıcı")]
            USERTICKETREPORTER = 2,

            [Display(Name = "İş Emrini Kayıt Altına Alan")]
            USERTICKETRECORDER = 3,

            [Display(Name = "İş Emri Atanılan")]
            USERTICKETWORKER = 4,

            [Display(Name = "Atama Grup Yöneticisi")]
            ADMINASSIGNMENTGROUP = 5

        }

        /// <summary>
        /// TicketStateFlowDto ile bağlantılıdır
        /// Burada yapılacak herhangi bir değişiklik veritabanına yansıtılmalıdır(id dahil)
        /// Name -> Dto daki Name, Enum Adı -> Dto daki NormalizedName, 
        /// tamsayı ->  Dto daki Id
        /// </summary>
        public enum TicketStateFlowEnum
        {
            [Display(Name = "Kişiye Ata")]
            ASSIGNTOUSER = 1,

            [Display(Name = "Gruba Ata")]
            ASSIGNTOGROUP = 2,

            [Display(Name = "Çalışmaya Başla")]
            STARTTOWORKING = 3,

            [Display(Name = "Çözümle")]
            RESOLVE = 4,

            [Display(Name = "Askıya Al")]
            SUSPEND = 5,

            [Display(Name = "Beklemeye Al")]
            WAIT = 6,

            [Display(Name = "Yeniden Aç")]
            REOPEN = 7,

            [Display(Name = "İptal Et")]
            CANCEL = 8,

            [Display(Name = "Reddet")]
            REJECT = 9,

            [Display(Name = "Kapat")]
            CLOSE = 10
        }


        //TODO: Buraya ayrıca TicketStateTransitionEnumu tanımlanacak

        /// <summary>
        /// LocationCodeDto ile bağlantılıdır
        /// Burada yapılacak herhangi bir değişiklik veritabanına yansıtılmalıdır(id dahil)
        /// Name -> Dto daki Name, Enum Adı -> Dto daki NormalizedName, 
        /// tamsayı ->  Dto daki Id
        /// </summary>
        public enum LocationCodeEnum
        {
            [Display(Name = "UNDEFINED")]
            UNDEFINED = 0,

            [Display(Name = "BSHTY")]
            BSHTY = 1
        }



        #endregion Core

        #region PeriodicTicket

        public enum TicketPeriodEnum
        {
            [Display(Name = "Günlük")]
            DAILY = 1,

            [Display(Name = "Haftalık")]
            WEEKLY = 2,

            [Display(Name = "Aylık")]
            MONTHLY = 3,

            [Display(Name = "Yıllık")]
            YEARLY = 4
        }

        public enum PeriodicTicketDayEnum
        {
            [Display(Name = "Her Günde")]
            ALLDAY = 1,

            [Display(Name = "Haftaiçi Hergün")]
            ALLDAYEXCEPTWEEKEND = 2
        }

        #endregion PeriodicTicket

        #region BasicTicket

        public enum BasicTicketStatus
        {
            [Display(Name = "Yönlendirme Bekliyor")]
            WAITINGREDIRECTION = 9,

            [Display(Name = "Yönlendirildi")]
            REDIRECTED = 10,

            [Display(Name = "Belirsiz")]
            STATUSUNKNOWN = 12
        }

        #endregion 
    }
}
