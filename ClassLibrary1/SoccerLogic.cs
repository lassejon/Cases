namespace Soccer
{
    public class SoccerLogic
    {
        public SoccerLogic()
        {

        }

        public string Chant(int passes, string goal)
        {

            if(goal.ToLower().Equals("mål"))
            {
                return "Olé olé olé";   
            }

            if (passes >= 10)
            {
                return "High Five - Jubel!!!";
            }

            if (passes < 1)
            {
                return "Shh";
            }

            var chant = "";
            for (int i = 0; i < passes; i++)
            {
                chant += "Huh! ";
            }

            return chant;
        }
    }
}