namespace Core.UI
{
    public interface IDialogManager
    {
        T ShowDialog<T>() where T : DialogBase;
        
        /// <summary>
        /// Показываем сюжетный диалог
        /// Отличие от других диалогов, что в сюжетном мы отключаем остальные диалоги
        /// </summary>
        T ShowStoryDialog<T>() where T : DialogBase;
    }
}