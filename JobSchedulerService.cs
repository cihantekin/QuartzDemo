using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;

namespace QuartzDemo
{
    public class JobSchedulerService
    {
        public async Task StartAsync()
        {
            try
            {
                var schedulerContext = new StdSchedulerFactory();
                var scheduler = await schedulerContext.GetScheduler();

                if (!scheduler.IsStarted)
                {
                    await scheduler.Start();
                }

                await CreateMonthlyJob(scheduler);
                await CreateWeeklyJob(scheduler);
                await CreateDailyJob(scheduler);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task CreateMonthlyJob(IScheduler scheduler)
        {
            IJobDetail job = JobBuilder.Create<Job>().WithIdentity("Monthly", "group1").Build();

            ITrigger trigger = TriggerBuilder.Create().WithSchedule(CronScheduleBuilder.
                MonthlyOnDayAndHourAndMinute(1, 1, 1).
                InTimeZone(TimeZoneInfo.Local)).
                WithDescription("Monthle Job").
                Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        private async Task CreateWeeklyJob(IScheduler scheduler)
        {
            IJobDetail job = JobBuilder.Create<Job>().WithIdentity("Weekly", "group1").Build();

            ITrigger trigger = TriggerBuilder.Create().WithSchedule(CronScheduleBuilder
                .WeeklyOnDayAndHourAndMinute(DayOfWeek.Monday, 1, 0)
                .InTimeZone(TimeZoneInfo.Local))
                .WithDescription("Weekly Job")
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        private async Task CreateDailyJob(IScheduler scheduler)
        {
            IJobDetail job = JobBuilder.Create<Job>().WithIdentity("Daily", "group1").Build();

            ITrigger trigger = TriggerBuilder.Create().WithSchedule(CronScheduleBuilder
                .DailyAtHourAndMinute(1, 34)
                .InTimeZone(TimeZoneInfo.Local))
                .WithDescription("Daily Job")
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
