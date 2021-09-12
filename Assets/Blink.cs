using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    public Text isPausedText;
    public Text isTweeningText;
    public Text tweenIdText;
    private Image image;
    private int tweenId;

    void Start()
    {
        image = GetComponent<Image>();
        tweenId = LeanTween.alpha(image.rectTransform, 0.3f, 0.5f).setLoopPingPong().id;
        LeanTween.pause(tweenId);
    }

    public void StartBlinking()
    {
        LeanTween.resume(tweenId);
        Debug.Log("Resuming " + tweenId);
    }

    public void StopBlinking()
    {
        LeanTween.pause(tweenId);
        Debug.Log("Pausing " + tweenId);
    }

    void Update()
    {
        tweenIdText.text = "ID: " + tweenId.ToString();
        isPausedText.text = LeanTween.isPaused(tweenId).ToString();
        isTweeningText.text = LeanTween.isTweening(tweenId).ToString();
    }
}
