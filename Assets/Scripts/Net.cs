using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{
    private float zSpeed = 2f;
    private float ySpeed = 0.4f;
    private int zDirection = 1; // 1 for right, -1 for left
    private int yDirection = 1; // 1 for up, -1 for down
    private float zBoundary = 10f;
    private float yBoundaryMin = -4.5f;
    private float yBoundaryMax = -2.5f;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void SetDifficulty(float difficulty)
    {
        zSpeed *= difficulty;
        ySpeed *= difficulty;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            // move the net along z axis
            if (transform.position.z > zBoundary)
            {
                zDirection = -1;
            }
            else if (transform.position.z < -zBoundary)
            {
                zDirection = 1;
            }

            // move the net along y axis
            if (transform.position.y > yBoundaryMax)
            {
                yDirection = -1;
            }
            else if (transform.position.y < yBoundaryMin)
            {
                yDirection = 1;
            }

            // calculate the new position of the net
            Vector3 newPosition = transform.position;
            newPosition.z += zDirection * zSpeed * Time.deltaTime;
            newPosition.y += yDirection * ySpeed * Time.deltaTime;

            transform.position = newPosition;
        }
    }
}
