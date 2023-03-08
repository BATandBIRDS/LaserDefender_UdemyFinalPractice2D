using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 rawInput;
    Vector2 minBounds;
    Vector2 maxBounds;

    [SerializeField] float moveSpeed = 5f;

    // To avoid going out the scene, adding padding.
    [SerializeField] float paddingL;
    [SerializeField] float paddingR;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBot;

    Shooter shooter;

    void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    void Start()
    {
        InitBounds();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 delta = rawInput * Time.deltaTime * moveSpeed;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingL, maxBounds.x - paddingR);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBot, maxBounds.y - paddingTop);
        transform.position = newPos;
    }

    // Used UnityEngine.InputSystem
    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    // Adding padding code:
    void InitBounds()
    {
        Camera cam = Camera.main;
        minBounds = cam.ViewportToWorldPoint(new Vector2(0,0));
        maxBounds = cam.ViewportToWorldPoint(new Vector2(1,1));
    }

    void OnFire(InputValue value)
    {
        if(shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }
}
