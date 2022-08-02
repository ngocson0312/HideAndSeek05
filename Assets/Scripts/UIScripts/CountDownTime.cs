using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class CountDownTime : MonoBehaviour
{
    public UnityEvent AndTime;

    public static CountDownTime instance;
    [SerializeField] Image TimeImage;
    [SerializeField] Text TimeText;
    public float Duration, CurrentTime;
    public bool isEndGame =false;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        CurrentTime = Duration;
        TimeText.text = CurrentTime.ToString();
        StartCoroutine(TimeEnd());
    }

    IEnumerator TimeEnd()
    {
        if(GameManager.instance.level == 4|| GameManager.instance.level==9||GameManager.instance.level==14)
        {
            CurrentTime = 15f;
        }
        else
        {
            CurrentTime = 10f;
        }    
        while (CurrentTime>=0)
        {
            
            TimeImage.fillAmount = Mathf.InverseLerp(0, Duration, CurrentTime);
            
            TimeText.text = CurrentTime.ToString();
            yield return new WaitForSeconds(1f);
            CurrentTime--;

            if (CurrentTime <= 19 && CurrentTime > 9)
            {
                TimeText.color = Color.yellow;
            }
            else if (CurrentTime <= 9)
            {
                TimeText.color = Color.red;
            }
            else if (CurrentTime > 19)
            {
                TimeText.color = Color.green;
            }    
        }
        Debug.Log("end game");
       // Time.timeScale = 0;
        AndTime?.Invoke();


    }


}
