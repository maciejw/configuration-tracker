using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigurationTracker
{
  public interface IAccessedConfigurationPersister
  {
    Task Persist(IReadOnlyDictionary<string, string> accessedPaths);
  }
}
