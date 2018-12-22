using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selection_Info : MonoBehaviour
{ 
    private string tagName;
    private string temp;

    private bool click;

    public GameObject player;
    public GameObject hive;

    // Use this for initialization
    void Start ()
    {
        //buttonComponent.onClick.AddListener(HandleClick);
        player.SetActive(false);
        hive.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        tagName = Camera.main.GetComponent<Click>().name;
        click = Camera.main.GetComponent<Click>().clickUI;

        if (tagName == "Player")
        {
            //BEE_HUD
            player.SetActive(true);
            hive.SetActive(false);
            temp = "Player";
        }
        if (tagName == "Hive")
        {
            //HIVE HUD
            hive.SetActive(true);
            player.SetActive(false);
            temp = "Hive";
        }
        if(tagName == "Nothing")
        {
            player.SetActive(false);
            hive.SetActive(false);
            temp = "Nothing";
        }
        if (tagName == "HUD")
        {
            if (temp == "Player")
            {
                player.SetActive(true);
                hive.SetActive(false);
            }
            if (temp == "Hive")
            {
                hive.SetActive(true);
                player.SetActive(false);
            }
        }
    }
}
