﻿using System.Collections.Specialized;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using WebSite;

namespace WebSite
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AuthConfig.RegisterOpenAuth();

            var properties = new NameValueCollection();
            properties["quartz.scheduler.instanceName"] = "RemoteServer";

            // set thread pool info
            properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
            properties["quartz.threadPool.threadCount"] = "5";
            properties["quartz.threadPool.threadPriority"] = "Normal";

            //Scheduler configuration
            ISchedulerFactory sf = new StdSchedulerFactory(properties);
            IScheduler sched = sf.GetScheduler();
            sched.Start();

            //Job configuration

            //WUnderground JOB; izvršava se svakih 10 minuta, nema kraja
            IJobDetail job = JobBuilder.Create<QuartzWeatherUG>()
                .WithIdentity("Očitanje vremena", "Grupa1")
                .Build();

            DateTimeOffset startTime = DateBuilder.NextGivenSecondDate(null, 15);
            var trigger = (ISimpleTrigger)TriggerBuilder.Create()
                                                        .WithIdentity("Trigger očitanja vremena", "Grupa1")
                                                        .StartAt(startTime)
                                                        .WithSimpleSchedule(
                                                            x => x.WithIntervalInMinutes(10).RepeatForever())
                                                        .Build();

            //Run the Job
            DateTimeOffset? ft = sched.ScheduleJob(job, trigger);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
        }
    }
}
