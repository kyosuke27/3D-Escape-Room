using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountUpBlock : MonoBehaviour
{
    public bool isCheck;
    
    // チェックされた時と、チェックが外れた時の色を格納
    // 0: チェック外れ
    // 1: チェック済み
    public Material[] materials;
    
    // 自分自身のMaterial
    public Renderer _renderer;
    
    // void Start()
    // {
    //     _renderer = GetComponent<Renderer>();
    // }
    
    public void CheckBlock()
    {
        isCheck = !isCheck;
        _renderer.material = materials[1];
    }
    
    public void ClearBlock()
    {
        isCheck = false;
        _renderer.material = materials[0];
    }
}
