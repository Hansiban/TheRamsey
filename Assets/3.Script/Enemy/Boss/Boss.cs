using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("HP")]
    public int hp;
    public Health health;

    [Header("Make")]
    public int minTrans;
    public int maxTrans;
    public int makeCount;

    [Header("brick")]
    public GameObject[] bricks;

    [Header("Red")]
    [SerializeField] private GameObject red;
    private int spawnpoint;
    private List<int> spawnpoints;
    private Vector2 position;

    public void Awake()
    {
        health = GetComponent<Health>();
        spawnpoints = new List<int>();
        makeCount = 4;
        hp = 3;
    }
    public void Start()
    {
        Stomp();
    }

    public void Stomp()
    {
        for (int i = 0; i < makeCount; i++)
        {
            MakeRed();
        }
        Invoke("Makebrick", 1.5f);
    }

    public void Makebrick()
    {
        for (int i = 0; i < makeCount; i++)
        {
            GameObject obj;
            if (i == makeCount-1)
            {
                 obj = Instantiate(bricks[1]);
            }
            else
            {
                 obj = Instantiate(bricks[0]);
            }
            obj.transform.position = new Vector2(spawnpoints[i], 5);
        }
    }

    public void MakeRed()
    {
        while (true)
        {
            spawnpoint = Random.Range(minTrans, maxTrans);
            // 같은 스폰 포인트가 있는지 검사
            if (!spawnpoints.Contains(spawnpoint))
            {
                spawnpoints.Add(spawnpoint);
                position = new Vector2(spawnpoint, 0);
                var gameobject = Instantiate(red, position, Quaternion.identity);
                break;
            }
        }
    }

    public void Damage()
    {
        //대충 데미지 1 맞는 함수 뚝딱뚝딱
        health.curhealth--;
    }
}