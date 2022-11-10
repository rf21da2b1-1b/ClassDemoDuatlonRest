using ClassDemoDuatlonLib.model;

namespace ClassDemoDuatlonRest.managers
{
    public class DuathletesManager
    {
        /*
         * Statisk liste
         */
        private readonly static List<Duathlete> _duathletes = new List<Duathlete>()
        {
            new Duathlete("Eddy Quick Feet", 4, 100, 7200, 2100),
            new Duathlete("Heavy Peter", 3, 101, 7520, 2190),
            new Duathlete("Big Mike", 2, 102, 7350, 2390),
            new Duathlete("Fat Joey", 4, 103, 8256, 2676),
            new Duathlete("Magic Thomson", 1, 104, 6475, 2050)
        };

        // Til auto generering af Bib numre
        private static int nextBib = 105;

        /*
         * 4 metoder
         */
        public List<Duathlete> GetAll()
        {
            return new List<Duathlete>(_duathletes);
        }
        public Duathlete GetByBib(int bib)
        {
            Duathlete? dua = _duathletes.Find(d => d.Bib == bib);
            if (dua is null)
            {
                throw new KeyNotFoundException();
            }
            return dua;
        }
        public Duathlete Add(Duathlete dua)
        {
            // todo bib autogenereret
            dua.Bib = nextBib;
            nextBib++;

            _duathletes.Add(dua);
            return dua;
        }
        public Duathlete Delete(int bib)
        {
            Duathlete dua = GetByBib(bib);
            _duathletes.Remove(dua);
            return dua;
        }


        /*
         * Opgave 7
         */
        public Duathlete Add(int age, Duathlete dua)
        {
            dua.AgeGroup = ConvertAgeToGroup(age);
            dua.Validate();
            return Add(dua);
        }

        public List<Duathlete> GetAllByAge(int group)
        {
            return _duathletes.FindAll( d => d.AgeGroup == group);
        }

        private int ConvertAgeToGroup(int age)
        {
            if (age < 25)
            {
                return 1;
            }

            if (age <= 35)
            {
                return 2;
            }

            if (age <= 45)
            {
                return 3;
            }

            return 4;
            


        }
    }
}
