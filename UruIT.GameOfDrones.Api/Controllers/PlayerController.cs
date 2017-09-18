using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using UruIT.GameOfDrones.Bll.Player;
using UruIT.GameOfDrones.Dtl.Master;

namespace UruIT.GameOfDrones.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PlayerController : ApiController
    {

        #region Private Variables
        private readonly IPlayerBlo playerBlo;
        #endregion

        #region Constructors
        public PlayerController(IPlayerBlo _playerBlo)
        {
            playerBlo = _playerBlo;
        }
        #endregion

        #region Methods

        [Route("Api/Player/AddPlayer")]
        [HttpPost]
        public async Task<HttpResponseMessage> AddPlayer([FromBody]PlayerDto ePlayer)
        {
            await playerBlo.SavePlayer(ePlayer);
            return Request.CreateResponse(HttpStatusCode.OK, new { Message = playerBlo.Message });
        }

        [Route("Api/Player/GetAll")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAll()
        {
            await playerBlo.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, new { Message = playerBlo.Message });
        }

        #endregion
    }
}
