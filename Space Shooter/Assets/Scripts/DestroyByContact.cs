using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    //Public vars
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    public float health;
    public GameObject impact;

    //Private vars
    private SpaceGameController gameController;
    private Rigidbody rb;
    private float speedReduction;

    /***************Functions***************/

    /***************Private***************/
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<SpaceGameController>();
        } //End if (gameControllerObject != null)

        if (gameController == null)
        {
            Debug.Log("Cannot find 'SpaceGameController' script");
        } //End if (gameController == null)

        scoreValue += (int)((gameController.getCurrentWave() - 1) * 1.5);

        health += gameController.getCurrentWave() + Random.Range(0, gameController.getCurrentWave()) % (gameController.getCurrentWave() * (float)0.85);

        
        //Debug.Log("Health: " + health);
    } //End private void Start()

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        } //End 
    } //End 

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Asteroid")
        {
            Physics.IgnoreCollision(other, GetComponent<Collider>());
            return;
        } //End 
        else if (other.tag == "Boundary")
        {
            return;
        } //End 

        //Debug.Log(other);

        
        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, transform.position, transform.rotation);
            gameController.GameOver();
        } //End if (other.tag == "Player")
        else
        {
            if (other.tag == "Primary Bolt")
            {
                health -= 10;
                if (health > 0)
                {
                    //Vector3 speedReduction = new Vector3();
                    speedReduction = (float)0.75;
                    rb.velocity = rb.velocity * speedReduction;
                    Instantiate(impact, transform.position, transform.rotation);
                } //End 
            } //End 
        } //End else

        if (health <= 0)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            gameController.AddScore(scoreValue);
        } //End 

        Destroy(other.gameObject);
    } //End private void OnTriggerEnter(Collider other)

} //End public class NewBehaviourScript : MonoBehaviour
