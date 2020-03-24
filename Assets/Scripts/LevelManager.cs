using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{

    public float respawnDelay;
    public PlayerMov _playerMov;
    public int coins;
    public Text coinText;

    // Start is called before the first frame update
    void Start()
    {
        _playerMov = FindObjectOfType<PlayerMov>();

        coinText.text = "Score : " + coins;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        StartCoroutine("RespawnCoroutine");
    }
    //making delay for respawn just for player
    public IEnumerator RespawnCoroutine()
    {
        _playerMov.gameObject.SetActive(false);
        yield return new WaitForSeconds(respawnDelay);

        _playerMov.transform.position = _playerMov.respawnPoint;
        _playerMov.gameObject.SetActive(true);
    }

    //collect coin
    public void AddCoins(int numberOfCoins)
    {
        coins = coins + numberOfCoins;
        coinText.text = "Score : " + coins;
    }

}
