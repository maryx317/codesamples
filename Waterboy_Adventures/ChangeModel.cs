using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Attached to model change objects
public class ChangeModel : MonoBehaviour
{
    public string characterModel;
    public GameObject modelSpawn;
    public GameObject chest;

    private TextController textcontroller;
    private GameObject player;
    private TriggerChestOpen chestScript;

    void Start()
    {
        textcontroller = GameObject.FindObjectOfType(typeof(TextController)) as TextController;
        player = GameObject.FindGameObjectWithTag("Player");
        chestScript = chest.GetComponent<TriggerChestOpen>();
    }

    void Update()
    {
        if (chestScript.isOpen())
        {
            GameObject parent = GameObject.Find("PersistenceParent");
            Component[] objects = parent.GetComponentsInChildren(parent.transform.GetType(), true);
            
            GameObject next = null;
            foreach (Component obj in objects)
            {
                if (obj.gameObject.name == characterModel)
                {
                    obj.gameObject.transform.position = modelSpawn.transform.position;
                    obj.gameObject.SetActive(true);
                    next = obj.gameObject;
                }
            }

            player.gameObject.SetActive(false);
            player = next;
            this.gameObject.SetActive(false);

            // tell treasure chest the model has successfully been changed. Reference TriggerChestOpen.cs
            chestScript.changeModel();
        }
        else if (chestScript.hasBeenOpened())
        {
            this.gameObject.SetActive(false);
        }
    }
}
