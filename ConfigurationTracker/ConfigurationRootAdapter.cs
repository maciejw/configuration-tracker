using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace ConfigurationTracker
{
  internal class ConfigurationRootAdapter : IConfigurationRoot
  {
    private IConfigurationRoot configurationRoot;
    private readonly AccessedConfigurationTracker tracker;

    internal ConfigurationRootAdapter(IConfigurationRoot configurationRoot, AccessedConfigurationTracker tracker)
    {
      this.configurationRoot = configurationRoot;
      this.tracker = tracker;
    }

    public string this[string key]
    {
      get { return configurationRoot[key]; }
      set { configurationRoot[key] = value; }
    }

    public IEnumerable<IConfigurationProvider> Providers => configurationRoot.Providers;

    public IEnumerable<IConfigurationSection> GetChildren()
    {
      return configurationRoot.GetChildren().Select(section => new ConfigurationSectionAdapter(section, tracker));
    }

    public IChangeToken GetReloadToken()
    {
      return configurationRoot.GetReloadToken();
    }

    public IConfigurationSection GetSection(string key)
    {
      return new ConfigurationSectionAdapter(configurationRoot.GetSection(key), tracker);
    }

    public void Reload()
    {
      configurationRoot.Reload();
    }
  }
}
