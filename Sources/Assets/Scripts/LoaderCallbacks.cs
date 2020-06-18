using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallbacks : MonoBehaviour
{
    private bool firstUpdate = true;

    private void Awake()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (firstUpdate)
        {
            firstUpdate = false;
            Loader.LoaderCallback();
        }
    }
}
