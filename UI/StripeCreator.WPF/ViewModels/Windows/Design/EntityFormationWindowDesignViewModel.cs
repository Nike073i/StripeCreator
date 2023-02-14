﻿using StripeCreator.Business.Models;
using System;
using System.Windows.Controls;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model окна формирования сущности для design-mode
    /// </summary>
    public class EntityFormationWindowDesignViewModel : EntityFormationWindowViewModel
    {
        #region Singleton

        /// <summary>
        /// Экземпляр для показа в Design-mode
        /// </summary>
        public static EntityFormationWindowDesignViewModel Instance => new();

        #endregion

        #region Design Data 

        private static Client _client = new(new("Николай", "Иванов"), new("+79156300123", "example@mail.ru", "Вконтакте - vk.com/id536525252"));
        private static Control _designView = new ClientFormationView(new(_client));
        private static string _designTitle = "Формирование клиента";
        private static Action<object?> _emptyAction = (_) => { };

        #endregion

        #region Constructors 

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public EntityFormationWindowDesignViewModel() : base(_designView, _designTitle, _emptyAction, _emptyAction) { }

        #endregion
    }
}
