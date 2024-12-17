using Microsoft.EntityFrameworkCore;


namespace Parser.Data
{
    public class ParserContext: DbContext
    {
        public DbSet<AddedData> addedDatas { get; set; }
        public DbSet<PriceLog> priceLogs { get; set; }

        public ParserContext(DbContextOptions<ParserContext> options): base (options) 
        { 
        }

    }
}
