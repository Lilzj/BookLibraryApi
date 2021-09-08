using EBook_Library.Data;
using EBook_Library.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
            if (context.Users.Any())
            {
                return;
            }
            else
            {
                //Get Usermanager from IoC container
                var userManager = app.ApplicationServices.CreateScope()
                                              .ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                //Seed Users
                var appUsers = GetSampleData<AppUser>(File.ReadAllText(path + "User.json"));

                foreach (var user in appUsers)
                {
                        await userManager.CreateAsync(user, UserPassword);
                }


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
