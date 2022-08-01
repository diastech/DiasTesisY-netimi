using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using error = DiasShared.Errors;

namespace DiasBusinessLogic.Shared.Error
{
    public static class Errors
    {

        public static class General
        {

            #region Success
            public static error.Error SuccessGetById(string entityName)
            {
                return new error.Error($"success", $"{entityName} has that Id.", "Kayıt Başarılı şekilde getirildi.", true);
            }
            public static error.Error SuccessUpdate(string entityName)
            {
                return new error.Error($"success", $"{entityName} is updated successfully.", "Başarılı Şekilde Güncelleme Yapıldı.", true);
            }
            public static error.Error SuccessInsert(string entityName)
            {
                return new error.Error($"success", $"this {entityName} is inserted successfully.", "Başarılı bir şekilde kaydedildi.", true);
            }
            public static error.Error GetListSuccess(string entityName)
            {
                return new error.Error($"success", $"{entityName} grid is loaded successfully.", "Başarılı bir şekilde listelendi.", true);
            }
            public static error.Error SuccessDelete(string entityName)
            {
                return new error.Error($"success", $"this {entityName} grid is deleted successfully.", "Başarılı Şekilde Kayıt Silindi.", true);
            }
            public static error.Error Success(string entityName)
            {
                return new error.Error($"success", $"this {entityName} operation executed successfully.", "Başarılı Şekilde Operasyon Gerçekleştirildi.", true);
            }

            /// <summary>
            /// Kesinlikle canlı ortamda kullanılmayacak
            /// Lokalde patlamayan ama sunucuda patlayan hatalar için kullanılacak
            /// </summary>
            /// <param name="entityName"></param>
            /// <param name="exceptionMessage">Exception InnerMessage olmalıdır</param>
            /// <returns></returns>
            public static error.Error ErrorExceptionError(string entityName, string exceptionMessage)
            {
                return new error.Error($"error", exceptionMessage + $"  {entityName}", "hata oluştu.", false);
            }

            #endregion

            #region Error
            public static error.Error ErrorUpdate(string entityName)
            {
                return new error.Error($"error", $"an error occur when updating {entityName}.", "Güncellerken hata oluştu.", false);
            }
            public static error.Error ErrorInsert(string entityName)
            {
                return new error.Error($"error", $"an error occur when inserting {entityName}.", "Eklerken hata oluştu", false);
            }
            public static error.Error GridListError(string entityName)
            {
                return new error.Error($"error", $"an error occur when grid loading {entityName}.", "Grid yüklenirken hata oluştu", false);
            }
            public static error.Error ErrorDelete(string entityName)
            {
                return new error.Error($"error", $"an error occur when deleting {entityName}.", "Silinirken hata oluştu.", false);
            }
            public static error.Error MappingError(string entityName)
            {
                return new error.Error($"mapping.error", $"an error occur when mapping {entityName}.", "İstek Yok.", false);
            }
            public static error.Error ErrorGetById(string entityName)
            {
                return new error.Error($"error", $"{entityName} has not that Id", "Kayıt Bulunamadı.", false);
            }

            public static error.Error ErrorGetList(string entityName)
            {
                return new error.Error($"error", $"an error occur when loading list {entityName}.", "Liste yüklenirken hata oluştu", false);
            }
            #endregion

            #region Other
            public static error.Error NotFoundDatabaseServer()
            {
                return new error.Error($"database.not.found", $"database is not found.", "Veritabanı bulunamadı.", false);
            }

            public static error.Error GeneralServerError()
            {
                return new error.Error($"general.server.error", $"general server error.", "Genel Sunucu Hatası.", false);
            }
            public static error.Error RequestNull(string entityName)
            {
                return new error.Error($"request.null", $"{entityName} request is not found.", "İstek Yok.", false);
            }
            public static error.Error ConnectionTimeout()
            {
                return new error.Error($"connection.timeout", $"Database timeout error", "Timeout Hatası", false);
            }
            public static error.Error ArgumentNullException()
            {
                return new error.Error($"argumentnullexception", $"parameter dont allow null.", "Parametre hatası.", false);
            }
            public static error.Error None()
            {
                return null;
            }
            public static error.Error ModelisInValid(string entityName)
            {
                return new error.Error($"model.is.invalid", $"this {entityName} is invalid.", "Model Invalid", false);
            }
            #endregion




        }

    }
}
