using UnityEngine;
using System.Collections;

public class TouchCharacterController : MonoBehaviour {
    int inputTouchCount = 0;
    Touch touchInput;
    TouchPhase touchPhase;
    Ray screenRay;
    RaycastHit hit;
    bool rayCastOnHit = false;
    bool isMoving = false;
    Vector2 deltaPosition;
    private Transform cachedTransform;

    [SerializeField]
    float dragSpeed = 0.00005f;


	// Use this for initialization
	void Start () {
        cachedTransform = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () 
    {
        inputTouchCount = Input.touchCount;


        for (int i = 0; i < inputTouchCount; i++)
        {
            touchInput = Input.GetTouch(i);
            touchPhase = touchInput.phase;
            deltaPosition = Input.GetTouch(0).deltaPosition;

            switch (touchPhase)
            {
                case TouchPhase.Began:
                    screenRay = Camera.main.ScreenPointToRay(touchInput.position);

                    rayCastOnHit = Physics.Raycast(screenRay, out hit);
                    CheckIfTouched(rayCastOnHit, hit, i);
                    break;
                case TouchPhase.Moved:
                    MoveCharacter(i);
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    isMoving = false;
                    break;
                case TouchPhase.Canceled:
                    isMoving = false;
                    break;
            }
        }
       

       
	}

    private void MoveCharacter(int touchIndex)
    {
        if (touchIndex == 0 && isMoving == true)
        {
            
            //float dragSpeed = gameObject.GetComponent<ICharacterStats>().Move_Speed;

            cachedTransform.position = new Vector3(deltaPosition.x * dragSpeed + cachedTransform.position.x, 
                                                   cachedTransform.position.y,
                                                   deltaPosition.y * dragSpeed + cachedTransform.position.z);        
            
        }  
    }

    private void CheckIfTouched(bool onHit, RaycastHit _hit, int touchIndex)
    {
        
        if (onHit)
        {
            if (_hit.collider.tag == "Player" && touchIndex <= 0)
            {
                Debug.Log(_hit);
                isMoving = true;
            }
        }
    }
}
