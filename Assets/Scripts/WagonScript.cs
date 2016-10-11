using UnityEngine;
using System.Collections;

public class WagonScript : MonoBehaviour {
    public Color wagonColor;

	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().material.color = wagonColor;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            wagonColor = wagonColor - (other.GetComponent<Renderer>().material.color) / 10;
            GetComponent<Renderer>().material.color = wagonColor;

            other.GetComponent<EnemyScript>().GoToHeaven();
        }
    }
}
