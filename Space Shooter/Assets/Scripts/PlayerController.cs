using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;

} //End public class Bounadry

public class PlayerController : MonoBehaviour
{
    //Public vars
    public float speed, tilt, fireRate;
    public Boundary boundary;
    public GameObject shot;
    public Transform singleShotSpawn;
    public Transform doubleShotSpawn1, doubleShotSpawn2;
    public Transform tripleShotSpawn1, tripleShotSpawn2, tripleShotSpawn3;
    public float damage, force, currentHealth, maxHealth, currentShield, maxShield;

    //Private vars
    private Rigidbody rb;
    private float nextFire;
    private AudioSource shotSource;
    private SpaceGameController gameController;
    private int selectedWeapon;
    private UpgradeMenu upgradeMenu;

    /***************Functions***************/

    /***************Private***************/
    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        selectedWeapon = 1;
        rb = GetComponent<Rigidbody>();
        shotSource = GetComponent<AudioSource>();

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<SpaceGameController>();
        } //End if (gameControllerObject != null)

        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        } //End if (gameController == null)
        Debug.Log("Player Start() ran");
        gameControllerObject = GameObject.FindWithTag("UpgradeMenu");
        if (gameControllerObject != null)
        {
            Debug.Log("Found the upgrade menu script");
            upgradeMenu = gameControllerObject.GetComponent<UpgradeMenu>();
        } //End if (gameControllerObject != null)

        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        } //End if (gameController == null)
    } //End private void Start()
    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (selectedWeapon == 1)
            {
                selectedWeapon = 3;
            } //End 
            else
            {
                selectedWeapon -= 1;
            } //End 
            Debug.Log("Scrolled down");
            Debug.Log("selectedWeapon =" + selectedWeapon);
        } //End 
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectedWeapon == 3)
            {
                selectedWeapon = 1;
            } //End 
            else
            {
                selectedWeapon += 1;
            } //End 
            Debug.Log("Scrolled up");
            Debug.Log("selectedWeapon =" + selectedWeapon);
        } //End

        //Debug.Log("gameStarted = " + gameController.getGameStarted());
        if (Input.GetButton("Fire1") && Time.time > nextFire && gameController.getGameStarted() && !gameController.getUpgradeMenuOpen())
        {
            nextFire = Time.time + fireRate;

            if (gameController.getScore() < 1000 || selectedWeapon == 1)
            {
                Instantiate(shot, singleShotSpawn.position, singleShotSpawn.rotation);
            } //End 
            else if (gameController.getScore() < 2500 || selectedWeapon == 2)
            {
                Instantiate(shot, doubleShotSpawn1.position, doubleShotSpawn1.rotation);
                Instantiate(shot, doubleShotSpawn2.position, doubleShotSpawn2.rotation);
            } //End 
            else if (gameController.getScore() >= 2500 || selectedWeapon == 3)
            {
                Instantiate(shot, tripleShotSpawn1.position, tripleShotSpawn1.rotation);
                Instantiate(shot, tripleShotSpawn2.position, tripleShotSpawn2.rotation);
                Instantiate(shot, tripleShotSpawn3.position, tripleShotSpawn3.rotation);
            } //End 

            shotSource.Play();
        } //End if (Input.GetButton("Fire1") && Time.time > nextFire)
    } //End private void Update()
    /// <summary>
    /// 
    /// </summary>
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement  = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    } //End void FixedUpdate()

    /***************Public***************/
    /// <summary>
    /// 
    /// </summary>
    /// <param name="change"></param>
    public void setChangeSelectedWeapon(int change)
    {
        Debug.Log("Weapon changed to " + change);
        selectedWeapon = change;
    } //End public void setChangeSelectedWeapon(int change)
    /// <summary>
    /// 
    /// </summary>
    /// <param name="increase"></param>
    public void increaseDamage(float increase)
    {
        damage += increase;
        upgradeMenu.updateMenuText();
    } //End public void increaseDamage(float increase)
    /// <summary>
    /// 
    /// </summary>
    /// <param name="increase"></param>
    public void increaseFireRate(float increase)
    {
        fireRate += increase;
        upgradeMenu.updateMenuText();
    } //End public void increaseFireRate(float increase)
    /// <summary>
    /// 
    /// </summary>
    /// <param name="increase"></param>
    public void increaseForce(float increase)
    {
        force += increase;
        upgradeMenu.updateMenuText();
    } //End public void increaseForce(float increase)
    /// <summary>
    /// 
    /// </summary>
    public void restoreHealth()
    {

    } //End public void restoreHealth()
    /// <summary>
    /// 
    /// </summary>
    /// <param name="increase"></param>
    public void increaseHealth(float increase)
    {
        maxHealth += increase;
        upgradeMenu.updateMenuText();
    } //End public void incraseHealth(float increase)
    /// <summary>
    /// 
    /// </summary>
    /// <param name="increase"></param>
    public void increaseSpeed(float increase)
    {
        speed += increase;
        upgradeMenu.updateMenuText();
    } //End 
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public float getCurrentHealth()
    {
        return currentHealth;
    } //End public float getCurrentHealth()
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public float getMaxHealth()
    {
        return maxHealth;
    } //End public float getMaxHealth()
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public float getcurrentShield()
    {
        return currentShield;
    } //End public float getcurrentShield()
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public float getMaxShield()
    {
        return maxShield;
    } //End public float getMaxShield()
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public float getSpeed()
    {
        return speed;
    } //End public float getSpeed()
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public float getDamage()
    {
        return damage;
    } //End public float getDamage()
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public float getFireRate()
    {
        return fireRate;
    } //End public float getFireRate()
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public float getForce()
    {
        return force;
    } //End public float getForce()
} //End public class PlayerController : MonoBehaviour
