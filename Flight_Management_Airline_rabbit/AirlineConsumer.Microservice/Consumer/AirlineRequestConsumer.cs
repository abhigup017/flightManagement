using AirlineConsumer.Microservice.Interface;
using AirlineConsumer.Microservice.Service;
using Common;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineConsumer.Microservice.Consumer
{
    public class AirlineRequestConsumer : IConsumer<AirlineRegistrationRequest>
    {
        private readonly ILogger<AirlineRequestConsumer> _logger;
        AirlineRegistrationManagementRepository _airlineRegistrationManagementRepository;

        public AirlineRequestConsumer(ILogger<AirlineRequestConsumer> logger)
        {
            _logger = logger;
            _airlineRegistrationManagementRepository = new AirlineRegistrationManagementRepository();
        }
        /// <summary>
        /// Consumer Method for AirlineRegistrationRequest
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        #region Consumer Method
        public async Task Consume(ConsumeContext<AirlineRegistrationRequest> context)
        {
            try
            {
                _airlineRegistrationManagementRepository.RegisterAirline(context.Message);
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message + "Stack Trace:" + ex.StackTrace);
            }
            
        }
        #endregion
    }
}
