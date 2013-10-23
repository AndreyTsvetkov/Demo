using System.Web.Http;
using System.Web.Mvc;
using BookKeeping.Domain;
using BookKeeping.Domain.Store;
using Microsoft.Practices.Unity;
using Unity.WebApi;

// ReSharper disable CheckNamespace
namespace BookKeeping
{
	public static class DependenciesConfig
	{
		public static IUnityContainer BuildDependencies()
		{
			var container = new UnityContainer();
			container.RegisterType<IExpenseService, ExpenseService>();
			container.RegisterType<IRepository<Expense>, EFRepository<Expense>>();
			container.RegisterType<IDbContextFor<Expense>, EFDataContext>(new InjectionConstructor("DbConnection"));
			
			
			// Одно для API-контроллеров
			GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
			// Другое - для обычных контроллеров
			DependencyResolver.SetResolver(new Microsoft.Practices.Unity.Mvc.UnityDependencyResolver(container));

			return container;
		}
	}
}