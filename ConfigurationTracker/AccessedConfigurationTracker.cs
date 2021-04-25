using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationTracker
{
  internal class AccessedConfigurationTracker : IAsyncDisposable
  {
    private readonly Dictionary<string, string> accessedPaths;
    private readonly IAccessedConfigurationPersister persister;

    public AccessedConfigurationTracker(IAccessedConfigurationPersister persister)
    {
      accessedPaths = new Dictionary<string, string>();
      this.persister = persister;
    }
    public async ValueTask DisposeAsync()
    {
      await persister.Persist(accessedPaths);
    }

    internal void Track(string path, string value)
    {
      lock (accessedPaths)
      {
        accessedPaths[path] = value;
      }
    }
  }
}
