namespace StackDo.Interface
{
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
