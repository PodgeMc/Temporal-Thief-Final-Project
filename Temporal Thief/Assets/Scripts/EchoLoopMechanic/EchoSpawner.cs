using UnityEngine;
using System.Collections.Generic;

public class EchoSpawner : MonoBehaviour
{
    public EchoGhost ghostPrefab; // drag your ghost prefab here
    public int maxEchoes = 5; // how many ghosts can exist at once

    readonly List<EchoGhost> active = new(); // list of ghosts we spawned

    public void SpawnEcho(EchoRecording rec)
    {
        if (active.Count >= maxEchoes) // if too many ghosts already
        {
            Destroy(active[0].gameObject); // delete the oldest one
            active.RemoveAt(0); // remove it from the list
        }

        var g = Instantiate(ghostPrefab, rec.frames[0].pos, rec.frames[0].rot); // make a new ghost at the first frame
        g.Init(rec); // give the ghost the recording to follow
        g.OnFinished += GhostFinished; // when it ends, call us
        active.Add(g); // remember this ghost in our list
    }

    void GhostFinished(EchoGhost g)
    {
        active.Remove(g); // forget this ghost
        Destroy(g.gameObject); // remove it from the scene
    }
}
