using log4net;
using log4net.Config;
using System;
using System.Web;
using System.Web.Http;
using Quartz;
using Quartz.Impl;
using Unity;
using ApiServiceTest.Unity;

namespace ApiServiceTest
{
    public class WebApiApplication : HttpApplication
    {
        private static IScheduler _scheduler;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(WebApiApplication));
        protected void Application_Start()
        {
            UnityConfig.RegisterComponents();
            ConfigureQuartz();
            GlobalConfiguration.Configure(WebApiConfig.Register);

            XmlConfigurator.Configure(new System.IO.FileInfo(Server.MapPath("~/log4net.config")));
            _logger.Info("Application Started");
        }
        protected void Application_End()
        {
            _scheduler?.Shutdown();
        }

        private void ConfigureQuartz()
        {
            try
            {
                ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
                _scheduler = schedulerFactory.GetScheduler().Result;

                _scheduler.JobFactory = new UnityJobFactory(UnityConfig.Container);

                _scheduler.Start().Wait();

                IJobDetail job = JobBuilder.Create<UpdateVIPCustomersJob>()
                    .WithIdentity("UpdateVIPCustomersJob", "CustomerJobs")
                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("SecondlyTrigger", "CustomerJobs")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInMinutes(1)
                        .RepeatForever())
                    .Build();

                _scheduler.ScheduleJob(job, trigger).Wait();

                _logger.Info("Quartz.NET scheduler configured successfully.");
            }
            catch (Exception ex)
            {
                _logger.Error($"Quartz.NET configuration failed: {ex.Message}");
            }
        }


    }
}
