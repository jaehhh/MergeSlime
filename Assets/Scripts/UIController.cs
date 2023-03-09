using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum ClickSound { fuse = 0, }

// UI 관련
public class UIController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textTouchUI;
    [SerializeField]
    Canvas canvas;
    [SerializeField]
    GameObject panelAlarm;
    [SerializeField]
    TextMeshProUGUI textCoin;

    private AudioSource audio;
    [SerializeField]
    private AudioClip[] clips;


    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public void AlarmMessage(string message)
    {
        panelAlarm.GetComponentInChildren<TextMeshProUGUI>().text = message;

        StartCoroutine(EnableAndDisable(panelAlarm));
    }

    private IEnumerator EnableAndDisable(GameObject target)
    {
        panelAlarm.SetActive(true);

        yield return new WaitForSeconds(2f);

        panelAlarm.SetActive(false);
    }

    public void UpdateCoin(int coin)
    {
        textCoin.text = $"Coin {coin}";
    }

    public void TouchEffect(int point)
    {
        TextMeshProUGUI clone = Instantiate(textTouchUI);
        clone.transform.SetParent(canvas.transform);

        clone.text = $"+{point}";

        StartCoroutine("TouchEffectMove", clone);
    }

    private IEnumerator TouchEffectMove(TextMeshProUGUI textTouchUI)
    {
        textTouchUI.transform.position = Input.mousePosition;

        float time = 0;

        while (time < 0.5f)
        {
            time += Time.deltaTime;

            textTouchUI.transform.position += Vector3.up * 0.4f;

            yield return null;
        }

        Destroy(textTouchUI.gameObject);
    }

    public void Sound(ClickSound sound)
    {
        audio.Pause();
        audio.clip = null;
        audio.clip = clips[(int)sound];
        audio.Play();
    }
}
