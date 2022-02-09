using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace Andrehard.CharacterStats
{
    [Serializable]
    public class CharacterStat
    {
        public float BaseValue; // C'est la valeur par defaut qui pourra donc etre modifié
        public virtual float Value 
        {
            get
            {
                if (isDirty || BaseValue != lastBaseValue)
                {
                    lastBaseValue = BaseValue;
                    _value = CalculateFinalValue();
                    isDirty = false;
                }
                return _value;
            } 
        }


        protected bool isDirty = true;  // sert à appeler la fonction value que lorsqu'une modification est nécessaire
        protected float _value;         // la valeur plus récente qui a été changée
        protected float lastBaseValue = float.MinValue; // sert à forcer un calcul si on modifier "BaseValue" car "BaseValue" est public

        /*
         * le "readonly" permet de protéger la list, on ne peut pas la modifier en tant que null ou une nouvelle list dans une fonction
         * mais on peut agir sur son contenu ([0][1][2]).
         * on peut la modifier uniquement lorsqu'on est dans son constructeur ou dans sa déclaration
        */
        protected readonly List<StatModifier> statModifiers;
        public readonly ReadOnlyCollection<StatModifier> StatModifiers; //elle sert a rendre public "statModifiers" sans risque c'est un pointeur en gros

        public CharacterStat()
        {
            statModifiers = new List<StatModifier>();
            StatModifiers = statModifiers.AsReadOnly();
        }

        public CharacterStat(float baseValue) : this()
        {
            BaseValue = baseValue;
            statModifiers = new List<StatModifier>();
            StatModifiers = statModifiers.AsReadOnly();
        }

        public virtual void AddModifier(StatModifier mod) //Permets d'ajouter des valeurs dans notre liste "statModifiers"
        {
            isDirty = true;
            statModifiers.Add(mod);
            statModifiers.Sort(CompareModifierOrder);
        }

        public virtual bool RemoveModifier(StatModifier mod) //Permets de retirer des valeurs dans notre liste "statModifiers"
        {
            if (statModifiers.Remove(mod))
            {
                isDirty = true;
                return true;
            }
            return false;
        }
        public virtual bool RemoveAllModifiersFromSource(object source) //Retire tous les objets de la source purt banger celle la
        {
            bool didRemove = false;

            for (int i = statModifiers.Count - 1; i >= 0; i--)
            {
                if (statModifiers[i].Source == source)
                {
                    isDirty = true;
                    didRemove = true;
                    statModifiers.RemoveAt(i);
                }
            }
            return didRemove;
        }
        protected virtual int CompareModifierOrder(StatModifier a, StatModifier b) //Compare 2 objets avec ses 3 possibilités pour pouvoir agir dans le bon ordre
        {
            if (a.Order < b.Order)
                return -1;
            else if (a.Order > b.Order)
                return 1;
            return 0; // if (a.Order == b.Order)
        }

        protected virtual float CalculateFinalValue() //Permet de calculer les modifications selon le type
        {
            float finalValue = BaseValue;
            float sumPercentAdd = 0;

            for (int i = 0; i < statModifiers.Count; i++)
            {
                StatModifier mod = statModifiers[i];

                if (mod.Type == StatModType.Flat)
                {
                    finalValue += mod.Value;
                }
                else if (mod.Type == StatModType.PercentAdd)
                {
                    sumPercentAdd += mod.Value;
                    if(i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
                    {
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                }
                else if (mod.Type == StatModType.PercentMult)
                {
                    finalValue *= 1 + mod.Value;
                }
            }

            // 12.0001F != 12
            return (float)Math.Round(finalValue, 4);
        }
    }
}