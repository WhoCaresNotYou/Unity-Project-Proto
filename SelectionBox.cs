using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBox : MonoBehaviour
{
    [SerializeField]
    private RectTransform selectImage;

    Vector3 startPos;
    Vector3 endPos;

	// Use this for initialization
	void Start ()
    {
        selectImage.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                startPos = hit.point;
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            selectImage.gameObject.SetActive(false);
        }

        if(Input.GetMouseButton(0))
        {
            if(!selectImage.gameObject.activeInHierarchy)
            {
                selectImage.gameObject.SetActive(true);
            }

            endPos = Input.mousePosition;

            Vector3 start = Camera.main.WorldToScreenPoint(startPos);
            start.z = 0f;

            Vector3 center = (start + endPos) / 2f;

            selectImage.position = center;

            float sizeX = Mathf.Abs(start.x - endPos.x);
            float sizeY = Mathf.Abs(start.y - endPos.y);

            selectImage.sizeDelta = new Vector2(sizeX, sizeY); 
        }
	}
}
