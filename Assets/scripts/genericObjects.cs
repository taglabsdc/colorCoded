using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genericObjects : MonoBehaviour {

	public enum objType {collectible, powerUp, debuff, door};

	public objType myIdentity;

    //collectible variables
    public AudioClip mySound;


	// Use this for initialization
	void Start () {
		if (myIdentity == objType.collectible) {
			gameObject.tag = "collectible";
			gameObject.GetComponent<BoxCollider2D> ().isTrigger = true;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

     public void playSound()
    {
        Debug.Log(gameObject.name);
    }
}
