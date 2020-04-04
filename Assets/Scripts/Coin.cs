using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private LevelManager _levelManager;
    public int coinValue;
    //private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "myPlayer")
        {
            _levelManager.AddCoins(coinValue);
            //this.GetComponent<AudioSource>().Play();
            //soundManager.sndMan.PlayCoinSound();
            Destroy(gameObject);
            
            
        }
        
    }
}
