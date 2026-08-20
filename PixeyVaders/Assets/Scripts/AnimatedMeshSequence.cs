using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class AnimatedMeshSequence : MonoBehaviour
{
    public float FramesPerSecond = 12;
    public bool PlayAtStart = true;
    public Mesh[] Meshes;
    protected MeshFilter MyMeshFilter;
    protected int Position;
    
    public bool IsPlaying { get; private set; }

    void Awake()
    {
        MyMeshFilter = GetComponent<MeshFilter>();
        IsPlaying = false;

        if (!CheckIfReady()) return;
        MyMeshFilter.mesh = Meshes[0];

        if (PlayAtStart)
            Play();
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(IsPlaying)
                Stop();
            else
                Play();
        }
        */
    }

    public void Play()
    {
        if (!CheckIfReady()) return;

        if (!IsPlaying)
        {
            IsPlaying = true;
            StartCoroutine(AnimateMesh());
        }
    }

    public void Stop()
    {
        if (!CheckIfReady()) return;

        if (IsPlaying)
        {
            IsPlaying = false;
            StopCoroutine("AnimateMesh");
        }
    }

    IEnumerator AnimateMesh()
    {
        var waitTime = 1/FramesPerSecond;

        while (IsPlaying)
        {
            IncrementPlayhead();
            MyMeshFilter.mesh = Meshes[Position];
            yield return new WaitForSeconds(waitTime);
        }
    }

    void IncrementPlayhead()
    {
        Position++;
        if (Position > Meshes.Length - 1)
            Position = 0;
    }

    bool CheckIfReady()
    {
        if (Meshes.Length > 0)
            return true;
        Debug.LogError("No Meshes have been added to the AnimatedMeshSequence");
        return false;
    }
}
