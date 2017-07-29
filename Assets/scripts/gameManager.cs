using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public enum winSetting { collectWin, enemyWin, itemWin };



public class gameManager : MonoBehaviour {

    public static gameManager instance = null;

    [Tooltip("NEXT LEVEL NAME: type in the name of the next level (case sensitive), will load next scene the win function is used")]
    public string nextLevelName;

    [Tooltip("This should be found as a text Object under the canvas_UserInterace prefab /Assets/prefabs")]
    public Text winText;

    [Tooltip(" Collect Win: find enough collectibles to win. \nEnemy Win: defeat enough enemies to win. \nItem Win: grab an object or run into object (door, castle, flag) to win")]
    public winSetting winCondition;

    public float nextLevelDelay;

    //CollectWin elements
    public int collectCount;
    public int current_Collect;
    [Tooltip("This is the UI text object that will display how many objects you've collected")]
    public Text collectUI;

    //EnemyWin elements
    public int enemyCount;
    public int current_Enemy;

    public GameObject questItem;

    public Text text_PlayerHealth;

    public bool won;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //GameObject p = GameObject.FindGameObjectWithTag("Player");
        //p.SetActive(true);
    }

            // Use this for initialization
    void Start () {
        
        winText.gameObject.SetActive(false);
        current_Enemy = 0;
        current_Collect = 0;
        won = false;
        collectUI.text = "x"+current_Collect;

        
	}
	
	// Update is called once per frame
	void Update () {
        if(winCondition == winSetting.enemyWin)
        {
            if (current_Enemy == enemyCount)
            {
                runWin();
            }
        }
        if(winCondition == winSetting.collectWin)
        {
            if (current_Collect == collectCount)
            {
                runWin();
            }
        }

        	
	}

    public void runWin()
    {
        won = true;
        winText.text = "You Win";
        winText.gameObject.SetActive(true);
        
        if(nextLevelName != null)
        {
            if (!string.IsNullOrEmpty(nextLevelName))
            {
                Invoke("loadLevel", nextLevelDelay);
            }
        }     
    }

    public void runLost()
    {
        winText.text = "Game Over";
        winText.gameObject.SetActive(true);
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        p.SetActive(false);
        
           
        Invoke("restartLevel", nextLevelDelay);
    }

    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void loadLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }
}



[CustomEditor(typeof(gameManager))]
public class gameManagerEditor : Editor
{
    override public void OnInspectorGUI()
    {
        gameManager GM = target as gameManager;

        GM.nextLevelName = EditorGUILayout.TextField("Next Level Name", GM.nextLevelName);

        GM.winText = (Text)EditorGUILayout.ObjectField("Win Text", GM.winText, typeof(Text), true);

        GM.nextLevelDelay = EditorGUILayout.FloatField("Next Level Delay", GM.nextLevelDelay);

        GM.winCondition = (winSetting)EditorGUILayout.EnumPopup(GM.winCondition);

        GM.collectUI = (Text)EditorGUILayout.ObjectField("Collectible Text Object", GM.collectUI, typeof(Text), true);

        GM.text_PlayerHealth = (Text)EditorGUILayout.ObjectField("Player Health Text Object", GM.text_PlayerHealth, typeof(Text), true);


        //*******WIN CONDITION*************
        if (GM.winCondition == winSetting.collectWin)
        {
            GM.collectCount = EditorGUILayout.IntField("Collect Count: ", GM.collectCount);
            
        }
        if(GM.winCondition == winSetting.enemyWin)
        {
            GM.enemyCount = EditorGUILayout.IntField("Enemy Count: ", GM.enemyCount);
            
        } 

        if(GM.winCondition == winSetting.itemWin)
        {
            GM.questItem = (GameObject)EditorGUILayout.ObjectField("Quest Item", GM.questItem, typeof(GameObject), true);
            if(GM.questItem != null)
            {
                GM.questItem.tag = "victory";
                if(GM.questItem.GetComponent<BoxCollider2D>() != null)
                {
                    GM.questItem.GetComponent<BoxCollider2D>().isTrigger = true;
                }
            }
            
        } //End of ITEM WIN

    }

}

