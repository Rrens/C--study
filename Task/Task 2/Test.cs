using NUnit.Framework;
using System;
using System.Collections.Generic;
using FormulatrixRepository;

namespace FormulatrixTests
{
  [TestFixture]
  public class RepositoryManagerTests
  {
    private RepositoryManager<string> _manager = null!; // changed

    [SetUp]
    public void Setup()
    {
      _manager = new RepositoryManager<string>(
        new InMemoryRepository<string>(),
        new JsonValidator(),
        new XmlValidator()
      );
      _manager.Initialize();
    }

    [Test]
    public void Initialize_CalledTwice_ShouldThrow()
    {
      Assert.Throws<InvalidOperationException>(() => _manager.Initialize());
    }

    [Test]
    public void Register_BeforeInitialize_ShouldThrow()
    {
      var uninit = new RepositoryManager<string>(
        new InMemoryRepository<string>(), new JsonValidator(), new XmlValidator());

      Assert.Throws<InvalidOperationException>(() => uninit.Register("a", "{}", 1));
    }

    [Test]
    public void Register_EmptyName_ShouldThrow()
    {
      Assert.Throws<ArgumentException>(() => _manager.Register("", "{}", 1));
    }

    [Test]
    public void Register_InvalidContent_ShouldThrow()
    {
      Assert.Throws<ArgumentException>(() => _manager.Register("a", "invalid", 1));
    }

    [Test]
    public void Register_DuplicateName_ShouldThrow()
    {
      _manager.Register("a", "{}", 1);

      Assert.Throws<InvalidOperationException>(() => _manager.Register("a", "{}", 1));
    }

    [Test]
    public void Retrieve_ShouldReturnRegisteredContent()
    {
      _manager.Register("a", "{}", 1);

      Assert.AreEqual("{}", _manager.Retrieve("a"));
    }

    [Test]
    public void Retrieve_ItemNotFound_ShouldThrow()
    {
      Assert.Throws<KeyNotFoundException>(() => _manager.Retrieve("a"));
    }

    [Test]
    public void GetType_ShouldReturnCorrectType()
    {
      _manager.Register("json", "{}", 1);
      _manager.Register("xml", "<r/>", 2);

      Assert.AreEqual(1, _manager.GetType("json"));
      Assert.AreEqual(2, _manager.GetType("xml"));
    }

    [Test]
    public void Deregister_ShouldRemoveItem()
    {
      _manager.Register("a", "<r/>", 2);
      _manager.Deregister("a");

      Assert.Throws<KeyNotFoundException>(() => _manager.Retrieve("a"));
    }
  }
}
