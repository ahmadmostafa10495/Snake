using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SnakeScript : MonoBehaviour
{
    int[,] grid = new int[15, 10];
    int snakeScore = 2, snakeX = 0, snakeY = 4;
    Transform snakeTransform;
    float lastMove;
    float timeInBetweenMoves = 0.1f;
    Vector3 direction;
    bool hasLost = false;
    public GameObject applePreFab;
    public Text scoreText; 
    // Use this for initialization
    void Start()
    {
        snakeTransform = transform;
        direction = Vector3.right;
        grid[snakeX, snakeY] = snakeScore;
        grid[8, 4] = -1;
        GameObject go = Instantiate(applePreFab) as GameObject;
        go.transform.position = new Vector3(8, 4, 0);
        go.name = "Apple";
        scoreText.text = "Score: " + snakeScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
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


            if (snakeX >= grid.GetLength(0) || snakeX < 0 || snakeY >= grid.GetLength(1) || snakeY < 0)
            {
                Debug.Log("Game Over");
                hasLost = true;
            }
            else
            {
                if (grid[snakeX, snakeY] == -1)
                {
                    snakeScore++;
                    scoreText.text = "Score: " + snakeScore.ToString();
                    GameObject toDestroy = GameObject.Find("Apple");
                    Destroy(toDestroy);
                    for (int i = 0; i < grid.GetLength(0); i++)
                    {
                        for (int j = 0; j < grid.GetLength(1); j++)
                        {
                            if (grid[i, j] > 0)
                            {
                                grid[i, j]++;
                            }
                        }
                    }
                    bool appleCreated = false;
                    while (!appleCreated)
                    {
                        int x = UnityEngine.Random.Range(0, grid.GetLength(0));
                        int y = UnityEngine.Random.Range(0, grid.GetLength(1));
                        if (grid[x, y] == 0)
                        {
                            grid[x, y] = -1;
                            GameObject apple = Instantiate(applePreFab) as GameObject;
                            apple.transform.position = new Vector3(x, y, 0);
                            apple.name = "Apple";
                            appleCreated = true;
                        }
                    }
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
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (direction != Vector3.down)
            {
                direction = Vector3.up;
                Debug.Log("Move Up");
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (direction != Vector3.right)
            {
                direction = Vector3.left;
                Debug.Log("Move Left");
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (direction != Vector3.up)
            {
                direction = Vector3.down;
                Debug.Log("Move Down");
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (direction != Vector3.left)
            {
                direction = Vector3.right;
                Debug.Log("Move Right");
            }
        }

    }
    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene("Menu");
    }
    public void OnBackAgainClicked()
    {
        SceneManager.LoadScene("Game");
    }
}