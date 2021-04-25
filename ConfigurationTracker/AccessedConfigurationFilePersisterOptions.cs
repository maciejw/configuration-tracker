using Microsoft.Extensions.Options;

namespace ConfigurationTracker
{
  internal class AccessedConfigurationFilePersisterOptions
  {
    public class Configuration : IConfigureOptions<AccessedConfigurationFilePersisterOptions>
    {
      public void Configure(AccessedConfigurationFilePersisterOptions options)
      {
        options.FilePath = "accessed-configuration-paths.log";
      }
    }
    public string FilePath { get; internal set; }
  }
}
