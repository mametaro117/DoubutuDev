using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{

    #region Singleton

    private static EffectManager instance_Effect;

    public static EffectManager Instance_Effect
    {
        get
        {
            if (instance_Effect == null)
            {
                instance_Effect = (EffectManager)FindObjectOfType(typeof(EffectManager));

                if (instance_Effect == null)
                {
                    Debug.LogError(typeof(EffectManager) + "is nothing");
                }
            }
            return instance_Effect;
        }
    }

    #endregion Singleton

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    [SerializeField]
    private GameObject Effect_Dia;
    [SerializeField]
    private GameObject Effect_Heart;
    [SerializeField]
    private GameObject Effect_Star;
    [SerializeField]
    private GameObject Effect_Spark;
    [SerializeField]
    private GameObject Effect_Light;
    [SerializeField]
    private GameObject Effect_Smoke;
    [SerializeField]
    private GameObject Effect_Firework;
    [SerializeField]
    private GameObject Effect_Hit;
    [SerializeField]
    private GameObject Effect_Magic;

    public static class EffectKind
    {
        public const int Diamond = 0;
        public const int Heart = 1;
        public const int Star = 2;
        public const int Spark = 3;
        public const int Light = 4;
        public const int Smoke = 5;
        public const int FireWork = 6;
        public const int Hit = 7;
        public const int Magic = 8;
    }



    //やる事を関数でまとめた
    private void EffectProcess(GameObject PlayEffect, Vector2 EffectPos, float Magnification, GameObject Target)
    {
        PlayEffect.transform.position = EffectPos;
        PlayEffect.transform.localScale = new Vector3(Magnification, Magnification, Magnification);
        PlayEffect.transform.SetParent(Target.transform);
        ParticleSystem particlesystem = PlayEffect.GetComponent<ParticleSystem>();
        var main = particlesystem.main;
        if (PlayEffect != null)
            Destroy(PlayEffect, main.duration);
    }

    //実行する時はコレ↓
    //EffectManager.Instance_Effect.PlayEffect(0, new Vector2(0, 0), 1.0f);

    /// <summary>
    /// エフェクトナンバーを指定する方
    /// 引数:(エフェクトナンバー,ポジション,拡大率)
    /// 0:Diamond
    /// 1:Heart
    /// 2:Star
    /// 3:Spark
    /// 4:Light
    /// 5:Smoke
    /// 6:Firework
    /// 7:Hit
    /// 8:Magic
    /// </summary>
    /// <param name="Effect"></param>
    public void PlayEffect(int EffectNum, Vector2 EffectPos, float Magnification, GameObject Target)
    {
        GameObject PlayEffect = null;
        switch (EffectNum)
        {
            case 0:
                PlayEffect = Instantiate(Effect_Dia) as GameObject;
                break;
            case 1:
                PlayEffect = Instantiate(Effect_Heart) as GameObject;
                break;
            case 2:
                PlayEffect = Instantiate(Effect_Star) as GameObject;
                break;
            case 3:
                PlayEffect = Instantiate(Effect_Spark) as GameObject;
                break;
            case 4:
                PlayEffect = Instantiate(Effect_Light) as GameObject;
                break;
            case 5:
                PlayEffect = Instantiate(Effect_Smoke) as GameObject;
                break;
            case 6:
                PlayEffect = Instantiate(Effect_Firework) as GameObject;
                break;
            case 7:
                PlayEffect = Instantiate(Effect_Hit) as GameObject;
                break;
            case 8:
                PlayEffect = Instantiate(Effect_Magic) as GameObject;
                break;
            default:
                PlayEffect = Instantiate(Effect_Dia) as GameObject;
                break;
        }
        Debug.Log(EffectNum);
        EffectProcess(PlayEffect, EffectPos, Magnification, Target);
    }
}
