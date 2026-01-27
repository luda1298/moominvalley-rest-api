using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using System.Text;
using System.Windows;

public class Moomin
{
    public int Number { get; set; }
    public string Name { get; set; }
}

class Program
{
    private const string URL = "https://localhost:7295/api/MoominValley";

    static async Task Main()
    {
        while (true)
        {
            Console.WriteLine("Valitse vaihtoehto:");
            Console.WriteLine("1. Hakea kaikki muumit");
            Console.WriteLine("2. Hakea muumi nimella");
            Console.WriteLine("3. Lisää muumi");
            Console.WriteLine("0. Poistu ohjelmasta");

            string input = Console.ReadLine();

            if (input == "0")
            {
                Console.WriteLine("Ohjelma suljetaan. Hei hei!");
                break;
            }

            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine("Virheellinen syöte, anna numero 0–3.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    await ListAllMoomins();
                    break;
                case 2:
                    await SearchMoominsByName();
                    break;
                case 3:
                    await AddNewMoomin();
                    break;
                default:
                    Console.WriteLine("Virheellinen valinta, yritä uudelleen.");
                    break;
            }

            Console.WriteLine("\nPaina Enter jatkaaksesi...");
            Console.ReadLine();
            Console.Clear();

            // Kaikkien muumien hakeminen
            static async Task ListAllMoomins()
            {
                using HttpClient client = new HttpClient();

                var result = await client.GetAsync(URL);

                if (result.IsSuccessStatusCode)
                {
                    var value = await result.Content.ReadAsStringAsync();
                    Console.WriteLine(value);
                }
            }


            // Name parametrilla hakeminen
            static async Task SearchMoominsByName()
            {
                Console.WriteLine("Syötä muumin nimi:");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Syötä muumin nimi.");
                    return;
                }
                using HttpClient client = new HttpClient();

                try
                {
                    var response = await client.GetAsync($"{URL}?name={input}");

                    if (response.IsSuccessStatusCode)
                    {
                        var value = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(value);
                    }
                    else
                    {
                        Console.WriteLine($"Virhe: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Virhe: {ex.Message}");
                }

            }

            // Muumin lisääminen
            static async Task AddNewMoomin()
            {
                while (true)
                {
                    Console.Write("Anna numero: ");
                    string number = Console.ReadLine();
                    Console.Write("Anna nimi: ");
                    string name = Console.ReadLine()!;

                    if (!int.TryParse(number, out int id) || string.IsNullOrWhiteSpace(name))
                    {
                        Console.WriteLine("Anna kelvollinen numero ja nimi.");
                        continue;
                    }

                    var newMoomin = new
                    {
                        Number = number,
                        Name = name
                    };

                    using var client = new HttpClient();
                    var json = JsonSerializer.Serialize(newMoomin);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    try
                    {
                        var response = await client.PostAsync(URL, content);

                        if (response.IsSuccessStatusCode)
                        {
                            var responseText = await response.Content.ReadAsStringAsync();
                            Console.WriteLine($"Muumi lisätty:\n{responseText}");
                            return;
                        }
                        else
                        {
                            Console.WriteLine($"Virhe: {response.StatusCode}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Virhe: {ex.Message}");
                    }

                }
            }
        }
    }
}
