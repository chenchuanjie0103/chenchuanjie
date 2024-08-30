using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWorks : MonoBehaviour
{
    GameObject firesworks;
    ParticleSystem ps;
    void Start()
    {
        firesworks = GameObject.Find("Particle System");
        ps = firesworks.GetComponent<ParticleSystem>();
    }
    void Update()
    {
    }
    void OnMouseUp()
    {
        //firesworks.SetActive(!firesworks.active);
        if (ps.isPlaying)
        {
            ps.Pause();
        }
        else
        {
            ps.Play();
        }
    }




    //private void Awake()
    //{
    //    Debug.Log("Awake!");
    //}
    //public void OnEnable()
    //{
    //    Debug.Log("OnEnable!");
    //}
    //void Start()
    //{
    //    Debug.Log("Start!");
    //}
    //private void FixedUpdate()
    //{
    //    Debug.Log("FixedUpdate!");
    //}
    //void Update()
    //{
    //    Debug.Log("Update!");
    //}
    //private void LateUpdate()
    //{
    //    Debug.Log("LateUpdate!");
    //}
    //private void OnWillRenderObject()
    //{
    //    Debug.Log("OnWillRenderObject!");
    //}
    //private void OnPreCull()
    //{
    //    Debug.Log("OnPreCull!");
    //}
    //private void OnBecameVisible()
    //{
    //    Debug.Log("OnBecameVisible!");
    //}
    //private void OnBecameInvisible()
    //{
    //    Debug.Log("OnBecameInvisible!");
    //}
    //private void OnPreRender()
    //{
    //    Debug.Log("OnPreRender!");
    //}

    //private void OnRenderObject()
    //{
    //    Debug.Log("OnRenderObject!");
    //}
    //private void OnPostRender()
    //{
    //    Debug.Log("OnPostRender!");
    //}
    //private void OnRenderImage(RenderTexture source, RenderTexture destination)
    //{
    //    Debug.Log("OnRenderImage!");
    //}
    //private void OnDrawGizmos()
    //{
    //    Debug.Log("OnDrawGizmos!");
    //}
    //private void OnGUI()
    //{
    //    Debug.Log("OnGUI!");
    //}
    //private void OnApplicationPause(bool pause)
    //{
    //    Debug.Log("OnApplicationPause!");
    //    if (pause)
    //    {
    //        Debug.Log("Pause!");
    //    }
    //    else
    //    {
    //        Debug.Log("Resume!");
    //    }
    //}
    //private void OnApplicationFocus(bool focus)
    //{
    //    Debug.Log("OnApplicationFocus!");
    //    if (focus)
    //    {
    //        Debug.Log("Focus!");
    //    }
    //    else
    //    {
    //        Debug.Log("Unfocus!");
    //    }

    //}
    //private void OnDisable()
    //{
    //    Debug.Log("OnDisable!");
    //}
    //private void OnDestroy()
    //{
    //    Debug.Log("OnDestroy!");
    //}
    //private void OnApplicationQuit()
    //{
    //    Debug.Log("OnApplicationQuit!");
    //}
}
