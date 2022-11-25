using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FloatUpText : MonoBehaviour
{
    TMP_Text text;
    float alpha = 1;

    // Start is called before the first frame update
    void Start()
    {
        text = this.GetComponent<TMP_Text>();
        text.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(0, 20, 0);
        alpha -= 0.05f;
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
        if (alpha < 0) Destroy(this.gameObject);
    }
}
