using log4net;
using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace DLNLTT
{
    public class NLTTService
    {

        public NLTTService()
        {
        }

        public async Task Start()
        {
            await Scheduler();

        }

        public async Task Stop()
        {
           
        }

        private static async Task Scheduler()
        {
            ReadJson rj = new ReadJson();
            var items = rj.GetDataFromJson();

            NameValueCollection props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);

            IScheduler sched = await factory.GetScheduler();
            await sched.Start();

            IJobDetail job = JobBuilder.Create<ThuThapSoLieu>().WithIdentity("myjob", "group1")

                .Build();


            ITrigger trigger = TriggerBuilder.Create().WithIdentity("trigger1", "group1")
                .WithDailyTimeIntervalSchedule
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
