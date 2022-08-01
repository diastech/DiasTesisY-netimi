using DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using System.Linq;
using System;

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.StaticDatabaseContext
{
    public static class DiasFacilityManagementSqlServerInitializer
    {
        public static void InitializeAndSeedData()
        {
            using (var context = new DiasFacilityManagementSqlServer())
            {
                context.Database.EnsureCreated();

                #region lstSchema
                InitializeTicketStates(context);
                InitializeMenuPages(context);
                InitializeResolutionFormQuestionTypes(context);
                InitializeTicketStateTransitions(context);
                InitializeWorkShifts(context);
                InitializeTicketProperties(context);
                InitializeTicketStateRole(context);
                InitializeApiControllerDescription(context);
                InitializeApiActionDescription(context);
                InitializeBasicTicketState(context);
                InitializeTicketStateTransitions(context);
                InitializeTicketStateFlow(context);
                InitializeTicketStateFlowRole(context);
                InitializeTicketStateTransitionFlow(context);


                //..
                //TODO :Diğer lst tabloları içinde aynı method yazılacak(Location ve LocationV2 hariç)

                #endregion lstSchema

            }

        }

        private static void InitializeTicketStates(DiasFacilityManagementSqlServer context)
        {
            TicketState testRow = context.TicketStates.FirstOrDefault(b => b.Name == "Yeni");

            if (testRow == null)
            {
                context.TicketStates.Add(new TicketState { Id = 1, Name = "Yeni", NormalizedName = "NEW" });
            }

            testRow = context.TicketStates.FirstOrDefault(b => b.Name == "Atandı");

            if (testRow == null)
            {
                context.TicketStates.Add(new TicketState { Id = 2, Name = "Atandı" });
            }

            testRow = context.TicketStates.FirstOrDefault(b => b.Name == "Üzerinde Çalışılıyor");

            if (testRow == null)
            {
                context.TicketStates.Add(new TicketState { Id = 3, Name = "Üzerinde Çalışılıyor" });
            }

            testRow = context.TicketStates.FirstOrDefault(b => b.Name == "Çözümlendi");

            if (testRow == null)
            {
                context.TicketStates.Add(new TicketState { Id = 4, Name = "Çözümlendi" });
            }

            testRow = context.TicketStates.FirstOrDefault(b => b.Name == "Kapatıldı");

            if (testRow == null)
            {
                context.TicketStates.Add(new TicketState { Id = 5, Name = "Kapatıldı" });
            }

            testRow = context.TicketStates.FirstOrDefault(b => b.Name == "Askıya Alındı");

            if (testRow == null)
            {
                context.TicketStates.Add(new TicketState { Id = 6, Name = "Askıya Alındı" });
            }

            testRow = context.TicketStates.FirstOrDefault(b => b.Name == "Beklemede");

            if (testRow == null)
            {
                context.TicketStates.Add(new TicketState { Id = 7, Name = "Beklemede" });
            }

            testRow = context.TicketStates.FirstOrDefault(b => b.Name == "İptal Edildi");

            if (testRow == null)
            {
                context.TicketStates.Add(new TicketState { Id = 8, Name = "İptal Edildi" });
            }

            context.SaveChanges();
        }

        //TODO: Yeni menü güncellemelerine göre düzeltilecek
        //TODO : Web UI da MenuPageV2'e geçilince bu metod silinecek
        private static void InitializeMenuPages(DiasFacilityManagementSqlServer context)
        {
            //MenuPage testRow = context.MenuPages.FirstOrDefault(b => b.MenuText == "Root");

            //if (testRow == null)
            //{
            //    context.MenuPages.Add(new MenuPage { Id = 0, HierarchicalOrder = 0, HierarchicalLevel = 0, ParentId = null,  MenuText = "Root", UrlPath = null, MenuIcon = null, ExpandOnStart = false, MenuImagePath = null });
            //}

            //testRow = context.MenuPages.FirstOrDefault(b => b.MenuText == "İş Emri Modülü");

            //if (testRow == null)
            //{
            //    context.MenuPages.Add(new MenuPage { Id = 15, MenuText = "İş Emri Modülü" });
            //}

            //testRow = context.MenuPages.FirstOrDefault(b => b.MenuText == "İş Emri");

            //if (testRow == null)
            //{
            //    context.MenuPages.Add(new MenuPage { Id = 16, MenuText = "İş Emri" });
            //}

            //testRow = context.MenuPages.FirstOrDefault(b => b.MenuText == "Hızlı İş Emri");

            //if (testRow == null)
            //{
            //    context.MenuPages.Add(new MenuPage { Id = "17", MenuText = "Hızlı İş Emri" });
            //}

            //testRow = context.MenuPages.FirstOrDefault(b => b.MenuText == "Planlı İş Emirleri");

            //if (testRow == null)
            //{
            //    context.MenuPages.Add(new MenuPage { Id = "18", MenuText = "Planlı İş Emirleri" });
            //}

            //testRow = context.MenuPages.FirstOrDefault(b => b.MenuText == "Varlık");

            //if (testRow == null)
            //{
            //    context.MenuPages.Add(new MenuPage { Id = "8", MenuText = "Varlık" });
            //}

            //testRow = context.MenuPages.FirstOrDefault(b => b.MenuText == "Varlık Sorgulama");

            //if (testRow == null)
            //{
            //    context.MenuPages.Add(new MenuPage { Id = "9", MenuText = "Varlık Sorgulama" });
            //}

            //testRow = context.MenuPages.FirstOrDefault(b => b.MenuText == "Kullanıcı İşlemleri");

            //if (testRow == null)
            //{
            //    context.MenuPages.Add(new MenuPage { Id = "14", MenuText = "Kullanıcı İşlemleri" });
            //}

            //testRow = context.MenuPages.FirstOrDefault(b => b.MenuText == "Sistem Yönetimi");

            //if (testRow == null)
            //{
            //    context.MenuPages.Add(new MenuPage { Id = "13", MenuText = "Sistem Yönetimi" });
            //}

            //testRow = context.MenuPages.FirstOrDefault(b => b.MenuText == "Power BI");

            //if (testRow == null)
            //{
            //    context.MenuPages.Add(new MenuPage { Id = "10", MenuText = "Power BI" });
            //}

            //testRow = context.MenuPages.FirstOrDefault(b => b.MenuText == "Bilkent ŞH Ceza Raporu");

            //if (testRow == null)
            //{
            //    context.MenuPages.Add(new MenuPage { Id = "11", MenuText = "Bilkent ŞH Ceza Raporu" });
            //}

            //testRow = context.MenuPages.FirstOrDefault(b => b.MenuText == "Mersin ŞH Ceza Raporu");

            //if (testRow == null)
            //{
            //    context.MenuPages.Add(new MenuPage { Id = "12", MenuText = "Mersin ŞH Ceza Raporu" });
            //}
        }

        private static void InitializeResolutionFormQuestionTypes(DiasFacilityManagementSqlServer context)
        {
            //TODO: tablo boş canlı ya geçince tekrar bakılacak.
        }

        private static void InitializeTicketStateTransitions(DiasFacilityManagementSqlServer context)
        {
            TicketStateTransition testRow = context.TicketStateTransitions.FirstOrDefault(b => (b.SourceTicketStateId == 1) && (b.DestinationTicketStateId == 2));

            if (testRow == null)
            {
                context.TicketStateTransitions.Add(new TicketStateTransition {Id = 1, SourceTicketStateId = 1, DestinationTicketStateId = 2 });
            }

            testRow = context.TicketStateTransitions.FirstOrDefault(b => b.DestinationTicketStateId == 1);

            if (testRow == null)
            {
                context.TicketStateTransitions.Add(new TicketStateTransition { SourceTicketStateId = 2, DestinationTicketStateId = 1 });
            }

            testRow = context.TicketStateTransitions.FirstOrDefault(b => b.DestinationTicketStateId == 3);

            if (testRow == null)
            {
                context.TicketStateTransitions.Add(new TicketStateTransition { SourceTicketStateId = 2, DestinationTicketStateId = 3 });
            }

            testRow = context.TicketStateTransitions.FirstOrDefault(b => b.DestinationTicketStateId == 2);

            if (testRow == null)
            {
                context.TicketStateTransitions.Add(new TicketStateTransition { SourceTicketStateId = 3, DestinationTicketStateId = 2 });
            }

            testRow = context.TicketStateTransitions.FirstOrDefault(b => b.DestinationTicketStateId == 4);

            if (testRow == null)
            {
                context.TicketStateTransitions.Add(new TicketStateTransition { SourceTicketStateId = 3, DestinationTicketStateId = 4 });
            }

            testRow = context.TicketStateTransitions.FirstOrDefault(b => b.DestinationTicketStateId == 3);

            if (testRow == null)
            {
                context.TicketStateTransitions.Add(new TicketStateTransition { SourceTicketStateId = 4, DestinationTicketStateId = 3 });
            }

            testRow = context.TicketStateTransitions.FirstOrDefault(b => b.DestinationTicketStateId == 5);

            if (testRow == null)
            {
                context.TicketStateTransitions.Add(new TicketStateTransition { SourceTicketStateId = 4, DestinationTicketStateId = 5 });
            }

            testRow = context.TicketStateTransitions.FirstOrDefault(b => b.DestinationTicketStateId == 6);

            if (testRow == null)
            {
                context.TicketStateTransitions.Add(new TicketStateTransition { SourceTicketStateId = 3, DestinationTicketStateId = 6 });
            }

            testRow = context.TicketStateTransitions.FirstOrDefault(b => b.DestinationTicketStateId == 3);

            if (testRow == null)
            {
                context.TicketStateTransitions.Add(new TicketStateTransition { SourceTicketStateId = 6, DestinationTicketStateId = 3 });
            }

            testRow = context.TicketStateTransitions.FirstOrDefault(b => b.DestinationTicketStateId == 7);

            if (testRow == null)
            {
                context.TicketStateTransitions.Add(new TicketStateTransition { SourceTicketStateId = 3, DestinationTicketStateId = 7 });
            }

            testRow = context.TicketStateTransitions.FirstOrDefault(b => b.DestinationTicketStateId == 3);

            if (testRow == null)
            {
                context.TicketStateTransitions.Add(new TicketStateTransition { SourceTicketStateId = 7, DestinationTicketStateId = 3});
            }

            testRow = context.TicketStateTransitions.FirstOrDefault(b => b.DestinationTicketStateId == 2);

            if (testRow == null)
            {
                context.TicketStateTransitions.Add(new TicketStateTransition { SourceTicketStateId = 5, DestinationTicketStateId = 2 });
            }

            testRow = context.TicketStateTransitions.FirstOrDefault(b => b.DestinationTicketStateId == 8);

            if (testRow == null)
            {
                context.TicketStateTransitions.Add(new TicketStateTransition { SourceTicketStateId = 3, DestinationTicketStateId = 8 });
            }

            testRow = context.TicketStateTransitions.FirstOrDefault(b => b.DestinationTicketStateId == 8);

            if (testRow == null)
            {
                context.TicketStateTransitions.Add(new TicketStateTransition { SourceTicketStateId = 4, DestinationTicketStateId = 8 });
            }

            testRow = context.TicketStateTransitions.FirstOrDefault(b => b.DestinationTicketStateId == 8);

            if (testRow == null)
            {
                context.TicketStateTransitions.Add(new TicketStateTransition { SourceTicketStateId = 6, DestinationTicketStateId = 8 });
            }
            testRow = context.TicketStateTransitions.FirstOrDefault(b => b.DestinationTicketStateId == 8);

            if (testRow == null)
            {
                context.TicketStateTransitions.Add(new TicketStateTransition { SourceTicketStateId = 7, DestinationTicketStateId = 8 });
            }
            testRow = context.TicketStateTransitions.FirstOrDefault(b => b.DestinationTicketStateId == 8);

            if (testRow == null)
            {
                context.TicketStateTransitions.Add(new TicketStateTransition { SourceTicketStateId = 1, DestinationTicketStateId = 8 });
            }
            testRow = context.TicketStateTransitions.FirstOrDefault(b => b.DestinationTicketStateId == 2);

            if (testRow == null)
            {
                context.TicketStateTransitions.Add(new TicketStateTransition { SourceTicketStateId = 2, DestinationTicketStateId = 2 });
            }
            testRow = context.TicketStateTransitions.FirstOrDefault(b => b.DestinationTicketStateId == 6);

            if (testRow == null)
            {
                context.TicketStateTransitions.Add(new TicketStateTransition { SourceTicketStateId = 2, DestinationTicketStateId = 6 });
            }
            testRow = context.TicketStateTransitions.FirstOrDefault(b => b.DestinationTicketStateId == 2);

            if (testRow == null)
            {
                context.TicketStateTransitions.Add(new TicketStateTransition { SourceTicketStateId = 6, DestinationTicketStateId = 2 });
            }
            testRow = context.TicketStateTransitions.FirstOrDefault(b => b.DestinationTicketStateId == 7);

            if (testRow == null)
            {
                context.TicketStateTransitions.Add(new TicketStateTransition { SourceTicketStateId = 2, DestinationTicketStateId = 7 });
            }

        }

        private static void InitializeWorkShifts(DiasFacilityManagementSqlServer context)
        {
            WorkShift testRow = context.WorkShifts.FirstOrDefault(b => b.ShiftName == "Vardiya 1");

            if (testRow == null)
            {
                context.WorkShifts.Add(new WorkShift { Id = 1, ShiftName = "Vardiya 1", ShiftStartTime = new DateTime(1900, 1, 1, 8, 0, 0), ShiftEndTime = new DateTime(1900, 1, 1, 16, 0, 0) });
            }

            testRow = context.WorkShifts.FirstOrDefault(b => b.ShiftName == "Vardiya 2");

            if (testRow == null)
            {
                context.WorkShifts.Add(new WorkShift { Id = 2, ShiftName = "Vardiya 2" });
            }

            testRow = context.WorkShifts.FirstOrDefault(b => b.ShiftName == "Vardiya 3");

            if (testRow == null)
            {
                context.WorkShifts.Add(new WorkShift { Id = 3, ShiftName = "Vardiya 3" });
            }
        }

        private static void InitializeTicketProperties(DiasFacilityManagementSqlServer context)
        {
            TicketPriority testRow = context.TicketProperties.FirstOrDefault(b => b.Name == "Kritik");

            if (testRow == null)
            {
                context.TicketProperties.Add(new TicketPriority { Id = 2, Name = "Kritik", NormalizedName = "CRITICAL" });
            }

            testRow = context.TicketProperties.FirstOrDefault(b => b.Name == "Yüksek Öncelikli");

            if (testRow == null)
            {
                context.TicketProperties.Add(new TicketPriority { Id = 3, Name = "Yüksek Öncelikli" });
            }

            testRow = context.TicketProperties.FirstOrDefault(b => b.Name == "Normal");

            if (testRow == null)
            {
                context.TicketProperties.Add(new TicketPriority { Id = 4, Name = "Normal" });
            }

            testRow = context.TicketProperties.FirstOrDefault(b => b.Name == "Düşük Öncelikli");

            if (testRow == null)
            {
                context.TicketProperties.Add(new TicketPriority { Id = 5, Name = "Düşük Öncelikli" });
            }
        }

        private static void InitializeBasicTicketState(DiasFacilityManagementSqlServer context)
        {
            BasicTicketState testRow = context.BasicTicketStates.FirstOrDefault(b => b.Name == "Yönlendirme bekliyor");

            if (testRow == null)
            {
                context.BasicTicketStates.Add(new BasicTicketState { Id = 9, Name = "Yönlendirme bekliyor", NormalizedName = "WAITINGREDIRECTION" });
            }

            testRow = context.BasicTicketStates.FirstOrDefault(b => b.Name == "Yönlendirildi");

            if (testRow == null)
            {
                context.BasicTicketStates.Add(new BasicTicketState { Id = 10, Name = "Yönlendirildi" });
            }

            testRow = context.BasicTicketStates.FirstOrDefault(b => b.Name == "Belirsiz");

            if (testRow == null)
            {
                context.BasicTicketStates.Add(new BasicTicketState { Id = 12, Name = "Belirsiz" });
            }

        }

        private static void InitializeApiActionDescription(DiasFacilityManagementSqlServer context)
        {
            ApiActionDescription testRow = context.ApiActionDescriptions.FirstOrDefault(b => (b.ApiControllerDescriptionId == 1) && (b.Name == "GetAll"));
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 1, Name = "GetAll", ApiControllerDescriptionId = 1, AuthorizationCode  = 1});
            }

            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 2, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "DeleteById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 3, Name = "DeleteById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 4, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 5, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 6, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 7, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "DeleteById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 8, Name = "DeleteById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 9, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 10, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAllTicketAttachmentsByTicketId");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 11, Name = "GetAllTicketAttachmentsByTicketId" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAllNoteAttachmentsByNoteId");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 12, Name = "GetAllNoteAttachmentsByNoteId" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAllTicketAttachmentsByBasicTicketId");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 13, Name = "GetAllTicketAttachmentsByBasicTicketId" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 14, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 15, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "DeleteById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 16, Name = "DeleteById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 17, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 18, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 19, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 20, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 21, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "DeleteById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 22, Name = "DeleteById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 23, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 24, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 25, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 26, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 27, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 28, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 29, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "DeleteById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 30, Name = "DeleteById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 31, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 32, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 33, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 34, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 35, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 36, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "DeleteById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 37, Name = "DeleteById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 38, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 39, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 40, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 41, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "DeleteById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 42, Name = "DeleteById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 43, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 44, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 45, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 46, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "DeleteById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 47, Name = "DeleteById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 48, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 49, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 50, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 51, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "DeleteById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 52, Name = "DeleteById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 53, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 54, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAllTicketAuditHistoryByTicketId");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 55, Name = "GetAllTicketAuditHistoryByTicketId" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 56, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 57, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "DeleteById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 58, Name = "DeleteById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 59, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 60, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAllTicketsByBasicTicketId");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 61, Name = "GetAllTicketsByBasicTicketId" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "UpdateTicketState");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 62, Name = "UpdateTicketState" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 63, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 64, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "DeleteById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 65, Name = "DeleteById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 66, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 67, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetTicketNoteByTicketId");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 68, Name = "GetTicketNoteByTicketId" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 69, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 70, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "DeleteById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 71, Name = "DeleteById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 72, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 73, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 74, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 75, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "DeleteById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 76, Name = "DeleteById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 77, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 78, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 79, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 80, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 81, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 82, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "DeleteById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 83, Name = "DeleteById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 84, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 85, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 86, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 87, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "DeleteById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 88, Name = "DeleteById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 89, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 90, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 91, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 92, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "DeleteById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 93, Name = "DeleteById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 94, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 95, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 96, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 97, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "DeleteById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 98, Name = "DeleteById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 99, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 100, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "InsertWithFastTicket");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 101, Name = "InsertWithFastTicket" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAllTicketsByBasicTicketId");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 102, Name = "GetAllTicketsByBasicTicketId" });
            }
            // testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "UpdateState");
            // if (testRow == null)
            // {
            //     context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 103, Name = "UpdateState" }); db de 103 gözlemlenmedi o sebeple comment yaptım
            // }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "UpdateState");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 104, Name = "UpdateState" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 105, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 106, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "DeleteById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 107, Name = "DeleteById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 108, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 109, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Login");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 110, Name = "Login" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAllUsers");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 111, Name = "GetAllUsers" });
            }

            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 112, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Delete");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 113, Name = "Delete" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 114, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 115, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 116, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 117, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Delete");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 118, Name = "Delete" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 119, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 120, Name = "Update" });
            }
            //testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAllUsers");
            //if (testRow == null)
            //{
            //    context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 121, Name = "GetAllUsers" });
            //}
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 122, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 123, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Delete");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 124, Name = "Delete" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 125, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 126, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 127, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 128, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Test");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 129, Name = "Test" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "TestAnonymous");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 130, Name = "TestAnonymous" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 131, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 132, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Delete");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 133, Name = "Delete" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 134, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 135, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetByUserId");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 136, Name = "GetByUserId" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 137, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 138, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Delete");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 139, Name = "Delete" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 140, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 141, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 142, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 143, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Delete");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 144, Name = "Delete" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 145, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 146, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetByUserId");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 147, Name = "GetByUserId" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 148, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 149, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Delete");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 150, Name = "Delete" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 151, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 152, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 153, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 154, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Delete");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 155, Name = "Delete" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 156, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 157, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Delete");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 158, Name = "Delete" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 159, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 160, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 161, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 162, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Delete");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 163, Name = "Delete" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 164, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 165, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 166, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 167, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 168, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Delete");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 169, Name = "Delete" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 170, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 171, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 172, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 173, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Delete");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 174, Name = "Delete" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 175, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 176, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 177, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 178, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Delete");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 179, Name = "Delete" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 180, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 181, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAllUserLoginsByUserId");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 182, Name = "GetAllUserLoginsByUserId" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAll");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 183, Name = "GetAll" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetById");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 184, Name = "GetById" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Delete");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 185, Name = "Delete" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Insert");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 186, Name = "Insert" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "Update");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 187, Name = "Update" });
            }
            testRow = context.ApiActionDescriptions.FirstOrDefault(b => b.Name == "GetAllUserTokensByUserId");
            if (testRow == null)
            {
                context.ApiActionDescriptions.Add(new ApiActionDescription { Id = 188, Name = "GetAllUserTokensByUserId" });
            }

        }

        private static void InitializeApiControllerDescription(DiasFacilityManagementSqlServer context)
        {
            ApiControllerDescription testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "AssignmentGroup");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 1, Name = "AssignmentGroup" });
            }

            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "Attachment");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 2, Name = "Attachment" });

            }

            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "BasicTicket");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 3, Name = "BasicTicket" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "BasicTicketState");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 4, Name = "BasicTicketState" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "BasicTicketWrapper");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 5, Name = "BasicTicketWrapper" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "Location");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 6, Name = "Location" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "LocationWrapper");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 7, Name = "LocationWrapper" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "MenuPage");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 8, Name = "MenuPage" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "PeriodicTicket");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 9, Name = "PeriodicTicket" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "PeriodicTicketWrapper");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 10, Name = "PeriodicTicketWrapper" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "ResolutionForm");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 11, Name = "ResolutionForm" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "TicketAuditHistory");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 12, Name = "TicketAuditHistory" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "Ticket");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 13, Name = "Ticket" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "TicketNote");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 14, Name = "TicketNote" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "TicketPriority");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 15, Name = "AttaTicketPrioritychment" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "TicketReasonCategory");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 16, Name = "TicketReasonCategory" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "TicketReasonCategoryWrapper");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 17, Name = "TicketReasonCategoryWrapper" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "TicketReason");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 18, Name = "TicketReason" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "TicketRelatedLocation");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 19, Name = "TicketRelatedLocation" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "TicketState");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 20, Name = "TicketState" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "TicketWrapper");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 21, Name = "TicketWrapper" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "UserMenuPage");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 22, Name = "UserMenuPage" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "WeatherForecast");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 23, Name = "WeatherForecast" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "Authentication");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 24, Name = "Authentication" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "Authorization");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 25, Name = "Authorization" });

            }

            //testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "UserRole");
            //if (testRow == null)
            //{
            //    context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 26, Name = "UserRole" });

            //}

            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "Users");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 27, Name = "Users" });

            }

            //  testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "WeatherForecastControllerIdentity");
            //  if (testRow == null)
            //   {
            //    context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 28, Name = "WeatherForecastControllerIdentity" }); db de 28 gözlemlenmedi o sebeple comment yaptım

            //}

            //testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "WeatherForecastControllerIdentity");
            //if (testRow == null)
            //{
            //    context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 29, Name = "WeatherForecastControllerIdentity" });

            //}

            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "ApiActionDescription");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 30, Name = "ApiActionDescription" });

            }

            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "ApiControllerDescription");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 31, Name = "ApiControllerDescription" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "AttachmentWrapper");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 32, Name = "AttachmentWrapper" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "AuthenticationTest");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 33, Name = "AuthenticationTest" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "CompanyRoleClaim");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 34, Name = "CompanyRoleClaim" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "CompanyRole");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 35, Name = "CompanyRole" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "CompanyRoleUser");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 36, Name = "CompanyRoleUser" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "MenuPageV2");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 37, Name = "MenuPageV2" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "TicketNoteWrapper");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 38, Name = "TicketNoteWrapper" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "TicketStateFlow");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 39, Name = "TicketStateFlow" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "TicketStateFlowRole");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 40, Name = "TicketStateFlowRole" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "TicketStateFlowWrapper");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 41, Name = "TicketStateFlowWrapper" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "TicketStateRole");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 42, Name = "TicketStateRole" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "UserClaim");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 43, Name = "UserClaim" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "UserLogin");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 44, Name = "UserLogin" });

            }
            testRow = context.ApiControllerDescriptions.FirstOrDefault(b => b.Name == "UserToken");
            if (testRow == null)
            {
                context.ApiControllerDescriptions.Add(new ApiControllerDescription { Id = 45, Name = "UserToken" });

            }


        }

        private static void InitializeTicketStateRole(DiasFacilityManagementSqlServer context)
        {
            TicketStateRole testRow = context.TicketStateRoles.FirstOrDefault(b => b.Name == "AnonymousTicketReporter");
            if (testRow == null)
            {
                context.TicketStateRoles.Add(new TicketStateRole { Id = 1, Name = "İş Emri Bildiren Anonim Kişi", NormalizedName= "ANONYMOUSTICKETREPORTER" });
            }

            testRow = context.TicketStateRoles.FirstOrDefault(b => b.Name == "UserTicketReporter");
            if (testRow == null)
            {
                context.TicketStateRoles.Add(new TicketStateRole { Id = 2, Name = "UserTicketReporter" });

            }

            testRow = context.TicketStateRoles.FirstOrDefault(b => b.Name == "UserTicketRecorder");
            if (testRow == null)
            {
                context.TicketStateRoles.Add(new TicketStateRole { Id = 3, Name = "UserTicketRecorder" });

            }
            testRow = context.TicketStateRoles.FirstOrDefault(b => b.Name == "UserTicketWorker");
            if (testRow == null)
            {
                context.TicketStateRoles.Add(new TicketStateRole { Id = 4, Name = "UserTicketWorker" });

            }
            testRow = context.TicketStateRoles.FirstOrDefault(b => b.Name == "AdminAssignmentGroup");
            if (testRow == null)
            {
                context.TicketStateRoles.Add(new TicketStateRole { Id = 5, Name = "AdminAssignmentGroup" });

            }
        }

        private static void InitializeTicketStateFlow(DiasFacilityManagementSqlServer context)
        {
            TicketStateFlow testRow = context.TicketStateFlows.FirstOrDefault(b => b.Name == "Kişiye Ata");
            if (testRow == null)
            {
                context.TicketStateFlows.Add(new TicketStateFlow { Id = 1, Name = "Kişiye Ata", NormalizedName = "ASSIGNTOUSER" });
            }

            testRow = context.TicketStateFlows.FirstOrDefault(b => b.Name == "Gruba Ata");
            if (testRow == null)
            {
                context.TicketStateFlows.Add(new TicketStateFlow { Id = 2, Name = "Gruba Ata" });

            }

            testRow = context.TicketStateFlows.FirstOrDefault(b => b.Name == "Çalışmaya Başla");
            if (testRow == null)
            {
                context.TicketStateFlows.Add(new TicketStateFlow { Id = 3, Name = "Çalışmaya Başla" });

            }
            testRow = context.TicketStateFlows.FirstOrDefault(b => b.Name == "Çözümle");
            if (testRow == null)
            {
                context.TicketStateFlows.Add(new TicketStateFlow { Id = 4, Name = "Çözümle" });

            }
            testRow = context.TicketStateFlows.FirstOrDefault(b => b.Name == "Askıya Al");
            if (testRow == null)
            {
                context.TicketStateFlows.Add(new TicketStateFlow { Id = 5, Name = "Askıya Al" });

            }
            testRow = context.TicketStateFlows.FirstOrDefault(b => b.Name == "Beklemeye Al");
            if (testRow == null)
            {
                context.TicketStateFlows.Add(new TicketStateFlow { Id = 6, Name = "Beklemeye Al" });

            }
            testRow = context.TicketStateFlows.FirstOrDefault(b => b.Name == "Yeniden Aç");
            if (testRow == null)
            {
                context.TicketStateFlows.Add(new TicketStateFlow { Id = 7, Name = "Yeniden Aç" });

            }
            testRow = context.TicketStateFlows.FirstOrDefault(b => b.Name == "İptal Et");
            if (testRow == null)
            {
                context.TicketStateFlows.Add(new TicketStateFlow { Id = 8, Name = "İptal Et" });

            }
            testRow = context.TicketStateFlows.FirstOrDefault(b => b.Name == "Reddet");
            if (testRow == null)
            {
                context.TicketStateFlows.Add(new TicketStateFlow { Id = 9, Name = "Reddet" });

            }
            testRow = context.TicketStateFlows.FirstOrDefault(b => b.Name == "Kapat");
            if (testRow == null)
            {
                context.TicketStateFlows.Add(new TicketStateFlow { Id = 10, Name = "Kapat" });

            }
        }

        private static void InitializeTicketStateFlowRole(DiasFacilityManagementSqlServer context)
        {
            TicketStateFlowRole testRow = context.TicketStateFlowRoles.FirstOrDefault(b => (b.TicketStateRoleId == 1) && (b.TicketStateTransitionFlowId == 1));
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 1, TicketStateRoleId = 1, TicketStateTransitionFlowId = 1, PermissionGranted = false });
            }

            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 2);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 2, TicketStateTransitionFlowId = 2 });

            }


            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 3);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 3, TicketStateTransitionFlowId = 3 });

            }


            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 4);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 4, TicketStateTransitionFlowId = 4 });

            }


            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 5);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 5, TicketStateTransitionFlowId = 5 });

            }


            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 6);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 6, TicketStateTransitionFlowId = 6 });

            }


            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 7);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 7, TicketStateTransitionFlowId = 7 });

            }


            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 8);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 8, TicketStateTransitionFlowId = 8 });

            }


            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 9);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 9, TicketStateTransitionFlowId = 9 });

            }


            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 10);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 10, TicketStateTransitionFlowId = 10 });

            }


            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 11);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 11, TicketStateTransitionFlowId = 11 });

            }


            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 12);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 12, TicketStateTransitionFlowId = 12 });

            }


            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 13);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 13, TicketStateTransitionFlowId = 13 });

            }


            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 14);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 14, TicketStateTransitionFlowId = 14 });

            }


            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 15);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 15, TicketStateTransitionFlowId = 15 });

            }


            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 16);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 16, TicketStateTransitionFlowId = 16 });

            }


            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 17);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 17, TicketStateTransitionFlowId = 17 });

            }

            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 18);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 18, TicketStateTransitionFlowId = 18 });

            }

            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 19);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 19, TicketStateTransitionFlowId = 19 });

            }

            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 20);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 20, TicketStateTransitionFlowId = 20 });

            }

            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 21);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 21, TicketStateTransitionFlowId = 21 });

            }

            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 22);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 22, TicketStateTransitionFlowId = 22 });

            }

            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 23);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 23, TicketStateTransitionFlowId = 23 });

            }

            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 24);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 24, TicketStateTransitionFlowId = 24 });

            }

            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 25);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 25, TicketStateTransitionFlowId = 25 });

            }

            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 1);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 26, TicketStateTransitionFlowId = 1 });

            }

            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 2);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 27, TicketStateTransitionFlowId = 2 });

            }

            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 3);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 28, TicketStateTransitionFlowId = 3 });

            }

            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 4);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 29, TicketStateTransitionFlowId = 4 });

            }

            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 5);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 30, TicketStateTransitionFlowId = 5 });

            }

            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 6);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 31, TicketStateTransitionFlowId = 6 });

            }

            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 7);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 32, TicketStateTransitionFlowId = 7 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 8);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 33, TicketStateTransitionFlowId = 8 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 9);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 34, TicketStateTransitionFlowId = 9 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 10);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 35, TicketStateTransitionFlowId = 10 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 11);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 36, TicketStateTransitionFlowId = 11 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 12);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 37, TicketStateTransitionFlowId = 12 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 13);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 38, TicketStateTransitionFlowId = 13 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 14);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 39, TicketStateTransitionFlowId = 14 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 15);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 40, TicketStateTransitionFlowId = 15 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 16);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 41, TicketStateTransitionFlowId = 16 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 17);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 42, TicketStateTransitionFlowId = 17 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 18);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 43, TicketStateTransitionFlowId = 18 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 19);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 44, TicketStateTransitionFlowId = 19 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 20);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 45, TicketStateTransitionFlowId = 20 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 21);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 46, TicketStateTransitionFlowId = 21 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 22);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 47, TicketStateTransitionFlowId = 22 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 23);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 48, TicketStateTransitionFlowId = 23 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 24);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 49, TicketStateTransitionFlowId = 24 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 25);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 50, TicketStateTransitionFlowId = 25 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 1);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 51, TicketStateTransitionFlowId = 1 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 2);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 52, TicketStateTransitionFlowId = 2 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 3);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 53, TicketStateTransitionFlowId = 3 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 4);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 54, TicketStateTransitionFlowId = 4 });

            }

            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 5);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 55, TicketStateTransitionFlowId = 5 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 6);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 56, TicketStateTransitionFlowId = 6 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 7);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 57, TicketStateTransitionFlowId = 7 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 8);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 58, TicketStateTransitionFlowId = 8 });

            }

            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 9);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 59, TicketStateTransitionFlowId = 9 });

            }

            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 10);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 60, TicketStateTransitionFlowId = 10 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 11);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 61, TicketStateTransitionFlowId = 11 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 12);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 62, TicketStateTransitionFlowId = 12 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 13);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 63, TicketStateTransitionFlowId = 13 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 14);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 64, TicketStateTransitionFlowId = 14 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 15);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 65, TicketStateTransitionFlowId = 15 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 16);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 66, TicketStateTransitionFlowId = 16 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 17);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 67, TicketStateTransitionFlowId = 17 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 18);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 68, TicketStateTransitionFlowId = 18 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 19);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 69, TicketStateTransitionFlowId = 19 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 20);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 70, TicketStateTransitionFlowId = 20 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 21);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 71, TicketStateTransitionFlowId = 21 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 22);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 72, TicketStateTransitionFlowId = 22 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 23);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 73, TicketStateTransitionFlowId = 23 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 24);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 74, TicketStateTransitionFlowId = 24 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 25);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 75, TicketStateTransitionFlowId = 25 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 1);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 76, TicketStateTransitionFlowId = 1 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 2);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 77, TicketStateTransitionFlowId = 2 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 3);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 78, TicketStateTransitionFlowId = 3 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 4);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 79, TicketStateTransitionFlowId = 4 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 5);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 80, TicketStateTransitionFlowId = 5 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 6);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 81, TicketStateTransitionFlowId = 6 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 7);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 82, TicketStateTransitionFlowId = 7 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 8);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 83, TicketStateTransitionFlowId = 8 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 9);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 84, TicketStateTransitionFlowId = 9 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 10);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 85, TicketStateTransitionFlowId = 10 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 11);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 86, TicketStateTransitionFlowId = 11 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 12);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 87, TicketStateTransitionFlowId = 12 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 13);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 88, TicketStateTransitionFlowId = 13 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 14);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 89, TicketStateTransitionFlowId = 14 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 15);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 90, TicketStateTransitionFlowId = 15 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 16);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 91, TicketStateTransitionFlowId = 16 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 17);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 92, TicketStateTransitionFlowId = 17 });

            }

            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 18);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 93, TicketStateTransitionFlowId = 18 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 19);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 94, TicketStateTransitionFlowId = 19 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 20);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 95, TicketStateTransitionFlowId = 20 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 21);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 96, TicketStateTransitionFlowId = 21 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 22);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 97, TicketStateTransitionFlowId = 22 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 23);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 98, TicketStateTransitionFlowId = 23 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 24);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 99, TicketStateTransitionFlowId = 24 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 25);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 100, TicketStateTransitionFlowId = 25 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 1);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 101, TicketStateTransitionFlowId = 1 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 2);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 102, TicketStateTransitionFlowId = 2 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 3);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 103, TicketStateTransitionFlowId = 3 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 4);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 104, TicketStateTransitionFlowId = 4 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 5);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 105, TicketStateTransitionFlowId = 5 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 6);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 106, TicketStateTransitionFlowId = 6 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 7);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 107, TicketStateTransitionFlowId = 7 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 8);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 108, TicketStateTransitionFlowId = 8 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 9);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 109, TicketStateTransitionFlowId = 9 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 10);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 110, TicketStateTransitionFlowId = 10 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 11);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 111, TicketStateTransitionFlowId = 11 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 12);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 112, TicketStateTransitionFlowId = 12 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 13);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 113, TicketStateTransitionFlowId = 13 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 14);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 114, TicketStateTransitionFlowId = 14 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 15);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 115, TicketStateTransitionFlowId = 15 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 16);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 116, TicketStateTransitionFlowId = 16 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 17);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 117, TicketStateTransitionFlowId = 17 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 18);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 118, TicketStateTransitionFlowId = 18 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 19);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 119, TicketStateTransitionFlowId = 19 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 20);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 120, TicketStateTransitionFlowId = 20 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 21);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 121, TicketStateTransitionFlowId = 21 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 22);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 122, TicketStateTransitionFlowId = 22 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 23);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 123, TicketStateTransitionFlowId = 23 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 24);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 124, TicketStateTransitionFlowId = 24 });

            }
            testRow = context.TicketStateFlowRoles.FirstOrDefault(b => b.TicketStateTransitionFlowId == 25);
            if (testRow == null)
            {
                context.TicketStateFlowRoles.Add(new TicketStateFlowRole { Id = 125, TicketStateTransitionFlowId = 25 });

            }
        }

        private static void InitializeTicketStateTransitionFlow(DiasFacilityManagementSqlServer context)
        {
            TicketStateTransitionFlow testRow = context.TicketStateTransitionFlows.FirstOrDefault(b => (b.TicketStateTransitionId == 1) && (b.TicketStateFlowId == 1));
            if (testRow == null)
            {
                context.TicketStateTransitionFlows.Add(new TicketStateTransitionFlow {Id = 1, TicketStateTransitionId = 1, TicketStateFlowId = 1 });
            }

            testRow = context.TicketStateTransitionFlows.FirstOrDefault(b => b.TicketStateFlowId == 2);
            if (testRow == null)
            {
                context.TicketStateTransitionFlows.Add(new TicketStateTransitionFlow { TicketStateTransitionId = 1, TicketStateFlowId = 2 });

            }

            testRow = context.TicketStateTransitionFlows.FirstOrDefault(b => b.TicketStateFlowId == 9);
            if (testRow == null)
            {
                context.TicketStateTransitionFlows.Add(new TicketStateTransitionFlow { TicketStateTransitionId = 2, TicketStateFlowId = 9 });

            }
            testRow = context.TicketStateTransitionFlows.FirstOrDefault(b => b.TicketStateFlowId == 1);
            if (testRow == null)
            {
                context.TicketStateTransitionFlows.Add(new TicketStateTransitionFlow { TicketStateTransitionId = 4, TicketStateFlowId = 1 });

            }
            testRow = context.TicketStateTransitionFlows.FirstOrDefault(b => b.TicketStateFlowId == 2);
            if (testRow == null)
            {
                context.TicketStateTransitionFlows.Add(new TicketStateTransitionFlow { TicketStateTransitionId = 4, TicketStateFlowId = 2 });

            }
            testRow = context.TicketStateTransitionFlows.FirstOrDefault(b => b.TicketStateFlowId == 3);
            if (testRow == null)
            {
                context.TicketStateTransitionFlows.Add(new TicketStateTransitionFlow { TicketStateTransitionId = 6, TicketStateFlowId = 3 });

            }
            testRow = context.TicketStateTransitionFlows.FirstOrDefault(b => b.TicketStateFlowId == 10);
            if (testRow == null)
            {
                context.TicketStateTransitionFlows.Add(new TicketStateTransitionFlow { TicketStateTransitionId = 7, TicketStateFlowId = 10 });

            }
            testRow = context.TicketStateTransitionFlows.FirstOrDefault(b => b.TicketStateFlowId == 5);
            if (testRow == null)
            {
                context.TicketStateTransitionFlows.Add(new TicketStateTransitionFlow { TicketStateTransitionId = 8, TicketStateFlowId = 5 });

            }
            testRow = context.TicketStateTransitionFlows.FirstOrDefault(b => b.TicketStateFlowId == 3);
            if (testRow == null)
            {
                context.TicketStateTransitionFlows.Add(new TicketStateTransitionFlow { TicketStateTransitionId = 9, TicketStateFlowId = 3 });

            }
            testRow = context.TicketStateTransitionFlows.FirstOrDefault(b => b.TicketStateFlowId == 6);
            if (testRow == null)
            {
                context.TicketStateTransitionFlows.Add(new TicketStateTransitionFlow { TicketStateTransitionId = 10, TicketStateFlowId = 6 });

            }
            testRow = context.TicketStateTransitionFlows.FirstOrDefault(b => b.TicketStateFlowId == 2);
            if (testRow == null)
            {
                context.TicketStateTransitionFlows.Add(new TicketStateTransitionFlow { TicketStateTransitionId = 1, TicketStateFlowId = 2 });

            }
            testRow = context.TicketStateTransitionFlows.FirstOrDefault(b => b.TicketStateFlowId == 3);
            if (testRow == null)
            {
                context.TicketStateTransitionFlows.Add(new TicketStateTransitionFlow { TicketStateTransitionId = 11, TicketStateFlowId = 3 });

            }
            testRow = context.TicketStateTransitionFlows.FirstOrDefault(b => b.TicketStateFlowId == 7);
            if (testRow == null)
            {
                context.TicketStateTransitionFlows.Add(new TicketStateTransitionFlow { TicketStateTransitionId = 12, TicketStateFlowId = 7 });

            }
            testRow = context.TicketStateTransitionFlows.FirstOrDefault(b => b.TicketStateFlowId == 8);
            if (testRow == null)
            {
                context.TicketStateTransitionFlows.Add(new TicketStateTransitionFlow { TicketStateTransitionId = 13, TicketStateFlowId = 8 });

            }
            testRow = context.TicketStateTransitionFlows.FirstOrDefault(b => b.TicketStateFlowId == 8);
            if (testRow == null)
            {
                context.TicketStateTransitionFlows.Add(new TicketStateTransitionFlow { TicketStateTransitionId = 14, TicketStateFlowId = 8 });

            }
            testRow = context.TicketStateTransitionFlows.FirstOrDefault(b => b.TicketStateFlowId == 8);
            if (testRow == null)
            {
                context.TicketStateTransitionFlows.Add(new TicketStateTransitionFlow { TicketStateTransitionId = 15, TicketStateFlowId = 8 });

            }
            testRow = context.TicketStateTransitionFlows.FirstOrDefault(b => b.TicketStateFlowId == 8);
            if (testRow == null)
            {
                context.TicketStateTransitionFlows.Add(new TicketStateTransitionFlow { TicketStateTransitionId = 16, TicketStateFlowId = 8 });

            }
            testRow = context.TicketStateTransitionFlows.FirstOrDefault(b => b.TicketStateFlowId == 8);
            if (testRow == null)
            {
                context.TicketStateTransitionFlows.Add(new TicketStateTransitionFlow { TicketStateTransitionId = 17, TicketStateFlowId = 8 });

            }
            testRow = context.TicketStateTransitionFlows.FirstOrDefault(b => b.TicketStateFlowId == 1);
            if (testRow == null)
            {
                context.TicketStateTransitionFlows.Add(new TicketStateTransitionFlow { TicketStateTransitionId = 18, TicketStateFlowId = 1 });

            }
            testRow = context.TicketStateTransitionFlows.FirstOrDefault(b => b.TicketStateFlowId == 2);
            if (testRow == null)
            {
                context.TicketStateTransitionFlows.Add(new TicketStateTransitionFlow { TicketStateTransitionId = 18, TicketStateFlowId = 2 });

            }
            testRow = context.TicketStateTransitionFlows.FirstOrDefault(b => b.TicketStateFlowId == 5);
            if (testRow == null)
            {
                context.TicketStateTransitionFlows.Add(new TicketStateTransitionFlow { TicketStateTransitionId = 19, TicketStateFlowId = 5 });

            }
            testRow = context.TicketStateTransitionFlows.FirstOrDefault(b => b.TicketStateFlowId == 1);
            if (testRow == null)
            {
                context.TicketStateTransitionFlows.Add(new TicketStateTransitionFlow { TicketStateTransitionId = 20, TicketStateFlowId = 1 });

            }
            testRow = context.TicketStateTransitionFlows.FirstOrDefault(b => b.TicketStateFlowId == 2);
            if (testRow == null)
            {
                context.TicketStateTransitionFlows.Add(new TicketStateTransitionFlow { TicketStateTransitionId = 20, TicketStateFlowId = 2 });

            }
            testRow = context.TicketStateTransitionFlows.FirstOrDefault(b => b.TicketStateFlowId == 2);
            if (testRow == null)
            {
                context.TicketStateTransitionFlows.Add(new TicketStateTransitionFlow { TicketStateTransitionId = 1, TicketStateFlowId = 6 });

            }
            testRow = context.TicketStateTransitionFlows.FirstOrDefault(b => b.TicketStateFlowId == 6);
            if (testRow == null)
            {
                context.TicketStateTransitionFlows.Add(new TicketStateTransitionFlow { TicketStateTransitionId = 21, TicketStateFlowId = 2 });

            }

        }
    }
}
