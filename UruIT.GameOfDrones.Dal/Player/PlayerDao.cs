using UruIT.GameOfDrones.Dal.Factory;
using UruIT.GameOfDrones.Dtl.Master;
using UruIT.GameOfDrones.Model;

namespace UruIT.GameOfDrones.Dal.Player
{
    public class PlayerDao : GenericRepository<dbGameOfDronesEntities, Players, PlayerDto>, IPlayerDao
    {

    }
}
