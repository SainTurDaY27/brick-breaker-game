using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleResizerScript : MonoBehaviour
{
    public float speed = 3;
    public float percentage = 0.3F;
    public static float percentageValue;
    public static int remainingItem = 7;
    public static bool stillRemaining;

    // Start is called before the first frame update
    void Start()
    {
        stillRemaining = true;
    }

    // Update is called once per frame
    void Update()
    {
        percentageValue = percentage;
        transform.Translate(new Vector2(0F, -1F) * Time.deltaTime * speed);
        if (remainingItem == 0)
        {
            stillRemaining = false;
        }
    }
    
}