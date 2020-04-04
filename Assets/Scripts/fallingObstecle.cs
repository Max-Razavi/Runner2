using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingObstecle : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "myPlayer")
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Ground")
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 3f);
        }

    }
}
