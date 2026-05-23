using System.Collections.Concurrent; // untuk ConcurrentDictionary — dictionary yang aman dipakai banyak thread sekaligus

// class utama broker — mengelola semua channel notifikasi
public class NotifcationBroker
{
  // class inner: mewakili satu channel (saluran pesan)
  // private = hanya bisa dipakai di dalam NotifcationBroker
  private class NotificationChannel
  {
    // public Queue<string> MessageQueue { get; } = new Queue<string>(); // antrian pesan FIFO milik channel ini
    public int MaxCapacity { get; }                                    // batas maksimal pesan dalam antrian
    // public object LockToken { get; } = new object();                  // objek kunci untuk mencegah race condition
    public Queue<string> MessageQueue = new Queue<string>();
    // public readonly int MaxCapacity;
    public object LockToken = new object();

    // constructor: nama harus sama persis dengan nama class di atas
    public NotificationChannel(int maxCapacity)
    {
      MaxCapacity = maxCapacity; // simpan nilai kapasitas maksimal
    }
  }

  // dictionary semua channel yang terdaftar, key = nama channel
  // letaknya di sini (di NotifcationBroker), bukan di dalam NotificationChannel
  private readonly ConcurrentDictionary<string, NotificationChannel> _channels = new();

  // daftarkan channel baru dengan nama unik dan kapasitas antrian
  public void RegisterChannel(string channelName, int maxCapacity = 10)
  {
    var newChannel = new NotificationChannel(maxCapacity); // buat objek channel baru

    if (!_channels.TryAdd(channelName, newChannel)) // TryAdd gagal kalau nama sudah ada
    {
      throw new ArgumentException($"Channel '{channelName}' sudah terdaftar.");
    }
  }

  // kirim pesan ke channel tertentu
  // return true = berhasil, false = channel tidak ada atau antrian penuh
  public bool SendNotification(string channelName, string payload)
  {
    if (!_channels.TryGetValue(channelName, out var channel)) // cari channel berdasarkan nama
    {
      return false; // channel tidak ditemukan
    }

    lock (channel.LockToken) // kunci antrian agar tidak diakses thread lain bersamaan
    {
      if (channel.MessageQueue.Count >= channel.MaxCapacity) // cek apakah antrian sudah penuh
      {
        return false; // antrian penuh, pesan ditolak
      }

      Console.WriteLine($"Mengirim pesan ke channel '{channelName}': {payload}"); // log pengiriman

      channel.MessageQueue.Enqueue(payload); // masukkan pesan ke belakang antrian
      return true;                           // berhasil
    }
  }

  // ambil (dan hapus) pesan paling lama dari channel — urutan FIFO
  // return null kalau channel tidak ada atau antrian kosong
  public string? ConsumeNotification(string channelName)
  {
    if (!_channels.TryGetValue(channelName, out var channel)) // cari channel berdasarkan nama
    {
      return null; // channel tidak ditemukan
    }

    lock (channel.LockToken) // kunci antrian agar aman dari akses bersamaan
    {
      if (channel.MessageQueue.Count == 0) // cek apakah antrian kosong
      {
        return null; // tidak ada pesan
      }

      return channel.MessageQueue.Dequeue(); // ambil dan hapus pesan paling depan
    }
  }
}
