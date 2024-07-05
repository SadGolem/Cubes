using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetWorkUI : MonoBehaviour
{
    [SerializeField] private Button hostB;
    [SerializeField] private Button clientB;
    public event Action isHosting;
    void Awake()
    {
        hostB.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
            isHosting?.Invoke();
        });

        clientB.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
    }
}
