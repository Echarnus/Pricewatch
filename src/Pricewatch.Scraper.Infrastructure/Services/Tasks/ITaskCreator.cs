using System.Linq.Expressions;

namespace Pricewatch.Scraper.Infrastructure.Services.Tasks
{
	internal interface ITaskCreator
	{
		void AddOrUpdateRecurringTask(Expression<Action> action, TimeSpan interval);
	}
}