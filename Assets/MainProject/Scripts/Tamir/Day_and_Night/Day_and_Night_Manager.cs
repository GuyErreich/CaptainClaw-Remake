using TMPro;
using UnityEngine;

[ExecuteAlways]
public class Day_and_Night_Manager : MonoBehaviour
{
    [Header("Light")]

    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    [SerializeField, Range(0, 600)] private float TimeOfDay;


    [Header("Skyboxes")] 

    [SerializeField] private Material skybox1_Night;
    [SerializeField] private Material skybox2_Dawn;
    [SerializeField] private Material skybox3_Day;


    [Header("Audio")]

    [SerializeField] private AudioSource alarmAudio;
    [SerializeField] private AudioSource alarmSpeech;
    private int audioHBP = 0; //Alarm Audio Has Been Played
    private int speechHBP = 0; //Alarm Speech Has Been Played


    [Header("UI")]

    [SerializeField] private TMP_Text timerText;
    private float currentTimeOfDay = 60;
    private void Start()
    {
        timerText.enabled = false;
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
            if (timerText.enabled == false)
            {
                timerText.enabled = true;
            }
            timerText.text = currentTimeOfDay.ToString("f0");

        }
        PlayAlarm();
        SetSkybox();

    }

    private void PlayAlarm()
    {
        if (TimeOfDay >= 300 && !alarmAudio.isPlaying && audioHBP<1)
        {
            alarmAudio.Play();
            audioHBP++;
        }
        if (TimeOfDay >= 302 && !alarmSpeech.isPlaying &&speechHBP<1)
        {
            alarmSpeech.Play();
            speechHBP++;
        }
        if (TimeOfDay >= 310)
        {
            alarmAudio.Stop();
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
        if (TimeOfDay < 300)
        {
            RenderSettings.skybox = skybox1_Night;
        }
        if (TimeOfDay > 300 && TimeOfDay < 340)
        {
            RenderSettings.skybox = skybox2_Dawn;
        }
        if (TimeOfDay > 340)
        {
            RenderSettings.skybox = skybox3_Day;
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
