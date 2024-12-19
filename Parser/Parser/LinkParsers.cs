using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Parser.Data; 

namespace LinkParsers
{
    public static class LinkParser
    {
        //public static async Task LinkDomotex(ParserContext context, string name)
        //{
        //    string link = "";
        //    var data = context.addedDatas.FirstOrDefault(x => x.Name == name);
        //    if (data != null)
        //    {
        //        link = data.LinkCersanit;
        //    }

        //    var httpClient = new HttpClient();
        //    var LinkDomotex = await httpClient.GetAsync($"{link}");
        //    var LinkDomotexBody = await LinkDomotex.Content.ReadAsStringAsync();

        //    Regex regex = new Regex(@"'VALUE_VAT':'(\d+)'");

        //    var x = regex.Match(LinkDomotexBody);

        //    double price = Convert.ToDouble(x.Groups[1].Value);
        //}

        //public static async Task LinkVodoparad(ParserContext context, string name)
        //{
        //    string link = "";
        //    var data = context.addedDatas.FirstOrDefault(x => x.Name == name);
        //    if (data != null)
        //    {
        //        link = data.LinkVodoparad;
        //    }

        //    var httpClient = new HttpClient();
        //    var LinkVodoparad = await httpClient.GetAsync($"{link}");
        //    var LinkVodoparadBody = await LinkVodoparad.Content.ReadAsStringAsync();

        //    Regex regex = new Regex(@"data-price=""(\d+)""");

        //    var x = regex.Match(LinkVodoparadBody);

        //    double price = Convert.ToDouble(x.Groups[1].Value);
        //}

        public static async Task<double?> LinkDomotex(ParserContext context, string name)
        {
            var data = context.addedDatas.FirstOrDefault(x => x.Name == name);
            if (data == null || string.IsNullOrEmpty(data.LinkCersanit))
                return null;

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(data.LinkCersanit);
            var responseBody = await response.Content.ReadAsStringAsync();

            Regex regex = new Regex(@"'VALUE_VAT':'(\d+)'");
            var match = regex.Match(responseBody);

            if (match.Success && double.TryParse(match.Groups[1].Value, out double price))
            {
                return price;
            }

            return null;
        }

        public static async Task<double?> LinkVodoparad(ParserContext context, string name)
        {
            var data = context.addedDatas.FirstOrDefault(x => x.Name == name);
            if (data == null || string.IsNullOrEmpty(data.LinkVodoparad))
                return null;

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(data.LinkVodoparad);
            var responseBody = await response.Content.ReadAsStringAsync();

            Regex regex = new Regex(@"data-price=""(\d+)""");
            var match = regex.Match(responseBody);

            if (match.Success && double.TryParse(match.Groups[1].Value, out double price))
            {
                return price;
            }

            return null;
        }


    }
}


