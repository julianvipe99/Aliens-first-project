using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;


    int enemyCount;
    private void Awake()
    {
        int managers=GameObject.FindObjectsOfType<GameManager>().Length;

        if (managers > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(this);
        

    }
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        enemyCount = enemies.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    void OnSceneLoaded( Scene scene, LoadSceneMode mode)
    {
        GameObject.Find("Canvas").SetActive(false);
        foreach(GameObject enemy in enemies)
        {
            float minX = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x;
            float maxX = Camera.main.ViewportToWorldPoint(new Vector2(1,0)).x;

            float X = Random.Range(minX, maxX);
            Instantiate(enemy, new Vector3(X, 1.2f), Quaternion.identity);
        }
    }
}
