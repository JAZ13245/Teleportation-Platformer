using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class ManualZoom : MonoBehaviour
{
    [SerializeField]
    private float zoomSpeed = 1f;
    [SerializeField]
    private float minOrthoSize = 9f;
    [SerializeField]
    private float maxOrthoSize = 13f;

    public InputActions inputActions = null;

    private CinemachineVirtualCamera cam;
    private CinemachineComponentBase componentBase;
    private void Awake()
    {
        inputActions = new InputActions();
        cam = GetComponent<CinemachineVirtualCamera>();
        componentBase = cam.GetCinemachineComponent<CinemachineComponentBase>();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Gameplay.Zoom.performed += OnZoomPerformed;


    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.Gameplay.Zoom.performed -= OnZoomPerformed;


    }

    private void Update()
    {
        if(inputActions.Gameplay.Zoom.IsPressed())
        {
            float val = -(Mathf.Sign(inputActions.Gameplay.Zoom.ReadValue<float>()));
            cam.m_Lens.OrthographicSize = Mathf.Clamp(cam.m_Lens.OrthographicSize + val * zoomSpeed * Time.deltaTime, minOrthoSize, maxOrthoSize);
        }

    }
    private void OnZoomPerformed(InputAction.CallbackContext context)
    {
        

    }
}
