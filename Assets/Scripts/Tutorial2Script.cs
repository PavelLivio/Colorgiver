using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Tutorial2Script : MonoBehaviour
{


    public Text tutorialText;
    float timer = 0;

    string nextText = "";
    bool loadNextLevel;
    bool redSpawned;
    bool showNextText = false;
    bool bigEnemy;
    bool player2;
    bool nextLevel;
    GameObject tank;
    public GameObject tankprefab;
    GameObject playerOb;
    public GameObject playerprefab;
    GameObject redPickup;
    public GameObject pickupprefab;
    public GameObject enemyprefab;
    GameObject enemyRed;
    

    // Use this for initialization
    void Start()
    {
        if (GameLogicScript.i.TutorialLevelIndex == 1)
        {
            tutorialText.text = "Your Goal is to protect this Wagon! The enemies try to take his color.";
            
            
            SecondText();

        }
        else
        {
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (showNextText && Time.time > timer)
        {
            Debug.Log("show next");
            tutorialText.text = nextText;
            showNextText = false;
            


        }
        if (bigEnemy && (Time.time > timer || GameLogicScript.i.score == 30))
        {
            tutorialText.text = nextText;
            showNextText = false;
            tank = Instantiate(tankprefab, GameObject.FindGameObjectWithTag("Wagon").transform.position + Vector3.forward * 60, Quaternion.identity) as GameObject;
            bigEnemy = false;
            SecondPlayer();
        }
        if (player2 == true && (Time.time > timer || GameLogicScript.i.score == 60))
        {
            tutorialText.text = nextText;
            showNextText = false;
            playerOb = Instantiate(playerprefab, GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(-5, 0, 0), Quaternion.identity) as GameObject;
            player2 = false;
            RedPickUpSpawn();
            RedEnemySpawn();
        }
        if (redSpawned == true && (Time.time > timer || GameLogicScript.i.score == 90)) {
            tutorialText.text = nextText;
            showNextText = false;
            redSpawned = false;
            YellowEnemySpawn();
        }
        if (nextLevel && Time.time >timer)
        {
            Debug.Log("asdg");
            nextLevel = false;
            tutorialText.text = nextText;
            showNextText = false;
            LoadNextLevel();
        }
        if (loadNextLevel&& Time.time >timer ) {
            GameLogicScript.i.LoadNextLevel();
        }
    }
    void SecondText() {
        timer = Time.time + 5;
        nextText = "Each Enemy gives 10 points.";
        showNextText = true;
        BigEnemySpawns();

    }
    void BigEnemySpawns() {
        timer = Time.time + 30;
        nextText = "Here comes a big one. He needs 3 shots to die!";
       // showNextText = true;
        bigEnemy = true;
        
    }
    void SecondPlayer() {
        timer = Time.time + 13;
        nextText = "You get a second player! Pick with him the red powerup to kill the red enemies!";
       // showNextText = true;
        player2 = true;

        
    }
    void RedPickUpSpawn() {
        redPickup  = Instantiate(pickupprefab, GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(-5, 0, 0)+ Vector3.forward * 20, Quaternion.identity) as GameObject;
        redPickup.GetComponent<PowerUpScript>().colorToPlayer.r = 1;
        redPickup.GetComponent<PowerUpScript>().colorToPlayer.g = 0;
    }

    void RedEnemySpawn() {
        enemyRed = Instantiate(enemyprefab, GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(-20, 0, 0) + Vector3.forward * 80, Quaternion.identity) as GameObject;
        enemyRed .GetComponent<EnemyScript >().goalColor .r = 1;
        enemyRed.GetComponent<EnemyScript>().goalColor.g = 0;
        enemyRed = Instantiate(enemyprefab, GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(20, 0, 0) + Vector3.forward * 70, Quaternion.identity) as GameObject;
        enemyRed.GetComponent<EnemyScript>().goalColor.r = 1;
        enemyRed.GetComponent<EnemyScript>().goalColor.g = 0;
        enemyRed = Instantiate(enemyprefab, GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0, 0, 0) + Vector3.forward * 100, Quaternion.identity) as GameObject;
        enemyRed.GetComponent<EnemyScript>().goalColor.r = 1;
        enemyRed.GetComponent<EnemyScript>().goalColor.g = 0;
        showNextText = true;
        redSpawned = true;
        timer = Time.time + 20;
        nextText = "Here comes a yellow Enemy!/nHe needs a green and a red shot!";
    }
    void YellowEnemySpawn() {
        enemyRed = Instantiate(enemyprefab, GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0, 0, 0) + Vector3.forward * 80, Quaternion.identity) as GameObject;
        enemyRed.GetComponent<EnemyScript>().goalColor.r = 1;
        enemyRed.GetComponent<EnemyScript>().goalColor.g = 1;
        redPickup = Instantiate(pickupprefab, GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(-15, 0, 0) + Vector3.forward * 40, Quaternion.identity) as GameObject;
        redPickup.GetComponent<PowerUpScript>().colorToPlayer.r = 1;
        redPickup.GetComponent<PowerUpScript>().colorToPlayer.g = 0;
        redPickup = Instantiate(pickupprefab, GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(15, 0, 0) + Vector3.forward * 40, Quaternion.identity) as GameObject;
        redPickup.GetComponent<PowerUpScript>().colorToPlayer.r = 0;
        redPickup.GetComponent<PowerUpScript>().colorToPlayer.g = 1;
        showNextText = true;
        nextLevel = true;
        timer = Time.time + 10;
        nextText = "You finished the tutorial, now try your first Level.";
    }
    void LoadNextLevel()
    {
        timer = Time.time + 7;
        loadNextLevel = true;

    }
}
