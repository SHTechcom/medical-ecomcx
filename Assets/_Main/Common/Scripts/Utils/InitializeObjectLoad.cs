using System;
using UnityEngine;

public class InitializeObjectLoad : MonoBehaviour
{
    [SerializeField] private GameObject[] objs;
    [SerializeField] private bool dontDestroyOnLoad = true;

    private void Awake()
    {
        foreach (var obj in objs)
        {
            Instantiate(obj);
            if (dontDestroyOnLoad) DontDestroyOnLoad(obj);
        }
    }
}