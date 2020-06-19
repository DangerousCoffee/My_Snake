using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    private enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    private bool gameOverFlag;

    private Direction gridDirection;
    private Direction lastMoveDir;
    private Vector2Int gridPos;
    private float moveTimer;
    private float moveTimerMax;
    private int bodySize;
    private List<SnakeMovePos> bodyPositions;
    private List<SnakeBodySegment> bodyObjects;
    public static Snake snake;

    private void Awake()
    {
        snake = this;
        snake.gridPos = new Vector2Int(1, 1);
        snake.gridDirection = Direction.Right;
        snake.moveTimerMax = .15f;
        snake.moveTimer = snake.moveTimerMax;
        snake.bodyPositions = new List<SnakeMovePos>();
        snake.bodyObjects = new List<SnakeBodySegment>();
        snake.bodySize = 3;
        snake.gameOverFlag = false;

        for (int i = 0; i < bodySize; i++)
        {
            SnakeMovePos lastMovePos = null;
            if (bodyPositions.Count > 0)
            {
                lastMovePos = bodyPositions[0];
            }
            snake.bodyPositions.Add(new SnakeMovePos(lastMovePos, snake.gridPos, snake.gridDirection));
            snake.addBody();
        }

        Field.field.spawnFood();
    }

    void Update()
    {
        if (!snake.gameOverFlag)
        {
            inputHandler();
            movementHandler();
        }
    }

    private void movementHandler()
    {
        snake.moveTimer += Time.deltaTime;
        if (snake.moveTimer >= snake.moveTimerMax)
        {
            snake.moveTimer -= snake.moveTimerMax;

            SnakeMovePos lastMovePos = null;
            if (bodyPositions.Count > 0)
            {
                lastMovePos = bodyPositions[0];
            }

            snake.bodyPositions.Insert(0, new SnakeMovePos(lastMovePos, snake.gridPos, snake.gridDirection));

            snake.gridPos += directionVector();

            snake.gridPos = Field.field.checkBoundries(snake.gridPos);

            if (Field.field.checkFood(snake.gridPos))
            {
                SoundManager.playEatSound();
                snake.bodySize++;
                snake.addBody();
            }

            if (snake.bodyPositions.Count >= snake.bodySize + 1)
            {
                snake.bodyPositions.RemoveAt(snake.bodyPositions.Count - 1);
            }

            foreach(SnakeMovePos snakeMovePos in snake.bodyPositions)
            {
                if (snake.gridPos == snakeMovePos.getGridPos())
                {
                    SoundManager.playDeathSound();
                    Game_script.gameOver();
                    snake.gameOverFlag = true;
                }
            }
            transform.position = new Vector3(gridPos.x, gridPos.y);
            transform.eulerAngles = new Vector3(0, 0, angleFromVector(directionVector()) - 90);

            snake.updateBody();
            snake.lastMoveDir = snake.gridDirection;
        }
    }

    private void addBody()
    {
        snake.bodyObjects.Add(new SnakeBodySegment(snake.bodyObjects.Count));
    }

    private void updateBody()
    {
        for (int i = 0; i < snake.bodyObjects.Count; i++)
        {
            snake.bodyObjects[i].setMovePos(snake.bodyPositions[i]);
        }
    }

    private Vector2Int directionVector()
    {
        switch (snake.gridDirection)
        {
            case Direction.Right: return new Vector2Int(1, 0);
            case Direction.Left: return new Vector2Int(-1, 0);
            case Direction.Up: return new Vector2Int(0, 1);
            case Direction.Down: return new Vector2Int(0, -1);
            default: return new Vector2Int(0, 0);
        }
    }

    private void inputHandler()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (snake.lastMoveDir != Direction.Down)
            {
                snake.gridDirection = Direction.Up;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (snake.lastMoveDir != Direction.Up)
            {
                snake.gridDirection = Direction.Down;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (snake.lastMoveDir != Direction.Right)
            {
                snake.gridDirection = Direction.Left;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (snake.lastMoveDir != Direction.Left)
            {
                snake.gridDirection = Direction.Right;
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

    public List<Vector2Int> snakeBodyPositionList()
    {
        List<Vector2Int> gridPosList = new List<Vector2Int>();
        foreach (SnakeMovePos snakeMovePos in snake.bodyPositions)
        {
            gridPosList.Add(snakeMovePos.getGridPos());
        }
        return gridPosList;
    }

    private class SnakeBodySegment
    {

        private SnakeMovePos currPos;
        private Vector3 tmpPos = new Vector3(50, 50);
        private Transform transform;

        public SnakeBodySegment(int bodyIndex)
        {
            GameObject bodySegment = new GameObject("snakeBody", typeof(SpriteRenderer));
            bodySegment.transform.position = this.tmpPos;
            bodySegment.GetComponent<SpriteRenderer>().sprite = Assets.assets.bodySprite;
            this.transform = bodySegment.transform;
            bodySegment.GetComponent<SpriteRenderer>().sortingOrder = -bodyIndex;
        }

        public void setMovePos(SnakeMovePos movePos)
        {
            this.currPos = movePos;
            transform.position = new Vector3(this.currPos.getGridPos().x, this.currPos.getGridPos().y);
            transform.eulerAngles = new Vector3(0, 0, getAngle(this.currPos));
        }

        private int getAngle(SnakeMovePos movePos)
        {
            switch (movePos.GetDirection())
            {
                case Direction.Right: 
                    switch(movePos.getPrevDirection())
                    {
                        default: 
                            return 90;
                        case Direction.Down: 
                            return 45;
                        case Direction.Up:
                            return -45;
                    }
                case Direction.Left:
                    switch (movePos.getPrevDirection())
                    {
                        default:
                            return -90;
                        case Direction.Down:
                            return -45;
                        case Direction.Up:
                            return 45;
                    }
                case Direction.Up: 
                    switch (movePos.getPrevDirection())
                    {
                        default:
                            return 0;
                        case Direction.Left:
                            return 45;
                        case Direction.Right:
                            return -45;
                    }
                case Direction.Down: 
                    switch (movePos.getPrevDirection())
                    {
                        default:
                            return 180;
                        case Direction.Left:
                            return 180 - 45;
                        case Direction.Right:
                            return 180 + 45;
                    }
                default: return 0;
            }
        }
    }

    private class SnakeMovePos
    {

        private SnakeMovePos lastMovePos;
        private Vector2Int gridPos;
        private Direction direction;

        public SnakeMovePos(SnakeMovePos lastMovePos, Vector2Int gridPos, Direction direction)
        {
            this.lastMovePos = lastMovePos;
            this.gridPos = gridPos;
            this.direction = direction;
        }

        public Vector2Int getGridPos()
        {
            return this.gridPos;
        }

        public Direction GetDirection()
        {
            return this.direction;
        }

        public Direction getPrevDirection()
        {
            if(this.lastMovePos == null)
            {
                return Direction.Right;
            }
            return lastMovePos.GetDirection();
        }
    }
}
