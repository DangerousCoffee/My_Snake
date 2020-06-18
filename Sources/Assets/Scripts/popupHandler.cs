using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PopupHandler : MonoBehaviour
{

    private static Text popup;
    private static bool canMove = false;

    // Start is called before the first frame update
    void Start()
    {
        popup = transform.Find("popupText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.up, 1000);
    }

    public static void popupText(string popupStr)
    {
        popup.text = popupStr;
        canMove = true;
    }
}
