using Neo4j.Driver;
using Neo4jObjectMapper;

namespace Sample.Infrastructure.Persistence
{
    public class SampleNeoContext : NeoContext, INeoContext
    {
        public IDriver Driver { get; set; }
        public SampleNeoContext(IDriver driver) : base(driver)
        {
            this.Driver = driver;
        }
    }
}
