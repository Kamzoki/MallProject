using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]
    private Joystick MoverJoystick;
    [SerializeField]
    private Joystick RotatorJoystick;

    private CharacterController CC;
    private GameObject PlayerCamera;

    [SerializeField]
    private float movementSpeed = 20f;
    [SerializeField]
    private float LookSmoothness = 1f;
    [SerializeField]
    private float MaxVerticalLooking = 45;

    void Start()
    {
        CC = GetComponent<CharacterController>();
        PlayerCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void MovePlayer()
    {
        if (MoverJoystick && CC)
        {
            Vector3 newSpeed = new Vector3(MoverJoystick.Horizontal * movementSpeed,
                                      transform.position.y,
                                      MoverJoystick.Vertical * movementSpeed);

            CC.SimpleMove(newSpeed);
        }
        else
        {
            Debug.LogError("Missing CharacterController on player or Mover Joystick in Canvas (of tag MoverJS)");
        }
    }

    private void RotatePlayer()
    {
        if (RotatorJoystick && CC && PlayerCamera)
        {
            float YRotation = RotatorJoystick.Horizontal * LookSmoothness;

            float XRotation = RotatorJoystick.Vertical * LookSmoothness;

            XRotation = Mathf.Clamp(XRotation, -MaxVerticalLooking, MaxVerticalLooking);

            Quaternion newRotation = new Quaternion(0, YRotation, 0, 0);
        }

        else
        {
            Debug.LogError("Missing CharacterController on player, MainCamera not found, or Rotator Joystick in Canvas (of tag RotatorJS");
        }
    }
}
