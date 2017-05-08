using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    //Public vars
    public float speed;

    //Private vars
    private Rigidbody rb;
    private SpaceGameController gameController;
    private int currentWave;

    /***************Functions***************/

    /***************Private***************/
    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<SpaceGameController>();
        } //End if (gameControllerObject != null)

        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        } //End if (gameController == null)
        currentWave = gameController.getCurrentWave();

        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * (speed - randomSpeedIncrease(currentWave) - ((currentWave * Random.Range(0, currentWave * 2)) % currentWave) - (float)((currentWave - 1) * .55));
	} //End void Start()
	
    /// <summary>
    /// 
    /// </summary>
    /// <param name="wavesPassed"></param>
    /// <returns></returns>
    private int randomSpeedIncrease(int wavesPassed)
    {
        int spawnChance = Random.Range(0, 20);

        if (wavesPassed < 3)
        {
            if (spawnChance < 5)
            {
                return spawnChance % 3;
            } //End 
            else
            {
                return spawnChance % 2;
            } //End 
        } //End 
        else if (wavesPassed < 5)
        {
            if (spawnChance < wavesPassed / 2 + (wavesPassed % 5) + 1)
            {
                return spawnChance % 5;
            } //End 
            else
            {
                return spawnChance % 3;
            } //End 
        } //End 
        else if (wavesPassed < 7)
        {
            if (spawnChance < wavesPassed / 2 + (wavesPassed % 10) + 4)
            {
                return spawnChance % 8;
            } //End 
            else
            {
                return spawnChance % 4;
            } //End 
        } //End 
        else if (wavesPassed < 10)
        {
            return spawnChance % 10;
        } //End 
        else
        {
            return 0;
        } //End 
    } //End 

    /***************Public***************/
    public void reduceSpeedFromImpact(float speedReduction)
    {
        speed -= speedReduction;
    } //End public void reduceSpeedFromImpact(float speedReduction)
} //End public class Mover : MonoBehaviour
