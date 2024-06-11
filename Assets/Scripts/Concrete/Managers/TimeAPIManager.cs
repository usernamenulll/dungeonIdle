using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.Networking;

public class TimeAPIManager : MonoBehaviour
{
    private DateTime _currentDateTime = DateTime.MinValue;

    struct TimeData
    {
    public string datetime;
    }
    private void OnEnable()
    {
        GetRealTime();
    }

    public void GetRealTime()
    {
        StartCoroutine(GetRealTimeFromAPI());
    }
    IEnumerator GetRealTimeFromAPI()
    {
        string url = "https://worldtimeapi.org/api/ip";
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        Debug.Log("getting real datetime ...");
        webRequest.timeout = 3;
        while (true)
        {
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                TimeData timeData = JsonUtility.FromJson<TimeData>(webRequest.downloadHandler.text);

                _currentDateTime = DateTime.Parse(timeData.datetime);

                if (GameManager.Instance)
                {
                    GameManager.Instance.ActiveTime = _currentDateTime;
                    //Debug.Log(GameManager.Instance.ActiveTime);
                }

                //Debug.Log(_currentDateTime.ToString());
            }
            webRequest = UnityWebRequest.Get(url);
            webRequest.timeout = 3;
            yield return webRequest.SendWebRequest();
            yield return new WaitForSeconds(webRequest.timeout);

        }
    }


}

/*
        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            while (true)
            {

                TimeData timeData = JsonUtility.FromJson<TimeData>(webRequest.downloadHandler.text);

                _currentDateTime = DateTime.Parse(timeData.datetime);

                Debug.Log(_currentDateTime.ToString());
                webRequest = UnityWebRequest.Get(url);
                webRequest.timeout = 3;
                yield return webRequest.SendWebRequest();
                yield return new WaitForSeconds(webRequest.timeout);

            }
        }
        else
        {
            Debug.Log("error");
            Debug.Log(webRequest.error);
        }
*/
