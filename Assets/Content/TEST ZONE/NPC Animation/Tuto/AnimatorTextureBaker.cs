using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AnimatorTextureBaker : MonoBehaviour
{
    public ComputeShader infoTextGen;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BakeAnimation());
    }

    IEnumerator BakeAnimation()
    {
        Animator animator = GetComponent<Animator>();
        var clips = animator.runtimeAnimatorController.animationClips;
        var skin = GetComponentInChildren<SkinnedMeshRenderer>();
        var vCount = skin.sharedMesh.vertexCount;

        var mesh = new Mesh();
        animator.speed = 0f;
        var textWidth = Mathf.NextPowerOfTwo(vCount);
        foreach (var c in clips)
        {
            var frames = Mathf.NextPowerOfTwo((int)(c.length / 0.05f));
            var info = new List<VertInfo>();

            var positionRenderTexture = new RenderTexture(textWidth, frames, 0, RenderTextureFormat.ARGBHalf);
            var normalRenderTexture = new RenderTexture(textWidth, frames, 0, RenderTextureFormat.ARGBHalf);

            positionRenderTexture.name = string.Format("{0}.{1}posText", name, c.name);
            normalRenderTexture.name = string.Format("{0}.{1}normText", name, c.name);

            foreach (var renderTarget in new[] { positionRenderTexture, normalRenderTexture })
            {
                renderTarget.enableRandomWrite = true;
                renderTarget.Create();
                RenderTexture.active = renderTarget;
                GL.Clear(true, true, Color.clear);
            }

            animator.Play(c.name);
            yield return 0;
            for (var i = 0; i < frames; ++i)
            {
                animator.Play(c.name, 0, (float)i / frames);
                yield return 0;
                skin.BakeMesh(mesh);
                info.AddRange(Enumerable.Range(0, vCount).Select(idx => new VertInfo()
                {
                    possition = mesh.vertices[idx],
                    normal = mesh.normals[idx]
                }));
            }

            var buffer = new ComputeBuffer(info.Count, System.Runtime.InteropServices.Marshal.SizeOf(typeof(VertInfo)));
            buffer.SetData(info);

            var kernel = infoTextGen.FindKernel("CSMain");
            uint x, y, z;
            infoTextGen.GetKernelThreadGroupSizes(kernel, out x, out y, out z);

            infoTextGen.SetInt("VertCount", vCount);
            infoTextGen.SetBuffer(kernel, "meshInfo", buffer);
            infoTextGen.SetTexture(kernel, "OutPosition", positionRenderTexture);
            infoTextGen.SetTexture(kernel, "OutNormal", normalRenderTexture);

            infoTextGen.Dispatch(kernel, vCount / (int)x + 1, frames / (int)y + 1, frames / (int)z);
            buffer.Release();

#if UNITY_EDITOR

            var positiontexture = Convert(positionRenderTexture);
            var normaltexture = Convert(normalRenderTexture);

            Graphics.CopyTexture(positionRenderTexture, positiontexture);
            Graphics.CopyTexture(normalRenderTexture, normaltexture);

            Debug.Log(positionRenderTexture.name);


            string directoryPath = "Assets/Content/TEST ZONE/Baked";



            Debug.Log("valid");

            AssetDatabase.CreateAsset(positiontexture, Path.Combine(directoryPath, positionRenderTexture.name + ".asset"));
            AssetDatabase.CreateAsset(normaltexture, Path.Combine(directoryPath, normalRenderTexture.name + ".asset"));

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();


#endif

        }
        yield return null;
    }


    public Texture2D Convert(RenderTexture renderTexture)
    {
        var texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBAHalf, false);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(Rect.MinMaxRect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        RenderTexture.active = null;
        return texture;
    }

    public struct VertInfo
    {
        public Vector3 possition;
        public Vector3 normal;
    }
}
