using log4net;
using log4net.Config;
using System;
using System.Web;
using System.Web.Http;
using Quartz;
using Quartz.Impl;


namespace ApiServiceTest
{
    public class WebApiApplication : HttpApplication
    {
        private static IScheduler _scheduler;
        protected void Application_Start()
        {
            ConfigureQuartz();
            GlobalConfiguration.Configure(WebApiConfig.Register);

            XmlConfigurator.Configure(new System.IO.FileInfo(Server.MapPath("~/log4net.config")));

            ILog logger = LogManager.GetLogger(typeof(WebApiApplication));
            logger.Info("Application Started");
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

                _scheduler.Start().Wait();

                IJobDetail job = JobBuilder.Create<UpdateVIPCustomersJob>()
                    .WithIdentity("UpdateVIPCustomersJob", "CustomerJobs")
                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("HourlyTrigger", "CustomerJobs")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInHours(1)
                        .RepeatForever())
                    .Build();

                _scheduler.ScheduleJob(job, trigger).Wait();

                Console.WriteLine("Quartz.NET scheduler configured successfully.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Quartz.NET configuration failed: {ex.Message}");
            }
        }
    }
}
