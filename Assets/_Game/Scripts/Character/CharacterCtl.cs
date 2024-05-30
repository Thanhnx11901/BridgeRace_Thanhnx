using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCtl : GameUnit
{
    public EColor eColor;

    protected string currentAnimName;

    [SerializeField] private Animator anim;

    public Stack<BrickAdd> stackBricks = new Stack<BrickAdd>();

    [SerializeField] protected SkinnedMeshRenderer sMeshRenderer;

    protected Vector3 currentPosBrick;


    private void Awake()
    {
        currentPosBrick = new Vector3(0, 1.2f, -.6f);
    }

    // Update is called once per frame
   

    public void SetColor(EColor eColor)
    {
        this.eColor = eColor;
        sMeshRenderer.material = LevelManager.Instance.dataColor.GetColor(eColor);
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    protected void AddBrick()
    {
        BrickAdd SpawnBrickAdd = SimplePool.Spawn<BrickAdd>(PoolType.BrickAdd, transform);

        currentPosBrick.y += .3f;
        SpawnBrickAdd.SetColor(this.eColor);
        SpawnBrickAdd.TF.localPosition = currentPosBrick;
        SpawnBrickAdd.TF.localScale = new Vector3(.3f, .3f, .6f);
        stackBricks.Push(SpawnBrickAdd);
    }
    protected void RemoveBrick()
    {
        if (stackBricks.Count == 0) return;

        SimplePool.Despawn(stackBricks.Pop());
        currentPosBrick.y -= .3f;
    }
    public void ClearBrick()
    {
        if (stackBricks.Count == 0) return;

        while (stackBricks.Count > 0)
        {
            SimplePool.Despawn(stackBricks.Pop());
            currentPosBrick.y -= .3f;
        }
    }
}
