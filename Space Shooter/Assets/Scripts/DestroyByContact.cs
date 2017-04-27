using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    //Public vars
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    public float asteroidHealth;
    public GameObject impact;

    //Private vars
    private SpaceGameController gameController;
    private PlayerController playerController;
    private Rigidbody rb;
    private float speedReduction;
    private GameObject player;

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

        player = GameObject.FindWithTag("Player");
        /*
        if (playerObject != null)
        {
            gameController = playerObject.GetComponent<SpaceGameController>();
        } //End if (playerObject != null)
        */

        if (player == null)
        {
            Debug.Log("Cannot find 'SpaceGameController' script");
        } //End if (player == null)

        scoreValue += (int)((gameController.getCurrentWave() - 1) * 1.5);

        asteroidHealth += 1 + gameController.getCurrentWave() + Random.Range(0, gameController.getCurrentWave()) % (gameController.getCurrentWave() * (float)0.85);

        
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
            if (playerController.getCurrentShield() > 0)
            {
                if (playerController.getCurrentShield() >= asteroidHealth)
                {
                    Debug.Log("Just shield : " + asteroidHealth);
                    playerController.decreaseShield(asteroidHealth);
                } //End if (playerController.getCurrentShield() >= asteroidHealth)
                else
                {
                    Debug.Log("Shield and health : " + asteroidHealth);
                    playerController.decreaseHealth(asteroidHealth - playerController.getCurrentShield());
                    playerController.decreaseShield(playerController.getCurrentShield());

                    asteroidHealth = 0;
                } //End else
            } //End if (playerController.getCurrentShield() > 0)
            else
            {
                Debug.Log("Just health : " + asteroidHealth);
                playerController.decreaseHealth(asteroidHealth);

                asteroidHealth = 0;
            } //End 
        } //End if (other.tag == "Player")
        else
        {
            if (other.tag == "Primary Bolt")
            {
                asteroidHealth -= playerController.getDamage();
                if (asteroidHealth > 0)
                {
                    speedReduction = (float)0.85 - playerController.getForce();
                    rb.velocity = rb.velocity * speedReduction;
                    Instantiate(impact, transform.position, transform.rotation);
                } //End if (asteroidHealth > 0)
            } //End if (other.tag == "Primary Bolt")
        } //End else

        if (asteroidHealth <= 0)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            gameController.AddScore(scoreValue);
        } //End if (asteroidHealth <= 0)

        if (playerController.getCurrentHealth() <= 0)
        {
            Instantiate(playerExplosion, transform.position, transform.rotation);
            gameController.GameOver();
            player.SetActive(false);
            //Destroy(other.gameObject);
        } //End if (playerController.getCurrentHealth() <= 0)
    } //End private void OnTriggerEnter(Collider other)

} //End public class NewBehaviourScript : MonoBehaviour
