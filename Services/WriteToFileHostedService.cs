using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuthorsAPI.Services
{
    public class WriteToFileHostedService : IHostedService, IDisposable
    {
        private readonly Microsoft.Extensions.Hosting.IHostEnvironment _environment;
        private readonly string _fileName = "File_1.txt";
        private Timer _timer;

        public WriteToFileHostedService(IHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //When the app is init on the server.
            _timer = new Timer(callback: DoWorkAsync,null,TimeSpan.Zero,TimeSpan.FromSeconds(5));

            //await WriteToFileAsync("WriteToFileHostedService: Process Started");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            //When the app is stopped on the server. MS does not guarante it.
            await WriteToFileAsync("WriteToFileHostedService: Process Stopped");
            _timer?.Change(Timeout.Infinite,0);

        }

        private async Task WriteToFileAsync(string message)
        {
            var path = $@"{_environment.ContentRootPath}\wwwroot\{_fileName}";
            using(var writer = new StreamWriter(path, append: true))
            {
                await writer.WriteLineAsync(message);
            }
        
        }

        private async void DoWorkAsync(object state)
        {
            //await WriteToFileAsync($"WriteToFileHostedService: Doing some work at {DateTime.Now:MM dd yyyy hh:mm:ss}");
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
