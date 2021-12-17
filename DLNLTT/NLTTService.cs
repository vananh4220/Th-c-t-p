using log4net;
using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;

namespace DLNLTT
{
    public class NLTTService
    {
        //khai báo một biến static readonly toàn cục cho tất cả các class cần log
        protected static readonly ILog log = LogManager.GetLogger(typeof(NLTTService));

        public async Task Start()
        {
            log.Info("Start");
            await Scheduler();
        }

        public async Task Stop()
        {
            log.Info("Stop");
        }

        private static async Task Scheduler()
        {
            ReadJson rj = new ReadJson();
            var items = rj.GetDataFromJson();

            IScheduler sched = await StdSchedulerFactory.GetDefaultScheduler();
            await sched.Start();

            IJobDetail job = JobBuilder.Create<ThuThapSoLieu>().Build();

            ITrigger trigger = TriggerBuilder.Create().WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInSeconds(items.Timer)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(items.GioBD, items.PhutBD))
                    .EndingDailyAt(TimeOfDay.HourAndMinuteOfDay(items.GioKT, items.PhutKT))
                  )
                .Build();

            await sched.ScheduleJob(job, trigger);
        }
    }
}
