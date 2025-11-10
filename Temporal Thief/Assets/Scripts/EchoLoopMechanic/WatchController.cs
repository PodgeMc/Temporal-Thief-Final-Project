using UnityEngine;

public class WatchController : MonoBehaviour
{
    public EchoRecorder recorder; // drag the Player (with EchoRecorder) here

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // when we press E on the keyboard
        {
            if (!recorder.IsRecording) recorder.StartRecording(); // start recording if we weren’t
            else recorder.StopRecording(); // otherwise stop and spawn a ghost
        }
    }
}
