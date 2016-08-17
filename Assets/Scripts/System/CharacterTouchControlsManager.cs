using UnityEngine;
using System.Collections;

public class CharacterTouchControlsManager : MonoBehaviour
{
	#region Data Members

	private bool startRotating = false;
	private bool startMoving = false;
	private int numberOfTouchInputs;
	private Ray screenRay;
	private Touch touchInput;
	private TouchPhase touchPhase;
	private RaycastHit hit;
	private GameObject playerObj;
	private Vector3 initFingerRotationPosition;
	private Transform cachedTransform;
	private Vector3 startingPosition;
	private Vector2 deltaPosition;

	#endregion

	#region Setters & Getters

	#endregion

	#region Built-in Unity Methods

	void Start()
	{
		playerObj = null;
	}

	void Update()
	{
		numberOfTouchInputs = Input.touchCount;
	
		for(int i = 0; i < numberOfTouchInputs; i++)
		{
			touchInput = Input.GetTouch(i);
			touchPhase = touchInput.phase;

			switch(touchPhase)
			{
			case TouchPhase.Began:

				//Create a ray cast from Camera through the screen
				screenRay = Camera.main.ScreenPointToRay(touchInput.position);
			
				if(i == 0)
				{
					//Set focus character
					SetSystemToFocusCharacter(screenRay);

					//This call initializes rotation
					RotatePlayerObject(playerObj, touchInput.position, touchPhase);
				}
					
				break;

			case TouchPhase.Moved:

				if(i == 0)
				{
					//Character movement
					DragPlayerObject(deltaPosition, playerObj, touchPhase);

					//This is where actual rotation is done
					RotatePlayerObject(playerObj, touchInput.position, touchPhase);
				}

				break;

			case TouchPhase.Stationary:

			
				break;

			case TouchPhase.Ended:

				//Re-initialize booleans
				startRotating = false;
				startMoving = false;

				break;

			case TouchPhase.Canceled:

				//Re-initialize booleans
				startRotating = false;
				startMoving = false;

				break;
			
			}
		}
	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	private void SetSystemToFocusCharacter(Ray ray)
	{
		if(Physics.Raycast(ray, out hit))
		{
			if(hit.collider.tag == "Player")
			{
				playerObj = hit.collider.gameObject;
				playerObj.GetComponent<ICharacterProperties>().SelectCharacter();
				startMoving = true;
			}
		}
	}
		
	private void DragPlayerObject(Vector3 deltaPosition, GameObject playerRef, TouchPhase phase)
	{
		if(playerRef != null)
		{
			if(phase == TouchPhase.Began)
			{
				if(Physics.Raycast(screenRay, out hit))
				{
					if(hit.collider.tag == "Player")
					{
						deltaPosition = Input.GetTouch(0).deltaPosition;
						cachedTransform = playerRef.transform;
						startingPosition = cachedTransform.position;
					}
				}	
			}
			else if(phase == TouchPhase.Moved)
			{
				float dragSpeed = playerRef.GetComponent<ICharacterStats>().Move_Speed;

				cachedTransform.position = new Vector3(deltaPosition.x * dragSpeed + cachedTransform.position.x, 
													   cachedTransform.position.y,
													   deltaPosition.y * dragSpeed + cachedTransform.position.z);        
			}
		}
	}

	private void RotatePlayerObject(GameObject playerRef, Vector3 touchPos, TouchPhase phase)
	{
		if(playerRef != null)
		{
			if(phase == TouchPhase.Began)
			{
				if(Physics.Raycast(screenRay, out hit))
				{
					if(hit.collider.gameObject == null)
					{
						startRotating = true;
						initFingerRotationPosition = touchPos;
					}
				}
			}

			else if(phase == TouchPhase.Moved && startRotating)
			{
				if(touchPos.y > initFingerRotationPosition.y)
				{
					playerRef.transform.Rotate(Vector3.up, -10.0f);
				}
				else if(touchPos.y < initFingerRotationPosition.y)
				{
					playerRef.transform.Rotate(Vector3.up, 10.0f);
				}

				initFingerRotationPosition = touchPos;
			}
		}
	}

	private void PlayerDash(GameObject playerRef, Touch input, Ray ray)
	{
		if(input.tapCount == 2)
		{
			if(Physics.Raycast(ray, out hit))
			{
				if(hit.collider.tag == "Player")
				{
					playerObj.GetComponent<ICharacterProperties>().SelectCharacter();
				}
			}
		}
	}
		
	private IEnumerator AndroidTouchTapHandler()
	{
		
	}

	#endregion
}
