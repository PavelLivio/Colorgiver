using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float colorRed;
	public float colorGreen;
	public float colorBlue;

	float speed = 4;
	public Rigidbody rB;

	Renderer myRenderer;

	// Use this for initialization
	void Start () {
		myRenderer = GetComponent<Renderer>();
		myRenderer.material.color = new Color(colorRed, colorGreen, colorBlue);
	}
	

	void FixedUpdate () {
		rB.MovePosition(rB.position + transform.forward * speed * Time.fixedDeltaTime);
	}

	public void ShootColor(){
		
	}
}
