using Quartz.Spi;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;
using log4net;

namespace ApiServiceTest.Unity
{
    public class UnityJobFactory : IJobFactory
    {
        private readonly IUnityContainer _container;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(UnityJobFactory));

        public UnityJobFactory(IUnityContainer container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container), "Unity container cannot be null");
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                return (IJob)_container.Resolve(bundle.JobDetail.JobType);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error resolving job of type {bundle.JobDetail.JobType}: {ex.Message}");
                throw;
            }
        }

        public void ReturnJob(IJob job)
        {
        }
    }


}