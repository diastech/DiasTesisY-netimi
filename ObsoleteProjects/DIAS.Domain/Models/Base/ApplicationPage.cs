using DIAS.Domain;
using DIAS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DIAS.Domain.Models.Base
{    public class ApplicationPage
    {
        public string Id { get; set; }
        public int Order { get; set; }
        public int Level { get; set; }
        public string ParentId { get; set; }
        public string Text { get; set; }
        public string Path { get; set; }
        public string Icon { get; set; }
        public bool Expanded { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
    }    
}
