namespace StripeCreator.Core.Extensions
{
    /// <summary>
    /// Расширения для массивов <see cref="Array"/>
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Преобразование многомерного массива в <see cref="IEnumerable{T}"/> 
        /// </summary>
        /// <typeparam name="T">Тип элементов многомерного массива</typeparam>
        /// <param name="target">Многомерный массив</param>
        /// <returns>Последовательность элементов многомерного массива</returns>
        public static IEnumerable<T> ToEnumerable<T>(this T[,] target)
        {
            foreach (var item in target)
                yield return item;
        }
    }
}