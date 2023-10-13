using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 싱글톤 패턴을 사용하기 위한 인스턴스 변수
    private static GameManager _instance;
    // 인스턴스에 접근하기 위한 프로퍼티
    public static GameManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    private GameObject player;
    public GameObject canvas;
    [SerializeField] private GameObject EndUI;
    [SerializeField] private Text text;

    public void Quit()
    {
        Application.Quit();
    }
    public void GotoPetshop()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void GotoAcornVillage()
    {
        SceneManager.LoadScene(2);
        
    }
    public void GotoBoss()
    {
        SceneManager.LoadScene(3);
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        player = GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.FindGameObjectWithTag("Canvas");

        DontDestroyOnLoad(player);
        DontDestroyOnLoad(canvas);
        DontDestroyOnLoad(gameObject);
    }
    public void GameOver()
    {
        text.text = "! 게임오버 !";
        EndUI.SetActive(true);
        Destroy(player);
        Time.timeScale = 0;
    }
    public void GameClear()
    {
        text.text = "! 게임 클리어 !";
        EndUI.SetActive(true);
        Destroy(player);
        Time.timeScale = 0;
    }
}
