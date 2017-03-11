﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkDiscovery : NetworkDiscovery {

    void Start()
    {
        broadcastData = "Hello";
    }

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        Debug.Log("This data comes from: " + fromAddress);
        Debug.Log("This data has been received: " + data);
        //Parse le string
        string ip = fromAddress.Remove(0, 7);
        Network.Connect(fromAddress, 7777);
    }
}