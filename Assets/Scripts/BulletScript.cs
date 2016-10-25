using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
    Transform enemyTransform;
    public Rigidbody rb;
    public float speed;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
    public void EnemyWillGetShot(EnemyScript enemy, Color color) {
        GetComponent<Renderer>().material.color = color;
        enemyTransform = enemy.transform;
        GetComponent<TrailRenderer>().material.SetColor("_TintColor", color);
    }
    void FixedUpdate() {

        transform.LookAt(enemyTransform);
        rb.MovePosition(rb.position + transform.forward * speed * Time.fixedDeltaTime);
         
    }
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyScript>().GetsHitByPlayer(GetComponent<Renderer>().material.color);
            Destroy(gameObject);
        }
    }
}
