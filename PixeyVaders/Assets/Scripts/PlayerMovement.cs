using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region PlayerInput
    private PlayerInput input;
    private void OnEnable()
    {
        input.Player.Enable();
    }

    private void OnDisable()
    {
        input.Player.Disable();
    }
    #endregion

    private Rigidbody rb;

    private bool canMove = true;                //is the player allowed to move? don't let the user input button presses while moving
    private float moveX;                        //catch player action button presses
    public float moveDuration = 0.5f;           //How long movement lasts
    [SerializeField] private int hDelta = 10;   //How far to move the player ship
    public float tiltAmt = 30;                  //How much the player ship rotates

    public float leftBound = -70;               //Horiztonal bounds of the play area
    public float rightBound = 70;

    private void Awake()
    {
        input = new PlayerInput();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        moveX = input.Player.MoveHorizontal.ReadValue<float>();

        if (canMove)
        {
            if (moveX < 0 && transform.position.x >= leftBound || moveX > 0 && transform.position.x <= rightBound)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, tiltAmt * -(int)moveX);
                StartCoroutine(MoveToDestination((int)moveX));
            }
        }

    }
    IEnumerator MoveToDestination(int moveDirection)
    {
        //don't allow player input during the move
        canMove = false;

        //store the starting location before moving
        Vector3 currentPosition = transform.position;
        //assign the movement destination
        Vector3 moveDest = currentPosition;
        moveDest.x += hDelta * moveDirection;

        //how much time has passed and how long the move will take
        float elapsedTime = 0f;

        //move the ship
        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;
            transform.position = Vector3.Lerp(currentPosition, moveDest, t);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, 0f), t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        //once the move is mostly complete, make sure the ship is at the final position
        transform.position = moveDest;
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        //player can input again
        canMove = true;
    }
}