using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    public GameObject player;

    [SerializeField]
    private UIManager _uiManager;

    void Start() 
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Update() 
    {
        if (gameOver == true)
        {   
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(player, Vector3.zero, Quaternion.identity); 
                gameOver = false;
                _uiManager.HideTitleScreen();
            }
        }   
    }

}
