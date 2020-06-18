using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assets : MonoBehaviour
{

    public static Assets assets;
    public Sprite foodSprite;
    public Sprite bodySprite;

    private void Awake()
    {
        assets = this;
    }

}
