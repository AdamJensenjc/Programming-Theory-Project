using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Net : MonoBehaviour
{
    protected float zSpeed = 2f;
    protected float ySpeed = 0.6f;
    protected int zDirection = 1; // 1 for right, -1 for left
    protected int yDirection = 1; // 1 for up, -1 for down
    protected float zBoundary = 10f;
    protected float yBoundaryMin = -4.5f;
    protected float yBoundaryMax = -1.5f;
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
            Move();
        }
    }

    protected abstract void Move();
}
