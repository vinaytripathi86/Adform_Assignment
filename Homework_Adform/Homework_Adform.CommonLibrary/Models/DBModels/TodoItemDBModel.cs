using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework_Adform.CommonLibrary.Models.DBModels
{
    public class TodoItemDBModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Notes { get; set; }
        public long CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual UserDbModel User { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long ListId { get; set; }
        [ForeignKey("ListId")]
        public virtual TodoListDbModel List { get; set; }
        public long? LabelId { get; set; }
        [ForeignKey("LabelId")]
        public virtual LabelDbModel Label { get; set; }
    }
}
