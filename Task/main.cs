using System;
using System.Collections.Generic;
using System.Text;

// ==========================================
// SOAL 4: Mengubah logika menjadi Class Object
// ==========================================
public class FizzBuzzGenerator
{
        // Menyimpan aturan dalam Dictionary (Key: angka pembagi, Value: kata yang dicetak)
        private readonly Dictionary<int, string> _rules = new Dictionary<int, string>();

        // API untuk menambah aturan secara dinamis
        public void AddRule(int input, string output)
        {
            if (_rules.ContainsKey(input))
            {
                _rules[input] = output; // Update jika sudah ada
            }
            else
            {
                _rules.Add(input, output);
            }
        }

        // Fungsi untuk memproses satu angka berdasarkan aturan yang didaftarkan
        public string ProcessNumber(int x)
        {
            StringBuilder result = new StringBuilder();

            // Iterasi setiap aturan yang ada
            foreach (var rule in _rules)
            {
                if (x % rule.Key == 0)
                {
                    result.Append(rule.Value);
                }
            }

            // Jika tidak ada aturan yang terpenuhi, kembalikan angka itu sendiri
            return result.Length > 0 ? result.ToString() : x.ToString();
        }

        // Fungsi untuk mencetak dari 1 sampai n
        public void PrintRange(int n)
        {
            List<string> outputs = new List<string>();
            for (int i = 1; i <= n; i++)
            {
                outputs.Add(ProcessNumber(i));
            }
            
            // Mencetak dengan format dipisahkan koma seperti contoh soal
            Console.WriteLine(string.Join(", ", outputs));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // ------------------------------------------
            // DEMO SOAL 1 & 2: Logika Dasar & Tambah 'Jazz' (7)
            // ------------------------------------------
            Console.WriteLine("--- Demo Soal 1 & 2 (n = 15) ---");
            FizzBuzzGenerator basicGen = new FizzBuzzGenerator();
            basicGen.AddRule(3, "foo");
            basicGen.AddRule(5, "bar");
            basicGen.AddRule(7, "jazz"); // Tambahan Soal 2
            
            basicGen.PrintRange(15);
            Console.WriteLine();

            // Tes spesifik Soal 2 untuk x=21, x=35, x=105
            Console.WriteLine($"x=21  -> {basicGen.ProcessNumber(21)}");   // foojazz
            Console.WriteLine($"x=35  -> {basicGen.ProcessNumber(35)}");   // barjazz
            Console.WriteLine($"x=105 -> {basicGen.ProcessNumber(105)}");  // foobarjazz
            Console.WriteLine("\n-----------------------------------------\n");


            // ------------------------------------------
            // DEMO SOAL 3 & 4: Menggunakan Tabel Aturan Baru & API Class
            // ------------------------------------------
            Console.WriteLine("--- Demo Soal 3 & 4 (Custom Rules via API) ---");
            
            // Inisialisasi class sesuai perintah Soal 4
            FizzBuzzGenerator myClass = new FizzBuzzGenerator();
            
            // Memasukkan aturan dari tabel Soal 3 menggunakan fungsi AddRule
            myClass.AddRule(3, "foo");
            myClass.AddRule(4, "baz");
            myClass.AddRule(5, "bar");
            myClass.AddRule(7, "jazz");
            myClass.AddRule(9, "huzz");

            // Jalankan untuk melihat hasilnya (Contoh sampai n = 40)
            Console.WriteLine("Hasil cetak dari 1 sampai 40 dengan aturan baru:");
            myClass.PrintRange(40);
        }
    }