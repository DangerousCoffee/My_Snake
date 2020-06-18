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
        field.width = 29;
        field.height = 29;

    }

    public void spawnFood()
    {
        do
        {
            field.foodPos = new Vector2Int(Random.Range(-field.width/2, field.width/2), Random.Range(-field.height/2, field.height/2));
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
            Game_script.updateScore();
            return true;
        }
        return false;
    } 

    public Vector2Int checkBoundries(Vector2Int gridPos)
    {
        if (gridPos.x < -field.width/2)
        {
            gridPos.x = field.width/2; 
        }
        if (gridPos.x > field.width/2)
        {
            gridPos.x = -field.width/2;
        }
        if (gridPos.y < -field.height/2)
        {
            gridPos.y = field.height/2;
        }
        if (gridPos.y > field.height/2)
        {
            gridPos.y = -field.height/2;
        }
        return gridPos;
    }
}

