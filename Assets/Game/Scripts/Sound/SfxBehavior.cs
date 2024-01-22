using System.Collections;
using UnityEngine;

public class SfxBehavior : MonoBehaviour
{
    public IEnumerator audioTimer(float duration = 0f)
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}
