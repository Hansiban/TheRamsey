using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // �̱��� ������ ����ϱ� ���� �ν��Ͻ� ����
    private static GameManager _instance;
    // �ν��Ͻ��� �����ϱ� ���� ������Ƽ
    public static GameManager Instance
    {
        get
        {
            // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
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

        // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� �����Ѵ�.
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
        text.text = "! ���ӿ��� !";
        EndUI.SetActive(true);
        Destroy(player);
        Time.timeScale = 0;
    }
    public void GameClear()
    {
        text.text = "! ���� Ŭ���� !";
        EndUI.SetActive(true);
        Destroy(player);
        Time.timeScale = 0;
    }
}
