using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFormChange : MonoBehaviour
{
    [Serializable]
    struct ClassAnim
    {
        public AnimationClip attack;
        public AnimationClip idle;
        public AnimationClip move;
    }
    [SerializeField] ClassAnim[] classAnims;
    [SerializeField] AnimatorOverrideController over;

    public void FormChange(PlayerClassEnum pClass)
    {
        over["Attack"] = classAnims[(int)pClass].attack;
        over["Idle"] = classAnims[(int)pClass].idle;
        over["Move"] = classAnims[(int)pClass].move;
    }
}
