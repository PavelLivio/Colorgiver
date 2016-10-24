using UnityEngine;
using System.Collections;

public class PowerUpScript : MonoBehaviour {
    public Color colorToPlayer;
    // Use this for initialization
    public Rigidbody rB;
    public float speed;
    public GameObject cube;
	void Start () {
        cube.GetComponent<Renderer>().material.color = colorToPlayer;
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
    void FixedUpdate()
    {
        rB.MovePosition(rB.position + transform.forward * speed * Time.fixedDeltaTime);
    }

}
