using Andrehard.CharacterStats; //IMPORTANT oublie pas de mettre ca dans tous tes items
public class item
{
    /*   
     *   Première méthode pour écrire tes items
     *   
                                                                            //We need to store our modifiers in variables before adding them to the stat.
        StatModifier mod1, mod2;
        public void Equip(Character c)
        {
            mod1 = new StatModifier(10, StatModType.Flat);
            mod2 = new StatModifier(0.1f, StatModType.PercentMult);
            c.Strength.AddModifier(mod1);
            c.Strength.AddModifier(mod2);
        }
                                                                            // Here we need to use the stored modifiers in order to remove them.
                                                                            // Otherwise they would be "lost" in the stat forever.
        public void Unequip(Character c)
        {
            c.Strength.RemoveModifier(mod1);
            c.Strength.RemoveModifier(mod2);
        }
    */


    //Seconde méthode en utilisant sources (elle est bien plus simple)


    public void Equip(Character c)
    {
        c.Strength.AddModifier(new StatModifier(10, StatModType.Flat, this));
        c.Strength.AddModifier(new StatModifier(0.1f, StatModType.PercentMult, this));
    }
    public void Unequip(Character c)
    {
        c.Strength.RemoveAllModifiersFromSource(this);
    }
}