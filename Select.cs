using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{
    private GameObject SelectionHighlight;

    private PlayerController pC;

    private string tagName;

    [HideInInspector]
    public bool currentlySelected;

    // Use this for initialization
    void Start()
    { 
        Camera.main.gameObject.GetComponent<Click>().selectableObjects.Add(this.gameObject); 
        currentlySelected = false;
        pC = gameObject.GetComponent<PlayerController>();
        SelectionHighlight = this.gameObject.transform.GetChild(0).gameObject;
        Selected();
    }

    public void Selected()
    {
        tagName = Camera.main.GetComponent<Click>().name;

        if (currentlySelected == false)
        {
            SelectionHighlight.SetActive(false);
            if (gameObject.tag == "Player")
            {
                pC.enabled = !pC.enabled;
            }
        }
        else
        {
            SelectionHighlight.SetActive(true);
            if (gameObject.tag == "Player")
            {
                pC.enabled = !pC.enabled;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
