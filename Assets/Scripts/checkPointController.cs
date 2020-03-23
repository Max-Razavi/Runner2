using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPointController : MonoBehaviour
{

    private SpriteRenderer changeSpriteRendere;
    public Sprite blackFlag,greenFlag;    
    public bool checkPointReached;



    // Start is called before the first frame update
    void Start()
    {
        changeSpriteRendere = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "player")
        {
            changeSpriteRendere.sprite = greenFlag;
            checkPointReached = true;
        }
        
    }

    


}
