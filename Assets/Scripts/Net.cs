using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{
    private float speed = 2;
    private int direction = 1; //1 for right, -1 for left
    private float zBoundry = 10;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void SetDifficulty(float difficulty)
    {
        speed *= difficulty;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isGameActive)
        {
            //move the net between (-20, 20) on the z axis
            if (transform.position.z > zBoundry)
            {
                direction = -1;
            }
            else if (transform.position.z < -zBoundry)
            {
                direction = 1;
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + direction * speed * Time.deltaTime);
        }
    }
}
