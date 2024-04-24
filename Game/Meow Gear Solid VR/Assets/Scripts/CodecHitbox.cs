using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodecHitbox : MonoBehaviour
{
    [SerializeField] private Renderer codecRenderer;
    [SerializeField] private GameObject codecTrigger;
    // Start is called before the first frame update
    void Start()
    {
        Color invisible;
        invisible = codecRenderer.material.color;
        invisible.a = 0f;
        codecRenderer.material.color = invisible;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider col)
    {
        if((col.gameObject.tag == "Player"))
        {
            Destroy(codecTrigger);
        }
    }
}
