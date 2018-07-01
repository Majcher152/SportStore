using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IServiceProvider services)
        {
            ApplicationDbContext context = services.GetRequiredService<ApplicationDbContext>();
            //context.Database.Migrate();

            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Kayak",
                        Description = "A boat for one person.",
                        Category = "Water Sports",
                        Price = 275
                    },
                    new Product
                    {
                        Name = "Life jacket",
                        Description = "Protects and gives charm.",
                        Category = "Water Sports",
                        Price = 48.95M
                    },
                    new Product
                    {
                        Name = "Ball",
                        Description = "Approved by FIFA size and weight.",
                        Category = "Football",
                        Price = 19.50M
                    },
                    new Product
                    {
                        Name = "Corner flag",
                        Description = "Thanks to them your playing field will like profesionally.",
                        Category = "Football",
                        Price = 34.95M
                    },
                    new Product
                    {
                        Name = "Stadion",
                        Description = "Folding stadion for 35 000 people.",
                        Category = "Football",
                        Price = 79500
                    },
                    new Product
                    {
                        Name = "Cap",
                        Description = "Increase brain's effectieveness by 75%.",
                        Category = "Chess",
                        Price = 16
                    },
                    new Product
                    {
                        Name = "Unstable chair",
                        Description = "Decrease enemy's chances.",
                        Category = "Chess",
                        Price = 29.95M
                    },
                    new Product
                    {
                        Name = "Human chessboard",
                        Description = "Pleasure game for whole familly.",
                        Category = "Chess",
                        Price = 75
                    },
                    new Product
                    {
                        Name = "Shining king",
                        Description = "A figure covered with gold and studded with daimonds.",
                        Category = "Chess",
                        Price = 1200
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
