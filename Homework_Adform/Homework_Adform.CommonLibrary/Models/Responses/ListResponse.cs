using System;
using System.Collections.Generic;

namespace Homework_Adform.CommonLibrary.Models.Responses
{
    public class ListResponse
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public long? LabelId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<ItemResponse> Items { get; set; }
    }
}
