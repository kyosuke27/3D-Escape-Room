using System.Dynamic;
using UnityEngine;

// Materialを変化させる
// objectに設定して、タップされた際にMaterialを変化させる
// TapObjectChangeとの違いはオブジェクト自身のMaterialを変更することでタップされたことを設定する
public class TapObjectChangeMaterial : TapColider
{
    public int Index { get; private set; }

    // 0: 未選択
    // 1: 選択済み
    public Material[] materials;

    public Renderer _renderer;

    protected override void OnTap()
    {
        base.OnTap();

        Index++;
        if (Index >= materials.Length)
        {
            Index = 0;
            _renderer.material = materials[Index];
        }
        else
        {
            _renderer.material = materials[Index];
        }

    }
    
    public void SetMaterialIndex(int index)
    {
        Index = index;
        _renderer.material = materials[Index];
    }
    
}
