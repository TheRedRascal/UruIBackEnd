using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Reflection;
using System.Threading.Tasks;
using UruIT.GameOfDrones.Bll.Player;
using UruIT.GameOfDrones.Common.Global;
using UruIT.GameOfDrones.Dal.Log;
using UruIT.GameOfDrones.Dal.Player;
using UruIT.GameOfDrones.Model;

namespace UruIT.GameOfDrones.Configuration.GlobalConfigurations
{
    /// <summary>
    /// Handle the global autofacsetup, for setting up the depency injection and inversion of control.
    /// </summary>
    public class AutoFacSetup
    {
        #region Private Variables
        private readonly ContainerBuilder builder;
        private readonly RegisterLogging regLogging;
        #endregion

        #region Constructors
        public AutoFacSetup()
        {
            builder = new ContainerBuilder();
            regLogging = new RegisterLogging(new LoggingDao());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Register all required components and services.
        /// </summary>
        /// <param name="ApiAssembly">Get the the apiassembly when calling the register.
        /// this assembly is required for register the WebApi Module for DI.</param>
        /// <returns>The registered container, so the webapi can resolve the depency resolver at return</returns>
        public IContainer RegisterDependencies(Assembly ApiAssembly)
        {
            try
            {
                RegisterModelServices();
                RegisterDalServices();
                RegisterCommonServices();
                RegisterBllServices();
                RegisterApiAssembly(ApiAssembly);
                return builder.Build();
            }
            catch (Exception Error)
            {
                Task.Run(async () => { await regLogging.SaveLog(Error); });
                return null;
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Register any required depedencies that are located on the Model Layer(UruIT.GameOfDrones.Model)
        /// </summary>
        private void RegisterModelServices()
        {
            builder.RegisterType<dbGameOfDronesEntities>().As<dbGameOfDronesEntities>();
        }

        /// <summary>
        /// Register any required depedencies that are located on the Model Dal(UruIT.GameOfDrones.Dal)
        /// </summary>
        private void RegisterDalServices()
        {
            builder.RegisterType<LoggingDao>().As<ILoggingDao>();
            builder.RegisterType<PlayerDao>().As<IPlayerDao>();
        }

        private void RegisterCommonServices()
        {
            builder.RegisterType<RegisterLogging>().As<IRegisterLogging>();
        }

        private void RegisterApiAssembly(Assembly ApiAssembly)
        {
            builder.RegisterApiControllers(ApiAssembly);
        }

        private void RegisterBllServices()
        {
            builder.RegisterType<PlayerBlo>().As<IPlayerBlo>();
        }

        #endregion


    }
}
