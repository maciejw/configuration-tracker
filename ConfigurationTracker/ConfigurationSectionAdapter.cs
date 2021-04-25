using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace ConfigurationTracker
{
  internal class ConfigurationSectionAdapter : IConfigurationSection
  {
    private IConfigurationSection configurationSection;
    private readonly AccessedConfigurationTracker tracker;

    public ConfigurationSectionAdapter(IConfigurationSection configurationSection, AccessedConfigurationTracker tracker)
    {
      this.configurationSection = configurationSection;
      this.tracker = tracker;
    }

    public string this[string key]
    {
      get { return configurationSection[key]; }
      set { configurationSection[key] = value; }
    }

    public string Key { get { return configurationSection.Key; } }
    public string Path { get { return configurationSection.Path; } }
    public string Value
    {
      get
      {
        tracker.Track(this.Path, configurationSection.Value);
        return configurationSection.Value;
      }
      set { configurationSection.Value = value; }
    }

    public IEnumerable<IConfigurationSection> GetChildren()
    {
      return configurationSection.GetChildren().Select(section => new ConfigurationSectionAdapter(section, tracker));
    }

    public IChangeToken GetReloadToken()
    {
      return configurationSection.GetReloadToken();
    }

    public IConfigurationSection GetSection(string key)
    {
      return new ConfigurationSectionAdapter(configurationSection.GetSection(key), tracker);
    }
  }
}
