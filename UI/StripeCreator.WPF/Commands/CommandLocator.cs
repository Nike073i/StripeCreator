namespace StripeCreator.WPF
{
    /// <summary> Вспомогательный класс для получения команд </summary>
    public class CommandLocator
    {
        #region Команды для сущности нитей

        /// <summary> Команда получения всех нитей </summary>
        public static GetAllThreadsCommand GetAllThreadsCommand => IoC.GetRequiredService<GetAllThreadsCommand>();

        #endregion

        #region Команды для сущности тканей

        /// <summary> Команда получения всех тканей </summary>
        public static GetAllClothsCommand GetAllClothsCommand => IoC.GetRequiredService<GetAllClothsCommand>();

        #endregion

        #region Команды для сущности клиентов

        /// <summary> Команда получения всех клиентов </summary>
        public static GetAllClientsCommand GetAllClientsCommand => IoC.GetRequiredService<GetAllClientsCommand>();

        /// <summary> Команда создания клиента </summary>
        public static SaveClientCommand SaveClientCommand => IoC.GetRequiredService<SaveClientCommand>();

        #endregion

        #region Команды для сущности продукции

        /// <summary> Команда получения всех продукции </summary>
        public static GetAllProductsCommand GetAllProductsCommand => IoC.GetRequiredService<GetAllProductsCommand>();

        #endregion
    }
}
