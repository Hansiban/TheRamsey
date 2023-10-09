using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Camera : MonoBehaviour
{
    CinemachineVirtualCamera cine;
    private GameObject player;
    [SerializeField] private GameObject backGround;
    // Start is called before the first frame update
    void Start()
    {
        //LookAt, Follow�� Player ���̱�
        cine = GetComponent<CinemachineVirtualCamera>();
        player = GameObject.FindGameObjectWithTag("Player");
        cine.LookAt = player.transform;
        cine.Follow = player.transform;
        backGround = GameObject.FindGameObjectWithTag("BackGround");
    }
    private void Update()
    {
        //��� ī�޶� ���̱�
        backGround.transform.position = gameObject.transform.position;
    }
}
