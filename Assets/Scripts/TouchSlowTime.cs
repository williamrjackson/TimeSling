using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSlowTime : MonoBehaviour {

    [SerializeField]
    private TouchAxisCtrl m_TouchControl;
    [SerializeField]
    [Range(0, 1)]
    private float m_TouchTimeScale = .25f;
    [SerializeField]
    [Range(0, 1)]
    private float m_TimeChangeSpeed = .25f;

    private Coroutine coroToTimeScale;
    // Use this for initialization
    void Start () {
        m_TouchControl.OnTouch += OnTouch;
        m_TouchControl.OnUntouch += OnUntouch;
	}

    void OnTouch()
    {
        if (coroToTimeScale != null)
            StopCoroutine(coroToTimeScale);

        coroToTimeScale = StartCoroutine(CurveToTimescale(m_TouchTimeScale));
    }
    void OnUntouch()
    {
        if (coroToTimeScale != null)
            StopCoroutine(coroToTimeScale);

        coroToTimeScale = StartCoroutine(CurveToTimescale(1));
    }

    private IEnumerator CurveToTimescale(float targetTimeScale)
    {
        float initialTimeScale = Time.timeScale;
        float elapsedTime = 0;
        while (elapsedTime < m_TimeChangeSpeed)
        {
            elapsedTime += Time.unscaledDeltaTime;
            Time.timeScale = Wrj.Utils.MapToCurve.Ease.Lerp(initialTimeScale, targetTimeScale, Mathf.InverseLerp(0, m_TimeChangeSpeed, elapsedTime));
            yield return new WaitForEndOfFrame();
        }
    }
}
