using System;

namespace Homework_Adform.CommonLibrary.Models.Responses
{
    public class ItemResponse
    {
        public long Id { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long ListId { get; set; }
        public long? LabelId { get; set; }
    }
}
