using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menuplay : MonoBehaviour {

	public void Button_OnClick_Play()
    {
        Application.LoadLevel(1);
    }
    public void Button_OnClick_Quit()
    {
        Application.Quit();
    }
}
