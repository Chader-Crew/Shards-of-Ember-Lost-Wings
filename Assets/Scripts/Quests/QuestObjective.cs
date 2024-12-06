using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestObjective : QuestData
{
    //public QuestParent[] parents; //quantos objetos compartilham da mesma quest

    public abstract void Objective();
}
