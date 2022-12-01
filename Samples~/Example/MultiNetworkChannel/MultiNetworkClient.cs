using System.Text;
using UnityEngine;
using UnityEngine.UI;
using HeNuo.Network;

namespace HeNuo.Example
{
    public class MultiNetworkClient : MonoBehaviour
    {
        KCPClientChannel kcpClientChannel;

        [Header("KCP")]
        [SerializeField] Button btnKcpConnectC2S;
        [SerializeField] Button btnKcpDisconnectC2S;
        [SerializeField] InputField iptKcp;
        [SerializeField] Button btnKcpSendMessageC2S;

        void Start()
        {
            kcpClientChannel = new KCPClientChannel("TEST_KCP_CLIENT");

            kcpClientChannel.OnConnected += KcpClient_OnConnected;
            kcpClientChannel.OnDataReceived += KcpClient_OnDataReceived;
            kcpClientChannel.OnDisconnected += KcpClient_OnDisconnected;

            btnKcpConnectC2S?.onClick.AddListener(KcpConnectC2S);
            btnKcpDisconnectC2S?.onClick.AddListener(KcpDisconnectC2S);
            btnKcpSendMessageC2S?.onClick.AddListener(KcpSendMessageC2S);
        }

        void Update()
        {
            //channel的TickRefresh函数可自定义管理轮询，networkManager的作用是存放通道并调用TickRefresh。
            //由于存在多种网络方案的原因，通道对应的具体事件需要由使用者自定义解析，框架不提供具体数据。
            //这里保留client，由update管理轮询
            kcpClientChannel.TickRefresh();
        }
        void OnDestroy()
        {
            kcpClientChannel.AbortChannnel();
        }

        #region KCP_CLIENT
        void KcpConnectC2S()
        {
            kcpClientChannel.Connect(MultiNetworkConstants.LOCALHOSET, MultiNetworkConstants.KCP_PORT);
        }
        void KcpDisconnectC2S()
        {
            kcpClientChannel.Disconnect();
        }
        void KcpSendMessageC2S()
        {
            var msg = iptKcp?.text;
            if (!string.IsNullOrEmpty(msg))
                kcpClientChannel.SendMessage(Encoding.UTF8.GetBytes(msg));
        }
        void KcpClient_OnConnected()
        {
            Debug.Log($"KCP_CLIENT Connected");
        }
        void KcpClient_OnDataReceived(byte[] data)
        {
        }
        void KcpClient_OnDisconnected()
        {
            Debug.LogError($"KCP_CLIENT Disconnected");
        }
        #endregion
    }
}