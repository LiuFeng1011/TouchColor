using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIBaseLayer : BaseUnityObject {
    protected float showTime = 0, hideTime = 0, maxActionTime = 0.5f, actionHeight = -100f;

    public static AnimationCurve action;

    protected UILabel actionLabel;

    public void ActionUpdate()
    {
        ShowAction();
        HideAction();
    }

    public virtual void Init(){
        if (action == null)
        {
            Keyframe[] ks = {
                new Keyframe(0f,0f),
                //new Keyframe(0.3f,-0.15f),
                new Keyframe(1f,1f)
            };

            action = new AnimationCurve(ks);
        }

        actionLabel = gameObject.AddComponent<UILabel>();
        actionLabel.text = "";
        actionLabel.gameObject.SetActive(false);
        actionLabel.gameObject.SetActive(true);
    }

    public virtual void Show(){
        gameObject.SetActive(true);
        showTime = maxActionTime;
        transform.localPosition = new Vector3(0, actionHeight, 0);
        actionLabel.color = new Color(1, 1, 1, 0);
    }

    public virtual void Hide(){
        if (!gameObject.activeSelf) return;
        hideTime = maxActionTime;
    }


    protected virtual void ShowAction()
    {
        if (showTime <= 0) return;
        showTime -= Time.deltaTime;
        float rate = action.Evaluate(showTime / maxActionTime);

        float scale = actionHeight * rate;
        transform.localPosition = new Vector3(0, scale, 0);

        actionLabel.color = new Color(1, 1, 1, 1-rate);
    }

    protected virtual void HideAction()
    {
        if (hideTime <= 0) return;
        hideTime -= Time.deltaTime*2;

        if(hideTime <= 0){
            gameObject.SetActive(false);
        }

        float rate = hideTime / maxActionTime;

        actionLabel.color = new Color(1, 1, 1, rate);
    }

	
}
