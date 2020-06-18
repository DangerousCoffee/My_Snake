using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        Game_scene,
        Loading
    }

    private static Action loaderCallback;

    public static void Load()
    {
        loaderCallback = () =>
        {
            SceneManager.LoadScene(Scene.Game_scene.ToString());
        };

        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    public static void LoaderCallback()
    {
        if (loaderCallback != null)
        {
            loaderCallback();
            loaderCallback = null;
        }
    }
}
