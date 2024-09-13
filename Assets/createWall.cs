using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createWall : MonoBehaviour
{
    public Transform lastWall;
    public GameObject wallPrefab;
    Vector3 lastPos;
    Camera cam;
    movement player;
    // Start is called before the first frame update
    void Start()
    {
        lastPos = lastWall.position;
        cam = Camera.main;
        player = FindObjectOfType<movement>();
        InvokeRepeating("wallCreate", 0.7f, 0.2f);
    }

    // Update is called once per frame


    public void wallCreate() 
    {
        float distance = Vector3.Distance(lastPos, player.transform.position);
        if(distance > cam.orthographicSize*2) return;


        Vector3 newPos = Vector3.zero;
        int rand = Random.Range(0,11);
        if(rand > 5)
            newPos = new Vector3(lastPos.x + 0.7071068f, lastPos.y, lastPos.z + 0.7071068f);
        else
            newPos = new Vector3(lastPos.x - 0.7273202f, lastPos.y, lastPos.z + 0.6886795f);

        GameObject newBlock = Instantiate(wallPrefab, newPos, Quaternion.Euler(0,45,0), transform);
        newBlock.transform.GetChild(0).gameObject.SetActive(rand %3 == 2);
        lastPos = newBlock.transform.position;
    }
}
