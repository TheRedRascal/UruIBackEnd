using System;
using System.Threading.Tasks;
using UruIT.GameOfDrones.Dal.Log;
using UruIT.GameOfDrones.Dtl.Master;

namespace UruIT.GameOfDrones.Common.Global
{
    public class RegisterLogging : IRegisterLogging
    {
        #region Global Variables
        private readonly ILoggingDao logginDao;
        #endregion

        #region Constructors
        public RegisterLogging(ILoggingDao _logginDao)
        {
            logginDao = _logginDao;
        }
        #endregion

        /// <summary>
        /// Save any logged error to database
        /// </summary>
        /// <param name="Error"></param>
        /// <returns></returns>
        #region Public Methods
        public async Task SaveLog(Exception Error)
        {
            LoggingDto eLogin = new LoggingDto()
            {
                FullException = Error.ToString(),
                ModuleName = Error.TargetSite.DeclaringType.ToString(),
                RegisterDate = DateTime.Now,
                SimpleError = Error.Message
            };

            logginDao.Add(eLogin);
            await logginDao.Save();
        }
        #endregion
    }
}
