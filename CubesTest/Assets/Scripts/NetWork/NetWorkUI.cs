using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetWorkUI : MonoBehaviour
{
    [SerializeField] private Button hostB;
    [SerializeField] private Button clientB;
        
    void Awake()
    {
        hostB.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });

        clientB.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
    }
}
