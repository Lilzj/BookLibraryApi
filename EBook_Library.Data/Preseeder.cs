using EBook_Library.Data;
using EBook_Library.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace EBookLibrary.Data
{
    public class Preseeder
    {
        static string path = Path.GetFullPath(@"../EBook_Library.Data/JsonData/");

        private const string UserPassword = "P@ssword1";

        public async static void EnsurePopulated(IApplicationBuilder app)
        {
            //Get the Db context
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<EBookContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (context.Books.Any())
            {
                return;
            }
            else
            {             
                //Seed Book
                var Books = GetSampleData<Book>(File.ReadAllText(path + "Book.json"));
                await context.Books.AddRangeAsync(Books);

                await context.SaveChangesAsync();
            }
        }


        //Get sample data from json files
        private static List<T> GetSampleData<T>(string location)
        {
            var output = JsonSerializer.Deserialize<List<T>>(location, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return output;
        }
    }
}
