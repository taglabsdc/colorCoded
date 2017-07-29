using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class adv : System.Object
{
    [Tooltip("MAX SPAWN: Use to set the maximum number of objects spawned")]
    public int maxSpawn;
    //[Tooltip("")]
    [Tooltip("SPAWN RUN TIME: Use this to set the amount of time (in seconds) you want your spawner to run")]
    public float spawnRunTime;

    //[Tooltip("TRIGGER: requires a gameObject with a collider set to Is Trigger, when the player collides with trigger it starts/stops the spawner")]
    //public GameObject trigger;
}

public class spawner : MonoBehaviour {

    public GameObject spawnObj;
    [Tooltip("SPAWN DELAY: the amount of time (in seconds) between each objet spawned")]
    public float spawnDelay;
    float timer;

    public bool loopSpawn;
    [Tooltip("START ON AWAKE: Check if you want to immediately spawn objects when you start the game")]
    public bool startOnAwake;

    int current_Spawn; //current number of enemies we have already spawned
    float current_SpawnTime; //current value of the timer
    public adv advanced; //opens up our advances options panel

	// Use this for initialization
	void Start () {
        timer = 0;
       
        if (startOnAwake)
            loopSpawn = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (!gameManager.instance.won)  //make sure we haven't already won
        {
            if (advanced.maxSpawn > 0)
                loop_maxSpawn();
            if (advanced.spawnRunTime > 0)
                loop_spawnTimer();
        }
         

	}

    void loop_maxSpawn()
    {
        if (loopSpawn) //are we looping? 
        {
            if (timer < spawnDelay)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                Instantiate(spawnObj, transform.position, Quaternion.identity);
                advanced.maxSpawn--;
            }
        }
    }

    void loop_spawnTimer()
    {
        if (loopSpawn) //are we looping? 
        {
            if (timer < spawnDelay)
            {
                timer += Time.deltaTime;
            }
            else
            {
                advanced.spawnRunTime -= timer;
                timer = 0;
                Instantiate(spawnObj, transform.position, Quaternion.identity);
                
            }
        }
    }

    public void setSpawn(bool b)
    {
        loopSpawn = b;

    }
}
