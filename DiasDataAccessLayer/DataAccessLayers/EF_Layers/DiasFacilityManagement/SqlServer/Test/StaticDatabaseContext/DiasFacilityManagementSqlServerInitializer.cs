using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models;
using System.Linq;

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.StaticDatabaseContext
{
    public static class DiasFacilityManagementSqlServerInitializer
    {
        public static void InitializeAndSeedData()
        {
            using (var context = new DiasFacilityManagementSqlServer())
            {
                context.Database.EnsureCreated();

                #region lstSchema
                //InitializeTicketStates(context);
                //InitializeMenuPageV2s(context);
                //InitializeMenuPages(context);
                //InitializeResolutionFormQuestionTypes(context);
                //InitializeTicketStateTransitions(context);
                //InitializeWorkShifts(context);
                //InitializeTicketProperties(context);
                //..
                //TODO :Diğer lst tabloları içinde aynı method yazılacak(Location ve LocationV2 hariç)

                #endregion lstSchema

            }

        }

        //private static void InitializeTicketStates(DiasFacilityManagementSqlServer context)
        //{
        //    TicketState testRow = context.TicketStates.FirstOrDefault(b => b.StateDescription == "Açık");

        //    if (testRow == null)
        //    {
        //        context.TicketStates.Add(new TicketState { Id = 1, Name = "Açık"});
        //    }

        //    testRow = context.TicketStates.FirstOrDefault(b => b.Name == "Atandı");

        //    if (testRow == null)
        //    {
        //        context.TicketStates.Add(new TicketState { Id = 2, Name = "Atandı" });
        //    }

        //    testRow = context.TicketStates.FirstOrDefault(b => b.Name == "Çalışılıyor");

        //    if (testRow == null)
        //    {
        //        context.TicketStates.Add(new TicketState { Id = 3, Name = "Çalışılıyor" });
        //    }

        //    testRow = context.TicketStates.FirstOrDefault(b => b.Name == "Beklet");

        //    if (testRow == null)
        //    {
        //        context.TicketStates.Add(new TicketState { Id = 4, Name = "Beklet" });
        //    }

        //    testRow = context.TicketStates.FirstOrDefault(b => b.Name == "Reddet");

        //    if (testRow == null)
        //    {
        //        context.TicketStates.Add(new TicketState { Id = 5, Name = "Reddet" });
        //    }

        //    testRow = context.TicketStates.FirstOrDefault(b => b.Name == "Çözümlendi");

        //    if (testRow == null)
        //    {
        //        context.TicketStates.Add(new TicketState { Id = 6, Name = "Çözümlendi" });
        //    }

        //    testRow = context.TicketStates.FirstOrDefault(b => b.Name == "Kapalı");

        //    if (testRow == null)
        //    {
        //        context.TicketStates.Add(new TicketState { Id = 7, Name = "Kapalı" });
        //    }

        //    testRow = context.TicketStates.FirstOrDefault(b => b.Name == "Yeniden Açıldı");

        //    if (testRow == null)
        //    {
        //        context.TicketStates.Add(new TicketState { Id = 8, Name = "Yeniden Açıldı" });
        //    }

        //    context.SaveChanges();
        //}

        //private static void InitializeMenuPageV2s(DiasFacilityManagementSqlServer context) {

        //    MenuPageV2 testRow = context.MenuPageV2s.FirstOrDefault(b => b.MenuText == "Root");

        //    if (testRow == null)
        //    {
        //        context.MenuPageV2s.Add(new MenuPageV2 { Id = 0, MenuText = "Root" });
        //    }

        //    testRow = context.MenuPageV2s.FirstOrDefault(b => b.MenuText == "İş Emri Modülü");

        //    if (testRow == null)
        //    {
        //        context.MenuPageV2s.Add(new MenuPageV2 { Id = 18, MenuText = "İş Emri Modülü" });
        //    }

        //    testRow = context.MenuPageV2s.FirstOrDefault(b => b.MenuText == "İş Emri");

        //    if (testRow == null)
        //    {
        //        context.MenuPageV2s.Add(new MenuPageV2 { Id = 19, MenuText = "İş Emri" });
        //    }

        //    testRow = context.MenuPageV2s.FirstOrDefault(b => b.MenuText == "Hızlı İş Emri");

        //    if (testRow == null)
        //    {
        //        context.MenuPageV2s.Add(new MenuPageV2 { Id = 20, MenuText = "Hızlı İş Emri" });
        //    }

        //    testRow = context.MenuPageV2s.FirstOrDefault(b => b.MenuText == "Planlı İş Emirleri");

        //    if (testRow == null)
        //    {
        //        context.MenuPageV2s.Add(new MenuPageV2 { Id = 21, MenuText = "Planlı İş Emirleri" });
        //    }

        //    testRow = context.MenuPageV2s.FirstOrDefault(b => b.MenuText == "Varlık");

        //    if (testRow == null)
        //    {
        //        context.MenuPageV2s.Add(new MenuPageV2 { Id = 22, MenuText = "Varlık" });
        //    }

        //    testRow = context.MenuPageV2s.FirstOrDefault(b => b.MenuText == "Varlık Sorgulama");

        //    if (testRow == null)
        //    {
        //        context.MenuPageV2s.Add(new MenuPageV2 { Id = 23, MenuText = "Varlık Sorgulama" });
        //    }

        //    testRow = context.MenuPageV2s.FirstOrDefault(b => b.MenuText == "Kullanıcı İşlemleri");

        //    if (testRow == null)
        //    {
        //        context.MenuPageV2s.Add(new MenuPageV2 { Id = 17, MenuText = "Kullanıcı İşlemleri" });
        //    }

        //    testRow = context.MenuPageV2s.FirstOrDefault(b => b.MenuText == "Sistem Yönetimi");

        //    if (testRow == null)
        //    {
        //        context.MenuPageV2s.Add(new MenuPageV2 { Id = 16, MenuText = "Sistem Yönetimi" });
        //    }

        //    testRow = context.MenuPageV2s.FirstOrDefault(b => b.MenuText == "Power BI");

        //    if (testRow == null)
        //    {
        //        context.MenuPageV2s.Add(new MenuPageV2 { Id = 13, MenuText = "Power BI" });
        //    }

        //    testRow = context.MenuPageV2s.FirstOrDefault(b => b.MenuText == "Bilkent ŞH Ceza Raporu");

        //    if (testRow == null)
        //    {
        //        context.MenuPageV2s.Add(new MenuPageV2 { Id = 14, MenuText = "Bilkent ŞH Ceza Raporu" });
        //    }

        //    testRow = context.MenuPageV2s.FirstOrDefault(b => b.MenuText == "Mersin ŞH Ceza Raporu");

        //    if (testRow == null)
        //    {
        //        context.MenuPageV2s.Add(new MenuPageV2 { Id = 15, MenuText = "Mersin ŞH Ceza Raporu" });
        //    }
        //}

        //private static void InitializeMenuPages(DiasFacilityManagementSqlServer context)
        //{
        //    MenuPage testRow = context.MenuPages.FirstOrDefault(b => b.MenuText == "Root");

        //    if (testRow == null)
        //    {
        //        context.MenuPages.Add(new MenuPage { Id = "0", MenuText = "Root" });
        //    }

        //    testRow = context.MenuPages.FirstOrDefault(b => b.MenuText == "İş Emri Modülü");

        //    if (testRow == null)
        //    {
        //        context.MenuPages.Add(new MenuPage { Id = "15", MenuText = "İş Emri Modülü" });
        //    }

        //    testRow = context.MenuPages.FirstOrDefault(b => b.MenuText == "İş Emri");

        //    if (testRow == null)
        //    {
        //        context.MenuPages.Add(new MenuPage { Id = "16", MenuText = "İş Emri" });
        //    }

        //    testRow = context.MenuPages.FirstOrDefault(b => b.MenuText == "Hızlı İş Emri");

        //    if (testRow == null)
        //    {
        //        context.MenuPages.Add(new MenuPage { Id = "17", MenuText = "Hızlı İş Emri" });
        //    }

        //    testRow = context.MenuPages.FirstOrDefault(b => b.MenuText == "Planlı İş Emirleri");

        //    if (testRow == null)
        //    {
        //        context.MenuPages.Add(new MenuPage { Id = "18", MenuText = "Planlı İş Emirleri" });
        //    }

        //    testRow = context.MenuPages.FirstOrDefault(b => b.MenuText == "Varlık");

        //    if (testRow == null)
        //    {
        //        context.MenuPages.Add(new MenuPage { Id = "8", MenuText = "Varlık" });
        //    }

        //    testRow = context.MenuPages.FirstOrDefault(b => b.MenuText == "Varlık Sorgulama");

        //    if (testRow == null)
        //    {
        //        context.MenuPages.Add(new MenuPage { Id = "9", MenuText = "Varlık Sorgulama" });
        //    }

        //    testRow = context.MenuPages.FirstOrDefault(b => b.MenuText == "Kullanıcı İşlemleri");

        //    if (testRow == null)
        //    {
        //        context.MenuPages.Add(new MenuPage { Id = "14", MenuText = "Kullanıcı İşlemleri" });
        //    }

        //    testRow = context.MenuPages.FirstOrDefault(b => b.MenuText == "Sistem Yönetimi");

        //    if (testRow == null)
        //    {
        //        context.MenuPages.Add(new MenuPage { Id = "13", MenuText = "Sistem Yönetimi" });
        //    }

        //    testRow = context.MenuPages.FirstOrDefault(b => b.MenuText == "Power BI");

        //    if (testRow == null)
        //    {
        //        context.MenuPages.Add(new MenuPage { Id = "10", MenuText = "Power BI" });
        //    }

        //    testRow = context.MenuPages.FirstOrDefault(b => b.MenuText == "Bilkent ŞH Ceza Raporu");

        //    if (testRow == null)
        //    {
        //        context.MenuPages.Add(new MenuPage { Id = "11", MenuText = "Bilkent ŞH Ceza Raporu" });
        //    }

        //    testRow = context.MenuPages.FirstOrDefault(b => b.MenuText == "Mersin ŞH Ceza Raporu");

        //    if (testRow == null)
        //    {
        //        context.MenuPages.Add(new MenuPage { Id = "12", MenuText = "Mersin ŞH Ceza Raporu" });
        //    }
        //}

        //private static void InitializeResolutionFormQuestionTypes(DiasFacilityManagementSqlServer context) 
        //{ 
        // //TODO tablo boş canlı ya geçince tekrar bakılacak.
        //}

        //private static void InitializeTicketStateTransitions(DiasFacilityManagementSqlServer context)
        //{ 
        ////TODO tablo text veri yok.
        //}

        //private static void InitializeWorkShifts(DiasFacilityManagementSqlServer context)
        //{
        //    WorkShift testRow = context.WorkShifts.FirstOrDefault(b => b.ShiftName == "Vardiya 1");

        //    if (testRow == null)
        //    {
        //        context.WorkShifts.Add(new WorkShift { Id = 1, ShiftName = "Vardiya 1" });
        //    }

        //    testRow = context.WorkShifts.FirstOrDefault(b => b.ShiftName == "Vardiya 2");

        //    if (testRow == null)
        //    {
        //        context.WorkShifts.Add(new WorkShift { Id = 2, ShiftName = "Vardiya 2" });
        //    }

        //    testRow = context.WorkShifts.FirstOrDefault(b => b.ShiftName == "Vardiya 3");

        //    if (testRow == null)
        //    {
        //        context.WorkShifts.Add(new WorkShift { Id = 3, ShiftName = "Vardiya 3" });
        //    }
        //}

        //private static void InitializeTicketProperties(DiasFacilityManagementSqlServer context)
        //{
        //    TicketPriority testRow = context.TicketProperties.FirstOrDefault(b => b.Name == "Kritik");

        //    if (testRow == null)
        //    {
        //        context.TicketProperties.Add(new TicketPriority { Id = 1, Name = "Kritik" });
        //    }

        //    testRow = context.TicketProperties.FirstOrDefault(b => b.Name == "Yüksek Öncelikli");

        //    if (testRow == null)
        //    {
        //        context.TicketProperties.Add(new TicketPriority { Id = 2, Name = "Yüksek Öncelikli" });
        //    }

        //    testRow = context.TicketProperties.FirstOrDefault(b => b.Name == "Normal");

        //    if (testRow == null)
        //    {
        //        context.TicketProperties.Add(new TicketPriority { Id = 3, Name = "Normal" });
        //    }

        //    testRow = context.TicketProperties.FirstOrDefault(b => b.Name == "Düşük Öncelikli");

        //    if (testRow == null)
        //    {
        //        context.TicketProperties.Add(new TicketPriority { Id = 4, Name = "Düşük Öncelikli" });
        //    }
        //}

        //private static void InitializeBasicTicketState(DiasFacilityManagementSqlServer context) 
        //{
        //    BasicTicketState testRow = context.BasicTicketStates.FirstOrDefault(b => b.na == "Yönlendirme bekliyor");

        //    if (testRow == null)
        //    {
        //        context.BasicTicketStates.Add(new BasicTicketState { Id = 9, BasicStateDescription = "Yönlendirme bekliyor" });
        //    }

        //    testRow = context.BasicTicketStates.FirstOrDefault(b => b.BasicStateDescription == "Yönlendirildi");

        //    if (testRow == null)
        //    {
        //        context.BasicTicketStates.Add(new BasicTicketState { Id = 10, BasicStateDescription = "Yönlendirildi" });
        //    }           

        //    testRow = context.BasicTicketStates.FirstOrDefault(b => b.BasicStateDescription == "Belirsiz");

        //    if (testRow == null)
        //    {
        //        context.BasicTicketStates.Add(new BasicTicketState { Id = 12, BasicStateDescription = "Belirsiz" });
        //    }

        //}
    }
}
