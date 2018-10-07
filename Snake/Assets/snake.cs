using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snake : MonoBehaviour {
    int[,] grid = new int[10, 10];
    int snakeScore = 5, snakeX = 0, snakeY = 4;
    Transform snakeTransform;
    float lastMove;
    float timeInBetweenMoves = 0.35f;
    Vector3 direction;
    bool hasLost = false;
    public GameObject applePreFab;
	// Use this for initialization
	void Start () {
        snakeTransform = transform;
        direction = Vector3.right;
        grid[snakeX, snakeY] = snakeScore;
        grid[8, 4] = -1;
        Instantiate(applePreFab);
	}
	
	// Update is called once per frame
	void Update () {
        if (hasLost)
        {
            return;
        }
        if (Time.time - lastMove > timeInBetweenMoves)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] > 0)
                    {
                        grid[i, j]--;
                        if (grid[i, j] == 0)
                        {
                            GameObject toDestroy = GameObject.Find(i.ToString() + j.ToString());
                            if (toDestroy != null)
                            {
                                Destroy(toDestroy);
                            }
                        }
                    }
                }
            }

            lastMove = Time.time;

            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.transform.position = new Vector3(snakeX, snakeY, 0);
            go.name = snakeX.ToString() + snakeY.ToString();

            if (direction.x == 1)
            {
                snakeX++;
            }
            else if (direction.x == -1)
            {
                snakeX--;
            }
            if (direction.y == 1)
            {
                snakeY++;
            }
            else if (direction.y == -1)
            {
                snakeY--;
            }
            

            if (snakeX > 9 || snakeX < 0 || snakeY > 9 || snakeY < 0)
            {
                Debug.Log("Game Over");
                hasLost = true;
            }
            else
            {
                if (grid[snakeX, snakeY] == -1)
                {
                    snakeScore++;
                }
                else if (grid[snakeX, snakeY] != 0)
                {
                    Debug.Log("Game Over");
                    hasLost = true;
                }
                Debug.Log("Move");
                snakeTransform.position += direction;
                grid[snakeX, snakeY] = snakeScore;
                
            }
            
        }
        if (Input.GetKey(KeyCode.W))
        {
            direction = Vector3.up;
            Debug.Log("Move Up");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction = Vector3.left;
            Debug.Log("Move Left");
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction = Vector3.down;
            Debug.Log("Move Down");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction = Vector3.right;
            Debug.Log("Move Right");
        }
		
	}
}
