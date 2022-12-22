using UnityEngine;

public class sleep_screen : PauseMenu
{
    public void Sleep()
    {
        player.Sleep();
        Close();
    }
}
