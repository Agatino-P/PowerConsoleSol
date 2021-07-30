using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace PowerConsoleUI
{

    public class GreetingService : IGreetingService
    {
        private readonly ILogger<GreetingService> _log;
        private readonly IConfiguration _config;

        public GreetingService(ILogger<GreetingService> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }

        public void Run()
        {
            for (int runNumber = 0; runNumber < _config.GetValue<int>("LoopTimes"); runNumber++)
            {
                _log.LogInformation($"Run number: {runNumber}");
            }
        }
    }

}
