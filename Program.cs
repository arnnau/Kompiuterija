using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kompiuterija
***REMOVED***
    public class Program
    ***REMOVED***
        public static void Main(string[] args)
        ***REMOVED***
            CreateHostBuilder(args).Build().Run();
***REMOVED***

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                ***REMOVED***
                    webBuilder.UseStartup<Startup>();
        ***REMOVED***);
***REMOVED***
***REMOVED***
