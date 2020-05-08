using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using DefaultNamespace;
using Leap;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityOSC;

public class LeapMotionController : MonoBehaviour
{
    Controller _controller;
    private OSCClient _oscClient;
    private GameObject[] _fingers;
    
    public GameObject palmCenter;
    public GameObject thumbFinger;
    public GameObject indexFinger;
    public GameObject middleFinger;
    public GameObject ringFinger;
    public GameObject pinkyFinger;

    void Start ()
    {
        _controller = new Controller();
        _oscClient = new OSCClient(IPAddress.Loopback, 6448);
        _fingers = new[] {thumbFinger, indexFinger, middleFinger, ringFinger, pinkyFinger};
        
        Debug.Log($"Device Count: {_controller.Devices.Count}");
    }

    void Update ()
    {
        var frame = _controller.Frame();

        if (frame.Hands.Count > 0)
        {
            UpdateHandPosition(frame);
            SendOSCData();
        }
    }

    private void SendOSCData()
    {
        // calculate distances
        var handDistances = _fingers.Select(e =>
            Vector3.Distance(palmCenter.transform.position, e.transform.position)).ToArray();

        var msg = new OSCMessage("/wek/inputs");
        foreach (var handDistance in handDistances)
        {
            msg.Append(handDistance);
        }
        _oscClient.Send(msg);
    }

    private void UpdateHandPosition(Frame frame)
    {
        // get first hand
        var hand = frame.Hands.First();

        palmCenter.transform.position = hand.PalmPosition.ToUnityVector();

        for (var i = 0; i < _fingers.Length; i++)
        {
            _fingers[i].transform.position = hand.Fingers[i].TipPosition.ToUnityVector();
        }
    }
}
