namespace Sample.Api.Models
{
    public class GetListRequest
    {
        public virtual int Offset { get; set; } = 0;

        public virtual int Count { get; set; } = 20;
    }
}
