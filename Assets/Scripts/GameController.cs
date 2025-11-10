using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public Transform asterPrefab;
    private float spawnTime = 1f;
    private float spawnDelay = 0f;
    public TMP_Text timerText;
    public int count = 0;
    public TMP_Text winText;
    public Collider2D playerCol;
    private bool gameOver = false;
    private float asteroidSpeed = 3f;



    void Start()
    {
        InvokeRepeating("Spawn", spawnDelay, spawnTime);
        winText.text = "";
        timerText.text = "0";
    }

    private void Awake()
    {

            Instance = this;
        
    }


    public void AddScore(int scoreToAdd)
    {
        count += scoreToAdd; // Increment the central score
        timerText.text = count.ToString();
    }

    public void setGameOver(bool b)
    {
        gameOver = b;
    }
    void Spawn()
    {
        // All possible spawn positions (corners + sides)
        Vector3[] spawnPositions = new Vector3[]
        {
        new Vector3(-8f,  0f, 0f),  // left middle
        new Vector3( 8f,  0f, 0f),  // right middle
        new Vector3( 0f,  6f, 0f),  // top middle
        new Vector3( 0f, -6f, 0f),  // bottom middle
        new Vector3(-8f,  6f, 0f),  // top-left corner
        new Vector3( 8f,  6f, 0f),  // top-right corner
        new Vector3(-8f, -6f, 0f),  // bottom-left corner
        new Vector3( 8f, -6f, 0f)   // bottom-right corner
        };

        // Pick a random spawn point
        Vector3 spawnPos = spawnPositions[Random.Range(0, spawnPositions.Length)];

        // Instantiate asteroid
        Transform meteorT = Instantiate(asterPrefab, spawnPos, Quaternion.identity);
        GameObject meteor = meteorT.gameObject;

        // Calculate direction toward center (0,0,0)
        Vector3 target = new Vector3(0f, 0f, 0f); 
        Vector3 dir = (target - spawnPos).normalized;

        // Apply velocity (ensure prefab has Rigidbody2D)
        Rigidbody2D rb = meteor.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0f; // no falling
            rb.drag = 0f;         // no slowdown
            rb.velocity = dir * asteroidSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);

        }
    }
}
