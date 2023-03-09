using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    private void Update()
    {
#if UNITY_ANDROID || UNITY_IPHONE
        if(Input.GetTouch(0).phase == TouchPhase.Began)
        {
            SceneManager.LoadScene("MainScene");
        }

#else
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("MainScene");
        }

#endif
    }
}
