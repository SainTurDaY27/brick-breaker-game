using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    private Vector2Int size = new Vector2Int(10, 7);
    private Vector2 offset = new Vector2(1.7F, 0.7F);
    private GameObject currentBrickPrefab;
    public GameObject brickPrefab1;
    public GameObject brickPrefab2;
    public GameObject brickPrefab3;
    public static int brickLv1Score = 10;
    public static int brickLv2Score = 20;
    public static int brickLv3Score = 50;
    public static bool isPlaying = false;
    public static int lives = 3;
    public static int score = 0;
    public static bool isGameOver = false;
    public static int bricksRemaining = 70;
    public Text scoreUI;
    public Text gameAnnouncement;
    

    // Start is called before the first frame update
    void Start()
    {
        // Add bricks to the game
        for (int row = 0; row < size.x; row++)
        {
            for (int column = 0; column < size.y; column++)
            {
                if (column <= 1)
                {
                    currentBrickPrefab = brickPrefab1;
                }
                else if (column <= 4 && column > 1)
                {
                    currentBrickPrefab = brickPrefab2;
                }
                else if (column <= 6 && column > 4)
                {
                    currentBrickPrefab = brickPrefab3;
                }
                GameObject newBrick = Instantiate(currentBrickPrefab, transform);
                newBrick.transform.position = new Vector2((float)((size.x - 1) * 0.5F - row) * offset.x, column * offset.y);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && isPlaying == false)
        {
            gameAnnouncement.text = " ";
            Debug.Log("Game Started");
        }

        // If the game is over, wait for the player to press space bar to restart the game
        if (isGameOver == true)
        {
            // Pause the game
            Time.timeScale = 0;
            gameAnnouncement.text = "GAME OVER, PRESS 'SPACE BAR' TO RESTART";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ResetGame();
                Time.timeScale = 1;
            }
        }
        
        // When all bricks are destroyed, restart the game
        if (bricksRemaining == 0)
        {
            Debug.Log("Game finished, Restarting...");
            Time.timeScale = 0;
            ResetGame();
            Time.timeScale = 1;
        }
        scoreUI.text = "SCORE: " + score + " LIVES: " + lives;
    }

    private void Awake()
    {
        lives = 3;
        score = 0;
        isGameOver = false;
        isPlaying = false;
        bricksRemaining = 70;
        PaddleResizerScript.remainingItem = 7;
        FloorHelperScript.remainingItem = 7;
    }

    public static void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
