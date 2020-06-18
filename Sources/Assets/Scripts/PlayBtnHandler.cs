using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayBtnHandler : MonoBehaviour
{
    public void onClick()
    {
        Loader.Load();
    }
}
