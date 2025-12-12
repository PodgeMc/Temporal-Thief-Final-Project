using UnityEngine;

public class EchoRecorder : MonoBehaviour
{
    public Transform target; // what we record (drag the Player here)
    public float sampleRate = 50f; // how many times per second we save a moment
    public float maxDuration = 100f; // safety limit so you can’t record forever
    public EchoSpawner spawner; // who will create the echo ghost (drag GameManager here)

    public bool IsRecording { get; private set; } // are we recording right now?

    EchoRecording rec; // the current recording we’re filling
    float t = 0f; // how many seconds we’ve been recording
    float accum = 0f; // timer to space out samples evenly


    private void Start()
    {
        
    }

    void Update()
    {
        if (!IsRecording) return; // if we aren’t recording, do nothing

        t += Time.deltaTime; // add time passed since last frame
        accum += Time.deltaTime; // add to our sampling timer
        float step = 1f / sampleRate; // how often to save a moment

        while (accum >= step) // if it’s time to save a moment
        {
            accum -= step; // remove one step of time
            rec.frames.Add(new PoseFrame { // make a new saved moment
                t = t, // the time of this moment
                pos = target.position, // where we are
                rot = target.rotation // which way we face
            }); // add the moment to the list
        }

        if (t >= maxDuration) StopRecording(); // if too long, stop recording
    }

    public void StartRecording()
    {
        if (IsRecording) return; // don’t start twice
        rec = new EchoRecording(); // make a fresh empty recording
        t = 0f; accum = 0f; // reset timers
        IsRecording = true; // we are now recording
        Debug.Log("[Echo] Recording started"); // helpful message
    }

    public void StopRecording()
    {
        if (!IsRecording) return; // don’t stop if not recording
        IsRecording = false; // we are no longer recording
        rec.duration = t; // store total time the recording lasted
        Debug.Log($"[Echo] Recording stopped ({t:0.00}s)"); // helpful message
        if (rec.frames.Count > 1) spawner.SpawnEcho(rec); // if we saved anything, make a ghost
    }
}
