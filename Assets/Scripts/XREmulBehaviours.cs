using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XREmulBehaviours : MonoBehaviour {
  public Camera activeCamera;

	// Use this for initialization
	void Start () {
		
	}

  enum CameraDirectivityState
  {
    Idle,
    Activated
  }

  private CameraDirectivityState cameraDirectivityState = CameraDirectivityState.Idle;
  private Vector3 lastMousePosition;
	
	// Update is called once per frame
	void Update () {
    Debug.Log(cameraDirectivityState);

    switch (cameraDirectivityState)
    {
      case CameraDirectivityState.Idle:
        if (Input.GetMouseButton(0)) // Primary mouse button is down
        {
          lastMousePosition = Input.mousePosition;

          cameraDirectivityState = CameraDirectivityState.Activated;
        }
        break;
      case CameraDirectivityState.Activated:
        if (!Input.GetMouseButton(0)) // Primary mouse button is not down
        {
          cameraDirectivityState = CameraDirectivityState.Idle;
        }
        else
        {
          Debug.Assert(lastMousePosition != null);

          var deltaMousePosition = Input.mousePosition - lastMousePosition;
          var normalizedDeltaMousePosition = new Vector3(deltaMousePosition.x / Screen.width,
                                                         deltaMousePosition.y / Screen.height);
          float weight = 100.0f; // FIXME: it's not exact name of scala.

          activeCamera.transform.Rotate(new Vector3(-weight * normalizedDeltaMousePosition.y,
                                                     weight * normalizedDeltaMousePosition.x));
          lastMousePosition = Input.mousePosition;
        }
        break;
    }
	}
}
