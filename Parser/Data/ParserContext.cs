using Microsoft.EntityFrameworkCore;

namespace Parser.Data
{
    public class ParserContext: DbContext
    {

        public ParserContext(DbContextOptions<ParserContext> options): base (options) 
        { 
        }

    }
}
