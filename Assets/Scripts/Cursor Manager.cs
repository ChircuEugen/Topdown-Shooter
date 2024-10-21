using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorIcon;
    Vector2 cursorHotspot;

    // Start is called before the first frame update
    void Start()
    {
        cursorHotspot = new Vector2(cursorIcon.width / 2, cursorIcon.height / 2);
        Cursor.SetCursor(cursorIcon, cursorHotspot, CursorMode.Auto);
    }
}
