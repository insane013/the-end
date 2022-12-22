using UnityEngine;

public class story_sheet : ActiveTarget
{
    public string title;
    [TextArea]
    public string description;

    private hub HUB;
    private StoryWindow stWindow;

    private void Start()
    {
        HUB = GameObject.FindGameObjectWithTag("HUB").GetComponent<hub>();
        stWindow = HUB.storyWindow;
        if (stWindow == null)
        {
            print("Stiry window not found!");
        }
    }

    public override void Activate()
    {
        stWindow.Activate(title, description);
    }
}
