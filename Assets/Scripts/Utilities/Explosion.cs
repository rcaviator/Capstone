using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : PauseableObject
{
    //float lifeTimer = 0f;
    //Animator anim;

	// Use this for initialization
	protected override void Awake ()
    {
        base.Awake();
        //anim = GetComponent<Animator>();

        int rand = Random.Range(0, 5);
        switch (rand)
        {
            case 0:
                AudioManager.Instance.PlayGamePlaySoundEffect(GameSoundEffect.Blast1);
                break;
            case 1:
                AudioManager.Instance.PlayGamePlaySoundEffect(GameSoundEffect.Blast2);
                break;
            case 2:
                AudioManager.Instance.PlayGamePlaySoundEffect(GameSoundEffect.Blast3);
                break;
            case 3:
                AudioManager.Instance.PlayGamePlaySoundEffect(GameSoundEffect.Blast4);
                break;
            case 4:
                AudioManager.Instance.PlayGamePlaySoundEffect(GameSoundEffect.Blast5);
                break;
            case 5:
                AudioManager.Instance.PlayGamePlaySoundEffect(GameSoundEffect.Blast6);
                break;
            default:
                break;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            Destroy(gameObject);
        }
	}
}
