namespace Homework_Adform.CommonLibrary.Models
{
    public class APIResponse<T>
    {
        public bool IsSucess { get; set; }
        public T Result { get; set; }
        public string Message { get; set; }
    }
}
