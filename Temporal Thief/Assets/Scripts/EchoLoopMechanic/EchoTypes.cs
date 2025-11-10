using UnityEngine;
using System.Collections.Generic;

[System.Serializable] public struct PoseFrame
{
    public float t; // time since we started recording (in seconds)
    public Vector3 pos; // where the player was at time t
    public Quaternion rot; // which way the player was facing at time t
}

public class EchoRecording
{
    public List<PoseFrame> frames = new(); // a list of all saved moments
    public float duration = 0f; // how long the recording lasts
}
