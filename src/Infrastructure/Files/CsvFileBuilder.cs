//using System.Globalization;
//using MyClothesCA.Application.Common.Interfaces;
//using MyClothesCA.Application.TodoLists.Queries.ExportTodos;
//using MyClothesCA.Infrastructure.Files.Maps;
//using CsvHelper;

//namespace MyClothesCA.Infrastructure.Files;

//public class CsvFileBuilder : ICsvFileBuilder
//{
//    public byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records)
//    {
//        using var memoryStream = new MemoryStream();
//        using (var streamWriter = new StreamWriter(memoryStream))
//        {
//            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

//            csvWriter.Configuration.RegisterClassMap<TodoItemRecordMap>();
//            csvWriter.WriteRecords(records);
//        }

//        return memoryStream.ToArray();
//    }
//}
