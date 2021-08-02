using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerAndScore : MonoBehaviour
{
    public Text timerText;
    public Text scoreText;

    public int minutes = 2;
    public int seconds = 59;
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Start the timer
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        //Updates the score constantly for every enemy killed
        scoreText.text = "Score: " + score;
        //Updates the timer to display on screen
        if (seconds < 10)
        {
            //Small quality of life to show single digits correctly
            timerText.text = "Time Left: \n" + minutes + ":0" + seconds;
        }
        else
            timerText.text = "Time Left: \n" + minutes + ":" + seconds;
    }

    //Timer to update the timer in minutes
    IEnumerator Timer()
    {
        //If the seconds goes below 0
        if (seconds == -1)
        {
            //...and if the minutes are 0
            if (minutes == 0)
            {
                //Game over, timer ended, kill the player
                GameObject.FindWithTag("Player").GetComponent<PlayerController>().lives = 0;
                GameObject.FindWithTag("Player").GetComponent<Ragdoll>().isDead = true;
                Application.Quit();
            }
            //Otherwise...
            else
            {
                //A minute has passed
                minutes -= 1;
                //Reset the seconds
                seconds = 59;
            }
        }
        yield return new WaitForSeconds(1);
        //A second has passed
        seconds -= 1;
        //Continue to update after every second.
        StartCoroutine(Timer());
    }
}
