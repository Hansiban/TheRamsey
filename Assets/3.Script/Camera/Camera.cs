using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Camera : MonoBehaviour
{
    CinemachineVirtualCamera cine;
    private GameObject player;
    [SerializeField] private GameObject backGround;
    [SerializeField] private GameObject backGround2;
    // Start is called before the first frame update
    void Start()
    {
        //LookAt, Follow�� Player ���̱�
        cine = GetComponent<CinemachineVirtualCamera>();
        player = GameObject.FindGameObjectWithTag("Player");
        cine.LookAt = player.transform;
        cine.Follow = player.transform;
    }
    private void Update()
    {
        //��� ī�޶� ���̱�
        backGround.transform.position = gameObject.transform.position + Vector3.forward;
        backGround2.transform.position = gameObject.transform.position + Vector3.forward;
    }
}
