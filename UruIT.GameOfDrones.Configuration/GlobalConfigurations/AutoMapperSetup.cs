using AutoMapper;
using System.Threading.Tasks;
using UruIT.GameOfDrones.Common.Global;
using UruIT.GameOfDrones.Dal.Log;
using UruIT.GameOfDrones.Dtl.Master;
using UruIT.GameOfDrones.Model;

namespace UruIT.GameOfDrones.Configuration.GlobalConfigurations
{
    /// <summary>
    /// Register the mapping entities
    /// </summary>
    public class AutoMapperSetup
    {
        #region Private Variables
        private readonly RegisterLogging regLoggin;
        #endregion

        #region Constructors
        public AutoMapperSetup()
        {
            regLoggin = new RegisterLogging(new LoggingDao());
        }
        #endregion

        /// <summary>
        /// Create the registers and mapping between each Dto and Model Entity.
        /// </summary>
        #region Public Methods
        public void RegisterMapping()
        {
            try
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Logging, LoggingDto>().ReverseMap();
                    cfg.CreateMap<Players, PlayerDto>().ReverseMap();
                });
            }
            catch (System.Exception Error)
            {
                Task.Run(async () => { await regLoggin.SaveLog(Error); });
            }
        }
        #endregion

    }
}
