using UnityEngine;
using System.Collections;
using ObjectTypes;

public class CharacterCommand : MonoBehaviour
{
	#region Data Members

	//Public:

	//Private:
	private Ray touchRay;
	private RaycastHit hit;
	private Collider playerCol;
	private Transform cachedTransform;
	private Touch touchInput;
	private TouchPhase touchPhase;
	private int touchCount;
	private bool isTapped = false;
	private Vector2 deltaTouchPosition;
	private Vector3 initialPositionForRotation;
	private int doubleTap = 0;
	private bool doubleTapBegin = false;
	private int layerMask = 1 << 31;

//HACK - This variable is mainly for debugging purposes:
	[SerializeField]
	float movementCost = 1.0f;

	#endregion

	#region Setters & Getters

	#endregion

	#region Built-in Unity Methods

	void Awake()
	{
		this.enabled = false;
		layerMask = ~layerMask;
	}

	// Use this for initialization
	void Start ()
	{
		playerCol = gameObject.GetComponent<Collider>();
		cachedTransform = gameObject.transform;
	}

	// Update is called once per frame
	void Update ()
	{
		touchCount = Input.touchCount;
		
		if(touchCount > 0)
		{
			deltaTouchPosition = Input.GetTouch(0).deltaPosition;

			for(int i = 0; i < touchCount; i++)
			{
				touchInput = Input.GetTouch(i);
				touchPhase = touchInput.phase;

				switch(touchPhase)
				{
				case TouchPhase.Began:

					EvaluateTouchTap(i);

					break;

				case TouchPhase.Moved:

					EvaluateTouchMoved(i);

					break;

				case TouchPhase.Stationary:

					break;

				case TouchPhase.Ended:
				case TouchPhase.Canceled:

					isTapped = false;

					break;
				}
			}
		}
	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	/// <summary>
	/// Evaluates the touch tap.
	/// </summary>
	/// <param name="i">The index.</param>
	private void EvaluateTouchTap(int i)
	{
		//Declaring local variables.
		bool isHit = false;

		touchRay = Camera.main.ScreenPointToRay(touchInput.position);

		//This should only be the raycast in this object. Mask 
		//layer 31 from being hit by the raycast. 
		isHit = Physics.Raycast(touchRay, out hit, layerMask);	
	
		switch(i)
		{
		case 0:

			SetCharacterFocus(isHit);
			DeselectCharacterFocus(isHit);
							
			break;

		case 1:
			
			initialPositionForRotation = touchInput.position;

			break;
		}
		
	}

	/// <summary>
	/// Evaluates the touch moved.
	/// </summary>
	/// <param name="i">The index.</param>
	private void EvaluateTouchMoved(int i)
	{
		switch(i)
		{
		case 0:

			DragCharacter();

			break;

		case 1:

			RotateCharacter();
			
			break;
		}
	}

	/// <summary>
	/// Sets the character focus and other initialization stuff
	/// for when TouchPhase.BEGIN is true.
	/// </summary>
	/// <param name="_hit">Hit.</param>
	private void SetCharacterFocus(bool _hit)
	{
		if(_hit && hit.collider.tag == "Player")
		{
			isTapped = true;

			cachedTransform = gameObject.transform;
		}
	}

	/// <summary>
	/// Deselects the character focus.
	/// </summary>
	/// <param name="_hit">If set to <c>true</c> hit.</param>
	private void DeselectCharacterFocus(bool _hit)
	{
		if(!_hit)
		{
			doubleTap++;

			if(!doubleTapBegin)
			{
				StartCoroutine(DoubleTapTracker());
			}

			if(doubleTap == 2 && doubleTapBegin)
			{
				gameObject.GetComponent<ICharacterProperties>().Is_Selected = false;
				SquadManager.Instance.Focused_Character = null;
				this.enabled = false;
			}
		}
	}

	/// <summary>
	/// Drags the character.
	/// </summary>
	private void DragCharacter()
	{
		//Declaring local variables
		float dragSpeed = gameObject.GetComponent<ICharacterStats>().Move_Speed;

		if(isTapped && gameObject.GetComponent<ICharacterStats>().Current_Character_Stamina > 0)
		{
			//Update position, relative to x-z plane, based on touch
			cachedTransform.position = new Vector3(deltaTouchPosition.x * dragSpeed + cachedTransform.position.x, 
											 	   cachedTransform.position.y,
												   deltaTouchPosition.y * dragSpeed + cachedTransform.position.z);

			//Update character stamina
			gameObject.GetComponent<ICharacterStats>().Current_Character_Stamina -= movementCost;
		}
	}

	/// <summary>
	/// Rotates the character.
	/// </summary>
	private void RotateCharacter()
	{
		//Declaring local variables
		Vector3 newTouchPosition = touchInput.position;

		//Rotate only when the finger tap is not on the 
		//the character object.
		if(!isTapped && touchCount == 2)
		{
			if(newTouchPosition.y > initialPositionForRotation.y)
			{
				gameObject.transform.Rotate(Vector3.up, 10.0f);
			}
			else if(newTouchPosition.y < initialPositionForRotation.y)
			{
				gameObject.transform.Rotate(Vector3.up, -10.0f);
			}

			initialPositionForRotation = newTouchPosition;
		}
	}

	/// <summary>
	/// Meelee attack functionality implemented here.
	/// </summary>
	/// <param name="_hit">If set to <c>true</c> hit.</param>
	/// DEPRECATED
	private void MeleeAttack(bool _hit)
	{
		//Declaring local variables
		float distance;
		ICharacterStats charRef;

		if(_hit && hit.collider.tag == "Enemy")
		{
			distance = Vector3.Distance(gameObject.transform.position, hit.collider.gameObject.transform.position);
			charRef = gameObject.GetComponent<ICharacterStats>();

			if(distance <= charRef.Attack_Range && charRef.Current_Character_Stamina > 0)
			{
				//HACK -- Mark for: Subject To Change
				//We might add 'Critical' Physical Damage here later.
				CombatSystem.Instance.DealPhysicalDamage(hit.collider.gameObject, charRef.Attack_Damage);

				//HACK -- Mark for: Subject To Change
				//This value will be changed later on.
				charRef.Current_Character_Stamina -= 20.0f;
			}
		}
	}

	private IEnumerator DoubleTapTracker()
	{
		doubleTapBegin = true;
		yield return new WaitForSeconds(1.0f);
		doubleTapBegin = false;
		doubleTap = 0;
	}
		
	#endregion
}
