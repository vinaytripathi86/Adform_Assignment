using System;

namespace Homework_Adform.CommonLibrary.Models.DTOs
{
    public class CreateItemDto
    {
        public string Notes { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public long ListId { get; set; }
        public long? LabelId { get; set; }
    }

    public class UpdateItemDto : CreateItemDto
    {
        public long Id { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    }

    public class DeleteItemDto
    {
        public long ListId { get; set; }
        public long Id { get; set; }
        public long CreatedBy { get; set; }
    }

    public class GetItemsDto
    {
        public long ListId { get; set; }
        public long CreatedBy { get; set; }
    }

    public class GetItemDto : GetItemsDto
    {
        public long Id { get; set; }
    }

    public class ItemDto
    {
        public long Id { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long ListId { get; set; }
        public long? LabelId { get; set; }
    }
}
