using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xwing : MonoBehaviour
{
    [Header("Movement")]
    public Transform shape;
    public float speed;
    public float turnSpeed;
    public float maxY;
    public float maxX;
    public float maxYRotation;
    public float maxXRotation;

    [Header("Attack")]
    public GameObject bulletPrefab;
    public Transform[] posRotBullet;
    public Transform reticle;

    private AudioSource audio;
    private float _horizontal;
    private float _vertical;
    private float _xMouse;
    private float _yMouse;
    private bool _attack;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Locked;
        Aim();
    }

    void Update()
    {
        InputPlayer();
        Movement();
        Turning();
        Attack();
    }

    private void Aim()
    {
        foreach (Transform pos in posRotBullet)
        {
            pos.LookAt(reticle);
        }
    }

    private void InputPlayer()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _xMouse = Input.GetAxis("Mouse X");
        _yMouse = Input.GetAxis("Mouse Y");
        _attack = Input.GetMouseButtonDown(0);
    }

    void Movement()
    {
        float horizontalMove = _horizontal * speed * Time.deltaTime;
        if (transform.position.x + horizontalMove > maxX || transform.position.x + horizontalMove < -maxX)
        {
            horizontalMove = 0;
        }

        float verticalMove = _vertical * speed * Time.deltaTime;
        if (transform.position.y + verticalMove > maxY || transform.position.y + verticalMove < -maxY)
        {
            verticalMove = 0;
        }

        Vector3 direction = new Vector3(horizontalMove, verticalMove, 0);

        transform.Translate(direction);
    }

    void Turning()
    {
        
        Vector3 rotation = new Vector3(-_yMouse, _xMouse, 0);
        shape.transform.Rotate(rotation.normalized * turnSpeed * Time.deltaTime);

        Vector3 currentRotation = shape.transform.localRotation.eulerAngles;
        currentRotation.y = ConvertToAngle180(currentRotation.y);
        currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYRotation, maxYRotation);
        currentRotation.x = ConvertToAngle180(currentRotation.x);
        currentRotation.x = Mathf.Clamp(currentRotation.x, -maxXRotation, maxXRotation);
        currentRotation.z = 0;
        shape.transform.localRotation = Quaternion.Euler(currentRotation);
    }

    private static float ConvertToAngle180(float input)
    {
        if (input > 180)
        {
            return input - 360;
        }
        else
        {
            return input;
        }
    }

    void Attack()
    {
        if (_attack)
        {
            audio.Play();

            for (int i = 0; i < posRotBullet.Length; i++)
            {
                Instantiate(bulletPrefab, posRotBullet[i].position, posRotBullet[i].rotation);
            }
        }
    }
}
