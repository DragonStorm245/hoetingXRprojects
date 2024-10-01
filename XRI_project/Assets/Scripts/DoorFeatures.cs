using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorFeatures : CoreFeatures //Inheritence
{
    [Header("Door Configuration")]

    [SerializeField]
    private Transform doorPivot; //Controls pivot

    [SerializeField]
    private float maxAngle = 90.0f;

    [SerializeField]
    private bool reverseAngleDirection = false;

    [SerializeField]
    private float doorSpeed = 1.0f;

    [SerializeField]
    private bool open = false;

    [SerializeField]
    private bool MakeKinematicOnOpen = false;

    [Header("Interactions Configuration")]

    [SerializeField]
    private XRSocketInteractor socketInteractor;

    [SerializeField]
    private XRSimpleInteractable simpleInteractable;

    private void Start()
    {
        socketInteractor?.selectEntered.AddListener((s) => 
        {
            OpenDoor();
            PlayOnStart();
        });

        simpleInteractable?.selectEntered.AddListener((s) =>
        {
            OpenDoor();
        });
    }

    public void OpenDoor()
    {
        if (!open)
        {
            PlayOnStart();
            open = true;
            StartCoroutine(ProcessedMotion());
        }
    }

    private IEnumerator ProcessedMotion()
    {
        //Constantly look to confirm door is open
        while (open)
        {
            var angle = doorPivot.localEulerAngles.y < 100 ? doorPivot.localEulerAngles.y : doorPivot.localEulerAngles.y -360;

            angle = reverseAngleDirection ? Mathf.Abs(angle) : angle;

            if (angle <= maxAngle)
            {
                doorPivot?.Rotate(Vector3.up, doorSpeed * Time.deltaTime * (reverseAngleDirection ? -1 : 1));
            }

            else
            {
                //when done interacting, turn off rigidbody
                open = false;
                var featureRigidBody = GetComponent<Rigidbody>();
                if (featureRigidBody != null && MakeKinematicOnOpen) featureRigidBody.isKinematic = true;

            }

            yield return null;
        }
    }
}
