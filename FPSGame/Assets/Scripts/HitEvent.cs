using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

public class HitEvent : MonoBehaviour
{
    public EnemyFSM enemy;

    public void PlayerHit()
    {
        enemy.AttackAction();
        if (GameManager.Instance.player.hp <= 0)
        {
            enemy.anim.SetTrigger("Win");
            GameManager.Instance.source.Stop();
            GameManager.Instance.source.clip = GameManager.Instance.clip[1].clip;
            GameManager.Instance.source.Play();
        }
    }

}