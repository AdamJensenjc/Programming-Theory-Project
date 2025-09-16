using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyNet : Net
{
    protected override void Move()
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

        // calculate the new position of the net
        Vector3 newPosition = transform.position;
        newPosition.z += zDirection * zSpeed * Time.deltaTime;

        transform.position = newPosition;
    }
}
