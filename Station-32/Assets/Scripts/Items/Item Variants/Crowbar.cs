
public class Crowbar : Item
{
    public override void Use()
    {
        CrowbarInteractable crowbarInteractable = PlayerCamera.GetComponentRaycast<CrowbarInteractable>();

        if (crowbarInteractable != null)
            crowbarInteractable.Interact();


    }
    protected override void DisableItem()
    {

    }
}
