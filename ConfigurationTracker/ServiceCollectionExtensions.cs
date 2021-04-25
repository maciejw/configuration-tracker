using ConfigurationTracker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class ServiceCollectionExtensions
  {
    public static void TrackAccessedConfiguration(this IServiceCollection @this)
    {
      @this.ConfigureOptions<AccessedConfigurationFilePersisterOptions.Configuration>();
      @this.AddSingleton<AccessedConfigurationTracker>();
      @this.TryAddSingleton<IAccessedConfigurationFormater, AccessedConfigurationYamlFormater>();
      @this.TryAddSingleton<IAccessedConfigurationPersister, AccessedConfigurationFilePersister>();

      @this.Decorate<IConfiguration>((originalConfiguration, serviceProvider) =>
      {
        var tracker = serviceProvider.GetRequiredService<AccessedConfigurationTracker>();

        if (originalConfiguration is IConfigurationRoot originalConfigurationRoot)
        {
          return new ConfigurationRootAdapter(originalConfigurationRoot, tracker);
        }
        return originalConfiguration;
      });
    }
  }
}
