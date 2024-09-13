using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 offset;
    [SerializeField] private float cam_speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        Vector3 newPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position , newPosition, cam_speed*Time.deltaTime);
    }
    
}
