using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TakeDamage : MonoBehaviour
{
    public float intensity=0f;

    PostProcessVolume _volume;
    Vignette _vignette;
    // Start is called before the first frame update
    void Start()
    {
        _volume = GetComponent<PostProcessVolume>();
        _volume.profile.TryGetSettings<Vignette>(out _vignette);

        if(!_vignette)
        {
            print("error, vignette empty");
        }
        else
        {
            _vignette.enabled.Override(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
