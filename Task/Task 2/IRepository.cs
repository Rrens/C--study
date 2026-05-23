using System.Collections.Generic;

namespace FormulatrixRepository
{
  public interface IRepository<T>
  {
    bool TryAdd(string name, T content, int type);
    bool TryGetValue(string name, out (T Content, int Type) item);
    bool TryRemove(string name);
  }
}