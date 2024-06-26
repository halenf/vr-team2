using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAndLightController : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] Transform sun;
    [SerializeField] Transform target;

    private void Start()
    {
        SetCameraPositionAndRotation(true, false, new Vector2(0.5f, 1.0f));
    }

    void Update()
    {
        bool setCameraSettings = Input.GetMouseButton(0);
        bool setSunSettings = Input.GetMouseButton(1);
        SetCameraPositionAndRotation(setCameraSettings, setSunSettings, GetMousePos01());
    }

    Vector2 GetMousePos01()
    {
        Vector2 resolution = new Vector2(Screen.width, Screen.height);
        Vector2 mousePos01 = (Input.mousePosition / resolution);
        return mousePos01;
    }

    void SetCameraPositionAndRotation(bool setCam, bool setSun, Vector2 input)
    {
        if (setCam)
        {
            Quaternion targetRot = Quaternion.Euler(Mathf.Clamp01(1.0f - input.y) * 90.0f, (0.5f - Mathf.Clamp01(1.0f - input.x)) * 360.0f, 0);
            cam.transform.rotation = targetRot;
            cam.transform.position = target.transform.position + (cam.transform.rotation * -Vector3.forward * 10.0f);
        }

        if (setSun)
        {
            Quaternion sunRot = Quaternion.Euler(15f + Mathf.Clamp01(1.0f - input.y) * 30f, (0.5f - Mathf.Clamp01(1.0f - input.x)) * 180f, 0f);
            sun.transform.rotation = sunRot;
        }
    }
}
