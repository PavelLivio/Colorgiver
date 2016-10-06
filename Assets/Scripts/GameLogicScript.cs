using UnityEngine;
using System.Collections;

public class GameLogicScript : MonoBehaviour {

	GameObject currentDragedGO;
	Color currentPlayerColor;
	GameObject currentDragedGhostGO;
	public LayerMask playerLayer;
	public LayerMask notPlayerLayer;
	public GameObject ghostPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100, playerLayer)){
				Debug.DrawLine(ray.origin, hit.point);
				if(hit.collider.CompareTag("Player")){
					currentDragedGhostGO = Instantiate(ghostPrefab, hit.point, Quaternion.identity) as GameObject;
					currentDragedGO = hit.collider.gameObject;
					currentPlayerColor = currentDragedGO.GetComponent<Renderer>().material.color;
					currentPlayerColor = new Color(currentPlayerColor.r, currentPlayerColor.g, currentPlayerColor.b, currentDragedGhostGO.GetComponent<Renderer>().material.color.a);
					currentDragedGhostGO.GetComponent<Renderer>().material.color = currentPlayerColor;
					//currentDragedGO.GetComponent<Renderer>().material.color = Color.red;
				}
			}
		}

		if(currentDragedGO && Input.GetMouseButton(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100, notPlayerLayer)){
				currentDragedGhostGO.transform.position = hit.point;
			}
		}

		if(currentDragedGO && Input.GetMouseButtonUp(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100, notPlayerLayer)){
				if(hit.collider.CompareTag("Enemy")){
					EnemyScript tEnemyScript = hit.collider.GetComponent<EnemyScript>();
					tEnemyScript.GetsHittedByPlayer(currentPlayerColor);
				}else{
					currentDragedGO.transform.position = hit.point;
				}
				//currentDragedGO.GetComponent<Renderer>().material.color = Color.white;
				Destroy(currentDragedGhostGO);
			}
		}
	
	}
}
