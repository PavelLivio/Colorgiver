using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public Renderer bodyRenderer;
	public Renderer headRenderer;
	public TextMesh textM;

	public float colorR;
	public float colorG;
	public float colorB;

	public float colorTargetR;
	public float colorTargetG;
	public float colorTargetB;

	// Use this for initialization
	void Start () {
		headRenderer.material.color = new Color(colorTargetR, colorTargetG, colorTargetB);
	}
	
	public void GetsHittedByPlayer (Color inColor ) {
		bodyRenderer.material.color = inColor;
		textM.text = "R"+inColor.r+" G"+inColor.g+" B"+inColor.b;
	}
}
