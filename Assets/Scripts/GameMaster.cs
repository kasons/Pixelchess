using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    public bool selectPhase { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        selectPhase = true;
    }

    public void BeginBattle()
    {
        selectPhase = false;
        Debug.Log("Battle phase!");
    }
}
