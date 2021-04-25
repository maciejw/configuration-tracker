namespace ConfigurationTracker
{
  internal class AccessedConfigurationYamlFormater : IAccessedConfigurationFormater
  {
    private static string FormatValue(string value)
    {
      return $"'{value.Replace("'", "''")}'";
    }

    private static string FormatKey(string key)
    {
      return key.Replace(":", "_");
    }

    public string Format(string key, string value)
    {
      return $"{FormatKey(key)}: {FormatValue(value)}";
    }
  }
}
