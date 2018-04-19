using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScoresComboLabel : MonoBehaviour {

    const float maxTime = 1.0f, maxHight = 0.1f;

    Vector3 startPos;
    UILabel label;

    public AnimationCurve heightAC,colorAC,scaleAC;

    float time;

    public void Init(Vector3 startPos, int scores)
    {
        this.startPos = startPos;
        label = transform.GetComponent<UILabel>();
        label.text = "+" + scores;
    }
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > maxTime)
        {
            Destroy(gameObject);
        }

        float rate = time / maxTime;
        float val = heightAC.Evaluate(rate);


        transform.position = startPos + new Vector3(0, maxHight * val, 0);

        float colorval = colorAC.Evaluate(rate);
        label.color = new Color(label.color.r, label.color.g, label.color.b, 1 - colorval);

        float scale = scaleAC.Evaluate(rate);
        transform.localScale = new Vector3(scale, scale, 1);
    }

}
