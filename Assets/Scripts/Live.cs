using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Live : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    [SerializeField]Sprite newSprite;

    bool isDead=false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isDead)
        {
            changeSprite();
        }
    }

    public void changeSprite()
    {
        spriteRenderer.sprite = newSprite;

    }

    public void deadHeart()
    {
        isDead = true;
    }

   
}
