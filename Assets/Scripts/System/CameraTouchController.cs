using UnityEngine;
using System.Collections;

public class CameraTouchController : MonoBehaviour 
{
	#region Data Members

	private Ray screenRay;
	private RaycastHit hit;
	private Touch touchInput;
	private TouchPhase touchPhase;
	private int touchCount;
	private int layerMask;
	private bool panCamera = false;
	private Vector2 deltaTouchPosition;

	[SerializeField]
	private float cameraSpeed = 2.0f;

	[SerializeField]
	private float perspectiveZoomSpeed = 0.5f;

	[SerializeField]
	private float orthoZoomSpeed = 0.5f;

	[SerializeField]
	private float orthoZoomLimit = 0.1f;

	[SerializeField]
	private float perspectiveZoomLimitMin = 0.1f;

	[SerializeField]
	private float perspectiveZoomLimitMax = 150.0f;

	#endregion

	#region Setters & Getters

	#endregion

	#region Built-in Unity Methods

	// Use this for initialization
	void Start () 
	{
		layerMask = 1 << 31;
		layerMask = ~layerMask;
	}

	// Update is called once per frame
	void Update () 
	{
		if(SquadManager.Instance.Focused_Character == null)
		{
			switch(Input.touchCount)
			{
			case 1:

				CameraPanning();

				break;

			case 2:

				CameraZoom();

				break;
			}

		}
	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	/// <summary>
	/// Method for handling camera panning.
	/// </summary>
	private void CameraPanning()
	{
		touchCount = Input.touchCount;
	
		if(touchCount > 0)
		{
			TouchPhase phase = Input.GetTouch(0).phase;
			deltaTouchPosition = Input.GetTouch(0).deltaPosition;

			switch(phase)
			{
			case TouchPhase.Began:

				if(touchCount == 1)
				{
					
				}

				//Do a raycast
				Ray screenRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
				RaycastHit hit;

				//Mask Layer 31, which is the layer to be used for targeting.
				if(Physics.Raycast(screenRay, out hit, layerMask))
				{	
					if(hit.collider == null || hit.collider.tag != "Player" || hit.collider.tag != "Enemy")
					{
						Debug.Log("Panning Camera");
						panCamera = true;
					}
				}
				else
				{
					Debug.Log("Panning Camera");
					panCamera = true;
				}

				break;

			case TouchPhase.Moved:
				
				if(panCamera)
				{
					transform.Translate(new Vector3(-deltaTouchPosition.x * Time.deltaTime, 0.0f, -deltaTouchPosition.y * Time.deltaTime), Space.World);
				}

				break;

			case TouchPhase.Ended:
			case TouchPhase.Canceled:

				panCamera = false;

				break;
			}
		}
	}

	/// <summary>
	/// Method for handling camera zooming.
	/// </summary>
	private void CameraZoom()
	{
		//Declaring local variables
		Touch firstTouch;
		Touch secondTouch;
		Vector2 touchZeroPreviousPosition;
		Vector2 touchOnePreviousPosition;
		float previousTouchDeltaMagnitude;
		float touchDeltaMagnitude;
		float deltaMagnitudeDifference;
		Camera cam;

		cam = gameObject.GetComponent<Camera>();
		firstTouch = Input.GetTouch(0);
		secondTouch = Input.GetTouch(1);

		touchZeroPreviousPosition = firstTouch.position - firstTouch.deltaPosition;
		touchOnePreviousPosition = secondTouch.position - secondTouch.deltaPosition;

		previousTouchDeltaMagnitude = (touchZeroPreviousPosition - touchOnePreviousPosition).magnitude;
		touchDeltaMagnitude = (firstTouch.position - secondTouch.position).magnitude;

		deltaMagnitudeDifference = previousTouchDeltaMagnitude - touchDeltaMagnitude;

		if(cam.orthographic)
		{
			cam.orthographicSize += deltaMagnitudeDifference * orthoZoomSpeed;
			cam.orthographicSize = Mathf.Max(cam.orthographicSize, orthoZoomLimit);
		}
		else
		{
			cam.fieldOfView += deltaMagnitudeDifference * perspectiveZoomSpeed;
			cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, perspectiveZoomLimitMin, perspectiveZoomLimitMax);
		}
	}

	#endregion
}
