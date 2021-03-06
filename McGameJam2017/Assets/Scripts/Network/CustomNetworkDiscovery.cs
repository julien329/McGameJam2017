﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkDiscovery : NetworkDiscovery {
    public bool isConnected = false;

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        if(!isConnected)
        {
            Debug.Log("This data comes from: " + fromAddress);
            Debug.Log("This data has been received: " + data);
            //Parse le string
            string ip = fromAddress.Remove(0, 7);
            Debug.Log(ip);
            //Network.Connect(ip, 25000);
            CustomNetworkManager.singleton.networkAddress = ip;
            CustomNetworkManager.singleton.StartClient();

            isConnected = true;
        }
    }
}
