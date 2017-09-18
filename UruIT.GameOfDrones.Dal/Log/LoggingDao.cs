using UruIT.GameOfDrones.Dal.Factory;
using UruIT.GameOfDrones.Dtl.Master;
using UruIT.GameOfDrones.Model;

namespace UruIT.GameOfDrones.Dal.Log
{
    public class LoggingDao : GenericRepository<dbGameOfDronesEntities, Logging, LoggingDto>, ILoggingDao
    {


    }
}
