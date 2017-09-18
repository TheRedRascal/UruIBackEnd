using UruIT.GameOfDrones.Dal.Factory;
using UruIT.GameOfDrones.Dtl.Master;
using UruIT.GameOfDrones.Model;

namespace UruIT.GameOfDrones.Dal.Player
{
    public interface IPlayerDao : IGenericRepository<Players, PlayerDto>
    {

    }
}