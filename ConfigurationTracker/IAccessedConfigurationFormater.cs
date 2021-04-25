namespace ConfigurationTracker
{
  public interface IAccessedConfigurationFormater
  {
    string Format(string key, string value);
  }
}
