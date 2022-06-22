namespace Sample.Domain.Currency.GraphModel
{
    public class CurrencyDestinationRoute
    {
        public int index { get; set; }
        public string sourceNodeName { get; set; }
        public string targetNodeName { get; set; }
        public float totalCost { get; set; }
        public string[] nodeNames { get; set; }
        public float[] costs { get; set; }
        public CurrencyGraphModel[] path { get; set; }
    }
}
