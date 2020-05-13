using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assets : MonoBehaviour
{

    public static Assets assets;
    public Sprite bodySprite;
    public Sprite headSprite;
    private void Awake()
    {
        assets = this;
    }

}
