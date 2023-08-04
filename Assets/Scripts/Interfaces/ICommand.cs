namespace Interfaces
{
    public interface ICommand
    {
        
    }
    
    public interface IReactCommand<T> where T : struct, ICommand
    {
        void CommandReact(T command);
    }
}