using System;
using System.Collections.Generic;

namespace Homework_Adform.CommonLibrary.Models.DTOs
{
    public class CreateListDto
    {
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public long? LabelId { get; set; }
        public long CreatedBy { get; set; }
    }

    public class UpdateListDto : CreateListDto
    {
        public long Id { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    }

    public class DeleteListDto
    {
        public long Id { get; set; }
        public long CreatedBy { get; set; }
    }

    public class GetListsDto
    {
        public long CreatedBy { get; set; }
    }

    public class GetListDto : GetListsDto
    {
        public long Id { get; set; }
    }

    public class ListDto
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public long? LabelId { get; set; }        
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<ItemDto> Items { get; set; }
    }
}
