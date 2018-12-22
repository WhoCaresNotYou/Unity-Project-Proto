using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Click : MonoBehaviour
{
    [SerializeField]
    private LayerMask clickableLayer;

    private List<GameObject> selectedObjects;

    [HideInInspector]
    public List<GameObject> selectableObjects;

    [HideInInspector]
    public string name, temp;

    private Vector3 mousePos1;
    private Vector3 mousePos2;

    public bool clickUI;

    void Awake()
    {
        selectedObjects = new List<GameObject>();
        selectableObjects = new List<GameObject>();

        temp = "Nothing";
        clickUI = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            /*FOR DRAG FUNCTION*/
            mousePos1 = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            RaycastHit hit;
    
            /*IF CLICKED ON SELECTABLE OBJECTS*/
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
                                out hit, 100.0f, clickableLayer))
            {
                Select selected = hit.collider.GetComponent<Select>();

                /*MULTIPLE OBJECTS TO SELECTION*/
                if (Input.GetKey("left ctrl"))
                {
                    /*OBJECTS WITH THE SAME TAG CAN BE MULTI-SELECTED*/
                    if (selected.tag == selectedObjects[selectedObjects.Count - 1].tag || name == selectedObjects[selectedObjects.Count].tag)
                    {
                        /*IF THE SELECTED OBJECT IS NOT ALREADY SELECTED,
                          IT WILL BE SELECTED*/
                        if (selected.currentlySelected == false)
                        {
                            selectedObjects.Add(hit.collider.gameObject);
                            selected.currentlySelected = true;
                            selected.Selected();
                        }
                        /*IF THE SELECTED OBJECT IS ALREADY SELECTED,
                          IT WILL BE DE-SELECTED*/
                        else
                        {
                            selectedObjects.Remove(hit.collider.gameObject);
                            if (selectedObjects.Count == 0)
                            {
                                name = "Nothing";
                                clickUI = false;
                            }
                            selected.currentlySelected = false;
                            selected.Selected();
                        }
                    }
                }
                /*WILL CLEAR PREVIOUS SELECTION,
                  AND ADD THE NEW SELECTED OBJECT*/
                else
                {
                    ClearSelection();

                    selectedObjects.Add(hit.collider.gameObject);
                    selected.currentlySelected = true;
                    selected.Selected();
                }            

                name = selected.tag;
            }
            /*IF NO SELECTABLES ARE CLICKED,
              SELECTION WILL BE CLEARED*/
            else
            {
                ClearSelection();
                name = "Nothing";
            }

            /*IF CLICKED ON UI OR CANVAS ELEMENTS*/
            /*NOT WORKING PRESENTLY*/
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                clickUI = true;
                name = "HUD";
            }
        }

        /*FOR DRAG FUNCTIONALLITY*/
        if (Input.GetMouseButtonUp(0))
        {
            mousePos2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            if (mousePos1 != mousePos2)
            {
                SelectObjects();
            }
        }

    }

    /*SELECTING MULTIPLE OBJECTS*/
    void SelectObjects()
    {
        List<GameObject> remObjects = new List<GameObject>();

        float width = mousePos2.x - mousePos1.x;
        float height = mousePos2.y - mousePos1.y;

        if(Input.GetKey("left ctrl") == false)
        {
            ClearSelection();
        }

        Rect selectRect = new Rect(mousePos1.x, mousePos1.y, width, height);

        int i = 0;

        foreach (GameObject selectObject in selectableObjects)
        {
            if(selectObject != null)
            { 
                if(selectRect.Contains(Camera.main.WorldToViewportPoint(selectObject.transform.position), true))
                {            
                    /*FIRST OBJECT'S TAG IS SAVED, AND
                      USED IS USED TO SEPERATE SELECTABLES WITH DIFFERENT TAGS*/
                    if(i == 0)
                    {
                        temp = selectObject.tag;
                        
                    }
                    if (temp == selectObject.tag)
                    {
                        selectedObjects.Add(selectObject);
                        selectObject.GetComponent<Select>().currentlySelected = true;
                        selectObject.GetComponent<Select>().Selected();

                        name = selectObject.tag;
                    }
                    else
                    {
                        ClearSelection();

                        break;
                    }

                    i++;
                }
            }
            /*REMOVING OBJECTS WITH NULL*/
            else
            {
                remObjects.Add(selectObject);

                temp = "Nothing";
                name = "Nothing";
            }
        }
        /*CLEARING remObjects LIST*/
        if(remObjects.Count > 0)
        {
            foreach(GameObject rem in remObjects)
            {
                selectableObjects.Remove(rem);
            }

            remObjects.Clear();
        }
    }

    void ClearSelection()
    {
        if (selectedObjects.Count > 0)
        {
            foreach (GameObject obj in selectedObjects)
            {
                if (obj != null)
                {
                    obj.GetComponent<Select>().currentlySelected = false;
                    obj.GetComponent<Select>().Selected();
                }
            }
            selectedObjects.Clear();   
        }
    }

    public bool ClickUI()
    {
        return clickUI;
    }

}

 