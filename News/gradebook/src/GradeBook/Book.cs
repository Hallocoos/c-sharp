using System;
using System.Collections.Generic;

namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public class Book
    {
        private List<double> grades;
        private string name;
        public const string CATEGORY = "Science";

        public string Name
        {
            get;
            set;
        }

        public Book(string name)
        {
            this.grades = new List<double>();
            this.Name = name;
        }

        public void AddGrade(double grade)
        {
            if (grade <= 100 && grade >= 0)
            {
                this.grades.Add(grade);

                if (GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
        }

        public event GradeAddedDelegate GradeAdded;

        public void AddGrade(char letter)
        {
            switch (letter)
            {
                case 'A':
                    AddGrade(90);
                    break;
                case 'B':
                    AddGrade(80);
                    break;
                case 'C':
                    AddGrade(70);
                    break;
                case 'D':
                    AddGrade(60);
                    break;
                case 'F':
                    AddGrade(50);
                    break;
                default:
                    AddGrade(0);
                    break;
            }
        }

        public Statistics GetStatistics()
        {
            var result = new Statistics();
            result.High = double.MinValue;
            result.Average = 0.0;
            result.Low = double.MaxValue;

            for (var index = 0; index < grades.Count; index += 1)
            {
                result.Low = Math.Min(grades[index], result.Low);
                result.High = Math.Max(grades[index], result.High);
                result.Average += grades[index];
            }
            result.Average /= grades.Count;

            switch (result.Average)
            {
                case var d when d >= 90:
                    result.letter = 'A';
                    break;
                case var d when d >= 80:
                    result.letter = 'B';
                    break;
                case var d when d >= 70:
                    result.letter = 'C';
                    break;
                case var d when d >= 60:
                    result.letter = 'D';
                    break;
                case var d when d >= 50:
                    result.letter = 'F';
                    break;
            }
            return (result);
        }
    }
}