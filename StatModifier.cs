namespace Andrehard.CharacterStats
{
    public enum StatModType
    {
        Flat = 100,         // ex : attaque : 10 + 10 = 20
        PercentAdd = 200,   // ex : +10% + 10%(1.1 + 1.1) = 1.2     = 20% de bonus
        PercentMult = 300,  // ex : +10% * 10%(1.1 * 1.1) = 1.21    = 21% de bonus ( dans le cas d'un talent de personnage cela pourrait faire l'affaire )
    }
    public class StatModifier
    {
        public readonly float Value;        //sa valeur
        public readonly StatModType Type;   //son type (Flat, PercentAdd, PercentMult)
        public readonly int Order;          //on peut modifier l'ordre de ces types, à la base, Flat sera en premier, ensuite PercentAdd en second et PercenMult en 3e
        /*
         * l'odre est important car :
         * base     : 10
         * Item     : +20
         * Talent   : +10%
         * 
         * Sur cet exemple si on décide de prendre le talent avant l'item, le bonus ne sera activé que sur la base : 10 * 1.1 + 20 = 31
         * Grace à l'ordre, le calcul sera donc                                                                      :(10+20) * 1.1  = 33
         */
        public readonly object Source;      //l'objet qui agit sur ces modifications (this)
        /* Constructeur StatModifier
         * 
         * Obligatoire :
         *      valeur
         *      type
         * Optionnels :
         *      order
         *      source
         */
        public StatModifier(float value, StatModType type, int order, object source)
        {
            Value = value;
            Type = type;
            Order = order;
            Source = source;
        }

        public StatModifier(float value, StatModType type) : this(value, type, (int)type, null) { }                     //constructeur si valeur et type 
        public StatModifier(float value, StatModType type, int order) : this(value, type, order, null) { }              //constructeur si valeur, type et ordre
        public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }    //constructeur si valeur, type et source
    }
}