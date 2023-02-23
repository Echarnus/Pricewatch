using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pricewatch.Scraper.Infrastructure.Services.Tasks
{
	internal class HangfireTaskCreator : ITaskCreator
	{
		private readonly BackgroundJobServer _jobServer;

		public HangfireTaskCreator()
		{
			_jobServer = new BackgroundJobServer();
		}

		public void AddOrUpdateRecurringTask(Expression<Action> action, TimeSpan interval)
		{
			RecurringJob.AddOrUpdate(action, Cron.Daily(interval.Hours, interval.Minutes));
		}
	}
}
