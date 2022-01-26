using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float horizontalSpeed = 10f;
    [SerializeField] float xRange = 10f;
    [SerializeField] float yRange = 10f;


    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float positionYawFactor = 3f;
    [SerializeField] float controlRollFactor = -20f;
    float xThrow, yThrow;
    void Update()
    {
        TranslationMovement();
        RotationMovement();
    }

    private void RotationMovement()
    {
        float pitchBasedOnPosition = transform.localPosition.y * positionPitchFactor;
        float pitchBasedOnThrow = yThrow * controlPitchFactor;


        float pitch = pitchBasedOnPosition + pitchBasedOnThrow; // around x axis rotation
        float yaw = transform.localPosition.x * positionYawFactor; // around y axis rotation
        float roll = xThrow * controlRollFactor; // around z axis rotation

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);

    }

    private void TranslationMovement()
    {
        // getting axis info(updown values)
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        // Horizontal movement of ship
        float xOffset = xThrow * Time.deltaTime * horizontalSpeed; // creating frame rate independent offset with some constant speed
        float rawXPos = transform.localPosition.x + xOffset; // local position of ship + new position
        // limiting the horizontal movement given range so ship will not go out of window
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);



        // Vertical movement of ship
        float yOffset = yThrow * Time.deltaTime * horizontalSpeed; // creating frame rate independent offset with some constant speed
        float rawYPos = transform.localPosition.y + yOffset; // local position of ship + new position
        // limiting the horizontal movement given range so ship will not go out of window
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);


        // setting position of ship accordingly
        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);

        // Debug.Log(horizontalThrow);
        // Debug.Log(verticalThrow);
    }
}
