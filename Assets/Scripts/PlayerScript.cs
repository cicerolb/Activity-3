using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    public bool isGameOver;
    public int kills;
    [SerializeField] private GameObject killCounter;
    [SerializeField] private TextMeshProUGUI enemiesKilled;
    public AudioSource explosion;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        explosion = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            StartCoroutine(GameOver());
            
        }
    }

    IEnumerator GameOver()
    {
        
        yield return new WaitForSeconds(1f);
        gameOver.SetActive(true);
        killCounter.SetActive(true);
        enemiesKilled.text = "kills: " + kills;

        Time.timeScale = 0;

    }


   
}
