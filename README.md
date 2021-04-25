# Accessed configuration tracker

Tracks path from ```IConfiguration``` accessed by application in runtime.
On Shutdown persists those accessed paths and its values to a file.

## Setup

```Startup.ConfigureServices```

```csharp
      if (environment.IsDevelopment())
      {
        services.TrackAccessedConfiguration();
      }
      services.ConfigureOptions<Section1.Configuration>();
```

Options that access ```IConfiguration```

```csharp
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

```

appsettings.json

```json
{
  "section1": {
    "property1": "value1",
    "property2": "value2",
    "property3": [{ "someData": 1 }, { "someData": 2 }],
    "property4": "value4"
  }
}
```

## Output

accessed-configuration-paths.log

```yml
Section1_Property1: 'value1'
Section1_Property2: 'value2'
Section1_Property3_0_SomeData: '1'
Section1_Property3_1_SomeData: '2'
```
