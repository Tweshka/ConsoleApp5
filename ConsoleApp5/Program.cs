using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class StudentDataLoader
{
    public class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal AverageGrade { get; set; }
    }

    public static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Необходимо указать путь к бинарному файлу в качестве аргумента.");
            return;
        }

        string binaryFilePath = args[0];

        // 1. Читаем данные о студентах из бинарного файла
        List<Student> students = ReadStudentsFromBinary(binaryFilePath);

        // 2. Создаем директорию Students на рабочем столе
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string studentsDirectory = Path.Combine(desktopPath, "Students");
        Directory.CreateDirectory(studentsDirectory);

        // 3. Раскидываем студентов по группам
        foreach (var group in students.Select(s => s.Group).Distinct())
        {
            string groupFilePath = Path.Combine(studentsDirectory, $"{group}.txt");

            // Собираем студентов из этой группы
            List<Student> groupStudents = students.Where(s => s.Group == group).ToList();

            // Записываем студентов в файл
            using (StreamWriter writer = new StreamWriter(groupFilePath, false, Encoding.UTF8))
            {
                foreach (var student in groupStudents)
                {
                    writer.WriteLine($"{student.Name},{student.DateOfBirth:yyyy-MM-dd},{student.AverageGrade}");
                }
            }

            Console.WriteLine($"Файл для группы {group} создан: {groupFilePath}");
        }

        Console.WriteLine("Данные успешно загружены и рассортированы по группам.");
    }

    // Метод для чтения данных о студентах из бинарного файла
    // (Вам нужно реализовать этот метод, исходя из формата бинарного файла)
    private static List<Student> ReadStudentsFromBinary(string binaryFilePath)
    {
        List<Student> students = new List<Student>();

        using (BinaryReader reader = new BinaryReader(File.Open(binaryFilePath, FileMode.Open)))
        {
            // Читаем количество студентов
            int studentCount = reader.ReadInt32();

            // Читаем информацию о каждом студенте
            for (int i = 0; i < studentCount; i++)
            {
                Student student = new Student
                {
                    Name = reader.ReadString(),
                    Group = reader.ReadString(),
                    DateOfBirth = DateTime.FromBinary(reader.ReadInt64()),
                    AverageGrade = reader.ReadDecimal()
                };

                students.Add(student);
            }
        }

        return students;
    }
}