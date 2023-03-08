using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Rigidbody2D rb;
	private float minVelocity = 7F;
    private float maxVelocity = 9F;
    private bool isColliding = false;
	public int brickCollisionCount = 0;
	public Transform paddleResizer;
	public Transform floorHelper;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameScript.isPlaying == false)
        {
            if (Input.GetButtonDown("Jump"))
            {
                // Force the ball to move at the begining of the game
				rb.AddForce(Vector2.up * 400);
				
				// Randomly choose the direction of the ball
				float random = Random.value;
				if (random < 0.5F)
                {
					rb.AddForce(Vector2.left * Random.Range(200, 400));
                }
                else if (random > 0.5F)
                {
                    rb.AddForce(Vector2.right * Random.Range(200, 400));
                }
                GameScript.isPlaying = true;
            }
        }
        
		// maintain the ball velocity
        if (rb.velocity.magnitude > maxVelocity || rb.velocity.magnitude < minVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;       
        }

		// If the ball is colliding with the ground, decrease the lives
        if (isColliding == true)
        {
            GameScript.lives--;
            Debug.Log("Remaining lives: " + GameScript.lives);
            if (GameScript.lives == 0)
            {
                Debug.Log("Game Over");
	            GameScript.isGameOver = true;
            }
            isColliding = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Ground"))
        {
            isColliding = true;
        }

        if (target.gameObject.CompareTag("BrickLv1"))
        {
            GameScript.score += GameScript.brickLv1Score;
			checkForSpecialItem(target);
            Destroy(target.gameObject);
            GameScript.bricksRemaining--;
        }

        if (target.gameObject.CompareTag("BrickLv2"))
        {
            GameScript.score += GameScript.brickLv2Score;
			checkForSpecialItem(target);
            Destroy(target.gameObject);
            GameScript.bricksRemaining--;
        }

        if (target.gameObject.CompareTag("BrickLv3"))
        {
            GameScript.score += GameScript.brickLv3Score;
			checkForSpecialItem(target);
            Destroy(target.gameObject);
            GameScript.bricksRemaining--;
        }

		// Floor helper
		if (target.gameObject.CompareTag("Floor"))
		{
			target.gameObject.SetActive(false);
			FloorHelperScript.isFloorActive = false;
		}
    }
	
	private void randomGeneratePaddleResizer(Collision2D target)
    {
		// Check if the brick is in the set of bricks that will generate paddle resizer item
		if (BrickScript.bricksWithPaddleResizerSet.Contains(brickCollisionCount))
        {
			Instantiate(paddleResizer, target.transform.position, Quaternion.identity);
            PaddleResizerScript.remainingItem--;
        }
	}
	
	private void randomGenerateFloorHelper(Collision2D target)
    {
		// Check if the brick is in the set of bricks that will generate floor helper item
	    if (BrickScript.bricksWithFloorHelperSet.Contains(brickCollisionCount))
        {
			Instantiate(floorHelper, target.transform.position, Quaternion.identity);
            FloorHelperScript.remainingItem--;
        }
	}

	private void checkForSpecialItem(Collision2D target)
    {
		randomGeneratePaddleResizer(target);
		randomGenerateFloorHelper(target);
		brickCollisionCount++;
	}
}
