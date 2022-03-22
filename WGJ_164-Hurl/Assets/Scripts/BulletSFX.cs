using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSFX : MonoBehaviour
{
    [SerializeField] AudioSource sfx = null;
    [Tooltip("How far the pitch can oscilate between 'bullets'")]
    [Range(0f, 0.5f)]
    [SerializeField] float pitchRange = 0.3f;
    [Tooltip("Base pitch level. Final pitch is ranged from (basePitch - pitchRange) to (basePitch + pitchRange)'")]
    [Range(0f, 2f)]
    [SerializeField] float basePitch = 1f;
    // Start is called before the first frame update
    void Start()
    {
        sfx.pitch = Random.Range(basePitch - pitchRange, basePitch + pitchRange);
    }
}
