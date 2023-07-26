/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera fpsCamera;
    public CinemachineVirtualCamera tpsCamera;
    public Light redSpotlight;

    private float currentZoomLevel = 1.0f;
    [SerializeField] private float zoomLevel1FOV = 60f;
    [SerializeField] private float zoomLevel2FOV = 40f;
    [SerializeField] private float zoomLevel3FOV = 20f;

    void Start()
    {
        fpsCamera.Priority = 10; //active 
        tpsCamera.Priority = 5;  //inactive 
    }

    void Update()
    {
        HandleZoomInput();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchToFPS();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchToTPS();
        }
        if (Input.GetKeyDown(KeyCode.F) && fpsCamera.Priority == 10 && fpsCamera.Priority == 10)
        {
            redSpotlight.enabled = !redSpotlight.enabled;
        }
    }

    void SwitchToFPS()
    {
        fpsCamera.Priority = 10; //Activate FPS 
        tpsCamera.Priority = 5;  //Deactivate TPS 
    }

    void SwitchToTPS()
    {
        fpsCamera.Priority = 5;  //Deactivate FPS 
        tpsCamera.Priority = 10; //Activate TPS 
    }

    void HandleZoomInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            currentZoomLevel = 1.0f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            currentZoomLevel = 0.5f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            currentZoomLevel = 0.2f;
        }

        // Apply zoom
        if (fpsCamera.Priority == 10&& fpsCamera.Priority == 10)
        {
            UpdateZoomLevel(fpsCamera);
        }
    
    }

    void UpdateZoomLevel(CinemachineVirtualCamera camera)
    {
        if (camera != null)
        {
            camera.m_Lens.FieldOfView = Mathf.Lerp(zoomLevel1FOV,  zoomLevel3FOV, currentZoomLevel);
        }
    }
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera fpsCamera;
    public CinemachineVirtualCamera tpsCamera;
    public Light redSpotlight;
    public Light directionalLight; // Add a reference to the directional light

    private float currentZoomLevel = 1.0f;
    [SerializeField] private float zoomLevel1FOV = 60f;
    [SerializeField] private float zoomLevel2FOV = 40f;
    [SerializeField] private float zoomLevel3FOV = 20f;

    void Start()
    {
        fpsCamera.Priority = 10; //active 
        tpsCamera.Priority = 5;  //inactive 
        directionalLight.enabled = false; // Disable the directional light at the start
    }

    void Update()
    {
        HandleZoomInput();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchToFPS();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchToTPS();
        }

        if (Input.GetKeyDown(KeyCode.F) && fpsCamera.Priority == 10 && tpsCamera.Priority == 5)
        {
            redSpotlight.enabled = !redSpotlight.enabled;
        }

        if (Input.GetKeyDown(KeyCode.P) && tpsCamera.Priority == 10 && fpsCamera.Priority == 5)
        {
            // Toggle the directional light on/off
            directionalLight.enabled = !directionalLight.enabled;

            // Rotate the directional light by 90 degrees on the Y-axis
            if (directionalLight.enabled)
            {
                directionalLight.transform.Rotate(new Vector3(0f, 90f, 0f));
            }
        }
    }

    void SwitchToFPS()
    {
        fpsCamera.Priority = 10; //Activate FPS 
        tpsCamera.Priority = 5;  //Deactivate TPS 
    }

    void SwitchToTPS()
    {
        fpsCamera.Priority = 5;  //Deactivate FPS 
        tpsCamera.Priority = 10; //Activate TPS 
    }

    void HandleZoomInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            currentZoomLevel = 1.0f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            currentZoomLevel = 0.5f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            currentZoomLevel = 0.2f;
        }

        // Apply zoom
        if (fpsCamera.Priority == 10 && tpsCamera.Priority == 5)
        {
            UpdateZoomLevel(fpsCamera);
        }
    }

    void UpdateZoomLevel(CinemachineVirtualCamera camera)
    {
        if (camera != null)
        {
            camera.m_Lens.FieldOfView = Mathf.Lerp(zoomLevel1FOV, zoomLevel3FOV, currentZoomLevel);
        }
    }
}
