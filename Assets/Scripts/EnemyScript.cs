using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public GameObject enemyDestroyParticlePrefab;

	public Renderer bodyRenderer;
	public Renderer headRenderer;
	public TextMesh textM;
	public TextMesh textGoalM;



    public Rigidbody enemyrb;
    public float upSpeed= 4f;
    bool isGoingHeaven;

	public Color myColor;
	public Color goalColor;
    public Color currentColor;


    public int tankHealth;
    public float forwardSpeed;

    //NavMeshAgent nav;
    Transform wagon;

    // Use this for initialization
    void Start () {
		headRenderer.material.color = goalColor;
		bodyRenderer.material.color = myColor;
		textGoalM.text = "R"+goalColor.r+" G"+goalColor.g+" B"+goalColor.b;
        enemyrb = GetComponent<Rigidbody>();
	}
    void Awake ()
    {
        wagon = GameObject.FindGameObjectWithTag("Wagon").transform;

 //       nav = GetComponent<NavMeshAgent>();
    }
	public void GetsHitByPlayer (Color inColor ) {
        if (CompareTag("Enemy"))
        {
            currentColor = GetComponent<Renderer>().material.color;

            currentColor = currentColor + inColor / tankHealth;

			if (compareColors(goalColor, currentColor)){
				GameLogicScript.i.audioH.Play("EnemyDestroyBling");
                DestroyMe();
			}else
                GetComponent<Renderer>().material.color = currentColor;
            /* if (compareColors(goalColor, inColor))
             {
                 DestroyMe();
             }



         }
         else if (CompareTag("Tank"))
         {
             tankGetsHit(inColor);
         */
        }
        currentColor = GetComponent<Renderer>().material.color;
        textM.text = "R" + (Mathf.RoundToInt(currentColor.r * 100) / 100f) + " G" + (Mathf.RoundToInt(currentColor.g * 100) / 100f) + " B" + (Mathf.RoundToInt(currentColor.b * 100) / 100f);
    }

	bool compareColors(Color inCol1, Color inCol2){
		if(inCol1.r <= inCol2.r && inCol1.g <= inCol2.g && inCol1.b<=inCol2.b)
			return true;
		else
			return false;
	}
    void Update()
    {
        transform.LookAt(wagon);
        if (isGoingHeaven)
        {
            enemyrb.MovePosition(transform.position + new Vector3(0, upSpeed*Time.deltaTime , 0));
            if (transform.position.y > 20)
                DestroyMe();

        }
        else
        {
            enemyrb.MovePosition(transform.position +transform.forward * forwardSpeed * Time.deltaTime);

            //nav.SetDestination(wagon.position);
        }
    }
    public void GoToHeaven()

    {
        isGoingHeaven = true;
        GetComponent<Renderer>().material.color = goalColor;
        
        

    }
    void DestroyMe()
    {
        Destroy(gameObject);
        GameObject particleGO = Instantiate(enemyDestroyParticlePrefab, transform.position, enemyDestroyParticlePrefab.transform.rotation) as GameObject;
        particleGO.GetComponent<ParticleSystem>().startColor = goalColor;
    }

  /*  void tankGetsHit (Color inColor)
    {
        currentColor = GetComponent<Renderer>().material.color;
        
        currentColor = currentColor + inColor / tankHealth;
        
        if (compareColors(goalColor, currentColor))
            DestroyMe();
        else
        GetComponent<Renderer>().material.color= currentColor;
    }*/
}

    

