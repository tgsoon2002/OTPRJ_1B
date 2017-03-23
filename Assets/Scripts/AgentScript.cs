using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgentScript : MonoBehaviour {
    int SIGHTLAYER;
    public float stamina;
    public float staminaRegen;
    public float staminaCap;
    private bool detectedPlayer;
    private float minstaminaSaveAmount;//amount to save considering attack. i.e 0.5 for 50%.
    private float staminaSaveAmount;//amount to save for moving.
    private float staminaCost;//stamina cost per update when moving.
    public GameObject targetEnemy; //made it public for taunt.
    private Vector3 locationOfEnemy;
    public string attackType; //range char might need to changed to melee depends on the design.
    public Vector3 initTarget;
    private bool outOfStamina;
    private UnityEngine.AI.NavMeshAgent agent;
    private Vector3 curpos;//position tracker
    public List<GameObject> enemyList;
    public float attackRange;
    private static Vector3 initloc;// remember init location.
    private bool wentToDefaultLoc = false;//for patrol.
    private float attackInterval;
    private bool attackCD;
    // Use this for initialization
    void Start()
    {
        SIGHTLAYER = 1 << 31;
        SIGHTLAYER = ~SIGHTLAYER;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); stamina = 200f;
        staminaRegen = 8.5f;
        staminaCap = 300f;
        detectedPlayer = false;
        wentToDefaultLoc = false;
        minstaminaSaveAmount = 0.1f;
        staminaSaveAmount = 0.8f;
        staminaCost = 2.50f;
        initloc = agent.transform.position;//remember init loc.
        initTarget = new Vector3(0.0f, 0.0f, -10.8f); //initailly given target location
        outOfStamina = false;
        curpos = agent.transform.position;
        enemyList = new List<GameObject>();
        attackCD = false;
        if (attackType == "Melee")//we can combine this if and else if we have attack range information on db, just read it from there.
        {
            attackRange = 2.0f;
        }
        else //case of ranged enemy
        {
            attackRange = 6.0f;//need to update it based on the attack range.
        }
        //Debug.Log(locationOfEnemy);
        agent.SetDestination(initTarget); //move to enemy location. in start, it will be 0.0,0.0,-10.8
        //Debug.Log(agent.destination);


    }
    
    #region Publics
    public void setLocation(Vector3 location)
    {
        agent.SetDestination(location);
    }
    public void stopMovement()
    {
        agent.Stop();
    }
    public void resumeMovement()
    {
        agent.Resume();
    }
    public void setInitTarget(Vector3 target)
    {
        initTarget = target;
    }
    public void setAttackRange(float range)
    {
        attackRange = range;
    }
    public void setAttackInterval(float seconds)
    {
        attackInterval = seconds;
    }
    #endregion
    #region Privates
    private float calculateDistance(Vector3 location)
    {
        float result;
        float x0 = location.x;
        float z0 = location.z;
        float x1 = gameObject.transform.position.x;
        float z1 = gameObject.transform.position.z;
        result = (z1 - z0) * (z1 - z0) + (x1 - x0) * (x1 - x0);
        result = Mathf.Sqrt(result);
        return result;
    }
    private GameObject ChooseTarget()//choosing a target with in sight range, determining with distance.
    {
        float shortest = 99999.0f;
        GameObject resultGameObject = gameObject;
        if (enemyList.Count > 0)
        {
            for (int i = 0; i < enemyList.Count; i++)//max will be 3 anyways.
            {
                float distance = calculateDistance(enemyList[i].transform.position);
                if (distance < shortest)
                {
                    shortest = distance;
                    resultGameObject = enemyList[i];
                }
            }
        }
        return resultGameObject;
    }
    private void SetTarget()
    {
        if (targetEnemy.tag != "Enemy")
        {
            locationOfEnemy = targetEnemy.transform.position;
            agent.SetDestination(locationOfEnemy);
            detectedPlayer = true;
        }
        else // if there is no target enemy,set it to the default one.
        {
            agent.SetDestination(locationOfEnemy);
        }
    }
    private void OnTriggerEnter(Collider coll)
    {
        //Debug.Log("onTriggerEnter Triggered");
        if (coll.tag == "Player")
        {
            enemyList.Add(coll.gameObject);
            detectedPlayer = true;
        }
    }
    private void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Player")
        {
            enemyList.Remove(coll.gameObject);
        }
        //Debug.Log("that weakling");
    }
    private bool isMoving()
    {
        //Debug.Log(curpos != agent.transform.position);
        return (curpos != agent.transform.position);
    }
    private bool isOutofStamina()
    {
        if (stamina < staminaCap * minstaminaSaveAmount)
        {
            outOfStamina = true;
        }
        else if (stamina > staminaCap * staminaSaveAmount)
        {
            outOfStamina = false;
        }
        return outOfStamina;
    }
    // Update is called once per frame
    private bool enoughDistance()
    {
        return (calculateDistance(locationOfEnemy) <= attackRange);
    }
    private void patrol()
    {
        if (wentToDefaultLoc == true)
        {
            Debug.Log("went to Default loc");
            agent.SetDestination(initloc);//patrol around the location
        }
    }
    private bool isAttackable(Vector3 startingPoint, Vector3 endPoint)
    {
        RaycastHit hit;
        bool line = Physics.Raycast(startingPoint, (endPoint - startingPoint), out hit);
        //Debug.Log(line);
        if (line == true)
        {
            //Debug.Log(hit.collider.tag);
            if (hit.collider.tag=="Wall")
            {

                //Debug.Log("there's a wall. gg");
                return false;
            }
            else
            {
                //Debug.Log("there's no wall");
                return true;
            }
            
        }
        else
        {
            //Debug.Log("there's no wall");
            return true;
        }
        

        //return true;
    }
    private void attackPlayer(string type, float Range, float interval)
    {
        if (attackType != "Melee")
        {
            if (!attackCD)
            {
                Debug.Log("create arrow");
                //Instantiate Projectile here and give direction and force
                StartCoroutine(wait(interval));
            }
        }
    }

    private IEnumerator wait(float interval)
    {
        attackCD = true;
        yield return new WaitForSeconds(interval);
        attackCD = false;
    }
    #endregion
    void Update ()
    {
        if (isOutofStamina() == true)
        {
            agent.Stop();
        }

        if (detectedPlayer == false)//when enemy is not detected.
        {

            if (isOutofStamina()==false)
            {
                agent.Resume();
                patrol();
            }
        }
        else//when enemy is detected.
        {
            

            #region Checking for target and setting the target
            targetEnemy = ChooseTarget();
            SetTarget();
            #endregion

            if (enoughDistance() == false && isOutofStamina() == false)
            {
                agent.Resume();
            }
            else if (enoughDistance() == true)
            {
                //agent.Stop();
                bool temp;
                temp = isAttackable(agent.transform.position, locationOfEnemy);
                if (temp == true)
                {
                    agent.Stop();
                    attackPlayer(attackType, attackRange, 5.0f);
                }
                else
                {
                    agent.Resume();
                }
            }
        }

        #region handles movement and stamina calc.
        if (isMoving() == true)
        {
            curpos = agent.transform.position;
            stamina -= staminaCost;
        }
        else
        {
            if (stamina < staminaCap - staminaRegen)
            {
                stamina += staminaRegen;
            }
        }
        #endregion
        #region for debugging.
        /*if(Input.GetMouseButtonDown(0)) // handling movement stuff. remove it later. only for debug.
         {
             agent.Resume();
             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             RaycastHit hit;
             if (Physics.Raycast(ray,out hit))//raycast stuff.
             { 
                 if (hit.collider.tag == "Ground")
                 {

                     agent.SetDestination(hit.point);
                     targetX = hit.point.x;
                     targetZ = hit.point.y;
                     Debug.Log(hit.point);
                 }
             }

         }*///end of debug stuff.
        #endregion
    }
}
