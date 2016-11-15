using UnityEngine;
using System.Collections;

public class WagonScript : MonoBehaviour {
    public Color wagonColor;
    public TextMesh wagonText;

	public Renderer wheelRendererR;
	public Renderer wheelRendererG;
	public Renderer wheelRendererB;
    public int tankGotHit= 0;

	public Rigidbody rB;
	public float speed;

	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().material.color = wagonColor;
        wagonText.text = "R" + (Mathf.RoundToInt(wagonColor.r*100) /100f) + " G" + (Mathf.RoundToInt(wagonColor.g * 100) / 100f) + " B" + (Mathf.RoundToInt(wagonColor.b * 100) / 100f) ;
		AdjustWheelColor();
    }

	void AdjustWheelColor(){
		wheelRendererR.material.color = new Color(wagonColor.r, 0,0,1);
		wheelRendererG.material.color = new Color(0,wagonColor.g,0,1);
		wheelRendererB.material.color = new Color(0,0,wagonColor.b,1);
	}
	
	void FixedUpdate () {
		rB.MovePosition(rB.position + transform.forward * speed * Time.fixedDeltaTime);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")|| other.CompareTag("Tank"))
        {
            other.enabled = false;
            wagonColor = wagonColor - (other.GetComponent<EnemyScript>().goalColor) / 10;
            GetComponent<Renderer>().material.color = wagonColor;
			AdjustWheelColor();
            tankGotHit += 1;
            wagonText.text = "R" + (Mathf.RoundToInt(wagonColor.r * 100) / 100f) + " G" + (Mathf.RoundToInt(wagonColor.g * 100) / 100f) + " B" + (Mathf.RoundToInt(wagonColor.b * 100) / 100f);
            if (wagonColor.r <= 0) {
                GameLogicScript.i.GameOver("red");
            }
            else if (wagonColor.b <= 0)
            {
                GameLogicScript.i.GameOver("blue");
            }
            else if (wagonColor.g <= 0)
            {
                GameLogicScript.i.GameOver("green");
            }
            other.GetComponent<EnemyScript>().GoToHeaven();
        }
    }
}
