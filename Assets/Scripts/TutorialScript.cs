using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour {

    public Text tutorialText;
    float timer = 0;

    string nextText = "";
    bool showNextText = false;
    bool firstMoveDone = false;
    bool firstPickedUp = false;
    bool activateNextEnemy;

    public GameObject firstEnemyGO;

    // Use this for initialization
    void Start ()
    {
        if (GameLogicScript.i.TutorialLevelIndex >= 0)
        {
            tutorialText.text = "Move the black sphere by drag and drop!";
        }
        else {
            enabled = false;
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (showNextText && Time.time > timer) {
            Debug.Log("show next");
            tutorialText.text = nextText;
            showNextText = false;
            if (activateNextEnemy)
            {
                foreach(GameObject powerUpGO in GameObject.FindGameObjectsWithTag("PowerUp"))
                {
                    Destroy(powerUpGO);
                }
                firstEnemyGO.SetActive(true);
                firstEnemyGO.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position + Vector3.forward *60;

                showNextText = true;
                nextText = "Here comes an enemy!\nDestroy it by draging and droping the player on it!";
                timer = Time.time + 2;
                activateNextEnemy = false;
            }
        }
	}

    public void MoveDone() {
        if (firstMoveDone) return;
        timer = Time.time + 2;
        tutorialText.text = "Great!";
        showNextText = true;
        nextText = "Pick up a green box!";
        Debug.Log("timer: "+ timer + " time: "+Time.time);
        firstMoveDone = true;
    }

    public void PowerUpPickedUp()
    {
        if (firstPickedUp) return;
        timer = Time.time + 2;
        tutorialText.text = "Great!";
        showNextText = true;
        activateNextEnemy = true;
        nextText = "Now to the real action!";
        firstPickedUp = true;

    }
}
