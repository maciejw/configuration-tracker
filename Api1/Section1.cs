using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Api1
{
  public class Section1
  {
    public class Configuration : IConfigureOptions<Section1>
    {
      private readonly IConfiguration configuration;

      public Configuration(IConfiguration configuration)
      {
        this.configuration = configuration;
      }

      public void Configure(Section1 options)
      {
        configuration.GetSection(nameof(Section1)).Bind(options);
      }
    }
    public class SomeDataSection
    {
      public int SomeData { get; set; }
    }

    public string Property1 { get; set; }
    public string Property2 { get; set; }
    public SomeDataSection[] Property3 { get; set; }
  }
}
