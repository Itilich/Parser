namespace Parser.Models
{
    public class ResultsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DateTime { get; set; }
        public string LinkDomotex { get; set; }
        public string LinkVodoparad { get; set; }
        public double? PriceDomotex { get; set; }
        public double? PriceVodoparad { get; set; }
    }
}
