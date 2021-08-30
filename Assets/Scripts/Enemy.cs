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

        for (int i = 0; i < nLives; i++)
        {
            lives.Add(Instantiate(live, this.transform.position - new Vector3(sizeX - (0.5f * i), sizeY - (1.6f)), this.transform.rotation));

        }

        foreach (GameObject temp in lives)
        {
            temp.transform.parent = this.transform;
        }
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
