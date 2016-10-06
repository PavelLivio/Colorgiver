using UnityEngine;
using System.Collections;

public class GameLogicScript : MonoBehaviour {

	GameObject currentDragedGO;
	Color currentPlayerColor;
	GameObject currentDragedGhostGO;
	public LayerMask playerLayer;
	public LayerMask notPlayerLayer;
	public GameObject ghostPrefab;
	public LineRenderer lR;

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
					lR.SetColors(currentPlayerColor, currentPlayerColor);
				}
			}
		}

		if(currentDragedGO && Input.GetMouseButton(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100, notPlayerLayer)){
				if(currentDragedGhostGO){
					currentDragedGhostGO.transform.position = hit.point;
					lR.SetPosition(0,currentDragedGO.transform.position);
					lR.SetPosition(1,currentDragedGhostGO.transform.position);
				}
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

				lR.SetPosition(0,Vector3.zero);
				lR.SetPosition(1,Vector3.zero);
			}
		}
	
	}
}
