using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Basketball : MonoBehaviour
{
    private float moveSpeed = 10f;
    private float force = 4.3f;
    private Rigidbody ballRb;
    private bool isTossed = false;
    private float zBoundry = 8f;
    private float yBoundryMax = 3.5f;
    private float yBoundryMin = 1.5f;
    private float horizontalInput;
    private float verticalInput;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        ballRb = GetComponent<Rigidbody>();
        ballRb.useGravity = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isGameActive)
            return;

        if (!isTossed)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector3.forward * horizontalInput * moveSpeed * Time.deltaTime);
            transform.Translate(Vector3.up * verticalInput * moveSpeed * Time.deltaTime);
            float zPos = Mathf.Clamp(transform.position.z, -zBoundry, zBoundry);
            float yPos =  Mathf.Clamp(transform.position.y, yBoundryMin, yBoundryMax);
            transform.position = new Vector3(transform.position.x, yPos, zPos);
        }

        if (Input.GetKeyDown(KeyCode.Space) &&!isTossed)
        {
            TossBall();
        }
    }
    void TossBall()
    {
        isTossed = true;
        ballRb.useGravity = true;
        ballRb.AddForce(new Vector3(-1, 2.2f, 0) * force, ForceMode.Impulse);
        ballRb.AddTorque(new Vector3(0, 0, -1) * force, ForceMode.Impulse);
        gameManager.SpawnBall();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTossed)
        {
            Destroy(gameObject);
        }
    }
}
