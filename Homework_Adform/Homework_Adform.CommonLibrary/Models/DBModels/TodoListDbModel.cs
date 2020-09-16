using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework_Adform.CommonLibrary.Models.DBModels
{
    public class TodoListDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Description { get; set; }        
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<TodoItemDBModel> Items { get; set; }
        public long? LabelId { get; set; }
        [ForeignKey("LabelId")]
        public virtual LabelDbModel Label { get; set; }
        public long CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual UserDbModel User { get; set; }
    }
}
