using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PunchFunction : MonoBehaviour
{
    [SerializeField] private GameObject HitBoxPrefab;
    [SerializeField] private Transform fist;
    public float speed;
    public bool leftGrip;
    public bool rightGrip;
    public bool punching;
    public float punchVelocityRequirement = 2f;


    //Below is for audio function
    public AudioSource source;
    public AudioClip whiff1;
    public AudioClip whiff2;
    // Start is called before the first frame update
    void Start()
    {
        punching = false;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(FistSpeed(fist));
    }

    public IEnumerator FistSpeed(Transform fist)
    {
        var previous = fist.localPosition;
        previous.y = 0;
        yield return new WaitForSeconds(.001f);
        var current = fist.localPosition;
        current.y = 0;
        speed = ((current - previous).magnitude) / Time.deltaTime;
        if((speed >= punchVelocityRequirement) && punching == false)
        {
            if(Random.Range(0, 2) == 0 )
            {
                punching = true;
                source.PlayOneShot(whiff2, .25f);
                StartCoroutine(Timeout(fist));

            }

            else
            {
                punching = true;
                source.PlayOneShot(whiff1, .25f);
                StartCoroutine(Timeout(fist));	
            }
        }
    }
    IEnumerator Timeout(Transform fist)
    {
        GameObject hitBox = Instantiate(HitBoxPrefab, fist.position, fist.rotation);
		yield return new WaitForSeconds(.3f);	
        punching = false;
        
    }
}
