using TMPro;
using UnityEngine;


public class Day_and_Night_Manager : MonoBehaviour
{
    [Header("Light")]

    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    [SerializeField, Range(0, 600)] private float TimeOfDay;


    [Header("Skyboxes")]

    [SerializeField] private Material skybox1_Night;
    [SerializeField] private Material skybox2_NightToDawn;
    [SerializeField] private Material skybox3_Dawn;
    [SerializeField] private Material skybox4_DawnToDay;
    [SerializeField] private Material skybox5_Day;
    [SerializeField] private Material skybox6_MidDay;

    [Header("Audio")]

    [SerializeField] private AudioSource alarmAudio;
    [SerializeField] private AudioSource alarmSpeech;
    private int audioHBP = 0; //Alarm Audio Has Been Played
    private int speechHBP = 0; //Alarm Speech Has Been Played


    [Header("UI")]

    [SerializeField] private GameObject timerTextParent;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameObject bell;
    public float currentTimeOfDay = 360;
    public GameObject textForAlarmSpeech;

    private void Start()
    {
        timerTextParent.SetActive(false);
    }
    private void Update()
    {
        if (Preset == null)
            return;

        currentTimeOfDay = 360 - TimeOfDay;
 

        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime;
            TimeOfDay %= 600;
            UpdateLighting(TimeOfDay / 600f);
        }
        else
        {
            UpdateLighting(TimeOfDay / 600f);
        }
        if (TimeOfDay >= 300)
        {
            timerTextParent.SetActive(true);
            bell.SetActive(true);
            timerText.text = currentTimeOfDay.ToString("f0");

        }
        PlayAlarm();
        SetSkybox();

    }

    private void PlayAlarm()
    {
        if (TimeOfDay >= 300 && !alarmAudio.isPlaying && audioHBP < 1)
        {
            alarmAudio.Play();
            audioHBP++;
        }
        if (TimeOfDay >= 302 && !alarmSpeech.isPlaying && speechHBP < 1)
        {
            alarmSpeech.Play();
            speechHBP++;
            textForAlarmSpeech.SetActive(true);

        }
        if (TimeOfDay >= 310)
        {
            alarmAudio.Stop();
            textForAlarmSpeech.SetActive(false);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }

    }
    private void SetSkybox()
    {
        if (TimeOfDay < 100)
        {
            RenderSettings.skybox = skybox1_Night;
        }
        if (TimeOfDay > 100 && TimeOfDay < 180)
        {
            RenderSettings.skybox = skybox2_NightToDawn;
        }
        if (TimeOfDay > 180 && TimeOfDay < 260)
        {
            RenderSettings.skybox = skybox3_Dawn;
        }
        if (TimeOfDay > 260 && TimeOfDay < 300)
        {
            RenderSettings.skybox = skybox4_DawnToDay;
        }
        if (TimeOfDay > 300 && TimeOfDay < 340)
        {
            RenderSettings.skybox = skybox5_Day;
        }
        if (TimeOfDay > 340)
        {
            RenderSettings.skybox = skybox6_MidDay;
        }
    }
    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}
