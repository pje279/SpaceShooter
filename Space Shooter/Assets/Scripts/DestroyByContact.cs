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
    private PlayerController playerController;
    private Rigidbody rb;
    private float speedReduction;

    /***************Functions***************/

    /***************Private***************/
    private void Start()
    {
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject != null)
        {
            playerController = playerControllerObject.GetComponent<PlayerController>();
        } //End if (playerControllerObject != null)

        if (playerControllerObject == null)
        {
            Debug.Log("SpaceGameController: Cannot find 'PlayerController' script");
        } //End if (playerControllerObject != null)

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
        } //End if (collision.gameObject.tag == "Asteroid")
    } //End private void OnCollisionEnter(Collision collision)

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Asteroid")
        {
            Physics.IgnoreCollision(other, GetComponent<Collider>());
            return;
        } //End if (other.tag == "Asteroid")
        else if (other.tag == "Boundary")
        {
            return;
        } //End else if (other.tag == "Boundary")

        //Debug.Log(other);


        if (other.tag == "Player")
        {
            playerController.decreaseHealth(health);

            if (playerController.getCurrentHealth() <= 0)
            {
                Instantiate(playerExplosion, transform.position, transform.rotation);
                gameController.GameOver();
            } //End if (playerController.getCurrentHealth() <= 0)
            else
            {
                health = 0;
            } //End else
        } //End if (other.tag == "Player")
        else
        {
            if (other.tag == "Primary Bolt")
            {
                health -= playerController.getDamage();
                if (health > 0)
                {
                    speedReduction = (float)0.2 + playerController.getForce();
                    rb.velocity = rb.velocity * speedReduction;
                    Instantiate(impact, transform.position, transform.rotation);
                } //End if (health > 0)
            } //End if (other.tag == "Primary Bolt")
        } //End else

        if (health <= 0)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            gameController.AddScore(scoreValue);
        } //End if (health <= 0)

        if (playerController.getCurrentHealth() <= 0)
        {
            Destroy(other.gameObject);
        } //End if (playerController.getCurrentHealth() <= 0)
    } //End private void OnTriggerEnter(Collider other)

} //End public class NewBehaviourScript : MonoBehaviour
