using System;

namespace Homework_Adform.CommonLibrary.Models.DTOs
{
    public class CreateLabelDTO
    {
        public string Description { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }

    public class AssignLabelDto
    {
        public long Id { get; set; }
        public long EntityId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdateDateTime { get; set; } = DateTime.UtcNow;
    }

    public class DeleteLabelDto
    {
        public long Id { get; set; }
        public long CreatedBy { get; set; }
    }

    public class LabelDto
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
