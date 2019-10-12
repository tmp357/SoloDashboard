using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using SoloDashboard.Repository.Contracts;
using Dashboard.Repository.Interfaces;
using SoloDashboard.Entities.Models;
using Dashboard.Repository.Repos;

namespace SoloDashboard
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            //registered dependency here
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IMockJobsRepository, MockJobsRepository>();
        }
    }
}