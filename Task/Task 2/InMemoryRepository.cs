using System.Collections.Concurrent;

namespace FormulatrixRepository
{
  public class InMemoryRepository<T> : IRepository<T>
  {
    private readonly ConcurrentDictionary<string, (T Content, int Type)> _storage = new();

    public bool TryAdd(string name, T content, int type)
    {
      return _storage.TryAdd(name, (content, type));
    }

    public bool TryGetValue(string name, out (T Content, int Type) item)
    {
      return _storage.TryGetValue(name, out item);
    }

    public bool TryRemove(string name)
    {
      return _storage.TryRemove(name, out _);
    }
  }
}