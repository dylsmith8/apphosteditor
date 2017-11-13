using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Web.Administration;
using Microsoft.Web.Management;
using System.Security.Cryptography.X509Certificates;
using System.Net;

namespace ApplicationHostEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServerManager manager = new ServerManager())
            {
                var config = manager.GetApplicationHostConfiguration();

                try
                {
                    ConfigurationSection accessSection = config.GetSection("system.webServer/security/access", "Default Web Site/WebInterface");

                    if (accessSection != null)
                    {
                        Console.WriteLine("Opened app host config successfully.");
                        accessSection["sslFlags"] = @"Ssl";
                    }

                    accessSection = config.GetSection("system.webServer/security/access", "Default Web Site/WebInterface/AttorneyCompliance.svc");

                    if (accessSection != null)
                    {
                        accessSection["sslFlags"] = @"Ssl, SslNegotiateCert, SslRequireCert";
                    }

                    manager.CommitChanges();
                    Console.WriteLine("Successfully committed changes");
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to open the app host config.");
                }
            }
        }
    }
}
