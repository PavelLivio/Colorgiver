using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float colorRed;
	public float colorGreen;
	public float colorBlue;

	Renderer myRenderer;

	// Use this for initialization
	void Start () {
		myRenderer = GetComponent<Renderer>();
		myRenderer.material.color = new Color(colorRed, colorGreen, colorBlue);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShootColor(){
		
	}
}
