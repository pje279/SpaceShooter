using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpaceGameController : MonoBehaviour
{
    //Public vars
    public GameObject hazard1, hazard2, hazard3;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait, startWait, waveWait;
    public float simultaneousSpawnWait;
    public GUIText scoreText, restartText, gameOverText;
    public GUIText waveText, healthText, shieldText;
    public GameObject mainMenu, upgradeMenu;
    public GameObject player;

    //Private vars
    private bool startGame, gameStarted, gamePaused, mainMenuOpen, upgradeMenuOpen;
    private int score;
    private bool gameOver, restart;
    private int currentWave, wavesPassed;
    private int simultaneousSpawns, curNumSpawnedHazards;
    private PlayerController playerController;
    private UpgradeMenu upgradeMenuController;
    private bool doubleBoltUnlocked, tripleBoltUnlocked;
    private int numAsteroidsDestroyed;
    private bool midWave;
    private bool playerHasShield;

    /***************Functions***************/

    /***************Private***************/
    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        initVars();

        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject != null)
        {
            playerController = playerControllerObject.GetComponent<PlayerController>();
        } //End if (playerControllerObject != null)

        if (playerControllerObject == null)
        {
            Debug.Log("SpaceGameController: Cannot find 'PlayerController' script");
        } //End if (playerControllerObject != null)

        GameObject upgradeControllerObject = GameObject.FindWithTag("Canvas");
        if (upgradeControllerObject != null)
        {
            //Debug.Log("playerControllerObject != null");
            upgradeMenuController = upgradeMenu.GetComponent<UpgradeMenu>();
        } //End if (playerControllerObject != null)

        if (upgradeControllerObject == null)
        {
            Debug.Log("SpaceGameController: Cannot find 'UpgradeMenu' script");
        } //End if (playerControllerObject != null)

        //showMainMenu();

    } //End void Start()
    /// <summary>
    /// 
    /// </summary>
    private void initVars()
    {
        scoreText.text = "";
        waveText.text = "";
        currentWave = 1;
        wavesPassed = 0;

        gameOverText.text = "";
        gameOver = false;

        restartText.text = "";
        restart = false;

        healthText.text = "";
        shieldText.text = "";

        startGame = false;
        gameStarted = false;

        mainMenuOpen = true;
        mainMenu.SetActive(mainMenuOpen);
        upgradeMenuOpen = false;
        upgradeMenu.SetActive(upgradeMenuOpen);

        score = 0;

        simultaneousSpawns = 1;
        curNumSpawnedHazards = 0;

        doubleBoltUnlocked = false;
        tripleBoltUnlocked = false;

        midWave = false;

        playerHasShield = false;

        numAsteroidsDestroyed = 0;
    } //End private void initVars()
    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {
        //Start the game from the menu button
        if (startGame && !gameStarted)
        {
            startGameFromBtn();
        } //End if (gameStarted)

        //Pause the game while playing
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escaped key pressed\ngamePause = " + gamePaused + "\ngameStarted = " + gameStarted);
            if (gamePaused && !gameStarted)
            {
                pauseGame();
                gamePaused = false;
            } //End if (gamePaused && !gameStarted)
            else if (!gamePaused && !gameStarted)
            {
                pauseGame();
                gamePaused = true;
            } //End else if (!gamePaused && !gameStarted)
        } //End if (Input.GetKeyDown(KeyCode.Escape))

        //Restart the game
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                player.GetComponent<Transform>().position = new Vector3(0, 0, 4);
                player.SetActive(true);

                clearAsteroids();
                hazardCount = 25;
                initVars();
                upgradeMenuController.resetMenu();
                playerController.resetStats();
                startGameFromBtn();

                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            } //End if (Input.GetKeyDown(KeyCode.R))
        } //End if (restart)

        if (midWave)
        {
            upgradeMenuOpen = false;
            upgradeMenu.SetActive(upgradeMenuOpen);
        } //End 
    } //End private void Update()
    /// <summary>
    /// 
    /// </summary>
    private void showMainMenu()
    {
        mainMenu.SetActive(mainMenuOpen);
    } //End private void showMainMenu()
    /// <summary>
    /// 
    /// </summary>
    private void updateScore()
    {
        scoreText.text = "Score: " + score;
    } //End void UpdateScore()
    /// <summary>
    /// 
    /// </summary>
    /// <param name="waveVal"></param>
    private void updateWave()
    {
        if (playerHasShield)
        {
            waveText.text = "Wave: " + currentWave;
        } //End if (playerHasShield)
    } //End private void updateWave()
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnWaves()
    {
        midWave = true;
        yield return new WaitForSeconds(startWait);

        while (true)
        {
            updateWave();
            curNumSpawnedHazards = 0;

            for (int i = 0; i < hazardCount; i++)
            {
                if (player.active != true)
                {
                    StopCoroutine(SpawnWaves());
                } //End if (player.active != true)

                if (simultaneousSpawns > 1 && curNumSpawnedHazards % currentWave == 0)
                {
                    for (int j = 0; j < simultaneousSpawns; j++)
                    {
                        if (player.active != true)
                        {
                            StopCoroutine(SpawnWaves());
                        } //End if (player.active != true)

                        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                        Quaternion spawnRotation = Quaternion.identity;
                        spawnAsteroid(spawnPosition, spawnRotation);
                        curNumSpawnedHazards += 1;

                        yield return new WaitForSeconds(simultaneousSpawnWait);
                    } //End 
                    
                } //End 
                else
                {
                    Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    Quaternion spawnRotation = Quaternion.identity;
                    spawnAsteroid(spawnPosition, spawnRotation);
                    curNumSpawnedHazards += 1;
                } //End else
                yield return new WaitForSeconds(spawnWait - (float)(wavesPassed * .035));
            } //End for (int i = 0; i < hazardCount; i++)

            if (player.active != true)
            {
                StopCoroutine(SpawnWaves());
            } //End if (player.active != true)

            if (hazardCount > 0)
            {
                yield return new WaitForSeconds(waveWait);
            } //End if (hazardCount > 0)

            if (gameOver)
            {
                restartText.text = "Press 'R' to restart";
                restart = true;
                break;
            } //End if (gameOVer)

            currentWave += 1;
            wavesPassed = currentWave;
            hazardCount += wavesPassed + currentWave + Random.Range(0, wavesPassed + currentWave);
            
            if (wavesPassed % 2 == 0 && wavesPassed != 0)
            {
                hazardCount += 10;
                
                if (simultaneousSpawns < 1)
                {
                    simultaneousSpawns += 1;
                } //End 
            } //End if (wavesPassed % 5 == 0)
            else
            {
                hazardCount += 3;
            } //End else
              //Debug.Log("hazardCount: " + hazardCount);

            midWave = false;
            initUpgradeMenu();
            //StartCoroutine(showUpgradeMenu());
            yield return new WaitUntil(() => !upgradeMenuOpen);
        } //End while (true)
    } //End void SpawnWaves()
    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    private void spawnAsteroid(Vector3 position, Quaternion rotation)
    {
        int spawnChance = Random.Range(0, 20);
        int whichType = Random.Range(0, 100) % 3;

        if (wavesPassed < 5)
        {
            if (spawnChance < 5)
            {
                if (whichType % 2 == 0)
                {
                    Instantiate(hazard2, position, rotation);
                } //End if (whichType % 2 == 0)
                else
                {
                    Instantiate(hazard3, position, rotation);
                } //End else
            } //End if (spawnChance < 5)

            Instantiate(hazard1, position, rotation);
        } //End if (wavesPassed < 5)
        else if (wavesPassed < 10)
        {
            if (spawnChance < wavesPassed / 2 + (wavesPassed % 5) + 1)
            {
                if (whichType % 2 == 0)
                {
                    Instantiate(hazard3, position, rotation);
                } //End if (whichType % 2 == 0)
                else
                {
                    Instantiate(hazard2, position, rotation);
                } //End else
            } //End if (spawnChance < wavesPassed / 2 + (wavesPassed % 5) + 1)
            
            Instantiate(hazard1, position, rotation);
        } //End else if (wavesPassed < 10)
        ///*
        else if (wavesPassed < 15)
        {
            if (spawnChance < wavesPassed / 2 + (wavesPassed % 10) + 4)
            {
                if (whichType % 2 == 0)
                {
                    Instantiate(hazard3, position, rotation);
                } //End if (whichType % 2 == 0)
                else
                {
                    Instantiate(hazard2, position, rotation);
                } //End else
            } //End if (spawnChance < wavesPassed / 2 + (wavesPassed % 10) + 4)

            Instantiate(hazard1, position, rotation);
        } //End else if (wavesPassed < 15)
        else if (wavesPassed < 20)
        {
            if (spawnChance < wavesPassed / 2 + (wavesPassed % 10) + 4)
            {
                if (whichType % 2 == 0)
                {
                    Instantiate(hazard3, position, rotation);
                } //End if (whichType % 2 == 0)
                else
                {
                    Instantiate(hazard2, position, rotation);
                } //End else
            } //End if (spawnChance < wavesPassed / 2 + (wavesPassed % 10) + 4)

            Instantiate(hazard1, position, rotation);
        } //End else if (wavesPassed < 20)
        else
        {
            if (spawnChance < wavesPassed / 2 + (wavesPassed % 10) + 4)
            {
                if (whichType % 2 == 0)
                {
                    Instantiate(hazard3, position, rotation);
                } //End if (whichType % 2 == 0)
                else
                {
                    Instantiate(hazard2, position, rotation);
                } //End else
            } //End if (spawnChance < wavesPassed / 2 + (wavesPassed % 10) + 4)

            Instantiate(hazard1, position, rotation);
        } //End else
        //*/
    } //End private void spawnAsteroid(Vector3 position, Quaternion rotation)
    /// <summary>
    /// 
    /// </summary>
    /// <param name="wavesPassed"></param>
    /// <returns></returns>
    private bool extraSpawnChance(int wavesPassed)
    {
        int spawnChance = Random.Range(0, 20);
        
        if (wavesPassed < 5)
        {
            if (spawnChance < 5)
            {
                return true;
            } //End 
        } //End 
        else if (wavesPassed < 10)
        {
            if (spawnChance < wavesPassed/2 + (wavesPassed % 5) + 1)
            {
                return true;
            } //End 
        } //End 
        else if (wavesPassed < 15)
        {
            if (spawnChance < wavesPassed / 2 + (wavesPassed % 10) + 4)
            {
                return true;
            } //End 
        } //End 
        else if (wavesPassed < 20)
        {

        } //End 
        else
        {
            return true;
        } //End 
        

        return false;
    }
    /// <summary>
    /// 
    /// </summary>
    private void pauseGame()
    {
        if (gamePaused)
        {
            Time.timeScale = 0;
        } //End (gamePaused)
        else
        {
            Time.timeScale = 1;
        } //End else

    } //End private void pauseGame()
    /// <summary>
    /// 
    /// </summary>
    private IEnumerator showUpgradeMenu()
    {
        initUpgradeMenu();

        yield return new WaitUntil(() => !upgradeMenuOpen);

        Debug.Log("upgradeMenuOpen == " + upgradeMenuOpen);

        upgradeMenu.SetActive(upgradeMenuOpen);
        updateScore();
        updateWave();

    } //End private IEnumerator showUpgradeMenu()
    /// <summary>
    /// 
    /// </summary>
    private void initUpgradeMenu()
    {
        upgradeMenuOpen = true;
        upgradeMenu.SetActive(upgradeMenuOpen);
        upgradeMenuController.Show();

        upgradeMenuController.updateMenuText();
        scoreText.text = "";
        waveText.text = "";
        healthText.text = "";
        shieldText.text = "";

        upgradeMenuController.updateMenuText();
    } //End private void initUpgradeMenu()
    /// <summary>
    /// 
    /// </summary>
    private void clearAsteroids()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

        for (int i = 0; i < asteroids.Length; i++)
        {
            Destroy(asteroids[i]);
        } //End for (int i = 0; i < asteroids.Length; i++)
    } //End GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

    /***************Public***************/
    /// <summary>
    /// 
    /// </summary>
    public void startGameFromBtn()
    {
        gameStarted = true;
        startGame = false;
        mainMenuOpen = false;
        mainMenu.SetActive(mainMenuOpen);
        updateScore();
        updateWave();
        updateHealth();
        StartCoroutine(SpawnWaves());
    } //End public void startGameFromBtn()
    /// <summary>
    /// 
    /// </summary>
    public void updateHealth()
    {
        healthText.text = "Health: " + playerController.getCurrentHealth().ToString("G") + "/" + playerController.getMaxHealth().ToString("G");
    } //End private void updateHealth()
    /// <summary>
    /// 
    /// </summary>
    public void updateShield()
    {
        if (playerHasShield)
        {
            shieldText.text = "Sheild: " + playerController.getCurrentShield().ToString("G") + "/" + playerController.getMaxShield().ToString("G");
        } //End 
    } //End public void updateShield()
    /// <summary>
    /// 
    /// </summary>
    /// <param name="newScoreValue"></param>
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        numAsteroidsDestroyed += 1;
        updateScore();

        if (score >= 1000 && !doubleBoltUnlocked)
        {
            doubleBoltUnlocked = true;
            playerController.setChangeSelectedWeapon(2);
        } //End 
        else if (score >= 2500 && !tripleBoltUnlocked)
        {
            tripleBoltUnlocked = true;
            playerController.setChangeSelectedWeapon(3);
        } //End 
    } //End public void AddScore(int newScoreValue)
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int getScore()
    {
        return score;
    } //End public int getScore()
    /// <summary>
    /// 
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public int subtractFromScore(int amount)
    {
        return score -= amount;
    } //End public int subtractFromScore(int amount)
    /// <summary>
    /// 
    /// </summary>
    public void GameOver()
    {
        hazardCount = 0;
        gameOverText.text = "Game Over";
        gameOver = true;
    } //End public void GameOver()
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int getCurrentWave()
    {
        return currentWave;
    } //End public int getCurrentWave()
    /// <summary>
    /// 
    /// </summary>
    public void startNextWave()
    {
        upgradeMenuOpen = false;
        playerController.restoreShield();

        upgradeMenu.SetActive(upgradeMenuOpen);
        updateScore();
        updateWave();
        updateHealth();
        updateShield();
    } //End public void startNextWave()
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool getGameStarted()
    {
        return gameStarted;
    } //End public bool getGameStated()
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool getUpgradeMenuOpen()
    {
        return upgradeMenuOpen;
    } //End public bool getUpgradeMenuOpen()
    /// <summary>
    /// 
    /// </summary>
    public void gameStopped()
    {

    } //End public void gameStopped()
    /// <summary>
    /// 
    /// </summary>
    public void setPlayerHasShield()
    {
        playerHasShield = true;
    } //End public void setPlayerHasShield()

} //End public class GameController : MonoBehaviour
