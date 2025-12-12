using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParadoxManager : MonoBehaviour
{
    //other scripts need tro interact with this
    //visual effect after being caught
    //trigger catching the player
    public static ParadoxManager Instance;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        Instance = this;
    }

    //Lose trigger
    public void TriggerParadox(Vector3 echoPos)
    {
        Debug.Log("Paradox trigger! Echo saw player.");
        //sound effects, animations, etc.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
