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
    private float horizontalInput;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        ballRb = GetComponent<Rigidbody>();
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
            transform.Translate(Vector3.forward * horizontalInput * moveSpeed * Time.deltaTime);
            float zPos = Mathf.Clamp(transform.position.z, -zBoundry, zBoundry);
            transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
        }

        if (Input.GetKeyDown(KeyCode.Space) &&!isTossed)
        {
            TossBall();
        }
    }
    void TossBall()
    {
        isTossed = true;
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
