using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

    [SerializeField] private float speed = 4f;
    private bool lookingRight = true;
    gameManager GameManager;
    Animator anim;
    [SerializeField] private Transform rayOrigin;
    Score gem_score;
    public ParticleSystem effect;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.FindObjectOfType<gameManager>();
        anim = gameObject.GetComponent<Animator>();
        gem_score = GameObject.FindObjectOfType<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.gameStarted) return;
        
        anim.SetTrigger("gameStarted");

        transform.position += new Vector3(0,0,1) * speed * Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            turn();
        }

        checkFalling();
    }

    float elapsedTime = 0;
    float freq = 1f / 5f;
    public void checkFalling() 
    {   
        if((elapsedTime += Time.deltaTime) > freq) 
        {
            if(!Physics.Raycast(rayOrigin.position , new Vector3(0,-1,0))) 
            {
                anim.SetTrigger("falling");
                GameManager.RestartGame();
                elapsedTime = 0;
            }
        } 
    }
     private void turn() 
    {
        if(lookingRight){
            transform.Rotate(new Vector3(0,1,0), -90);
         } 
         else {
            transform.Rotate(new Vector3(0,1,0), 90);
         }
        lookingRight = !lookingRight;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "crystal") 
        {
            makeScore();
            createEffect();
            Destroy(other.gameObject);
            Debug.Log("değdi");
        }
    }
    private void OnCollisionExit(Collision collision) 
    {
        Destroy(collision.gameObject, 2f);
    }

    void createEffect() 
    {
        var vfx =  Instantiate(effect, transform);
        Destroy(vfx, 1f);
    }
    void makeScore() 
    {
        gem_score.score++;
        gem_score.ScoreTxt.text = gem_score.score.ToString();

        if(gem_score.score > gem_score.HScore) 
        {
            gem_score.HScore = gem_score.score;
            gem_score.HiScoreTxt.text = gem_score.HScore.ToString();
            PlayerPrefs.SetInt("Hiscore", gem_score.HScore);
        }
    }
}
