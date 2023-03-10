using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float size = 1.5F;
    public Transform floorHelperPrefab;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameScript.isPlaying == true)
        {
            // Paddle that move horizontally, player can move paddle left and right with mouse cursor
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rb.position = new Vector2(mousePosition.x, rb.position.y);
        }

        // update paddle size
        transform.localScale = new Vector2(size, 1);       
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
		// Paddle Resizer special item
        if (target.gameObject.CompareTag("PaddleResizer") && PaddleResizerScript.stillRemaining == true)
        {
			Debug.Log("Paddle Resizer activated");
			// Paddle Resizer will randomly increase or decrease the paddle size
            float random = Random.value;
                    if (random < 0.5F)
                    {
                        size = size + PaddleResizerScript.percentageValue;
                    
                    } else if (random > 0.5F)
                    {
                        size = size - PaddleResizerScript.percentageValue;
                    }
        }
        
		// Floor Helper special item
        if (target.gameObject.CompareTag("FloorHelper") && FloorHelperScript.stillRemaining == true)
        {
            if (FloorHelperScript.isFloorActive == false)
            {
				// Instantiate floor helper below the paddle
				Debug.Log("Floor Helper activated");
            	Transform floorHelper = Instantiate(floorHelperPrefab, new Vector3(0, -4.5F, 0), Quaternion.identity);
				floorHelper.gameObject.SetActive(true);

				// Remove floor helper after a certain amount of time
				IEnumerator RemoveAfterSeconds(float seconds, GameObject gameObject)
    			{
           			yield return new WaitForSeconds(seconds);
            		gameObject.SetActive(false);
        		}
				StartCoroutine(RemoveAfterSeconds(FloorHelperScript.floorDuration, floorHelper.gameObject));	
            }
        }    
        Destroy(target.gameObject);
    }
}
