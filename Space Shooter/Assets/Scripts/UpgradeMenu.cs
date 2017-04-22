using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    //Public vars
    public Text pointsText;
    public Text wave;
    public Text repairCostTextBtn, increaseHealthTextBtn, increaseSpeedTextBtn, increaseShieldTextBtn;
    public Text increaseDamageTextBtn, increaseFireRateTextBtn, increaseForceTextBtn;
    public Text healthText, shieldText, speedText;
    public Text damageText, fireRateText, forceText;

    //Private vars
    private int repairCost, healthCost, speedCost, shieldCost;
    private int damageCost, fireRateCost, forceCost;
    private int healthLevel, speedLevel, shieldLevel;
    private int damageLevel, fireRateLevel, forceLevel;
    private SpaceGameController gameController;
    private PlayerController player;

    /***************Functions***************/

    /***************Private***************/
    /// <summary>
    /// 
    /// </summary>
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

        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject != null)
        {
            player = playerControllerObject.GetComponent<PlayerController>();
        } //End if (playerControllerObject != null)

        if (playerControllerObject == null)
        {
            Debug.Log("Cannot find 'PlayerController' script");
        } //End if (playerControllerObject != null)

        repairCost = calcRepairCost();
        healthCost = 1000;
        speedCost = 1000;
        shieldCost = 1250;

        damageCost = 750;
        fireRateCost = 750;
        forceCost = 750;

        healthLevel = speedLevel = shieldLevel = 1;
        damageLevel = fireRateLevel = forceLevel = 1;

        updateMenuText();
    } //End private void Start()
    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {

    } //End private void Update()
    /// <summary>
    /// 
    /// </summary>
    private void calcMenuOptionsPrices()
    {
        repairCost = calcRepairCost();
        healthCost = calcIncreaseHealth();
        speedCost = calcIncreaseSpeed();
        shieldCost = calcIncreaseShield();
        damageCost = calcIncreaseDamage();
        fireRateCost = calcIncreaseFireRate();
        forceCost = calcIncreaseForce();
    } //End private void calcMenuOptionsPrices()
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private int calcRepairCost()
    {
        if (player.getCurrentHealth() == player.getMaxHealth())
        {
            return 0;
        } //End if (player.getCurrentHealth() == player.getMaxHealth())
        else
        {
            return (int)((player.getMaxHealth() - player.getCurrentHealth()) * gameController.getCurrentWave() * 1.5 + gameController.getCurrentWave());
        } //End else
    } //End private int calcRepairCost()
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private int calcIncreaseHealth()
    {
        return (int)(healthCost * healthLevel * 1.5);
    } //End private int calcIncreaseHealth()
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private int calcIncreaseSpeed()
    {
        return speedCost * speedLevel;
    } //End private int calcIncreaseSpeed()
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private int calcIncreaseShield()
    {
        return (int)(shieldCost * shieldLevel * 1.5);
    } //End private int calcIncreaseShield()
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private int calcIncreaseDamage()
    {
        return (int)(damageCost * damageLevel * 1.5);
    } //Endprivate int calcIncreaseDamage()
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private int calcIncreaseFireRate()
    {
        return (int)(fireRateCost * fireRateLevel * 1.5);
    } //End private int calcIncreaseFireRate()
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private int calcIncreaseForce()
    {
        return (int)(forceCost * forceLevel * 1.5);
    } //End private int calcIncreaseForce()

    /***************Public***************/
    /// <summary>
    /// 
    /// </summary>
    public void updateMenuText()
    {
        pointsText.text = gameController.getScore() + " Pts";

        calcMenuOptionsPrices();

        //Player Stats
        healthText.text = "Health:  " + player.getCurrentHealth() + "/" + player.getMaxHealth();
        shieldText.text = "Shield:  " + player.getMaxShield().ToString();
        speedText.text = "Speed:  " + player.getSpeed().ToString();
        damageText.text = "Damage:  " + player.getDamage().ToString();
        fireRateText.text = "Fire Rate:  " + player.getFireRate().ToString();
        forceText.text = "Force:  " + player.getForce().ToString();

        //Upgrade Options
        wave.text = "Wave " + gameController.getCurrentWave();
        repairCostTextBtn.text = repairCost + " Pts";
        increaseHealthTextBtn.text = healthCost + " Pts";
        increaseSpeedTextBtn.text = speedCost + " Pts";
        increaseShieldTextBtn.text = shieldCost + " Pts";
        increaseDamageTextBtn.text = damageCost + " Pts";
        increaseFireRateTextBtn.text = fireRateCost + " Pts";
        increaseForceTextBtn.text = forceCost + " Pts";

    } //End public void updateMenuText()
    /// <summary>
    /// 
    /// </summary>
    public void nextWave()
    {
        gameController.startNextWave();
    } //End public void nextWave()

} //End public class UpgradeMenu : MonoBehaviour
