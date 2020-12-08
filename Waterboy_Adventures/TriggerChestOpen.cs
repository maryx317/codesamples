using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Attach to the treasure chest object for when the player collides with the treasure chest
public class TriggerChestOpen : MonoBehaviour
{
    public string[] nextModels;
    
    public enum Status
    {
        CLOSED,
        OPEN, 
        OPENED
    }

    private Animator anim;
    private Status state;
    private bool modelUpdated;
    private bool batteryPicked;

    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        foreach (string model in nextModels)
        {
            if (player.name == model)
            {
                state = Status.OPENED;
            }
        }

        anim = GetComponent<Animator>();
        if (state == Status.CLOSED) 
        {
            anim.SetBool("OpenChest", false);
        }
        else 
        {
            anim.SetBool("OpenChest", true);
        }
    }

    void Update()
    {
        if (batteryPicked && modelUpdated)
        {
            state = Status.OPENED;
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (state == Status.CLOSED)
        {
            state = Status.OPEN;
            anim.SetBool("OpenChest", true);
        }
    }

    public void changeModel()
    {
        modelUpdated = true;
    }

    public void collectBattery()
    {
        batteryPicked = true;
    }

    public bool isOpen()
    {
        return (state == Status.OPEN);
    }

    public bool hasBeenOpened()
    {
        return (state == Status.OPENED);
    }
}
