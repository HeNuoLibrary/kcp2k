using HeNuo.Network;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace HeNuo.Example
{
    public class MultiNetworkServer : MonoBehaviour
    {
        KCPServerChannel kcpServerChannel;

        [Header("KCP")]
        [SerializeField] Button btnKcpStartServer;
        [SerializeField] Button btnKcpStopServer;

        void Start()
        {
            kcpServerChannel = new KCPServerChannel("TEST_KCP_SERVER", MultiNetworkConstants.KCP_PORT);



            kcpServerChannel.OnConnected += KcpServer_OnConnected;
            kcpServerChannel.OnDataReceived += KcpServer_OnDataReceived;
            kcpServerChannel.OnDisconnected += KcpServer_OnDisconnected;
            //channel的TickRefresh函数可自定义管理轮询，networkManager的作用是存放通道并调用TickRefresh。
            //由于存在多种网络方案的原因，通道对应的具体事件需要由使用者自定义解析，框架不提供具体数据。
            //这里将server加入networkManager，由networkManager管理通道的轮询
            NetworkEntry.NetworkManager.AddChannel(kcpServerChannel);

            btnKcpStartServer?.onClick.AddListener(KcpStartServer);
            btnKcpStopServer?.onClick.AddListener(KcpStopServer);
        }

        #region KCP_SERVER
        void KcpStartServer()
        {
            kcpServerChannel.StartServer();
        }
        void KcpStopServer()
        {
            kcpServerChannel.StopServer();
        }
        void KcpServer_OnConnected(int conv)
        {
            Debug.Log($"KCP_SERVER conv: {conv} Connected");
        }
        void KcpServer_OnDataReceived(int conv, byte[] data)
        {
            Debug.Log($"KCP_SERVER receive data from conv: {conv} . Data: {Encoding.UTF8.GetString(data)}");
        }
        void KcpServer_OnDisconnected(int conv)
        {
            Debug.LogError($"KCP_SERVER conv: {conv} Disconnected");
        }
        #endregion
    }

}