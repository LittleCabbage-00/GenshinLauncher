using Genshin_Launcher.Models;
using System.Collections.Generic;

namespace Genshin_Launcher.Service.IService
{
    public interface IUserDataService
    {
        /// <summary>
        /// 读取用户数据文件到List
        /// </summary>
        /// <returns></returns>
        List<UserListModel> ReadUserList();
    }
}
