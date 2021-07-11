using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    // var outside ontrigger to not reset count
    private int apples = 0;

    // var for updating apple count
    [SerializeField] private Text applesText;

    [SerializeField] private AudioSource collectionSoundEffect;

    // ontrigger referes to calling apple "is trigger"
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Apple"))
        {
            Destroy(collision.gameObject);
            apples++;
            applesText.text = "Apples: " + apples;
            collectionSoundEffect.Play();
        }
    }
}
