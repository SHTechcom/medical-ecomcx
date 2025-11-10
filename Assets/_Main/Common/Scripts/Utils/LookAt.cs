using System;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Camera cam;
    public Transform target;

    private void LateUpdate()
    {
        if (!target) return;
        cam.transform.LookAt(target);
    }

    private void OnValidate()
    {
        if (cam != null && target != null)
        {
            cam.transform.LookAt(target);
        }
    }
}