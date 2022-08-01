using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DiasShared.Enums.Standart
{
    public class FileOperationEnums
    {
        public enum FileExtensionType
        {
            [Description(".pdf")]
            [Display(Name = "application/pdf")]
            pdf,

            [Description(".tif")]
            [Display(Name = "image/tiff")]
            tif,

            [Description(".tiff")]
            [Display(Name = "image/tiff")]
            tiff,

            [Description(".jpeg")]
            [Display(Name = "image/jpeg")]
            jpeg,

            [Description(".jpg")]
            [Display(Name = "image/jpeg")]
            jpg,

            [Description(".gif")]
            [Display(Name = "image/gif")]
            GIF,

            [Description(".png")]
            [Display(Name = "image/png")]
            png,

            [Description(".bmp")]
            [Display(Name = "image/bmp")]
            bmp,

            [Description(".jfif")]
            [Display(Name = "image/pjpeg")]
            jfif,

            [Description(".doc")]
            [Display(Name = "application/msword")]
            doc,

            [Description(".docx")]
            [Display(Name = "application/vnd.openxmlformats-officedocument.wordprocessingml.document")]
            docx,

            [Description(".rtf")]
            [Display(Name = "application/rtf")]
            rtf,

            [Description(".rtx")]
            [Display(Name = "text/richtext")]
            rtx,

            [Description(".txt")]
            [Display(Name = "text/plain")]
            txt,

            [Description(".xls")]
            [Display(Name = "application/vnd.ms-excel")]
            xls,

            [Description(".xlsx")]
            [Display(Name = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
            xlsx,

            [Description(".ppt")]
            [Display(Name = "application/vnd.ms-powerpoint")]
            ppt,

            [Description(".pptx")]
            [Display(Name = "application/vnd.openxmlformats-officedocument.presentationml.presentation")]
            pptx
        }       
    }
}
