using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// JH - Added the Cinemachine package so that I can declare a variable of
// type CinemachineVirtualCamera
using Cinemachine;

public class LevelManagerScript : MonoBehaviour
{
    public static bool facingRightMainBool = true;

    // JH - Create a variable that can hold a reference to a CinemachineVirtualCamera
    // component. This should be set via the inspector.
    public CinemachineVirtualCamera virtualCamera;

    public static LevelManagerScript instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // JH - A function that other scripts can call to set the Follow property on
    // the Cinemachine Virtual Camera to a Transfom for it to follow.
    public void setVCamFollow(Transform transformToFollow)
    {
        this.virtualCamera.Follow = transformToFollow;
    }
}
