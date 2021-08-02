using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Variables")]
    public Text playerLives;

    public Image weaponImage;

    public Text numBullets;

    public Text numLives;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        int weaponCount = 0;
        //For every weapon available...
        foreach (WeaponClass weapon in player.GetComponent<NewWeaponPickup>().weapons)
        {
            //If that weapon is the current weapon...
            if (player.GetComponent<Pawn>().weapon == weapon)
            {
                //Set the UI to show that weapon's image
                weaponImage.sprite = player.GetComponent<NewWeaponPickup>().weaponImages[weaponCount];
            }
            //Otherwise...
            else
            {
                //Move onto the next weapon
                weaponCount++;
            }
        }
        //Update the amount of bullets the player has left to use.
        numBullets.text = "Bullets: " + player.GetComponent<Pawn>().weapon.numberBullets;

        //Update the number of remaining lives
        numLives.text = "Lives: " + player.GetComponent<PlayerController>().lives;
    }

    //Pause menu buttons
    public void OnResumeButton()
    {
        GameManager.instance.Unpause();
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
