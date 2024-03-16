using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSSelfDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SelfDestroy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SelfDestroy()
    {
        Destroy(gameObject, 4f);
    }
}
