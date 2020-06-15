using CodeMonkey;
using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2Int gridDirection;
    private Vector2Int gridPos;
    private Vector2Int tailEnd;
    private float moveTimer;
    private float moveTimerMax;
    private int bodySize;
    private List<Vector2Int> body;
    public static Snake snake;

    private void Awake()
    {
        snake = this;
        snake.gridPos = new Vector2Int(1, 1);
        snake.gridDirection = new Vector2Int(1, 0);
        snake.moveTimerMax = .5f;
        snake.moveTimer = snake.moveTimerMax;
        snake.body = new List<Vector2Int>();
        snake.bodySize = 3;

        Field.field.spawnFood();
    }

    void Update()
    {
        inputHandler();
        movementHandler();
    }

    private void movementHandler()
    {
        snake.moveTimer += Time.deltaTime;
        if (snake.moveTimer >= snake.moveTimerMax)
        {
            snake.moveTimer -= snake.moveTimerMax;

            snake.body.Insert(0, snake.gridPos);

            snake.gridPos += snake.gridDirection;

            if (snake.body.Count >= snake.bodySize + 1)
            {
                snake.body.RemoveAt(snake.body.Count - 1);
            }

            for (int i = 0; i < snake.body.Count; i++)
            {
                Vector2Int currPos = snake.body[i];
                World_Sprite sprite = World_Sprite.Create(new UnityEngine.Vector3(currPos.x, currPos.y), UnityEngine.Vector3.one * .5f, Color.green);
                FunctionTimer.Create(sprite.DestroySelf, snake.moveTimerMax);
            }

            transform.position = new UnityEngine.Vector3(gridPos.x, gridPos.y);
            transform.eulerAngles = new UnityEngine.Vector3(0, 0, angleFromVector(gridDirection) - 90);

            if (Field.field.checkFood(snake.gridPos))
            {
                snake.bodySize++;
            }
        }
    }

    private void inputHandler()
    {
        Vector2Int lastDirection = snake.gridDirection;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (lastDirection.y != -1)
            {
                snake.gridDirection.x = 0;
                snake.gridDirection.y = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (lastDirection.y != 1)
            {
                snake.gridDirection.x = 0;
                snake.gridDirection.y = -1;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (lastDirection.x != 1)
            {
                snake.gridDirection.x = -1;
                snake.gridDirection.y = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (lastDirection.x != -1)
            {
                snake.gridDirection.x = 1;
                snake.gridDirection.y = 0;
            }
        }
    }

    private float angleFromVector(Vector2Int direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;
        return angle;
    }

    public Vector2Int getSnakePos()
    {
        return snake.gridPos;
    }
}
