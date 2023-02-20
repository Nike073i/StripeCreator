using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;

namespace StripeCreator.Statistic.Extensions
{
    /// <summary>
    /// Расширения для элементов <see cref="DocumentObject"/> формирования документа <see cref="Document"/>
    /// </summary>
    public static class PdfSharpExtensions
    {
        /// <summary>
        /// Добавление колонок в таблицу
        /// </summary>
        /// <param name="table">Таблица</param>
        /// <param name="widths">Размеры колонок</param>
        public static void AddColumns(this Table table, IEnumerable<double> widths)
        {
            foreach (var width in widths)
            {
                var unit = Unit.FromCentimeter(width);
                table.AddColumn(unit);
            }
        }

        /// <summary>
        /// Создание строки в таблице
        /// </summary>
        /// <param name="table">Таблица</param>
        /// <param name="data">Данные для вставки</param>
        /// <param name="headerRow">Формат строки заголовка</param>
        /// <returns>Строка таблицы</returns>
        public static Row CreateRow(this Table table, IEnumerable<string> data, bool headerRow = false)
        {
            var row = new Row();
            int index = 0;
            foreach (var item in data)
            {
                row.Cells[index].AddParagraph(item);
                row.Cells[index].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[index].Format.Font.Bold = headerRow;
                index++;
            }
            table.Rows.Add(row);
            return row;
        }

        /// <summary>
        /// Добавить отступ
        /// </summary>
        /// <param name="section">Секция документа</param>
        /// <param name="height">Высота отступа в см</param>
        public static void AddIndent(this Section section, double height)
        {
            var p = section.AddParagraph();
            p.Format.SpaceAfter = Unit.FromCentimeter(height);
        }
    }
}
