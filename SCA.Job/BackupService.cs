using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;

namespace SCA.Job
{
    public class BackupService : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                var scheduler = await GetScheduler();
                var serviceProvider = GetConfiguredServiceProvider();
                scheduler.JobFactory = new CustomJobFactory(serviceProvider);

                await scheduler.Start();
                await scheduler.ScheduleJob(GetDailyJob(), GetDailyJobTrigger());
            }
            catch (Exception ex)
            {
                throw new CustomConfigurationException(ex.Message);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }


        private static async Task<IScheduler> GetScheduler()
        {
            var props = new NameValueCollection { { "quartz.serializer.type", "binary" } };
            var factory = new StdSchedulerFactory(props);
            var scheduler = await factory.GetScheduler();
            return scheduler;
        }

        private IServiceProvider GetConfiguredServiceProvider()
        {
            var services = new ServiceCollection()
                .AddScoped<IDailyJob, DailyJob>()
                .AddScoped<IHelperService, HelperService>();
            return services.BuildServiceProvider();
        }

        private IJobDetail GetDailyJob()
        {
            return JobBuilder.Create<IDailyJob>()
                .WithIdentity("dailyjob", "dailygroup")
                .Build();
        }
        private ITrigger GetDailyJobTrigger()
        {
            return TriggerBuilder.Create()
                 .WithIdentity("dailytrigger", "dailygroup")
                 .StartNow()
                 .WithSimpleSchedule(x => x
                     .WithIntervalInSeconds(10)
                     .RepeatForever())
                 .Build();
        }
    }
}
