using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogicScript : MonoBehaviour {

    public int TutorialLevelIndex = -1; //-1 = not tutorial;

	public static GameLogicScript i;

    public WagonScript wagonS;

	GameObject currentDragedGO;
	Color currentPlayerColor;
	GameObject currentDragedGhostGO;
    GameObject currentSelectedGO;
    public LayerMask playerLayer;
	public LayerMask notPlayerLayer;
	public GameObject ghostPrefab;
	public LineRenderer lR;

	public Transform markerT;
	public AudioHolderScript audioH;

	public GameObject canvasGO;
	public Text scoreText;
	public float score = 0;
    public Text gameOverText;
    float timer = 0;
    bool timerOn = false;
    public bool firstGotHit;
    public int enemyNumber;

    public TutorialScript tutorialScript;
    public Tutorial2Script tutorial2Script;

    // Use this for initialization
    void Awake () {
        Debug.Log(TutorialLevelIndex);
		i = this;
		canvasGO.SetActive(true);
        wagonS = FindObjectOfType<WagonScript>();
        if (TutorialLevelIndex == 0) tutorialScript = GetComponent<TutorialScript>();
        else if (TutorialLevelIndex == 1) tutorial2Script = GetComponent<Tutorial2Script>();
        //else {
        //    foreach(GameObject.FindGame)
       // }

    }

	public void EnemyDied(EnemyScript inEnemyS){
		GameLogicScript.i.audioH.Play("EnemyDestroyBling");
		UpdateScore(10*inEnemyS.tankHealth);


        if (TutorialLevelIndex == 0)
        {
            Debug.Log("firstGotHit");
            firstGotHit = true;
        }
    }

	public void UpdateScore(float inMod){
		score += inMod;
		scoreText.text = "Score: "+score;
	}
	
	// Update is called once per frame
	void Update () {

        if (timerOn && Time.time > timer)
        {
            timerOn = false;
            ReloadLevel();
        }

        if (Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 300, playerLayer)){
				Debug.DrawLine(ray.origin, hit.point);
				if(hit.collider.CompareTag("Player")){
                    currentDragedGhostGO = Instantiate(ghostPrefab, hit.point+ new Vector3(0, 0.5f, 0), Quaternion.identity) as GameObject;
					currentDragedGO = hit.collider.gameObject;
					currentPlayerColor = currentDragedGO.GetComponent<Renderer>().material.color;
					currentPlayerColor = new Color(currentPlayerColor.r, currentPlayerColor.g, currentPlayerColor.b, currentDragedGhostGO.GetComponent<Renderer>().material.color.a);
					currentDragedGhostGO.GetComponent<Renderer>().material.color = currentPlayerColor;
					//currentDragedGO.GetComponent<Renderer>().material.color = Color.red;
					lR.SetColors(currentPlayerColor, currentPlayerColor);
				}
			}
		}

		if(currentDragedGO && Input.GetMouseButton(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 300, notPlayerLayer)){
				if(currentDragedGhostGO){
					currentDragedGhostGO.transform.position = hit.point+new Vector3 (0,0.5f,0);
					lR.SetPosition(0,currentDragedGO.transform.position);
					lR.SetPosition(1,currentDragedGhostGO.transform.position);

					/*if(hit.collider.CompareTag("Enemy")){
						markerT.gameObject.SetActive(true);
						markerT.transform.position = hit.collider.transform.position;
						markerT.LookAt(Camera.main.transform);
					}else{
						markerT.gameObject.SetActive(false);
					}*/
				}
			}
		}

		if(Input.GetMouseButtonUp(0)){

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 300, playerLayer))
            {
                currentSelectedGO = hit.collider.gameObject;
                SetMarker(currentSelectedGO);
            }
            else

            if (currentDragedGO)
            {
                if (Physics.Raycast(ray, out hit, 300, notPlayerLayer))
                {
                    // if not enemy
                    if(!hit.collider.CompareTag("Enemy"))
                    {
                        currentDragedGO.transform.position = hit.point + new Vector3(0, 0.5f, 0);
                        currentSelectedGO = currentDragedGO;
                        SetMarker(currentSelectedGO);
                        if (tutorialScript) tutorialScript.MoveDone();
                    }
                  /*  else
                    {
                        markerT.gameObject.SetActive(false);
                    }*/

                    //currentDragedGO.GetComponent<Renderer>().material.color = Color.white;
                   
                }
            }
            else
            {
                if (currentSelectedGO && Physics.Raycast(ray, out hit, 300, notPlayerLayer))
                {
                    //if (hit.collider.CompareTag("Enemy")){
                        //EnemyScript tEnemyScript = hit.collider.GetComponent<EnemyScript>();
                        currentSelectedGO.GetComponent<PlayerScript>().ShootEnemy(new Vector3 (hit.point.x,3, hit.point.z));
                        //tEnemyScript.GetsHitByPlayer(currentPlayerColor);
                    //}
                }
            }
			currentDragedGO= null;
            if (currentDragedGhostGO)
            {
                Destroy(currentDragedGhostGO);
                lR.SetPosition(0, Vector3.zero);
                lR.SetPosition(1, Vector3.zero);
            }
        }
        
	}

    public void GameOver(string incolor) {
        gameOverText.text = "You ran out of " + incolor + "\n Try again!";
        timer = Time.time + 3;
        timerOn = true;
        
    }

    public void LoadNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void SetMarker(GameObject currentGO) {
        markerT.gameObject.SetActive(true);
        markerT.transform.position = currentGO.transform.position;
        markerT.LookAt(Camera.main.transform);
        markerT.parent = currentGO.transform;
    }
}
