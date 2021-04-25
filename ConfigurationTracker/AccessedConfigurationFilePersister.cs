using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace ConfigurationTracker
{
  internal class AccessedConfigurationFilePersister : IAccessedConfigurationPersister
  {
    private readonly IAccessedConfigurationFormater formater;
    private readonly IOptions<AccessedConfigurationFilePersisterOptions> options;

    public AccessedConfigurationFilePersister(IAccessedConfigurationFormater formater, IOptions<AccessedConfigurationFilePersisterOptions> options)
    {
      this.formater = formater;
      this.options = options;
    }

    public async Task Persist(IReadOnlyDictionary<string, string> accessedPaths)
    {
      using (FileStream fileStream = File.Open(options.Value.FilePath, FileMode.Create, FileAccess.Write))
      {
        using (var streamWriter = new StreamWriter(fileStream))
        {
          foreach (var line in accessedPaths
            .Where(e => !string.IsNullOrEmpty(e.Value))
            .Select(e => formater.Format(e.Key, e.Value)))
          {
            await streamWriter.WriteLineAsync(line);
          }
        }
      }
    }
  }
}
