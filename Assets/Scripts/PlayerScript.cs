using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public Color col;

    public ColorBarScript colorBarScript;

	float speed;
	public Rigidbody rB;

    Renderer myRenderer;

	// Use this for initialization
	void Start () {
		myRenderer = GetComponent<Renderer>();
        Col = col;
        speed = GameLogicScript.i.wagonS.speed;
		//myRenderer.material.color = new Color(Col.r, Col.g, Col.b);
	}
	

	void FixedUpdate () {
		rB.MovePosition(rB.position + transform.forward * speed * Time.fixedDeltaTime);
	}

    Color Col {
        get
        {
            return col;
        }
        set
        {
            col = new Color(Mathf.Clamp01(value.r), Mathf.Clamp01(value.g), Mathf.Clamp01(value.b), Mathf.Clamp01(value.a));
            Debug.Log("col: " + col);
            myRenderer.material.color = col;
            colorBarScript.SetColor(col);
        }
    }

    public void ShootEnemy(EnemyScript enemy) {
        if (CanShoot())
        {
            Color shootColor= Color.black;
            if (Col.r > 0f) {
                shootColor.r = 1;
            }
            if (Col.b > 0f)
            {
                shootColor.b = 1;
            }
            if (Col.g > 0f)
            {
                shootColor.g = 1;
            }
            Col -= shootColor / 10f;
            enemy.GetsHitByPlayer(shootColor);

        }

    }

    public bool CanShoot()
    {
        if (Col.g > 0f || Col.b > 0f || Col.r > 0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PowerUp")){
            Col += other.gameObject.GetComponent<PowerUpScript >().colorToPlayer;
            Destroy(other.gameObject );
        }
    }
}
