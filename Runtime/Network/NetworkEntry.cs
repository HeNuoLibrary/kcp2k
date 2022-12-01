using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace HeNuo.Network
{
    /// <summary>
    /// 网络管理器 启动入口
    /// </summary>
    public class NetworkEntry : MonoBehaviour
    {
        public static NetworkEntry Instance { get; private set; }

        private static NetworkManager _networkManager;
        public static INetworkManager NetworkManager { get; private set; }

        private void Awake()
        {
            Instance = this;
            _networkManager = new NetworkManager();
            NetworkManager = _networkManager;
            _networkManager.OnInitialization();
        }

        private void Update()
        {
            _networkManager.OnRefresh();
        }

        private void OnDestroy()
        {
            _networkManager.OnTermination();
        }
    }
}
