using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceshipController : MonoBehaviour
{
    private int CoinCounter;
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        CoinCounter = 0;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coin")
        {
            //Grafik deaktivieren
            other.gameObject.GetComponent<Renderer>().enabled = false;
            other.gameObject.GetComponent<Collider2D>().enabled = false;
            //Destroy(other.gameObject);
            //Sound der Münze abspielen
            AudioSource Audio = other.gameObject.GetComponent<AudioSource>();
            Audio.Play();
            //Münze zerstören
            Destroy(other.gameObject, Audio.clip.length);

            CoinCounter++;

            scoreText.text = CoinCounter.ToString();
            Debug.Log("Score: " + CoinCounter);
        }
        else if (other.tag == "BigCoin")
        {
            //Grafik deaktivieren
            other.gameObject.GetComponent<Renderer>().enabled = false;
            other.gameObject.GetComponent<Collider2D>().enabled = false;
            //Sound der Münze abspielen
            AudioSource Audio = other.gameObject.GetComponent<AudioSource>();
            Audio.Play();
            //Audio.ignoreListenerPause = true;     ---> ignoriert Tonsperre z.B. im Pause-Menu
            //Münze zerstören
            Destroy(other.gameObject, Audio.clip.length);

            CoinCounter += 5;

            scoreText.text = CoinCounter.ToString();
            Debug.Log("Score: " + CoinCounter);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
