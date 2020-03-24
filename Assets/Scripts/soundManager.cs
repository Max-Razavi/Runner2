using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{

    public static soundManager sndMan;
    private AudioSource audioSource;
    private AudioClip[] coinSound;

    private int randomcoin;

    // Start is called before the first frame update
    void Start()
    {
        sndMan = this;
        audioSource = GetComponent<AudioSource>();
        coinSound = Resources.LoadAll<AudioClip>("Sounds");
        
    }

    // Update is called once per frame
    public void PlayCoinSound()
    {
        randomcoin = Random.Range(0, 5);
        audioSource.PlayOneShot(coinSound[randomcoin]);
    }
}
