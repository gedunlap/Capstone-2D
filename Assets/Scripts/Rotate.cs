using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // how many times 360 degrees per second
    [SerializeField] private float speed = 2f;

    private void Update()
    {
        // only want z value rotation / delta time makes update independent of framerate
        transform.Rotate(0, 0, 360 * speed * Time.deltaTime);
    }
}
