using System.Collections.Generic;

namespace Naumen
{
    public interface ISearcher
    {
        /// <summary>
        /// Обновляет внутренние структуры данных для последующего быстрого поиска
        /// </summary>
        /// <param name="classNames">Имена классов в проекте</param>
        /// <param name="modificationDates">Дата модификации класса в миллисекундах, прошедших с 1 января 1970 года</param>
        void Refresh(IEnumerable<string> classNames, IEnumerable<long> modificationDates);

        /// <summary>
        /// Ищет подходящие имена классов
        /// Название должно начинаться со start
        /// </summary>
        /// <param name="start">Начало имени класса</param>
        /// <returns>Список подходящих классов, упорядоченных по дате изменения</returns>
        IEnumerable<string> Guess(string start);
    }
}
