using Interfaces;
using UnityEngine.InputSystem;

namespace Data
{
    ///<summary>
    ///Обозначает нажатие на кнопку (или что-то другое в зависимости от конфигурации InputAction
    /// </summary>
    public struct InputStartedCommand : ICommand
    {
        public int Index;
        public InputAction.CallbackContext Context { get; set; }
    }

    ///<summary>
    ///Обозначает отпускание кнопки (или что-то другое в зависимости от конфигурации InputAction
    /// </summary>
    public struct InputEndedCommand: ICommand
    {
        public int Index;
        public InputAction.CallbackContext Context { get; set; }
    }

    ///<summary>
    ///Обозначает удерживание кнопки (или что-то другое в зависимости от конфигурации InputAction
    /// </summary>
    public struct InputCommand: ICommand
    {
        public int Index;
        public InputAction.CallbackContext Context { get; set; }
    }
}