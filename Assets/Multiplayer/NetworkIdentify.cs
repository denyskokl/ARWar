using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkIdentify : NetworkBehaviour {

    void Start()
    {
        if (isLocalPlayer)
        {
            GetComponent<PlayerControll>().enabled = true;
        }
    }
}
