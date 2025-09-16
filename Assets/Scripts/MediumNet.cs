using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumNet : Net// INHERITANCE
{
    protected override void Move()// POLYMORPHISM
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
