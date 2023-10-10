using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject canvas;
    public void GotoAcornVillage()
    {
        SceneManager.LoadScene(2);
    }
    public void GotoPetshop()
    {
        SceneManager.LoadScene(1);
    }
    private void Awake()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            canvas = GameObject.FindGameObjectWithTag("Canvas");
        }
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(canvas);
    }
}
