namespace StackDo.Interface
{
    /// <summary>
    /// Interface for displaying todo containers.
    /// </summary>
    interface ITodoContainerDisplay
    {
        /// <summary>
        /// Display the container and its todo.
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        string Display(ITodoContainer container);
    }
}
