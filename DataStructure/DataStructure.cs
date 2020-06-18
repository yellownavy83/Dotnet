using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace DataStructure
{
    public class DataStructure
    {
        static List<Student> studentList = new List<Student>();
        static void Main(string[] args)
        {
            studentList.Add(new Student("Tom", 17, 33.2, false)); 
            studentList.Add(new Student("Sara", 19, 53.4, true)); 
            studentList.Add(new Student("홍길동", 15, 130.2, false)); 
            studentList.Add(new Student("이순신", 16, 43.2, true)); 
            studentList.Add(new Student("Ace", 15, 23.2, false)); 
            Console.WriteLine(Environment.NewLine + "-------------- 원본 데이터 --------------------"); 
            foreach (var student in studentList) 
            { 
                Console.WriteLine(student); 
            }

            Console.WriteLine(Environment.NewLine + "-------------- Age 순 오름차순 정렬하기 --------------------"); 
            List<Student> SortedList = studentList.OrderBy(x => x.Age).ToList(); 
            foreach (var student in SortedList) 
            { 
                Console.WriteLine(student); 
            
            }
            Console.WriteLine(Environment.NewLine + "------------- Name 순 내림차순 정렬하기 --------------------"); 
            SortedList = studentList.OrderByDescending(x => x.Name).ToList(); 
            foreach (var student in SortedList) 
            { 
                Console.WriteLine(student); 
            }

            Console.WriteLine(Environment.NewLine + "----------- Weight 오름차순, 반환값없는 자체 정렬하기 -----------"); 
            studentList.Sort((x1, x2) => x1.Weight.CompareTo(x2.Weight)); 
            foreach (var student in studentList) 
            { 
                Console.WriteLine(student); 
            }

            Console.WriteLine(Environment.NewLine + "----------- Weight 내림차순, 반환값없는 자체 정렬하기 -----"); 
            studentList.Sort((x1, x2) => x2.Weight.CompareTo(x1.Weight)); 
            foreach (var student in studentList) 
            { 
                Console.WriteLine(student); 
            }

            Console.WriteLine(Environment.NewLine + "----------- Age순 이후, Weight 순으로 정렬하기 -----"); 
            SortedList = studentList.OrderBy(x => x.Age).ThenBy(x => x.Weight).ToList(); 
            foreach (var student in SortedList) 
            { 
                Console.WriteLine(student); 
            }

            Console.WriteLine(Environment.NewLine + "----------- Age순 이후, Weight, name 순으로 정렬하기 -----");
            SortedList = studentList.OrderBy(x => x.Age).ThenBy(x => x.Weight).ThenBy(x => x.Name).ToList();
            foreach (var student in SortedList)
            {
                Console.WriteLine(student);
            }

            Console.Read();

        }
    }

    class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public bool Glass { get; set; } = false; // 안경끼면, true 
        public Student(string name, int age, double weight, bool glass) 
        { 
            this.Name = name; 
            this.Age = age; 
            this.Weight = weight; 
            this.Glass = glass; 
        } 
        public override string ToString() 
        { 
            return $"-- Name={Name},\t Age={Age}, 몸무게={Weight}, \t 안경착용={Glass}--"; 
        } 
    }

}
