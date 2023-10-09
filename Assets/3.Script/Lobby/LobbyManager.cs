using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] GameObject SavePanel;
    [SerializeField] GameObject Setting;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Setting.activeSelf)
            {
                Setting.SetActive(false);
            }
            else if(SavePanel.activeSelf)
            {
                SavePanel.SetActive(false);
            }
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Save1()
    {
        SceneManager.LoadScene(1);
    }
}
