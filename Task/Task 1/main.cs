using System.Text;
public class FizzBuzz
{
  private readonly Dictionary<int, string> _rules = new Dictionary<int, string>();
  public void AddRule(int input, string output)
  {
    if (_rules.ContainsKey(input))
      _rules[input] = output;
    else
      _rules.Add(input, output);
  }
  public string ProcessNumber(int x)
  {
    if (_rules.ContainsKey(x))
    {
      return _rules[x];
    }
    StringBuilder result = new StringBuilder();
    foreach (var rule in _rules)
    {
      if (x % rule.Key == 0)
      {
        result.Append(rule.Value);
      }
    }

    return result.Length > 0 ? result.ToString() : x.ToString();
  }
  public void PrintRange(int n)
  {
    List<string> outputs = new List<string>();
    for (int i = 1; i <= n; i++)
    {
      outputs.Add(ProcessNumber(i));
    }
    Console.WriteLine(string.Join(", ", outputs));
  }
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("-----------------------------------------");
      Console.WriteLine("Task 1:");
      FizzBuzz Task_1 = new FizzBuzz();
      Task_1.AddRule(3, "foo");
      Task_1.AddRule(5, "bar");
      Task_1.AddRule(7, "jazz");
      Task_1.PrintRange(15);
      Console.WriteLine();
      Console.WriteLine("\n-----------------------------------------\n");
      Console.WriteLine("Task 2:");
      FizzBuzz Task_2 = new FizzBuzz();
      Task_2.AddRule(3, "foo");
      Task_2.AddRule(5, "bar");
      Task_2.AddRule(7, "jazz");
      Task_2.PrintRange(15);
      Console.WriteLine();
      Console.WriteLine($"X = 21: {Task_2.ProcessNumber(21)}");
      Console.WriteLine($"X = 35: {Task_2.ProcessNumber(35)}");
      Console.WriteLine($"X = 105: {Task_2.ProcessNumber(105)}");
      Console.WriteLine("\n-----------------------------------------\n");
      Console.WriteLine("Task 3:");
      FizzBuzz Task_3 = new FizzBuzz();
      Task_3.AddRule(3, "foo");
      Task_3.AddRule(4, "baz");
      Task_3.AddRule(5, "bar");
      Task_3.AddRule(7, "jazz");
      Task_3.AddRule(9, "huzz");
      Task_3.PrintRange(40);
    }
  }
}