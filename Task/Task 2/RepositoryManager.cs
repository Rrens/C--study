using System;
using System.Collections.Generic;

namespace FormulatrixRepository
{
  public class RepositoryManager<T>
  {
    private readonly IRepository<T> _repository;
    private readonly IValidator<T> _jsonValidator;
    private readonly IValidator<T> _xmlValidator;
    private bool _isInitialized = false;

    public RepositoryManager(IRepository<T> repository, IValidator<T> jsonValidator, IValidator<T> xmlValidator)
    {
      _repository = repository;
      _jsonValidator = jsonValidator;
      _xmlValidator = xmlValidator;
    }

    public void Initialize()
    {
      if (_isInitialized)
      {
        throw new InvalidOperationException("Repository has already been initialized.");
      }
      _isInitialized = true;
    }

    private void EnsureInitialized()
    {
      if (!_isInitialized)
      {
        throw new InvalidOperationException("Repository must be initialized before use.");
      }
    }

    public void Register(string itemName, T itemContent, int itemType)
    {
      EnsureInitialized();

      if (string.IsNullOrEmpty(itemName))
        throw new ArgumentException("Item name cannot be null or empty.");

      bool isValid = itemType switch
      {
        1 => _jsonValidator.Validate(itemContent),
        2 => _xmlValidator.Validate(itemContent),
        _ => throw new ArgumentException("Invalid item type. Only 1 (JSON) or 2 (XML) are supported.")
      };

      if (!isValid)
      {
        throw new ArgumentException("Item content validation failed.");
      }

      bool added = _repository.TryAdd(itemName, itemContent, itemType);
      if (!added)
      {
        throw new InvalidOperationException($"Item with name '{itemName}' already exists. Overwrite protected.");
      }
    }

    public T Retrieve(string itemName)
    {
      EnsureInitialized();

      if (_repository.TryGetValue(itemName, out var item))
      {
        return item.Content;
      }
      throw new KeyNotFoundException($"Item '{itemName}' not found.");
    }

    public int GetType(string itemName)
    {
      EnsureInitialized();

      if (_repository.TryGetValue(itemName, out var item))
      {
        return item.Type;
      }
      throw new KeyNotFoundException($"Item '{itemName}' not found.");
    }

    public void Deregister(string itemName)
    {
      EnsureInitialized();

      if (!_repository.TryRemove(itemName))
      {
        throw new KeyNotFoundException($"Item '{itemName}' not found to deregister.");
      }
    }
  }
}