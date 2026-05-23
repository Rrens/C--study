var brokerEmail = new NotifcationBroker();

brokerEmail.RegisterChannel("email", maxCapacity: 3);

brokerEmail.SendNotification("email", "Pesan 1: Selamat datang!");
brokerEmail.SendNotification("email", "Pesan 2: Verifikasi akun kamu.");
brokerEmail.SendNotification("email", "Pesan 3: Promo haclri ini!");
brokerEmail.SendNotification("email", "Pesan 4: Promo haclri ini!");

bool berhasilEmail = brokerEmail.SendNotification("email", "Pesan 4: Ini tidak akan masuk.");
Console.WriteLine($"Kirim pesan ke-4: {(berhasilEmail ? "berhasil" : "ditolak — antrian penuh")}");

Console.WriteLine();

// ambil pesan satu per satu dari antrian (FIFO — yang pertama masuk, pertama keluar)
Console.WriteLine("Mengambil pesan dari antrian:");
string? pesan;
while ((pesan = brokerEmail.ConsumeNotification("email")) != null) // terus ambil sampai antrian kosong
{
  Console.WriteLine($"  -> {pesan}");
}

// antrian sudah kosong, ConsumeNotification return null
Console.WriteLine("Antrian kosong.");
