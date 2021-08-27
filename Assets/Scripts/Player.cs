using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float bottomPadding = 1f;

    [Header("Blast")]
    [SerializeField] GameObject blast;
    [SerializeField] float nextFire;

    float xMin, xMax;

    float yMin, yMax;

    float sizeX, sizeY;

    float canFire;
    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBounderies();   
    }
    private void SetUpMoveBounderies()
    {
        Camera gameCamera = Camera.main;

        sizeX = (GetComponent<SpriteRenderer>()).bounds.size.x;
        sizeY = (GetComponent<SpriteRenderer>()).bounds.size.y;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + sizeX/2;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - sizeX/2;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + bottomPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 1, 0)).y - sizeY/2;

        
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        fire();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);

    }

    private void fire()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time>=canFire)
        {
            Instantiate(blast,transform.position - new Vector3(0,sizeY/2),transform.rotation);
            canFire=nextFire + Time.time;
        }
    }
}
