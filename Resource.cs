using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource : MonoBehaviour
{
    [HideInInspector]
    public float total;
    [HideInInspector]   
    public float current;
    [HideInInspector]
    public float limit;

    public Image ind;

    public bool clicked;
    public bool limitReached;

    // Start is called before the first frame update
    void Start()
    {
        clicked = false;
        limitReached = false;

        if (gameObject.tag == "Flowers")
        {
            total = 20.0f;
            current = 20.0f;
            limit = 4.0f;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(clicked)
        {
            Clicked();
        }
    }

    private void Clicked()
    {
        current -= 0.01f;

        ind.fillAmount = current / total;

        if (current <= 0)
        {
            Death();
        }

        Debug.Log((total - current) % 4);

        if ((total - current) % 4 <= 0.005f)
        {
            limitReached = true;
            clicked = false;
        }
    }
   
    private void Death()
    {
        Destroy(gameObject);
    }
}
