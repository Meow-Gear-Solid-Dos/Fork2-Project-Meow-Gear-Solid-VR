using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PunchFunction : MonoBehaviour
{
    [SerializeField] private GameObject HitBoxPrefab;
    [SerializeField] private Transform rightFist;
    [SerializeField] private Transform leftFist;
    public Rigidbody LeftControllerDevice;
    public Rigidbody RightControllerDevice;
    public Vector3 LeftControllerVelocity;
    public Vector3 RightControllerVelocity;
    public bool leftGrip;
    public bool rightGrip;
    public float punchVelocityRequirement = 2f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if((LeftControllerVelocity.magnitude >= punchVelocityRequirement) && leftGrip == true)
        {
            GameObject hitBox = Instantiate(HitBoxPrefab, leftFist.position, leftFist.rotation);
        }
        if((RightControllerVelocity.magnitude >= punchVelocityRequirement) && rightGrip == true)
        {
            GameObject hitBox = Instantiate(HitBoxPrefab, rightFist.position, rightFist.rotation);
        }
    }
}
