using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Monografico.Models;

namespace Monografico
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateWebHostBuilder(args).Build().Run();

            var host = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var serviceProvider = services.GetRequiredService<IServiceProvider>();
                    var configuration = services.GetRequiredService<IConfiguration>();

                    // TODO: Codigo para crear un usuario inicial en el sistema
                    ///Esto no debe usarse en producción
                    CreateFirstUser(serviceProvider.GetService<UserManager<Usuario>>(),
                                    serviceProvider.GetService<RoleManager<Rol>>()).Wait();

                }
                catch (Exception exception)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(exception, "Ocurrió un error creando los roles de usuario.");
                    //TODO: Pendiente definir donde se almacenará el log del sistema.
                }
            }

            // Run the application
            host.Run();
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
               .UseStartup<Startup>()
               .Build();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        public static async Task CreateFirstUser(UserManager<Usuario> _userManager, RoleManager<Rol> _roleManager)
        {
            var usuario = await _userManager.FindByNameAsync("Ven");
            if (usuario == null)
            {

                Usuario user = new Usuario
                {
                    UserName = "Ven",
                    Nombre = "Jose",
                    Apellido = "Garcia",
                    Email = "jose@hotmail.com",
                    PhoneNumber = "8093432342",
                    LockoutEnabled = false

                };
                var result = await _userManager.CreateAsync(user, "123456");

                if (result.Succeeded)
                {
                    var roleExist = await _roleManager.RoleExistsAsync("Administrador");
                    if (!roleExist)
                    {
                        var identityRole = new Rol
                        {
                            Name = "Administrador",
                            NormalizedName = "ADMINISTRADOR"

                        };
                        var roleResult = await _roleManager.CreateAsync(identityRole);
                        if (roleResult.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(user, "Administrador");
                        }
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "Administrador");
                    }
                }
            }
        }
    }
}