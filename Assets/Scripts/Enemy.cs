using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] bool direction;
    [SerializeField] int nLives = 1;
    [SerializeField] GameObject live;

    float maxX, minX;

    float sizeX, sizeY;

    List<GameObject> lives ;

    // Start is called before the first frame update
    void Start()
    {
        Camera gameCamera = Camera.main;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;

        sizeX = (GetComponent<SpriteRenderer>()).bounds.size.x;
        sizeY = (GetComponent<SpriteRenderer>()).bounds.size.y;


        lives = new List<GameObject>();
        setLives();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (direction)
            transform.Translate(new Vector2(speed * Time.deltaTime, 0));
        else
            transform.Translate(new Vector2(-speed * Time.deltaTime, 0));

        changeDir(transform.position.x);
        
    }
    void setLives()
    {
        int jumpLine = 0;
        float line = 0;
        float col = 0;
        for (int i = 0; i < nLives; i++)
        {
            if (jumpLine < 3)
            {
                
                lives.Add(Instantiate(live, this.transform.position - new Vector3(sizeX - (0.1f +col), sizeY - (1.6f+line)), Quaternion.identity));
                jumpLine++;
                col=0.5f+col;
            }
            else
            {
                lives.Add(Instantiate(live, this.transform.position - new Vector3(sizeX - (0.1f + col), sizeY - (1.6f + line)), Quaternion.identity));
                jumpLine = 0;
                line=0.5f+line;
                col = 0;
            }
        }

        foreach (GameObject temp in lives)
        {
            temp.transform.parent = this.transform;
        }
    }
    public void changeDir(float d)
    {
        if (d >= maxX)
            direction = false;
        else if(d <=minX)
            direction = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Blast")){

            lives[nLives-1].GetComponent<Live>().deadHeart();
            nLives--;

            Debug.Log(nLives);
            if (nLives == 0)
            {
                Destroy(this.gameObject);
                for(int i = 0; i < lives.Count; i++)
                {
                    Destroy(lives[i].gameObject);
                }
            }
        }
    }
}
