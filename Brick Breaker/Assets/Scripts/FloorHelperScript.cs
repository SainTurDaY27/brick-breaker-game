using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorHelperScript : MonoBehaviour
{
    public float speed = 3;
    public float duration = 10F;
    public static float floorDuration;
    public static int remainingItem = 7;
    public static bool stillRemaining;
    public static bool isFloorActive = false;

    // Start is called before the first frame update
    void Start()
    {
        stillRemaining = true;
    }

    // Update is called once per frame
    void Update()
    {
        floorDuration = duration;
        transform.Translate(new Vector2(0F, -1F) * Time.deltaTime * speed);
        if (remainingItem == 0)
        {
            stillRemaining = false;
        }
    }
}
