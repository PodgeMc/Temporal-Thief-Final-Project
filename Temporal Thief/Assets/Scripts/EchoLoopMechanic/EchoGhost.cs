using UnityEngine;

public class EchoGhost : MonoBehaviour
{
    public float playbackSpeed = 1f; // 1 means play at normal speed

    EchoRecording data; // the recording we will follow
    int i = 0; // which frame we are on
    float t = 0f; // how many seconds we’ve been playing

    public System.Action<EchoGhost> OnFinished; // tells spawner when we are done

    public void Init(EchoRecording rec)
    {
        data = rec; // remember the recording
        i = 0; t = 0f; // start from the beginning
        if (data.frames.Count > 0) // if we have at least one frame
        {
            transform.position = data.frames[0].pos; // move to the first spot
            transform.rotation = data.frames[0].rot; // face the first direction
        }
    }

    void Update()
    {
        if (data == null || data.frames.Count == 0) return; // nothing to play

        t += Time.deltaTime * playbackSpeed; // time is moving forward

        while (i + 1 < data.frames.Count && data.frames[i + 1].t <= t) i++; // step to the correct frame

        PoseFrame a = data.frames[i]; // current frame we are on
        if (i + 1 < data.frames.Count) // if there is a next frame
        {
            PoseFrame b = data.frames[i + 1]; // the next frame
            float span = Mathf.Max(0.0001f, b.t - a.t); // time between frames (avoid zero)
            float u = Mathf.Clamp01((t - a.t) / span); // how far between a and b (0..1)
            transform.position = Vector3.Lerp(a.pos, b.pos, u); // slide smoothly from a to b
            transform.rotation = Quaternion.Slerp(a.rot, b.rot, u); // turn smoothly from a to b
        }
        else // we are at the last frame
        {
            transform.position = a.pos; // stay at the last spot
            transform.rotation = a.rot; // keep the last rotation
        }

        if (t >= data.duration) OnFinished?.Invoke(this); // tell spawner we finished
    }
}
