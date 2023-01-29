using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbnormalGLight : MonoBehaviour
{
    [SerializeField] private float gravityScale = 0.3f;

    public float getGravityScale() {
        return gravityScale;
    }
}
