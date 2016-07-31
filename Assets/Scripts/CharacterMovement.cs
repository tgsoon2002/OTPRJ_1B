using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour 
{
    #region Data Members
    private Rigidbody2D phys;
    Touch touch;
    Transform cachedTransform;
    Vector3 startingPosition;
    Collider2D col;
    bool isMoving = false;
    float _horizontalLimit = Screen.height, _verticalLimit = Screen.width;

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
                        Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                        Vector2 touchPosition = new Vector2(wp.x, wp.y);

                        if (col == Physics2D.OverlapPoint(touchPosition))
                        {
                            isMoving = true;
                        }
                    }
                    break;
                case TouchPhase.Moved:
                    if (isMoving && Input.touchCount == 1)
                    {
                        DragObject(deltaPosition);
                    }
                    break;
                case TouchPhase.Ended:
                    isMoving = false;
                    break;
            }
        }
	}

    void DragObject(Vector2 deltaPosition)
    {
        cachedTransform.position = new Vector3(Mathf.Clamp((deltaPosition.x * dragSpeed) + cachedTransform.position.x, 
                startingPosition.x - _horizontalLimit, startingPosition.x + _horizontalLimit),
            Mathf.Clamp((deltaPosition.y * dragSpeed) + cachedTransform.position.y,
                startingPosition.y - _verticalLimit, startingPosition.y + _verticalLimit),
            cachedTransform.position.z);        
    }
}
