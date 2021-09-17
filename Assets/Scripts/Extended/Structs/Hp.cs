public struct Hp
{
    public enum typeOfUnit
    {
        player = 0, UFO

    }
    public static byte Set(typeOfUnit Enum)
    {
        switch (Enum)
        {
            case typeOfUnit.player:
                return 5;

            case typeOfUnit.UFO:
                return 1;

            default:
                return 1;


        }
    }


}
