using System;

namespace testNewAspectInjector.Services
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Date)}: {Date:O}";
        }
    }
}