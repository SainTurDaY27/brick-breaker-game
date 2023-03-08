using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D target)
    {
        // Remove special item when it touches the ground
        if (target.gameObject.CompareTag("PaddleResizer") || target.gameObject.CompareTag("FloorHelper"))
        {
            Destroy(target.gameObject);
        }
    }
}
