using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    //Sound nonsense is below
	public AudioSource source;
    public AudioClip footStep1;
    public AudioClip footStep2;
    public bool isWalking;
    public bool footSound;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("WalkingSpeed");
			if(speed >= 5 && footSound == false)
			{
                isWalking = true;
				if(Random.Range(0, 2) == 0 )
				{
					footSound = true;
					source.PlayOneShot(footStep2, .25f);
					StartCoroutine("Timeout");	
				}
				else
				{
					footSound = true;
					source.PlayOneShot(footStep1, .25f);
					StartCoroutine("Timeout");	
				}
				
			}
            else
            {
                isWalking = false;
            }
    }

    IEnumerator Timeout()
    {
			yield return new WaitForSeconds(.3f);	
			footSound = false;
        
    }
    public IEnumerator WalkingSpeed()
    {
        var previous = transform.position;
        previous.y = 0;
        yield return new WaitForSeconds(.001f);
        var current = transform.position;
        current.y = 0;
        speed = ((current - previous).magnitude) / Time.deltaTime;
    }
}
