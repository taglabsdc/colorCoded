using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public enum objType
{
    collectible, powerUp, debuff, pusher
};

public class genericObjects : MonoBehaviour {

	

	public objType myIdentity;
     
    public AudioClip mySound;
    
    //collectible variables
    public float value;

    //pusher variables
    public bool visible_Pusher;
    public float pushPower;


	// Use this for initialization
	void Start () {
		if (myIdentity == objType.collectible) {
			gameObject.tag = "collectible";
			gameObject.GetComponent<BoxCollider2D> ().isTrigger = true;
		}

        if(myIdentity == objType.pusher)
        {
            gameObject.tag = "pusher";
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            visible_Pusher = true;
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


[CustomEditor(typeof(genericObjects))]
public class genericObjectsEditor : Editor
{
    override public void OnInspectorGUI()
    {
        genericObjects myGui = target as genericObjects; //reference the script that has the variables we want to use

        // These guys should be visible no matter what
        myGui.myIdentity = (objType)EditorGUILayout.EnumPopup(myGui.myIdentity);
        myGui.mySound = (AudioClip)EditorGUILayout.ObjectField("My Sound", myGui.mySound, typeof(AudioClip), true);


        //different options for different objects

        if (myGui.myIdentity == objType.pusher) // PUSH OBJECT
        {
            myGui.pushPower = EditorGUILayout.FloatField("Push Power", myGui.pushPower);
            
        }

        if(myGui.myIdentity == objType.collectible)
        {

        }
    }
}
