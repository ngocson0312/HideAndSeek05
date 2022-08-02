using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Camera cam;
    [SerializeField] GameObject canvasMainUI;
    [SerializeField] GameObject InSideGameUI;
    [SerializeField] GameObject WinDialogUI;
    public bool isPlayGame = false;
    public bool zoomCam = false;
    private void Awake()
    {
        instance = this;
        cam.depth = 1;
    }


    public void Cam()
    {
        isPlayGame = true;
        cam.depth = -1;
        canvasMainUI.SetActive(false);
        InSideGameUI.SetActive(true);
        zoomCam = true;
    }

    public void EndTime()
    {
        isPlayGame = false;
        WinDialogUI.SetActive(true);
    }    
}
