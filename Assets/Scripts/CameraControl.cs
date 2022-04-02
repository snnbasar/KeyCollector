using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraControl : MonoBehaviour
{
    private PostProcessVolume volume;

    private bool dieEffectControl;

    private Vignette vignette;

    // Start is called before the first frame update
    void Start()
    {
        volume = gameObject.GetComponent<PostProcessVolume>();
        GameManager.instance.OnPlayerDied += SetDieEffectOn;
        GameManager.instance.OnStartGame += ResetDieEffect;
        volume.profile.TryGetSettings(out vignette);
        
    }


    private void SetDieEffectOn()
    {
        dieEffectControl = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dieEffectControl)
        {
            if (vignette.intensity.value > 0.5f)
                dieEffectControl = false;
            else
                vignette.intensity.value += Time.deltaTime;

        }
    }

    private void ResetDieEffect()
    {
        vignette.intensity.value = 0f;
    }
}
