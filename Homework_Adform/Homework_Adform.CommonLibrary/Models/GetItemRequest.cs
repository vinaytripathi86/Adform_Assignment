namespace Homework_Adform.CommonLibrary.Models
{
    public class GetItemRequest 
    {
        public long ListId { get; set; }
        public long Id { get; set; }        
    }

    public class GetItemsRequest
    {
        public long ListId { get; set; }
        public PaginationParameters Pagination { get; set; }
    }
}
