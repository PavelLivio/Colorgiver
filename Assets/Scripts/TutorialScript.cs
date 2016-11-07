using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{

    public Text tutorialText;
    float timer = 0;

    bool timerOn;
    string nextText = "";
    bool showNextText = false;
    bool firstMoveDone = false;
    bool firstPickedUp = false;
    bool activateNextEnemy;
    bool loadNextLevel = false;

    public GameObject firstEnemyGO;

    // Use this for initialization
    void Start()
    {
        if (GameLogicScript.i.TutorialLevelIndex == 0)
        {
            tutorialText.text = "Move the black sphere by drag and drop!";
        }
        else
        {
            enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (GameLogicScript.i.firstGotHit)
        {
            DoActionAfterFirstGotHit();
        }
        if (timerOn && Time.time > timer)
        {
            timerOn = false;

            Debug.Log(" execute timer text // time.time = " + Time.time + " timer = " + timer);
            if (showNextText) {
                Debug.Log("showText");
                tutorialText.text = nextText;
                showNextText = false;
            }

            if (loadNextLevel)
            {
                loadNextLevel = false;
                GameLogicScript.i.LoadNextLevel();
            }
            else if (activateNextEnemy)
            {
                DoActionAfterEnemyActivation();
            }
        }
    }

    void DoActionAfterFirstGotHit() {
        Debug.Log("firstGotHit ausfuehren (activateNextEnemy: " + activateNextEnemy + ")");
        showNextText = true;
        tutorialText.text = "Great! Let's move on to the next Tutorial";
        GameLogicScript.i.firstGotHit = false;
        SetTimer(2);
        loadNextLevel = true;
    }

    void DoActionAfterEnemyActivation() {
        foreach (GameObject powerUpGO in GameObject.FindGameObjectsWithTag("PowerUp"))
        {
            Destroy(powerUpGO);
        }
        firstEnemyGO.SetActive(true);
        firstEnemyGO.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position + Vector3.forward * 60;

        showNextText = true;
        nextText = "Here comes an enemy!\nDestroy it by draging and droping the player on it!";
        SetTimer(2);
        activateNextEnemy = false;
        Debug.Log("activateNextEnemy ausfuehren (loadNextLevel: " + activateNextEnemy + ")");
    }

    void SetTimer(int inTime) {
        timerOn = true;
        timer = Time.time + inTime;
        Debug.Log("time.time = " + Time.time + " timer = " + timer);

    }
    public void MoveDone()
    {
        if (firstMoveDone) return;
        SetTimer(2);
        tutorialText.text = "Great!";
        showNextText = true;
        nextText = "Pick up a green box!";
        Debug.Log("timer: " + timer + " time: " + Time.time);
        firstMoveDone = true;
    }

    public void PowerUpPickedUp()
    {
        if (firstPickedUp) return;
        SetTimer(2);
        tutorialText.text = "Great!";
        showNextText = true;
        activateNextEnemy = true;
        nextText = "Now to the real action!";
        firstPickedUp = true;

    }

   
}