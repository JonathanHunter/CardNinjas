namespace Assets.Scripts.Util
{
    public class Enums
    {
        public enum Direction { Up, Down, Left, Right, None };
        public enum CardTypes { Sword, Kunai, Heal, Smoke, Error };
        public enum FieldType { Blue, Red };
        public enum PlayerState { Idle = 0, MoveBegining, MoveEnding, Hit, Dead, BasicAttack, Sword };
        public enum GameStates  { Battle, CardSelection };
    }
}
