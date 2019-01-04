using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject currentCheckPoint;

    public Rigidbody2D PC;

    public GameObject PC2;

    //Particles
    public GameObject DeathParticle;
    public GameObject RespawnParticle;

    //Respawn Delay
    public float RespawnDelay;


    //Point Penalty on Death
    public int PointPenaltyOnDeath;

    //Store Gravity Value
    private float GravityStore;


	// Use this for initialization
	void Start () {
        PC = GameObject.Find("PC").GetComponent<Rigidbody2D>();
        PC2 = GameObject.Find("PC");
        //PC = FindObjectOfType<Rigidbody2D> ();
	}

    public void RespawnPlayer(){
        StartCoroutine("RespawnPlayerCo");
    }

    public IEnumerator RespawnPlayerCo(){
        //Generate Death Particle
        Instantiate (DeathParticle, PC.transform.position, PC.transform.rotation);
        //Hide PC
        // PC.enabled = false;
        PC2.SetActive(false);
        PC.GetComponent<Renderer>().enabled = false;
        //Gravity Reset
        GravityStore = PC.GetComponent<Rigidbody2D>().gravityScale;
        PC.GetComponent<Rigidbody2D>().gravityScale = 0f;
        PC.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //Point Penalty
        ScoreManager.AddPoints(-PointPenaltyOnDeath);
        //Debug Message
        Debug.Log ("Player Respawn");
        //Respawn Delay
        yield return new WaitForSeconds(RespawnDelay);
        //Gravity Restore
        PC.GetComponent<Rigidbody2D>().gravityScale = GravityStore;
        //Match PC's Transform Position
        PC.transform.position = currentCheckPoint.transform.position;
        //Show PC
        // PC.enable = true;
        PC2.SetActive(true);
        PC.GetComponent<Renderer> ().enabled = true;
        //Spawn PC
        Instantiate(RespawnParticle, currentCheckPoint.transform.position, currentCheckPoint.transform.rotation);
    }
                                       
}
