using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    public static HashSet<int> bricksWithFloorHelperSet = new HashSet<int>();
    public static HashSet<int> bricksWithPaddleResizerSet = new HashSet<int>();
    
    // Start is called before the first frame update
    void Start()
    {
        // random index for brick with FloorHelper
        for (int index = 0; index < 7; index++)
        {
            int randomBrickIndex = Random.Range(1, 70);
			// prevent duplicate random index
            if (bricksWithFloorHelperSet.Contains(randomBrickIndex))
            {
                index--;
            }
            bricksWithFloorHelperSet.Add(randomBrickIndex);
        }

        // random index for brick with PaddleResizer
        for (int index = 0; index < 7; index++)
        {
            int randomBrickIndex = Random.Range(1, 70);
			// prevent duplicate random index
            if (bricksWithPaddleResizerSet.Contains(randomBrickIndex))
            {
                index--;
            }
            bricksWithPaddleResizerSet.Add(randomBrickIndex);
            
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
