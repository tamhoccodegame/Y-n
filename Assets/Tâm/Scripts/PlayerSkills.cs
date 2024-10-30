using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills
{
    public enum SkillType
    {

    };

    public SkillType skillType;
    private int skillPoint;

    List<SkillType> unlockedSkillList;

    public PlayerSkills()
    {
        skillPoint = 0;
        unlockedSkillList = new List<SkillType>();
    }

    public void AddSkillPoint()
    {
        skillPoint++;
    }

    public int GetSkillPoints()
    {
        return skillPoint;
    }

    private void UnlockSkill(SkillType skillType)
    {
       
    }

    private bool IsSkillUnlocked(SkillType skillType)
    {
        return unlockedSkillList.Contains(skillType);
    }

    public bool TryUnlockSkill(SkillType skillType)
    {
        if (!IsSkillUnlocked(skillType))
        {
            skillPoint--;
            UnlockSkill(skillType);
            return true;
        }
        else
        {
            return false;
        }
    }

    

}
