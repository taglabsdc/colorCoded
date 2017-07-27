using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour {

    public GameObject spawnObj;
    public float spawnDelay;
    float timer;
    bool loopSpawn;
   

	// Use this for initialization
	void Start () {
        timer = 0;		
	}
	
	// Update is called once per frame
	void Update () {


        if (loopSpawn)
        {
            if(timer < spawnDelay)
            {
                timer += Time.deltaTime;
                //Instantiate object
            }else
            {
                timer = 0;
            }
            
        }
	}

    public void setSpawn(bool b)
    {
        loopSpawn = b;
    }
}
