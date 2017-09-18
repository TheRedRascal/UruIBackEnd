using System;
using System.Threading.Tasks;

namespace UruIT.GameOfDrones.Common.Global
{
    public interface IRegisterLogging
    {
        Task SaveLog(Exception Error);
    }
}