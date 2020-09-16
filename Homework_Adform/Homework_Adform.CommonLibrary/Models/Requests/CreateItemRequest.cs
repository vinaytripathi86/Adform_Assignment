namespace Homework_Adform.CommonLibrary.Models.Requests
{
    public class CreateItemRequest
    {
        public long ListId { get; set; }
        public string Note { get; set; }
        public long? LabelId { get; set; }
    }
}
