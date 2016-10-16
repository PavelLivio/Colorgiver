﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameLogicScript : MonoBehaviour {

	public static GameLogicScript i;

	GameObject currentDragedGO;
	Color currentPlayerColor;
	GameObject currentDragedGhostGO;
	public LayerMask playerLayer;
	public LayerMask notPlayerLayer;
	public GameObject ghostPrefab;
	public LineRenderer lR;

	public Transform markerT;
	public AudioHolderScript audioH;

	public GameObject canvasGO;
	public Text scoreText;
	float score = 0;


    // Use this for initialization
    void Awake () {
		i = this;
		canvasGO.SetActive(true);
	}

	public void EnemyDied(EnemyScript inEnemyS){
		GameLogicScript.i.audioH.Play("EnemyDestroyBling");
		UpdateScore(10*inEnemyS.tankHealth);
	}

	public void UpdateScore(float inMod){
		score += inMod;
		scoreText.text = "Score: "+score;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100, playerLayer)){
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

			if (Physics.Raycast(ray, out hit, 100, notPlayerLayer)){
				if(currentDragedGhostGO){
					currentDragedGhostGO.transform.position = hit.point+new Vector3 (0,0.5f,0);
					lR.SetPosition(0,currentDragedGO.transform.position);
					lR.SetPosition(1,currentDragedGhostGO.transform.position);

					if(hit.collider.CompareTag("Enemy")){
						markerT.gameObject.SetActive(true);
						markerT.transform.position = hit.collider.transform.position;
						markerT.LookAt(Camera.main.transform);
					}else{
						markerT.gameObject.SetActive(false);
					}
				}
			}
		}

		if(currentDragedGO && Input.GetMouseButtonUp(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100, notPlayerLayer)){
				if(hit.collider.CompareTag("Enemy")|| hit.collider.CompareTag("Tank"))
                {
					EnemyScript tEnemyScript = hit.collider.GetComponent<EnemyScript>();
					tEnemyScript.GetsHitByPlayer(currentPlayerColor);
				}else{
                    currentDragedGO.transform.position = hit.point+new Vector3 (0,0.5f,0);
                }
				//currentDragedGO.GetComponent<Renderer>().material.color = Color.white;
				Destroy(currentDragedGhostGO);

				lR.SetPosition(0,Vector3.zero);
				lR.SetPosition(1,Vector3.zero);

				markerT.gameObject.SetActive(false);
			}
		}
	
	}
}
