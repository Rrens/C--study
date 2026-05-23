# Test Scenarios for RepositoryManager\<T\>

Before using any method, `Initialize()` has to be called first. The class has 5 public methods that need to be tested: `Initialize`, `Register`, `Retrieve`, `GetType`, and `Deregister`.

---

## Initialize()

- Calling it once should work fine.
- Calling it a second time should throw `InvalidOperationException` because the repository is already initialized.
- Calling any other method before `Initialize()` should also throw `InvalidOperationException`.

---

## Register(name, content, type)

The method accepts a name, a content string, and a type (1 for JSON, 2 for XML).

- Registering with a valid JSON string and type 1 should succeed.
- Registering with a valid XML string and type 2 should succeed.
- If the name is empty or null, it should throw `ArgumentException`.
- If the content doesn't match the expected format (e.g. plain text passed as JSON), it should throw `ArgumentException` because validation fails.
- If the type is something other than 1 or 2, it should throw `ArgumentException`.
- Registering the same name twice should throw `InvalidOperationException` — overwriting is not allowed.
- Calling this before `Initialize()` should throw `InvalidOperationException`.

---

## Retrieve(name)

- If the item exists, it should return the content that was registered.
- If the item doesn't exist, it should throw `KeyNotFoundException`.
- Calling this before `Initialize()` should throw `InvalidOperationException`.

---

## GetType(name)

- If the item was registered as JSON (type 1), it should return 1.
- If the item was registered as XML (type 2), it should return 2.
- If the item doesn't exist, it should throw `KeyNotFoundException`.
- Calling this before `Initialize()` should throw `InvalidOperationException`.

---

## Deregister(name)

- If the item exists, it should be removed. Trying to retrieve it afterward should throw `KeyNotFoundException`.
- If the item doesn't exist, it should throw `KeyNotFoundException`.
- After the item is removed, the same name should be available to register again.
- Calling this before `Initialize()` should throw `InvalidOperationException`.
