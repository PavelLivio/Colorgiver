using UnityEngine;
using System.Collections;

public class WagonScript : MonoBehaviour {
    public Color wagonColor;
    public TextMesh wagonText;

	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().material.color = wagonColor;
        wagonText.text = "R" + (Mathf.RoundToInt(wagonColor.r*100) /100f) + " G" + (Mathf.RoundToInt(wagonColor.g * 100) / 100f) + " B" + (Mathf.RoundToInt(wagonColor.b * 100) / 100f) ;


    }
	
	// Update is called once per frame
	void Update () {
	    
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.enabled = false;
            wagonColor = wagonColor - (other.GetComponent<EnemyScript>().goalColor) / 10;
            GetComponent<Renderer>().material.color = wagonColor;
            wagonText.text = "R" + (Mathf.RoundToInt(wagonColor.r * 100) / 100f) + " G" + (Mathf.RoundToInt(wagonColor.g * 100) / 100f) + " B" + (Mathf.RoundToInt(wagonColor.b * 100) / 100f);

            other.GetComponent<EnemyScript>().GoToHeaven();
        }
    }
}
