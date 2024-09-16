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
    public int poolSize = 30;
    private Queue<GameObject> wallPool;
    // Start is called before the first frame update
    void Start()
    {
        lastPos = lastWall.position;
        cam = Camera.main;
        player = FindObjectOfType<movement>();
        InitializePool();
        InvokeRepeating("wallCreate", 0.7f, 0.2f);
    }

    // Update is called once per frame


    void InitializePool()
    {
        wallPool = new Queue<GameObject>();
        for(int i = 0; i< poolSize; i++) 
        {
            GameObject newBlock = Instantiate(wallPrefab, Vector3.zero, Quaternion.Euler(0,45,0), transform); 
            newBlock.SetActive(false);
            wallPool.Enqueue(newBlock);
        }
    }

    GameObject GetBlockFromPool() 
    {
        if(wallPool.Count > 0)
        {
            GameObject block = wallPool.Dequeue();
            block.SetActive(true);
            return block;  
        }
        else 
        {
            GameObject newBlock = Instantiate(wallPrefab, Vector3.zero , Quaternion.Euler(0,45,0), transform);            
            return newBlock;
        }
    }

    public void ReturnBlockToPool (GameObject block) 
    {
        block.SetActive(false);
        wallPool.Enqueue(block);
    }

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


        GameObject newBlock = GetBlockFromPool();
        newBlock.transform.position = newPos;
        newBlock.transform.rotation = Quaternion.Euler(0,45,0);
        newBlock.transform.GetChild(0).gameObject.SetActive(rand %3 == 2);
        lastPos = newBlock.transform.position;

        StartCoroutine(ReturnBlockAfterDelay(newBlock, 2f));
    
    }

    IEnumerator ReturnBlockAfterDelay(GameObject block, float delay) 
    {
      if(Vector3.Distance(lastPos, player.transform.position) > 1f) 
       {
        yield return new WaitForSeconds(delay);
       }
        
        ReturnBlockToPool(block);
    }

}