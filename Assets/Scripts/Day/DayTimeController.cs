using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using Sirenix.OdinInspector;
using static UnityEngine.Rendering.PostProcessing.PostProcessResources;

public class DayTimeController : MonoBehaviour
{
    const float secondsInDay = 24 * 3600f;

    float time = 6 * 3600f;
    [SerializeField] float timeScale = 60f; //1초가 1분처럼 
    [SerializeField] TextMeshProUGUI dayText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI dayNumberText;
    Vignette vignette;
    ShadowsMidtonesHighlights shadows;
    Color nightColor = new Color(0.01176471f, 0.01176471f, 0.01176471f);

    private WaitForSeconds seconds = new WaitForSeconds(0.02f);

    #region PublicVariables
    #endregion

    #region PrivateVariables
    private int days = 1;
    private Volume volume;

    int Hours
    {
        get
        {
            return (int)(time / 3600f);
        }
    }
    int Minutes
    {
        get
        {
            return (int)(time % 3600f / 60f);
        }
    }

    private bool isDay = true;
    #endregion

    #region PublicMethod
    #endregion
    #region PrivateMethod
    private void Start()
    {
        volume = gameObject.GetComponent<Volume>();
        volume.profile.TryGet<Vignette>(out vignette);
        volume.profile.TryGet<ShadowsMidtonesHighlights>(out shadows);

        dayNumberText.text = days.ToString() + "일차";
    }

    private void Update()
    {
        time += Time.deltaTime * timeScale;

        int hours = Hours;
        int minutes = Minutes;

        if (Minutes % 10 == 0)
        {
            string timeString = string.Format("{0:D2}:{1:D2}", hours, minutes);
            timeText.text = timeString;
        }

        //낮과 밤 기준 세우기
        if (time > secondsInDay * 19 / 24 && isDay)
        {
            DayToNight();
        }
        else if (time > secondsInDay * 6 / 24 && time < secondsInDay * 19 / 24 && !isDay)
        {
            NightToDay();
        }

        if (time > secondsInDay)
        {
            NextDay();
        }
    }

    [Button]
    private void DayToNight()//밤이 되었을 때
    {
        isDay = false;
        vignette.color.Override(nightColor);
        vignette.intensity.Override(0.7f);
        StartCoroutine(nameof(NightChanger));
        //색상 값 어떻게 줄건지 쓰기
        shadows.shadows.overrideState = true;
        shadows.shadows.Override(new Vector4 (0.48f, 0.7f, 1.0f, 0f));

        dayText.text = "밤";
    }

    [Button]
    private void NightToDay() //낮이 되었을 때
    {
        isDay = true;
        vignette.intensity.Override(0f);
        StartCoroutine(nameof(DayChanger));
        shadows.shadows.overrideState = false;

        dayText.text = "낮";
    }

    private void NextDay() //다음날
    {
        time = 0;
        days += 1;

        dayNumberText.text = days.ToString() + "일차";

    }

    private IEnumerator NightChanger() //밤으로 바꾸는 코루틴
    {
        for(int i = 0; i < 36; ++i)
        {
            vignette.intensity.Override(0.02f * i);
            yield return seconds;
        }
    }
    private IEnumerator DayChanger() //낮으로 바꾸는 코루틴
    {
        for(int i = 0; i < 36; ++i)
        {
            vignette.intensity.Override(0.7f - (0.02f * i));
            yield return seconds;
        }
    }


}
#endregion

