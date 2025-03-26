using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    
    //References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    //Variables
    [SerializeField, Range(0, 24)] private float TimeOfDay;

    private void Update()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime;
            TimeOfDay %= 24; //Clamp between 0-24
            UpdateLighting(TimeOfDay / 8f);
        }
        else
        {
            UpdateLighting(TimeOfDay / 8f);
        }
    }
    
    
    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3(45f, (1f - timePercent) * 360f, 0f));
        }
    }


    //Try to find a diretinal light to use if we haven't set one
    private void OnValidate()
        {
        if (DirectionalLight != null)
            return;

        //Search for lighting tab sun
        if(RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach(Light light in lights)
            {
                if(light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;

                }
            }
        }

    }


}
