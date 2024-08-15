using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.EventSystems;

public class RocketController : MonoBehaviour
{
    [SerializeField] private float maxDragDistance = 5f;
    [SerializeField] private float launchForceMultiplier = 10f;
    [SerializeField] private float minScaleY = 0.5f;
    [SerializeField] private float maxVelocity = 20f;
    [SerializeField] private float moveForce = 5f;
    [SerializeField] private float returnDelay = 1f;
    [SerializeField] private float rotationDuration = 0.5f;

    private Vector2 dragStartPos;
    private Vector2 dragEndPos;
    private bool isDragging = false;
    private bool isLaunched = false;
    private Rigidbody2D rb;
    private Vector3 originalScale;

    [SerializeField] private GameObject speedLineVFX;

    private RocketFuel rocketFuel;

    private bool isShieldActive;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
        rocketFuel = GetComponent<RocketFuel>(); 
    }

    private void Update()
    {
        HandleInput();
        CapVelocity();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))

        {   
            // if(DoubleTap check) // 
            // PowerUpManager.Instance.StartShieldPowerUp(); //

            if (!isLaunched && !IsPointerOverUI())
            {
                dragStartPos = GetMouseWorldPosition();
                isDragging = true;
            }
            else
            {
                HandleRotationInput();
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            dragEndPos = GetMouseWorldPosition();
            LaunchRocket();
            isDragging = false;
            isLaunched = true;
        }

        if (isDragging)
        {
            UpdateScale(GetMouseWorldPosition());
        }
    }

    private Vector2 GetMouseWorldPosition()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }

    private void LaunchRocket()
    {
        Vector2 dragVector = dragEndPos - dragStartPos;
        dragVector = Vector2.ClampMagnitude(dragVector, maxDragDistance);

        float launchForce = dragVector.magnitude * launchForceMultiplier;
        rb.AddForce(Vector2.up * launchForce, ForceMode2D.Impulse);

        ResetScale();
        StartCoroutine(ShowSpeedLines());
    }

    private void UpdateScale(Vector2 currentDragPos)
    {
        float dragMagnitude = Vector2.Distance(currentDragPos, dragStartPos);
        float scaleY = Mathf.Lerp(originalScale.y, minScaleY, Mathf.Clamp01(dragMagnitude / maxDragDistance));
        transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
    }

    private void ResetScale()
    {
        transform.DOScaleY(originalScale.y, 0.1f);  
    }

    private IEnumerator ShowSpeedLines()
    {
        speedLineVFX.SetActive(true);
        yield return new WaitForSeconds(1f);
        speedLineVFX.SetActive(false);
    }

    private void HandleRotationInput()
    {
        Vector2 tapPosition = GetMouseWorldPosition();
        float angle = (tapPosition.x < transform.position.x) ? 45f : -45f;

        RotateAndMoveRocket(angle);
    }

    private void RotateAndMoveRocket(float angle)
    {
        float targetZRotation = Mathf.Clamp(transform.eulerAngles.z + angle, -45f, 45f);

        if (!Mathf.Approximately(targetZRotation, transform.eulerAngles.z))
        {
            transform.DORotate(Vector3.forward * targetZRotation, rotationDuration, RotateMode.Fast)
                .OnComplete(() => ApplyMovementForce());

            Invoke(nameof(RotateBackToZero), returnDelay);
        }
    }

    private void ApplyMovementForce()
    {
        Vector2 moveDirection = transform.up;
        rb.AddForce(moveDirection * moveForce, ForceMode2D.Impulse);
    }

    private void RotateBackToZero()
    {
        float verticalVelocity = rb.velocity.y;
        transform.DORotate(Vector3.zero, rotationDuration, RotateMode.Fast).OnComplete(() =>
        {
            rb.velocity = new Vector2(0, verticalVelocity);
        });
    }

    private void CapVelocity()
    {
        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
    }

    private bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        switch (collision.collider.tag)
        {
            case "Goal":
                rb.velocity = Vector2.zero;
                UIManager.Instance.LevelCompleted();
                Debug.Log("YOU WON");
                break;

            case "Obstacle":
                if(isShieldActive){
                    isShieldActive = false;
                    break;
                }
                rb.velocity = Vector2.zero;
                UIManager.Instance.LevelFailed();
                Destroy(gameObject);
                Debug.Log("YOU LOSE");
                break;
        }
    }

    public void OnTriggerEnter2D(Collider2D collider){
        switch (collider.tag)
        {

            case "Star":
                StarManager.Instance.CollectStar(collider.gameObject);
                break;

            case "Fuel":
                if (rocketFuel != null)
                {
                    rocketFuel.AddFuel();
                }
                Destroy(collider.gameObject);
                break;
        }
    }

    public void OnFuelDepleted()
    {
        rb.velocity = Vector2.zero;
        UIManager.Instance.LevelFailed();
        Destroy(gameObject);
    }

    public void ActivateShield(){
        
        isShieldActive = true;

        // Additional shield activation logic // 

        Debug.Log("Shield Activated!");

    }

    public void ApplyPowerUp(IPowerUp powerUp){
        powerUp.Execute();
    }
}
