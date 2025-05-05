using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    public GameObject camera;
    public Vector2 camera_start_position;
    public Vector2 hearts_start_position;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Main Camera");
        camera_start_position = camera.transform.position;
        hearts_start_position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 camera_position = camera.transform.position;
        Vector2 hearts_position = transform.position;
        Vector2 camera_delta = camera_position - camera_start_position;
        transform.position = new Vector2(hearts_start_position.x + camera_delta.x, hearts_start_position.y + camera_delta.y);
    }
}
