using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UruIT.GameOfDrones.Common.Global;
using UruIT.GameOfDrones.Dal.Player;
using UruIT.GameOfDrones.Dtl.Master;

namespace UruIT.GameOfDrones.Bll.Player
{
    public class PlayerBlo : IPlayerBlo
    {
        #region Private Variables
        private readonly IPlayerDao playerDao;
        private readonly IRegisterLogging registerLog;
        #endregion

        #region Public Properties
        public MessageDto Message { get; set; }
        #endregion

        #region Constructors
        public PlayerBlo(IPlayerDao _playerDao, IRegisterLogging _registerLog)
        {
            playerDao = _playerDao;
            registerLog = _registerLog;
            Message = new MessageDto();
        }
        #endregion

        #region Public Methods
        public async Task SavePlayer(PlayerDto ePlayer)
        {
            try
            {
                if (ePlayer != null)
                {
                    if (!ValidatePlayerExists(ePlayer))
                    {
                        ePlayer.Score++;
                        await AddPlayer(ePlayer);
                    }
                    else
                    {
                        PlayerDto createdPlayer = FindPlayerByName(ePlayer.Name);

                        if (createdPlayer != null)
                        {
                            createdPlayer.Score++;
                            await EditPlayer(createdPlayer);
                        }
                        else
                        {
                            Message.Status = MessagesResource.Error;
                            Message.Message = MessagesResource.AddUserError;
                        }
                    }
                }
                else
                {
                    Message.Status = MessagesResource.Error;
                    Message.Message = MessagesResource.AddUserNoInfo;
                }
            }
            catch (Exception Error)
            {
                await registerLog.SaveLog(Error);
                Message.Clear();
                Message.Status = MessagesResource.Error;
                Message.Message = Error.Message;
            }
        }

        public async Task GetAll()
        {
            try
            {
                await Task.Run(() =>
                {
                    List<PlayerDto> lstPlayers = playerDao.GetAll().OrderBy(x => x.Score).ToList();
                    Message.Data = lstPlayers;
                    Message.Message = MessagesResource.Success;
                    Message.Status = MessagesResource.Success;
                });
            }
            catch (Exception Error)
            {
                await registerLog.SaveLog(Error);
                Message.Clear();
                Message.Status = MessagesResource.Error;
                Message.Message = Error.Message;
            }
        }


        #endregion

        #region Private Methods
        private bool ValidatePlayerExists(PlayerDto ePlayer)
        {
            try
            {
                return playerDao.FindBy(x => x.Name == ePlayer.Name).Any();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task AddPlayer(PlayerDto ePlayer)
        {
            try
            {
                playerDao.Add(ePlayer);
                await playerDao.Save();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task EditPlayer(PlayerDto ePlayer)
        {
            try
            {
                playerDao.Edit(ePlayer);
                await playerDao.Save();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private PlayerDto FindPlayerByName(string PlayerName)
        {
            try
            {
                return playerDao.FindBy(x => x.Name.ToLower().Trim().Equals(PlayerName)).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
