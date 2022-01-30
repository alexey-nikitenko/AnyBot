namespace BotStarter
{
    public class Constants
    {
        public static string solutiondir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        public const string angleValuesFile = "angleValues.json";
        public const string coordinatesFile = "coordinates.json";
        public const string parametersFile = "parameters.json";
        public const string images = "images";
        public const int viceVersa = 4;

        public const string leftMouseKeyMotor = "leftMouseKeyMotor";
        public const string rightMouseKeyMotor = "rightMouseKeyMotor";
        public const string leftClickInitialPosition = "leftClickInitialPosition";
        public const string leftClickGoalPosition = "leftClickGoalPosition";
        public const string rightClickInitialPosition = "rightClickInitialPosition";
        public const string rightClickGoalPosition = "rightClickGoalPosition";

        public const string heal = "Doctor give me a heal";
        public const string buff = "Doctor give me a buff";
        public const string follow = "Doctor follow me";
        public const string stop = "Doctor stop";
        public const string keep = "Doctor keep healing";
        public const string bot = "Doctor run bot";

        public const string manaDir = "Mana";
        public const string manaPot = "manaPot";
        public const string healDir = "Heal";
        public const string monsterDir = "Monster";
        public const string squadDir = "Squad";
        public const string followDir = "Follow";
        public const string skillsDir = "Skills";

        public const string clericHeal = "clericHeal";
        
        public const string manaLevel = "manaLevel.png";
        public const string squad = "squad.png";
        public const string followFile = "follow.png";
        public const string caseFile = "case.png";

    }
}
