using Action = VillainSearcher.Enums.Action;

namespace VillainSearcher.ViewModels.Base.VM.Interfaces
{
    internal interface IInteractable
    {
        Action Action { get; set; }
    }
}
