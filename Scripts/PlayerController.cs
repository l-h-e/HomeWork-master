using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float maxAngleV;           // Max Vertical Angle
    public float minAngleV;           // Min Vertical Angle
    public float maxAngleH;           // Max Horizontal Angle
    public float minAngleH;           // Min Horizontal Angle
    public float speed;               // Rotational Speed

    private Vector3 PlayerPosition;   // Player Position


    void Start()
    {
        // Initialize
        maxAngleV = 280;
        minAngleV = 80;
        maxAngleH = 280;
        minAngleH = 80;
        speed = 50;
        PlayerPosition = transform.position;
    }

    void Update()
    {
        float addRot = speed * Time.deltaTime;

        //Debug.Log("-----------------------------------------");
        //Debug.Log(transform.localEulerAngles.x);

        // Move Up
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            if (minAngleV > transform.localEulerAngles.x - addRot || transform.localEulerAngles.x - addRot > maxAngleV)
                transform.RotateAround(PlayerPosition, transform.right, -addRot);

        // Move Down
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            if (minAngleV > transform.localEulerAngles.x + addRot || transform.localEulerAngles.x + addRot > maxAngleV)
                transform.RotateAround(PlayerPosition, transform.right, addRot);

        // Move Right
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            transform.RotateAround(PlayerPosition, Vector3.up, addRot);

        // Move Left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            transform.RotateAround(PlayerPosition, Vector3.up, -addRot);
    }

    public bool isPlayerFront()
    {
        if (minAngleH < transform.localEulerAngles.y && transform.localEulerAngles.y < maxAngleH)
            return false;
        return true;
    }
}
