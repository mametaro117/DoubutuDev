using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {

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

    /// <summary>
    /// エフェクトナンバーを指定する方
    /// 引数:(エフェクトナンバー,ポジション)
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
    public void PlayEffect(int EffectNum, Vector2 EffectPos)
    {
        GameObject PlayEffect = null;
        switch(EffectNum)
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
        PlayEffect.transform.position = EffectPos;
        ParticleSystem particlesystem = PlayEffect.GetComponent<ParticleSystem>();
        var main = particlesystem.main;
        Destroy(PlayEffect, main.duration);
    }

    /// <summary>
    /// それぞれ関数がある方
    /// 引数:(ポジション)
    /// </summary>
    /// <param name="EffectPos"></param>
    public void PlayEffect_Dia(Vector2 EffectPos)
    {
        GameObject PlayEffect = Instantiate(Effect_Dia) as GameObject;
        PlayEffect.transform.position = EffectPos;
        ParticleSystem particlesystem = PlayEffect.GetComponent<ParticleSystem>();
        var main = particlesystem.main;
        Destroy(PlayEffect, main.duration);
    }

    /// <summary>
    /// それぞれ関数がある方
    /// 引数:(ポジション)
    /// </summary>
    /// <param name="EffectPos"></param>
    public void PlayEffect_Heart(Vector2 EffectPos)
    {
        GameObject PlayEffect = Instantiate(Effect_Heart) as GameObject;
        PlayEffect.transform.position = EffectPos;
        ParticleSystem particlesystem = PlayEffect.GetComponent<ParticleSystem>();
        var main = particlesystem.main;
        Destroy(PlayEffect, main.duration);
    }

    /// <summary>
    /// それぞれ関数がある方
    /// 引数:(ポジション)
    /// </summary>
    /// <param name="EffectPos"></param>
    public void PlayEffect_Star(Vector2 EffectPos)
    {
        GameObject PlayEffect = Instantiate(Effect_Star) as GameObject;
        PlayEffect.transform.position = EffectPos;
        ParticleSystem particlesystem = PlayEffect.GetComponent<ParticleSystem>();
        var main = particlesystem.main;
        Destroy(PlayEffect, main.duration);
    }

    /// <summary>
    /// それぞれ関数がある方
    /// 引数:(ポジション)
    /// </summary>
    /// <param name="EffectPos"></param>
    public void PlayEffect_Spark(Vector2 EffectPos)
    {
        GameObject PlayEffect = Instantiate(Effect_Spark) as GameObject;
        PlayEffect.transform.position = EffectPos;
        ParticleSystem particlesystem = PlayEffect.GetComponent<ParticleSystem>();
        var main = particlesystem.main;
        Destroy(PlayEffect, main.duration);
    }

    /// <summary>
    /// それぞれ関数がある方
    /// 引数:(ポジション)
    /// </summary>
    /// <param name="EffectPos"></param>
    public void PlayEffect_Light(Vector2 EffectPos)
    {
        GameObject PlayEffect = Instantiate(Effect_Light) as GameObject;
        PlayEffect.transform.position = EffectPos;
        ParticleSystem particlesystem = PlayEffect.GetComponent<ParticleSystem>();
        var main = particlesystem.main;
        Destroy(PlayEffect, main.duration);
    }

    /// <summary>
    /// それぞれ関数がある方
    /// 引数:(ポジション)
    /// </summary>
    /// <param name="EffectPos"></param>
    public void PlayEffect_Smoke(Vector2 EffectPos)
    {
        GameObject PlayEffect = Instantiate(Effect_Smoke) as GameObject;
        PlayEffect.transform.position = EffectPos;
        ParticleSystem particlesystem = PlayEffect.GetComponent<ParticleSystem>();
        var main = particlesystem.main;
        Destroy(PlayEffect, main.duration);
    }

    /// <summary>
    /// それぞれ関数がある方
    /// 引数:(ポジション)
    /// </summary>
    /// <param name="EffectPos"></param>
    public void PlayEffect_Firework(Vector2 EffectPos)
    {
        GameObject PlayEffect = Instantiate(Effect_Firework) as GameObject;
        PlayEffect.transform.position = EffectPos;
        ParticleSystem particlesystem = PlayEffect.GetComponent<ParticleSystem>();
        var main = particlesystem.main;
        Destroy(PlayEffect, main.duration);
    }

    /// <summary>
    /// それぞれ関数がある方
    /// 引数:(ポジション)
    /// </summary>
    /// <param name="EffectPos"></param>
    public void PlayEffect_Hit(Vector2 EffectPos)
    {
        GameObject PlayEffect = Instantiate(Effect_Hit) as GameObject;
        PlayEffect.transform.position = EffectPos;
        ParticleSystem particlesystem = PlayEffect.GetComponent<ParticleSystem>();
        var main = particlesystem.main;
        Destroy(PlayEffect, main.duration);
    }

    /// <summary>
    /// それぞれ関数がある方
    /// 引数:(ポジション)
    /// </summary>
    /// <param name="EffectPos"></param>
    public void PlayEffect_Magic(Vector2 EffectPos)
    {
        GameObject PlayEffect = Instantiate(Effect_Magic) as GameObject;
        PlayEffect.transform.position = EffectPos;
        ParticleSystem particlesystem = PlayEffect.GetComponent<ParticleSystem>();
        var main = particlesystem.main;
        Destroy(PlayEffect, main.duration);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
}
