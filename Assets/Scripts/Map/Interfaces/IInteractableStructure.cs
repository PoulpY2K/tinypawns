using Interfaces;

namespace Map.Interfaces
{
    public interface IInteractableStructure
    {
        public bool Activated { set; get; }
        public bool Destroyed { set; get; }
    }
}