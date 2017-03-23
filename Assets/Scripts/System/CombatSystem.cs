using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using ObjectTypes;

public class CombatSystem : MonoBehaviour, ISystemElement
{
    #region Data Members

    //Public

    //Private 
    [SerializeField]
    private FloatingDamageText dmgText;
    private static CombatSystem _instance;
    private GameObject canvasRef;
    private Ray screenRay;
    private RaycastHit hit;
    private Stack<DamageInsideStack> damageStack;
    private bool toPlay = true;
    private SystemDataType typeName = SystemDataType.COMBATSYSYEM;

    #endregion

    #region Helper Classes

    private class DamageInsideStack
    {
        public float damage;
        public GameObject victim;

        /// <summary>
        /// Initializes a new instance of the <see cref="CombatSystem+DamageInsideStack"/> class.
        /// </summary>
        /// <param name="val">Value.</param>
        /// <param name="theVictim">The victim.</param>
        public DamageInsideStack(float val, GameObject theVictim)
        {
            damage = val;
            victim = theVictim;
        }
    }



    #endregion

    #region Setters & Getters

    public static CombatSystem Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(CombatSystem)) as CombatSystem;

                if (!_instance)
                {
                    Debug.LogError("No CombatSystem GameObject detected in scene!");
                }
                else
                {
                    Debug.Log("Init() called");
                    _instance.Init();
                }
            }

            return _instance;
        }
    }

    public SystemDataType System_Type
    {
        get { return typeName; }
    }

    #endregion

    #region Built-in Unity Methods

    /// <summary>
    /// Start this instance.
    /// </summary>
    void Start()
    {
        Init();
        damageStack = new Stack<DamageInsideStack>();
    }

    /// <summary>
    /// Update this instance.
    /// </summary>
    void Update()
    {
        if (damageStack.Count > 0)
        {
            if (toPlay)
            {
                PlayDamageParticleEffect(damageStack.Pop());
                StartCoroutine(ExecuteDamageAnimationsAndCalculations());
            }
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Deals the physical damage.
    /// </summary>
    /// <param name="victim">Victim.</param>
    /// <param name="value">Value.</param>
    public void DealPhysicalDamage(GameObject victim, float value)
    {
        //Declaring local variables
        ICharacterStats victimStats = victim.GetComponent<ICharacterStats>();
        ICharacterProperties victimProperties = victim.GetComponent<ICharacterProperties>();
        DamageInsideStack damage;

        //HACK -- Mark for: Subject To Change
        //We might change how damage values are calculated
        //Instantiate the object.
        damage = new DamageInsideStack((value - victimStats.Physical_Defense), victim);

        damageStack.Push(damage);
    }

    /// <summary>
    /// Deals the magic damage.
    /// </summary>
    /// <param name="victim">Victim.</param>
    /// <param name="value">Value.</param>
    public void DealMagicDamage(GameObject victim, float value)
    {
        //TBD
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Initialize this instance.
    /// </summary>
    private void Init()
    {
        canvasRef = GameObject.Find("Canvas");
        dmgText = Resources.Load<FloatingDamageText>("Prefabs/TestPrefabs/PopupTextParent");
    }

    /// <summary>
    /// Call this function to display the damage
    /// values as a Particle Effect, Slash effects, etc.
    /// </summary>
    private void PlayDamageParticleEffect(DamageInsideStack val)
    {
        //Declaring local variables
        Debug.Log(dmgText);

        FloatingDamageText txtInstance = Instantiate(dmgText);
        Vector2 screenPos = Camera.main.WorldToScreenPoint(val.victim.transform.position);

        txtInstance.transform.SetParent(canvasRef.transform, false);
        txtInstance.transform.position = screenPos;
        txtInstance.SetDamageValueText(val.damage);
    }

    //This coroutine will add a delay to damage animations.
    private IEnumerator ExecuteDamageAnimationsAndCalculations()
    {
        toPlay = false;
        yield return new WaitForSeconds(0.05f);
        toPlay = true;
    }

    #endregion
}
