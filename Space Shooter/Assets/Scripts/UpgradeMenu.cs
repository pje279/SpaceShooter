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
    public GameObject gameControllerObject;
    public GameObject self;

    //Private vars
    private int repairCost, healthCost, speedCost, shieldCost;
    private int damageCost, fireRateCost, forceCost;
    private int healthLevel, speedLevel, shieldLevel;
    private int damageLevel, fireRateLevel, forceLevel;
    private SpaceGameController gameController;
    private PlayerController player;
    private CanvasGroup canvasGroup;

    /***************Functions***************/

    /***************Private***************/
    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        /*
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<SpaceGameController>();
        } //End if (gameControllerObject != null)

        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        } //End if (gameController == null)
        */

        Debug.Log("Trying to get the CanvasGroup");
        canvasGroup = self.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            Debug.Log("UpgradeMenu.cs: Couldn't find the CanvasGroup component");
        } //End if (cavasGroup == null)

        gameController = gameControllerObject.GetComponent<SpaceGameController>();

        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject != null)
        {
            player = playerControllerObject.GetComponent<PlayerController>();
        } //End if (playerControllerObject != null)

        if (playerControllerObject == null)
        {
            Debug.Log("Cannot find 'PlayerController' script");
        } //End if (playerControllerObject != null)

        /*
        repairCost = calcRepairCost();
        healthCost = 500;
        speedCost = 500;
        shieldCost = 750;

        damageCost = 350;
        fireRateCost = 400;
        forceCost = 400;

        healthLevel = speedLevel = shieldLevel = 0;
        damageLevel = fireRateLevel = forceLevel = 0;
        */

        updateMenuText();
    } //End private void Start()
    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        Debug.Log("Trying to get the CanvasGroup");
        canvasGroup = self.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            Debug.Log("UpgradeMenu.cs: Couldn't find the CanvasGroup component");
        } //End if (cavasGroup == null)

        gameController = gameControllerObject.GetComponent<SpaceGameController>();

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
        healthCost = 200;
        speedCost = 250;
        shieldCost = 250;

        damageCost = 150;
        fireRateCost = 200;
        forceCost = 250;

        healthLevel = speedLevel = shieldLevel = 0;
        damageLevel = fireRateLevel = forceLevel = 0;

        calcMenuOptionsPrices();
        updateMenuText();
    } //End private void Awake()
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
        Debug.Log(
            "CurHealth: " + player.getCurrentHealth() +
            "MaxHealth: " + player.getMaxHealth()
            );
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
        return (int)(healthCost * (healthLevel + 1) * (1 - 0.175 * (healthLevel + 1)));
    } //End private int calcIncreaseHealth()
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private int calcIncreaseSpeed()
    {
        return (int)(speedCost * (speedLevel + 1) * (1 - 0.175 * (speedLevel + 1)));
    } //End private int calcIncreaseSpeed()
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private int calcIncreaseShield()
    {
        return (int)(shieldCost * (shieldLevel + 1) * (1 - 0.175 * (shieldLevel + 1)));
    } //End private int calcIncreaseShield()
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private int calcIncreaseDamage()
    {
        return (int)(damageCost * (damageLevel + 1) * (1 - 0.175 * (damageLevel + 1)));
    } //Endprivate int calcIncreaseDamage()
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private int calcIncreaseFireRate()
    {
        return (int)(fireRateCost * (fireRateLevel + 1) * (1 - 0.175 * (fireRateLevel + 1)));
    } //End private int calcIncreaseFireRate()
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private int calcIncreaseForce()
    {
        return (int)(forceCost * (forceLevel + 1) * (1 - 0.175 * (forceLevel + 1)));
    } //End private int calcIncreaseForce()
    /// <summary>
    /// 
    /// </summary>
    private void updateRepairCostText()
    {
        repairCostTextBtn.text = repairCost + " Pts";
        healthText.text = "Health:  " + player.getCurrentHealth() + "/" + player.getMaxHealth();
    } //End private void updateRepairCostText()
    /// <summary>
    /// 
    /// </summary>
    private void updateHealthCostText()
    {
        increaseHealthTextBtn.text = healthCost + " Pts";
        healthText.text = "Health:  " + player.getCurrentHealth() + "/" + player.getMaxHealth();
    } //End private void updateHealthCostText()
    /// <summary>
    /// 
    /// </summary>
    private void updateSpeedCostText()
    {
        increaseSpeedTextBtn.text = speedCost + " Pts";
        speedText.text = "Speed:  " + player.getSpeed().ToString("F3");
    } //End private void updateSpeedCostText()
    /// <summary>
    /// 
    /// </summary>
    private void updateShieldCostText()
    {
        increaseShieldTextBtn.text = shieldCost + " Pts";
        shieldText.text = "Shield:  " + player.getMaxShield().ToString("F");
    } //End private void updateShieldCostText()
    /// <summary>
    /// 
    /// </summary>
    private void updateDamageCostText()
    {
        increaseDamageTextBtn.text = damageCost + " Pts";
        damageText.text = "Damage:  " + player.getDamage().ToString("F3");
    } //End private void updateDamageCostText()
    /// <summary>
    /// 
    /// </summary>
    private void updateFireRateCostText()
    {
        increaseFireRateTextBtn.text = fireRateCost + " Pts";
        fireRateText.text = "Fire Rate:  " + player.getFireRate().ToString("F3");
    } //End private void updateFireRateCostText()
    /// <summary>
    /// 
    /// </summary>
    private void updateForceCostText()
    {
        increaseForceTextBtn.text = forceCost + " Pts";
        forceText.text = "Force:  " + player.getForce().ToString("F3");
    } //End private void updateForceCostText()
    /// <summary>
    /// 
    /// </summary>
    private void updateRepairCost()
    {
        repairCost = calcRepairCost();
        updateRepairCostText();
        updatePointsAvailable();
    } //End private void updateRepairCost()
    /// <summary>
    /// 
    /// </summary>
    private void updateHealthCost()
    {
        healthCost = calcIncreaseHealth();
        updateHealthCostText();
        updatePointsAvailable();
    } //End private void updateHealthCost()
    /// <summary>
    /// 
    /// </summary>
    private void updateSpeedCost()
    {
        speedCost = calcIncreaseSpeed();
        updateSpeedCostText();
        updatePointsAvailable();
    } //End private void updateSpeedCost()
    /// <summary>
    /// 
    /// </summary>
    private void updateShieldCost()
    {
        shieldCost = calcIncreaseShield();
        updateShieldCostText();
        updatePointsAvailable();
    } //End private void updateShieldCost()
    /// <summary>
    /// 
    /// </summary>
    private void updateDamageCost()
    {
        damageCost = calcIncreaseDamage();
        updateDamageCostText();
        updatePointsAvailable();
    } //End private void updateDamageCost()
    /// <summary>
    /// 
    /// </summary>
    private void updateFireRateCost()
    {
        fireRateCost = calcIncreaseFireRate();
        updateFireRateCostText();
        updatePointsAvailable();
    } //End private void updateFireRateCost()
    /// <summary>
    /// 
    /// </summary>
    private void updateForceCost()
    {
        forceCost = calcIncreaseForce();
        updateForceCostText();
        updatePointsAvailable();
    } //End private void updateForceCost()
    /// <summary>
    /// 
    /// </summary>
    private void updatePointsAvailable()
    {
        pointsText.text = gameController.getScore() + " Pts";
    } //End private void updatePointsAvailable()

    /***************Public***************/
    /// <summary>
    /// 
    /// </summary>
    public void resetMenu()
    {
        repairCost = calcRepairCost();
        healthCost = 200;
        speedCost = 250;
        shieldCost = 250;

        damageCost = 150;
        fireRateCost = 200;
        forceCost = 250;

        healthLevel = speedLevel = shieldLevel = 0;
        damageLevel = fireRateLevel = forceLevel = 0;

        calcMenuOptionsPrices();
        updateMenuText();
    } //End public void resetMenu()
    /// <summary>
    /// 
    /// </summary>
    public void updateMenuText()
    {
        pointsText.text = gameController.getScore() + " Pts";

        //calcMenuOptionsPrices();

        //Player Stats
        
        healthText.text = "Health:  " + player.getCurrentHealth() + "/" + player.getMaxHealth();
        shieldText.text = "Shield:  " + player.getMaxShield().ToString("F");
        speedText.text = "Speed:  " + player.getSpeed().ToString("F3");
        damageText.text = "Damage:  " + player.getDamage().ToString("F3");
        fireRateText.text = "Fire Rate:  " + player.getFireRate().ToString("F3");
        forceText.text = "Force:  " + player.getForce().ToString("F3");

        //Upgrade Options
        wave.text = "Wave " + gameController.getCurrentWave();
        repairCostTextBtn.text = repairCost + " Pts";
        increaseHealthTextBtn.text = healthCost + " Pts";
        increaseSpeedTextBtn.text = speedCost + " Pts";
        increaseShieldTextBtn.text = shieldCost + " Pts";
        increaseDamageTextBtn.text = damageCost + " Pts";
        increaseFireRateTextBtn.text = fireRateCost + " Pts";
        increaseForceTextBtn.text = forceCost + " Pts";

        Debug.Log(
            "Health level " + healthLevel +
            "\nSpeed level " + speedLevel +
            "\nShield level " + shieldLevel +
            "\nDamage level " + damageLevel +
            "\nFire Raate level " + fireRateLevel +
            "\nForce level " + forceLevel
            );
    } //End public void updateMenuText()
    /// <summary>
    /// 
    /// </summary>
    public void nextWave()
    {
        Hide();
        gameController.startNextWave();
    } //End public void nextWave()
    /// <summary>
    /// 
    /// </summary>
    public void repairBtnPressed()
    {
        if (gameController.getScore() >= repairCost)
        {
            player.restoreHealth();
            gameController.subtractFromScore(repairCost);
            updateRepairCost();
        } //End if (gameController.getScore() >= repairCost)
        else
        {
            //Not enough points
            //Need to tell/indicate to the player somehow
        } //End 
    } //End public void repairBtnPressed()
    /// <summary>
    /// 
    /// </summary>
    public void increaseHealthBtnPressed()
    {
        if (gameController.getScore() >= healthCost)
        {
            player.increaseHealth((int)((player.getMaxHealth() * (float)(0.4 - 0.065 * healthLevel)) + gameController.getCurrentWave() * (float)1.65));
            gameController.subtractFromScore(healthCost);
            healthLevel += 1;
            updateHealthCost();
        } //End if (gameController.getScore() >= healthCost)
        else
        {
            //Not enough points
            //Need to tell/indicate to the player somehow
        } //End
    } //End public void increaseHealthBtnPressed()
    /// <summary>
    /// 
    /// </summary>
    public void increaseSpeedBtnPress()
    {
        if (gameController.getScore() >= speedCost)
        {
            player.increaseSpeed((player.getSpeed() * (float)(0.05 - 0.015 * speedLevel)) + gameController.getCurrentWave() * (float)0.005);
            gameController.subtractFromScore(speedCost);
            speedLevel += 1;
            updateSpeedCost();
        } //End 
        else
        {
            //Not enough points
            //Need to tell/indicate to the player somehow
        } //End
    } //End public void increaseSpeedBtnPress()
    /// <summary>
    /// 
    /// </summary>
    public void increaseShieldBtnPress()
    {
        if (gameController.getScore() >= shieldCost)
        {
            if (shieldLevel == 0)
            {
                player.increaseShield(10);
                gameController.setPlayerHasShield();
            } //End 
            else
            {
                player.increaseShield((int)((player.getMaxShield() * (float)(0.4 - 0.07 * shieldLevel)) + gameController.getCurrentWave() * (float)1.5));
            } //End else
            gameController.subtractFromScore(shieldCost);
            shieldLevel += 1;
            updateShieldCost();
        } //End 
        else
        {
            //Not enough points
            //Need to tell/indicate to the player somehow
        } //End
    } //End public void increaseShieldBtnPress()
    /// <summary>
    /// 
    /// </summary>
    public void increaseDamageBtnPress()
    {
        if (gameController.getScore() >= damageCost)
        {
            player.increaseDamage((player.getDamage() * (float)(0.1 - 0.065 * damageLevel) - gameController.getCurrentWave() * (float)0.005) + gameController.getCurrentWave() * (float)1.5);
            gameController.subtractFromScore(damageCost);
            damageLevel += 1;
            updateDamageCost();
        } //End 
        else
        {
            //Not enough points
            //Need to tell/indicate to the player somehow
        } //End
    } //End public void increaseDamageBtnPress()
    /// <summary>
    /// 
    /// </summary>
    public void increaseFireRateBtnPress()
    {
        if (gameController.getScore() >= fireRateCost)
        {
            player.increaseFireRate((player.getFireRate() * (float)(0.0125 - 0.00175 * fireRateLevel)) + (float)0.0025 * (gameController.getCurrentWave() * (float)1.5));
            gameController.subtractFromScore(fireRateCost);
            fireRateLevel += 1;
            updateFireRateCost();
        } //End 
        else
        {
            //Not enough points
            //Need to tell/indicate to the player somehow
        } //End
    } //End public void increaseFireRateBtnPress()
    /// <summary>
    /// 
    /// </summary>
    public void increaseForceBtnPressed()
    {
        if (gameController.getScore() >= forceCost)
        {
            player.increaseForce((player.getForce() * (float)(0.15 - 0.065 * forceLevel)) + (gameController.getCurrentWave() * (float)0.85) * (float)0.01);
            gameController.subtractFromScore(forceCost);
            forceLevel += 1;
            updateForceCost();
        } //End 
        else
        {
            //Not enough points
            //Need to tell/indicate to the player somehow
        } //End
    } //End public void increaseForceBtnPressed()
    /// <summary>
    /// 
    /// </summary>
    public void Hide()
    {
        canvasGroup.alpha = 0f; //this makes everything transparent
        canvasGroup.blocksRaycasts = false; //this prevents the UI element to receive input events
    } //End public void Hide()
    /// <summary>
    /// 
    /// </summary>
    public void Show()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    } //End public void Show()

} //End public class UpgradeMenu : MonoBehaviour
