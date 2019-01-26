using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    public Sprite otherSprite;

    // Start is called before the first frame update
    void Start()
    {
        bool randomSelect = (Random.value > 0.5f);

        if (randomSelect)
            GetComponent<SpriteRenderer>().sprite = otherSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
