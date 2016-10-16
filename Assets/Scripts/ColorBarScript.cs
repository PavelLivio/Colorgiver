using UnityEngine;
using System.Collections;

public class ColorBarScript : MonoBehaviour {

    public int GreenShots= 10;
    public bool CanShootGreen=true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	   
	}

    void CanShoot()
    {
        if (GreenShots > 0)
        {
            CanShootGreen = true;
        }
    }
}
