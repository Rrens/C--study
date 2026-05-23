namespace FormulatrixRepository
{
  public interface IValidator<T>
  {
    bool Validate(T content);
  }

  public class JsonValidator : IValidator<string>
  {
    private const int MaxLength = 1_000_000;
    private const int MaxDepth = 64;
    public bool Validate(string content)
    {
      if (string.IsNullOrWhiteSpace(content)) return false;

      if (content.Length > MaxLength) return false;

      try
      {
        var options = new System.Text.Json.JsonDocumentOptions
        {
          MaxDepth = MaxDepth,
          AllowTrailingCommas = false,
        };

        using var doc = System.Text.Json.JsonDocument.Parse(content, options);
        var kind = doc.RootElement.ValueKind;
        return kind == System.Text.Json.JsonValueKind.Object || kind == System.Text.Json.JsonValueKind.Array;
      }
      catch
      {
        return false;
      }
    }
  }

  public class XmlValidator : IValidator<string>
  {
    private const int MaxLength = 1_000_000;

    public bool Validate(string content)
    {
      if (string.IsNullOrWhiteSpace(content)) return false;

      if (content.Length > MaxLength) return false;

      try
      {
        var settings = new System.Xml.XmlReaderSettings
        {
          DtdProcessing = System.Xml.DtdProcessing.Prohibit,
          XmlResolver = null,
          MaxCharactersInDocument = MaxLength,
          MaxCharactersFromEntities = 1024
        };

        using var reader = System.Xml.XmlReader.Create(
          new System.IO.StringReader(content),
          settings
        );

        var doc = System.Xml.Linq.XDocument.Load(reader);
        return doc.Root != null;
      }
      catch
      {
        return false;
      }
    }
  }
}