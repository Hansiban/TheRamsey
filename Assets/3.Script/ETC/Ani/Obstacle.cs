using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Obstacle : MonoBehaviour
{
    [SerializeField] TilemapCollider2D tilecol;

    private void Awake()
    {
        tilecol = GetComponent<TilemapCollider2D>();
    }
    private void Update()
    {

    }
}
