using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SquadManager : MonoBehaviour
{
	#region Data Members

	private static SquadManager _instance;
	private List<GameObject> squadList;
	private GameObject focusedCharacter;

	#endregion

	#region Setters & Getters

	public static SquadManager Instance
	{
		get 
		{ 
			if(!_instance)
			{
				try
				{
					_instance = FindObjectOfType(typeof(SquadManager)) as SquadManager;

				}
				catch 
				{
					Debug.LogError("No CombatSystem GameObject detected in scene!");
				}
			}

			return _instance;
		}
	}

	public GameObject Focused_Character
	{
		get { return focusedCharacter; }
		set { focusedCharacter = value; }
	}

	public List<GameObject> Squad_List
	{
		get { return squadList; }
	}

	#endregion

	#region Built-in Unity Methods

	void Awake()
	{
		squadList = new List<GameObject>();
	}

	void Start()
	{
		Character[] refs = FindObjectsOfType(typeof(Character)) as Character[];

		foreach(Character _char in refs)
		{
			//Disable the input component of each character
			_char.gameObject.GetComponent<CharacterCommand>().enabled = false;
			squadList.Add(_char.gameObject);
		}
	}

	void Update()
	{
		if(Input.touchCount > 0)
		{
			TouchPhase phase = Input.GetTouch(0).phase;

			if(phase == TouchPhase.Began)
			{
				//Do a raycast
				Ray screenRay = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
				RaycastHit hit;

				if(Physics.Raycast(screenRay, out hit))
				{	
					if(hit.collider.tag == "Player")
					{
						if(!hit.collider.gameObject.GetComponent<ICharacterProperties>().Is_Selected)
						{
							hit.collider.gameObject.GetComponent<ICharacterProperties>().Is_Selected = true;
						
							if(focusedCharacter != null)
							{
								//Disable the previous focused character's CharacterComand component.
								focusedCharacter.GetComponent<ICharacterProperties>().Is_Selected = false;
								focusedCharacter.GetComponent<CharacterCommand>().enabled = false;
								focusedCharacter = null;
							}

							focusedCharacter = hit.collider.gameObject;
							focusedCharacter.GetComponent<CharacterCommand>().enabled = true;
						}
					}
				}
			}
		}
	}

	#endregion

	#region Public Methods

	#endregion

	#region Private Methods

	#endregion
}
