using System.ComponentModel.DataAnnotations;

namespace Parser.Data
{
    public class AddedData
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string LinkDomotex { get; set; }

        public string LinkVodoparad { get; set; }

    }
}
