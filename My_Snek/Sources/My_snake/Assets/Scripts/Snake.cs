using CodeMonkey;
using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2Int gridDirection;
    private Vector2Int gridPos;
    private float moveTimer;
    private float moveTimerMax;
    private int bodySize;
    private List<Vector2Int> posList;


    private void Awake()
    {
        Debug.Log("Snake script start");
        gridPos = new Vector2Int(1, 1);
        gridDirection = new Vector2Int(1, 0);
        moveTimerMax = .5f;
        moveTimer = moveTimerMax;
        posList = new List<Vector2Int>();
        bodySize = 3;
    }

    void Update()
    {
        inputHandler();
        movementHandler();
    }

    private void movementHandler()
    {
        moveTimer += Time.deltaTime;
        if (moveTimer >= moveTimerMax)
        {
            gridPos += gridDirection;
            moveTimer -= moveTimerMax;


            posList.Insert(0, gridPos);

            gridPos += gridDirection;

            if (posList.Count >= bodySize + 1)
            {
                posList.RemoveAt(posList.Count - 1);
            }
  

            for (int i = 0; i < posList.Count; i++)
            {
                Vector2Int currPos = posList[i];
                World_Sprite sprite = World_Sprite.Create(new Vector3(currPos.x, currPos.y), Vector3.one * .5f, Color.green);
                FunctionTimer.Create(sprite.DestroySelf, moveTimerMax);
            }

            transform.position = new Vector3(gridPos.x, gridPos.y);
            transform.eulerAngles = new Vector3(0, 0, angleFromVector(gridDirection) - 90);
        }
    }

    private void inputHandler()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (gridDirection.y != -1)
            {
                gridDirection.x = 0;
                gridDirection.y = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (gridDirection.y != 1)
            {
                gridDirection.x = 0;
                gridDirection.y = -1;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (gridDirection.x != 1)
            {
                gridDirection.x = -1;
                gridDirection.y = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (gridDirection.x != -1)
            {
                gridDirection.x = 1;
                gridDirection.y = 0;
            }
        }
    }

    private float angleFromVector(Vector2Int direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;
        return angle;
    }
}
