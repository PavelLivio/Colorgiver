﻿using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public GameObject enemyDestroyParticlePrefab;

	public Renderer bodyRenderer;
	public Renderer headRenderer;
	public TextMesh textM;
	public TextMesh textGoalM;

	public Color myColor;
	public Color goalColor;

    NavMeshAgent nav;
    Transform wagon;

    // Use this for initialization
    void Start () {
		headRenderer.material.color = goalColor;
		bodyRenderer.material.color = myColor;
		textGoalM.text = "R"+goalColor.r+" G"+goalColor.g+" B"+goalColor.b;
	}
    void Awake ()
    {
        wagon = GameObject.FindGameObjectWithTag("Wagon").transform;

        nav = GetComponent<NavMeshAgent>();
    }
	public void GetsHittedByPlayer (Color inColor ) {
		if(compareColors(goalColor, inColor)){
			Destroy(gameObject);
			GameObject particleGO = Instantiate( enemyDestroyParticlePrefab, transform.position, enemyDestroyParticlePrefab.transform.rotation) as GameObject;
			particleGO.GetComponent<ParticleSystem>().startColor = inColor;
		}else{
			bodyRenderer.material.color = inColor;
			textM.text = "R"+inColor.r+" G"+inColor.g+" B"+inColor.b;
		}
	}

	bool compareColors(Color inCol1, Color inCol2){
		if(inCol1.r == inCol2.r && inCol1.g == inCol2.g && inCol1.b==inCol2.b)
			return true;
		else
			return false;
	}
    void Update()
    {
        nav.SetDestination(wagon.position);
    }
}

    

