using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    private Vector2Int foodPos;
    private GameObject foodObj;
    private int width;
    private int height;
    public static Field field;

    private void Awake()
    {
        field = this;
        field.width = 10;
        field.height = 10;
    }

    public void spawnFood()
    {
        do
        {
            field.foodPos = new Vector2Int(Random.Range(0, field.width), Random.Range(0, field.height));
        } while (Snake.snake.getSnakePos() == field.foodPos);

        field.foodObj = new GameObject("food", typeof(SpriteRenderer));
        field.foodObj.GetComponent<SpriteRenderer>().sprite = Assets.assets.foodSprite;

        field.foodObj.transform.position = new Vector3(field.foodPos.x, field.foodPos.y);
    }

    public bool checkFood(Vector2Int snakeHeadPos)
    {
        if (snakeHeadPos == field.foodPos)
        {
            Object.Destroy(field.foodObj);
            spawnFood();
            return true;
        }
        return false;
    } 

}

