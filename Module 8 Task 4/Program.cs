using System.Formats.Asn1;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    class Program
    {
        static void Main(string[] args)
        {
            try//Создаем папку
            {
                DirectoryInfo di = new DirectoryInfo("D:/Domeny/Desktop/Students");
                if (!di.Exists)
                {
                    di.Create();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e}");
            }
            //десериализация
            BinaryFormatter formatter = new BinaryFormatter();

            using (var fs = new FileStream("D:/Domeny/Downloads/Students.dat", FileMode.OpenOrCreate))
            {
                Student[] student = (Student[])formatter.Deserialize(fs);//var newStudent = (Student)formatter.Deserialize(fs);
                Console.WriteLine("Объект десериализован");

                for (int i = 0; i < student.Length; i++)
                {
                    //Console.WriteLine($"Имя: {student[i].Name} --- Группа: {student[i].Group}. DOB: {student[i].DateOfBirth.ToString("dd/MM/yyyy")}");
                    try//Создаем файл
                    {
                        var fileInfo = new FileInfo("D:/Domeny/Desktop/Students/" + student[i].Group + ".txt");
                        if (!fileInfo.Exists)
                        {
                            using (StreamWriter sw = fileInfo.CreateText())
                            {
                                sw.WriteLine($"{student[i].Name}, {student[i].DateOfBirth.ToString("dd/MM/yyyy")}");
                            }
                        }
                        else
                        {
                            bool exists = false;//проверка занесен ли студент уже в файл?
                            using (StreamReader sr = new StreamReader("D:/Domeny/Desktop/Students/" + student[i].Group + ".txt"))
                            {
                                string line;
                                while ((line = sr.ReadLine()) != null)
                                {
                                    if(line.Contains($"{student[i].Name}, {student[i].DateOfBirth.ToString("dd/MM/yyyy")}"))
                                    {
                                        exists = true;
                                    }
                                }
                            }
                            if (!exists)//если не занесен
                            {
                                using (StreamWriter sw = fileInfo.AppendText())
                                {
                                    sw.WriteLine($"{student[i].Name}, {student[i].DateOfBirth.ToString("dd/MM/yyyy")}");
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Ошибка: {e}");
                    }
                }
            }

            Console.ReadLine();
        }
    }
}