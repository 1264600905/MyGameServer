using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using System.IO;
using log4net.Config;

namespace MyGameServer
{
    public class MyGameServer : ApplicationBase
    {
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();
        //当一个客户端请求
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            log.Info("一个客户端连接过来了........");
            return new ClientPeer(initRequest);
        }
        //初始化
        protected override void Setup()
        {
            //日志初始化
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(ApplicationRootPath, "log");
            FileInfo configFileInfo = new FileInfo(Path.Combine(this.BinaryPath, "log4net.config"));
            if (configFileInfo.Exists)
            {
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
                XmlConfigurator.ConfigureAndWatch(configFileInfo);
            }
            log.Info("服务器启动完成！");
        }
        //server关闭的时候
        protected override void TearDown()
        {
            log.Info("服务器关闭了");
        }
    }
}
