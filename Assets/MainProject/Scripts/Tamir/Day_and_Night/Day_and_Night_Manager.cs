using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteAlways]
public class Day_and_Night_Manager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    [SerializeField, Range(0, 600)] private float TimeOfDay;
    public Material skybox1_Night;
    public Material skybox2_Dawn;
    public Material skybox3_Day;

    private void Update()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            //(Replace with a reference to the game time)
            TimeOfDay += Time.deltaTime;
            TimeOfDay %= 600; //Modulus to ensure always between 0-24
            UpdateLighting(TimeOfDay / 600f);
        }
        else
        {
            UpdateLighting(TimeOfDay / 600f);
        }
        SetSkybox();
    }


    private void UpdateLighting(float timePercent)
    {
        //Set ambient and fog
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        //If the directional light is set then rotate and set it's color, I actually rarely use the rotation because it casts tall shadows unless you clamp the value
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
    //Try to find a directional light to use if we haven't set one
    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        //Search for lighting tab sun
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        //Search scene for light that fits criteria (directional)
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
