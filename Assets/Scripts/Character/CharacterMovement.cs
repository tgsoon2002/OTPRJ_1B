using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour 
{
	#region Data Members
	private Rigidbody2D phys;
	Transform cachedTransform;
	Vector3 startingPosition;
	Collider2D col;
	bool isMoving = false;
	float _horizontalLimit = Screen.height, _verticalLimit = Screen.width;

	private bool isRotating = false;
	private Vector2 startingTouchPosition_Screen;
	private Vector2 startingTouchPosition_World;

	[SerializeField]
	float dragSpeed = 0.001f;

	#endregion

	#region Setters & Getters

	#endregion

	#region Built-In Unity Methods

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	#endregion

	#region Helper Classes/Structs

	#endregion
	// Use this for initialization
	void Start () 
	{
		//phys = GetComponent<Rigidbody2D>();
		col = GetComponent<BoxCollider2D>();
		cachedTransform = transform;
		startingPosition = cachedTransform.position;
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.touchCount > 0)
		{            
			Vector2 deltaPosition = Input.GetTouch(0).deltaPosition;

			switch (Input.GetTouch(0).phase)
			{
			case TouchPhase.Began:

				if (Input.touchCount == 1)
				{
					startingTouchPosition_Screen = Input.GetTouch(0).position;
					startingTouchPosition_World = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

					if (col == Physics2D.OverlapPoint(startingTouchPosition_World))
					{
						isMoving = true;
					}
				}

				if(Input.touchCount == 2 && isMoving)
				{
					isRotating = true;
				}

				break;

			case TouchPhase.Moved:

				if (isMoving && Input.touchCount == 1)
				{
					DragObject(deltaPosition);
				}

				if(isMoving && isRotating && Input.touchCount == 2)
				{
					Vector2 newTouchPosition = Input.GetTouch(1).position;
					RotateObject(startingTouchPosition_Screen, newTouchPosition);
					startingTouchPosition_Screen = newTouchPosition;
				}

				break;

			case TouchPhase.Ended:

				isMoving = false;

				break;
			}
		}
	}

	private void DragObject(Vector2 deltaPosition)
	{
		cachedTransform.position = new Vector3(Mathf.Clamp((deltaPosition.x * dragSpeed) + cachedTransform.position.x, 
			startingPosition.x - _horizontalLimit, startingPosition.x + _horizontalLimit),
			Mathf.Clamp((deltaPosition.y * dragSpeed) + cachedTransform.position.y,
				startingPosition.y - _verticalLimit, startingPosition.y + _verticalLimit),
			cachedTransform.position.z);        
	}

	private void RotateObject(Vector2 start, Vector2 end)
	{
		if(end.y > start.y)
		{
			gameObject.transform.Rotate(Vector3.forward, -10.0f);
		}
		else if(end.y < start.y)
		{
			gameObject.transform.Rotate(Vector3.forward, 10.0f);
		}
	}
}
