using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HUD : MonoBehaviour {

    public GUISkin resourceSkin, ordersSkin, selectBoxSkin, mouseCursorSkin;

    public Texture2D activeCursor;
    public Texture2D selectCursor, leftCursor, rightCursor, upCursor, downCursor;
    public Texture2D[] moveCursors, attackCursors, harvestCursors;

    public bool MouseInBounds() {
        //Screen coordinates start in the lower-left corner of the screen
        //not the top left of the screen like the drawing coordinates do
        Vector3 mousePos = Input.mousePosition;
        bool insideWidth = mousePos.x >= 0 && mousePos.x <= Screen.width - ORDERS_BAR_WIDTH;
        bool insideHeight = mousePos.y >= 0 && mousePos.y <= Screen.height - RESOURCE_BAR_HEIGHT;
        return insideWidth && insideHeight;
    }

    public Rect GetPlayingArea() {
        return new Rect ( 0 , RESOURCE_BAR_HEIGHT , Screen.width - ORDERS_BAR_WIDTH , Screen.height - RESOURCE_BAR_HEIGHT );
    }

    private const int ORDERS_BAR_WIDTH = 150, RESOURCE_BAR_HEIGHT = 40, SELECTION_NAME_HEIGHT = 30;

    private void OnGUI() {
        
            DrawOrdersBar ( );
            DrawResourceBar ( );
        
    }

    private void DrawOrdersBar() {
        GUI.skin = ordersSkin;
        GUI.BeginGroup ( new Rect ( Screen.width - ORDERS_BAR_WIDTH , RESOURCE_BAR_HEIGHT , ORDERS_BAR_WIDTH , Screen.height - RESOURCE_BAR_HEIGHT ) );
        GUI.Box ( new Rect ( 0 , 0 , ORDERS_BAR_WIDTH , Screen.height - RESOURCE_BAR_HEIGHT ) , "" );

        string selectionName = "";
        

        if(!selectionName.Equals("")) {
            GUI.Label ( new Rect ( 0 , 10 , ORDERS_BAR_WIDTH , SELECTION_NAME_HEIGHT ) , selectionName );
        }

        GUI.EndGroup ( );
    }

    private void DrawResourceBar() {
        GUI.skin = resourceSkin;
        GUI.BeginGroup ( new Rect ( 0 , 0 , Screen.width , RESOURCE_BAR_HEIGHT ) );
        GUI.Box ( new Rect ( 0 , 0 , Screen.width , RESOURCE_BAR_HEIGHT ) , "" );
        GUI.EndGroup();
    }
}
