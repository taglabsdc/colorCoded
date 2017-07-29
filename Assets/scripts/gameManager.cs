using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;
//using AssemblyCSharp;




public class gameManager : MonoBehaviour {
	public enum winSetting { collectWin, enemyWin, itemWin };

    public static gameManager instance;



    [Tooltip("NEXT LEVEL NAME: type in the name of the next level (case sensitive), will load next scene the win function is used")]
    public string nextLevelName;

    [Tooltip("This should be found as a text Object under the canvas_UserInterace prefab /Assets/prefabs")]
    public Text winText;
	public Text coinText; 

    [Tooltip(" Collect Win: find enough collectibles to win. \nEnemy Win: defeat enough enemies to win. \nItem Win: grab an object or run into object (door, castle, flag) to win")]
    public winSetting winCondition;

    public float nextLevelDelay;

    public int collectCount;
    public int enemyCount;
    public GameObject questItem;
    public int current_Collect;
    public int current_Enemy;
    public bool won;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

            // Use this for initialization
    void Start () {
        winText.gameObject.SetActive(false);
        current_Enemy = 0;
        current_Collect = 0;
        won = false;
        Debug.Log(current_Enemy + " : " + enemyCount);
	}
	
	// Update is called once per frame
	void Update () {
        if(winCondition == winSetting.enemyWin)
        {
            if (current_Enemy == enemyCount)
            {
                won = true;
                winText.gameObject.SetActive(true);
                Invoke("loadLevel", nextLevelDelay);
            }
        }
        if(winCondition == winSetting.collectWin)
        {
            if (current_Collect == collectCount)
            {
                won = true;
                //winText.gameObject.SetActive(true);
                Invoke("loadLevel", nextLevelDelay);
            }
        }

        	
	}



    public void loadLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }

	public void changeCoinText(){


		coinText.text = "Test";
	}
}

