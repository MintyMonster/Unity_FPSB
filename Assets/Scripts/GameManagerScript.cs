using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{

    // Variables
    [SerializeField] Canvas countdownCanvas;
    [SerializeField] GameObject countdownText;
    [SerializeField] GameObject spawner;
    [SerializeField] GameObject HUDRoundTimer;
    [SerializeField] GameObject moneyCounter;
    [SerializeField] Canvas deathCanvas;

    private int waitTime = 50;
    private int seconds = 0;
    private int minutes = 0;

    private float round = 1f;
    private float countdownBetweenRounds = 3f;
    private float points = 0f;
    private float money = 0f;
    private float enemiesDefault = 10f;
    private float amountOfTurrets = 10f;
    private float EnemiesOnMap = 0f;
    private bool changeRound = false;
    private bool countdown = false;
    private float playerHealth = 100f;

    // Getters
    public float Round { get { return round; } }
    public float Points { get { return points; } }
    public float Money { get { return money; } }
    public float Health { get { return playerHealth; } set { playerHealth = value; } }

    // Function based setters
    public void AddPoints(float amount) => points += amount;
    public void AddMoney(float amount) => money += amount;
    public void AddRound() => round++;
    public float getAmountOfEnemies() { return (round * enemiesDefault); }

    private void Start()
    {
        EnemiesOnMap = getAmountOfEnemies(); // Get the total enemies
    }

    private void FixedUpdate()
    {
        // Set counters
        moneyCounter.GetComponent<Text>().text = $"${money.ToString()}";
        HUDRoundTimer.GetComponent<Text>().text = $"{minutes.ToString("D2")}:{seconds.ToString("D2")}";
        
        // Timer logic
        waitTime--;
        if(waitTime == 0)
        {
            seconds++;
            waitTime = 50;

            if(seconds >= 60)
            {
                seconds = 0;
                minutes++;
            }
        }
    }

    private void Update()
    { 
        
        if (!changeRound)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // Check enemies

            // Logic for handling money, points, enemies
            enemies.ToList<GameObject>().ForEach(x =>
            {
                if (x.GetComponent<EnemyManager>().Health <= 0)
                {
                    Destroy(x);
                    AddPoints(1);
                    AddMoney(5);
                    EnemiesOnMap -= 1;
                }
                
            });

            // (NOT WORKING) Round change
            if (EnemiesOnMap == 0)
            {
                AddRound();
                ChangeRound();
            }

            // (NOT WOKRKING) countdown for round change
            if (countdown) 
                StartCoroutine("CountDown");
            else
            {
                StopCoroutine("CountDown");
                countdown = false;
                countdownBetweenRounds = 3f;
                countdownCanvas.enabled = false;
            }
        }

        if (playerHealth <= 0) deathCanvas.enabled = true;
    }

    public void ChangeRound() // Change round (higher the round, better it is)
    {
        changeRound = true;

        // Set changeRound to true so that not checking for enemies
        // Throw up canvas
        // Set count down (visually in the canvas)
        // Start spawning enemies
        // Set changeRound to false, check for enemies

        countdownCanvas.enabled = true;
        countdown = true;

        Invoke("StartRound", 3);

    }
    // Start new round
    public void StartRound()
    {
        spawner.GetComponent<SpawnerManager>().Spawn = true;
        changeRound = false;
        spawner.GetComponent<SpawnerManager>().SpawnerOn = true;
    }

    // Countdown for UI
    IEnumerator CountDown()
    {
        for(; ; )
        {
            countdownText.GetComponent<Text>().text = countdownBetweenRounds.ToString() + "...";
            countdownBetweenRounds--;

            yield return new WaitForSeconds(1);
        }
    }
}
