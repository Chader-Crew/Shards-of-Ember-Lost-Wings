using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//estrutura de dados para guardar informacoes contextuais e passa-las pelas logicas de efeito de skills e acerto.
public class SkillData
{
    public Character owner;     //personagem que iniciou ou criou a skill
    public float damage;        //dano a ser dado em alvos
    public float stagger;
    public List<Character> targets;     //lista de alvos

    public SkillData Damage(float damage)
    {
        this.damage = damage;
        return this;
    }

    public SkillData Target(Character target)
    {
        targets = new List<Character>
        {
            target
        };
        return this;
    }

    public SkillData Owner(Character owner)
    {
        this.owner = owner;
        return this;
    }
}
