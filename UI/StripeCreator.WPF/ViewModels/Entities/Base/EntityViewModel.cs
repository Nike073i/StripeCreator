﻿using StripeCreator.Core.Models;
using System;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Базовый класс ViewModel доменной сущности
    /// </summary>
    /// <typeparam name="TEntity">Тип доменной сущности</typeparam>
    public abstract class EntityViewModel<TEntity> : IEntityViewModel where TEntity : Entity
    {
        #region Public properties

        /// <summary>
        /// Доменная модель сущности
        /// </summary>
        public TEntity Entity { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="entity"></param>
        public EntityViewModel(TEntity entity)
        {
            Entity = entity;
        }

        #endregion

        #region Interface implementations 

        public abstract EntityInfoViewModel GetData { get; }

        public Guid? GetEntityId() => Entity.Id;

        #endregion
    }
}
