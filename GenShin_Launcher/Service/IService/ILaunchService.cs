using System.Threading.Tasks;

namespace Genshin_Launcher.Service.IService
{
    public interface ILaunchService
    {
        /// <summary>
        /// 异步启动游戏
        /// </summary>
        Task RunGameAsync();


        /// <summary>
        /// 读取游戏账号列表
        /// </summary>
        void ReadUserList();


        /// <summary>
        /// 创建客户端列表
        /// </summary>
        void CreateGamePortList();
    }
}
