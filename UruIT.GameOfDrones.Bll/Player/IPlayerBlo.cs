using System.Threading.Tasks;
using UruIT.GameOfDrones.Dtl.Master;

namespace UruIT.GameOfDrones.Bll.Player
{
    public interface IPlayerBlo
    {
        MessageDto Message { get; set; }
        Task SavePlayer(PlayerDto ePlayer);
        Task GetAll();
    }
}