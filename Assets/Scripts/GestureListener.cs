using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityOSC;

public class GestureListener : MonoBehaviour
{
    private OSCServer _oscServer;
    
    // Start is called before the first frame update
    void Start()
    {
        _oscServer = new OSCServer(12000);
        _oscServer.PacketReceivedEvent += OscServerOnPacketReceivedEvent;
        _oscServer.Connect();
    }

    private void OscServerOnPacketReceivedEvent(OSCServer sender, OSCPacket packet)
    {
        if(!packet.Address.Equals("/wek/outputs"))
            return;

        var gestureStart = (float) packet.Data[0];
        var gestureEnd = (float) packet.Data[1];

        if (gestureStart > 0.9 && gestureEnd < 0.1)
        {
            Debug.Log("Starting gesture!");
        }

        if (gestureStart < 0.1 && gestureEnd > 0.9)
        {
            Debug.Log("Gestured ended!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
