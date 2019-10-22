using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using SCA.Job.Configuration;
using SCA.Job.Job;
using SCA.Job.Service;
using System;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;

namespace SCA.Job
{
    public class JobService : IHostedService
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
                .AddScoped<IUserJob, UserJob>()
                .AddScoped<IUserService, UserService>();
            return services.BuildServiceProvider();
        }

        private IJobDetail GetDailyJob()
        {
            return JobBuilder.Create<IUserJob>()
                .WithIdentity("dailyjob", "dailygroup")
                .Build();
        }
        private ITrigger GetDailyJobTrigger()
        {
            return TriggerBuilder.Create()
                 .WithIdentity("dailytrigger", "dailygroup")
                 .StartNow()
                 .WithSimpleSchedule(x => x
                     .WithIntervalInMinutes(1)
                     .RepeatForever())
                 .Build();
        }
    }
}