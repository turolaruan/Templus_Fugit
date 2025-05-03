using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatEffect : MonoBehaviour
{
    public float amplitude = 0.2f;
    public float frequency = 1f;

    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPos + Vector3.up * Mathf.Sin(Time.time * frequency) * amplitude;
        transform.Rotate(0f, 0f, 20f * Time.deltaTime);
    }
}
