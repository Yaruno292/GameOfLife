using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CameraController : MonoBehaviour
{

    public GameOfLife game;

    public float cameraSpeed = 10f;
    public float zoomSpeed = 7f;

    // just check keyboard input and change camera position
    // camera position is clamped to game size
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 newPos = transform.position;
        newPos.x += Time.deltaTime * x * cameraSpeed;
        newPos.y += Time.deltaTime * y * cameraSpeed;

        newPos.x = Mathf.Clamp(newPos.x, 0f, (float)(game.sizeX - 1));
        newPos.y = Mathf.Clamp(newPos.y, 0f, (float)(game.sizeY - 1));

        transform.position = newPos;

        if (Input.GetKey(KeyCode.Equals))
            Zoom(zoomSpeed);
        else if (Input.GetKey(KeyCode.Minus))
            Zoom(-zoomSpeed);
    }

    // primitive zoom method which changes cameras z position
    private void Zoom(float z)
    {
        Vector3 newPos = transform.position;
        newPos.z += Time.deltaTime * z;
        newPos.z = Mathf.Clamp(newPos.z, -50f, -10f);
        transform.position = newPos;
    }
}
