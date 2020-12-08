using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Attached to battery objects
public class BatteryPickUp : MonoBehaviour
{
    public Text hudBattery;
    public static int batteryCountInt = 0;
    public GameObject puzzleCompleteScreen;

    private GameObject battery;
    private GameObject chest;
    private GameObject player;
    private TriggerChestOpen chestScript;

    void Start()
    {
        chest = GameObject.FindGameObjectWithTag("Chest");
        if (chest != null)
        {
            chestScript = chest.GetComponent<TriggerChestOpen>();
        }

        battery = GameObject.FindGameObjectWithTag("Battery");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (chest != null)
        {
            if (chestScript.isOpen())
            {
                batteryCountInt++;
                hudBattery.text = "Batteries:" + batteryCountInt + "/4";
                battery.SetActive(false);
                
                // tell treasure chest that the battery has been collected. Reference TriggerChestOpen.cs
                chestScript.collectBattery();
            }
            else if (chestScript.hasBeenOpened())
            {
                if (battery != null)
                {
                    battery.SetActive(false);
                    battery = null;
                }
            }
        }
    }
}
