using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayerColorScript : MonoBehaviour {
    public int startingColor = 10;                            
    public int currentColor;                                  
    public Slider colorSlider;
    public Transform player;
                                                                // Use this for initialization
    void Start () {

       
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(colorSlider.transform.position);
        var wantedPos = Camera.main.WorldToViewportPoint(player.position);
        transform.position = wantedPos;
    }
}
