using UnityEngine;
using System.Collections;
using ObjectTypes;

public class DummyCharacter : MonoBehaviour, ICharacterStats, ICharacterProperties 
{
	[SerializeField]
	private float maxHealth;
	[SerializeField]
	private float currentHealth;
	[SerializeField]
	private float maxStamina;
	[SerializeField]
	private float currentStamina;
	[SerializeField]
	private float attackSpeed;
	[SerializeField]
	private float attackDamage;
	[SerializeField]
	private float attackRange;
	[SerializeField]
	private float physDef;
	[SerializeField]
	private float moveSpeed;
	[SerializeField]
	private bool isSelected;
	[SerializeField]
	private bool isMoving;
	[SerializeField]
	CharacterClassType cType = CharacterClassType.MELEE;

	public float Max_Character_Heralth
	{
		get { return maxHealth; }
		set { maxHealth = value; }
	}

	public float Current_Chacter_Health
	{
		get { return currentHealth; }
		set { currentHealth = value; }
	}

	public float Max_Character_Stamina
	{
		get { return maxStamina; }
		set { maxStamina = value; }
	}

	public float Current_Character_Stamina
	{	
		get { return currentStamina; }
		set { currentStamina = value; }
	}

	public float Attack_Speed
	{
		get { return attackSpeed; }
		set { attackSpeed = value; }
	}

	public float Move_Speed
	{
		get { return moveSpeed; }
		set { moveSpeed = value; }
	}

	public float Attack_Damage
	{
		get { return attackDamage; }
		set { attackDamage = value; }
	}

	public float Attack_Range
	{
		get { return attackRange; }
		set { attackRange = value; }
	}

	public float Physical_Defense
	{
		get { return physDef; }
		set { physDef = value; }
	}

	public bool Is_Selected
	{
		get { return isSelected; }
		set { isSelected = value; }
	}

	public bool Is_Moving
	{
		get { return isMoving; }
		set { isMoving = value; }
	}

	public CharacterClassType Character_Class_Type
	{
		get { return cType; }
	}
		
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SelectCharacter()
	{
		
	}

	public void DestroyCharacter()
	{
		
	}
}
