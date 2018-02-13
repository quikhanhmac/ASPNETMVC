using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TestCiqualAPI.Models;

namespace TestCiqualAPI
{
    class Program
    {

        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }
        static async Task RunAsync()
        {
            // Modifier le port selon les besoins
            client.BaseAddress = new Uri($"http://localhost:64443/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Create a new aliment
                Aliment aliment = new Aliment
                {
                    IdAliment = 1130,
                    Nom = "riz",
                    CodeFamille = "23.4"
                };

                var url = await CreateAlimentAsync(aliment);
                Console.WriteLine($"Aliment créé à l'url {url}");

                // Get the aliment
                aliment = await GetAlimentAsync(url.PathAndQuery);
                ShowAliment(aliment);

                // Update the aliment
                Console.WriteLine("Mise à jour du code de famille...");
                aliment.CodeFamille = "24.1";
                await UpdateAlimentAsync(aliment);

                // Get the updated aliment
                aliment = await GetAlimentAsync(url.PathAndQuery);
                ShowAliment(aliment);

                // Delete the aliment
                var statusCode = await DeleteAlimentAsync(aliment.IdAliment);
                Console.WriteLine($"Aliment supprimé (statut HTTP = {(int)statusCode})");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        private static HttpClient client = new HttpClient();
        static void ShowAliment(Aliment aliment)
        {
            Console.WriteLine($"{aliment.IdAliment} {aliment.Nom} {aliment.CodeFamille}");

        }
        static async Task<Uri> CreateAlimentAsync(Aliment aliment)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/Aliments", aliment);
            response.EnsureSuccessStatusCode();

            // retourne l'uri de la ressource créée
            return response.Headers.Location;

        }
        static async Task<Aliment> GetAlimentAsync(string path)
        {
            Aliment aliment = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                aliment = await response.Content.ReadAsAsync<Aliment>();
            }
            return aliment;
        }
        static async Task<Aliment> UpdateAlimentAsync(Aliment aliment)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/Aliments/{aliment.IdAliment}", aliment);
            response.EnsureSuccessStatusCode();

            // Deserialise l'employé mis à jour depuis le corps de la réponse
            aliment = await response.Content.ReadAsAsync<Aliment>();
            return aliment;
        }

        static async Task<HttpStatusCode> DeleteAlimentAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/Aliments/{id}");
            return response.StatusCode;
        }

    }
}

