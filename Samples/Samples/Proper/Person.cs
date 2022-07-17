using System;
using System.Collections.Generic;
using System.Linq;

namespace Samples.Proper
{
    public class Person
    {
        private readonly List<Person> _children;

        public Person(string name, DateOnly birthDate)
        {
            _children = new List<Person>();
            Name = name;
            BirthDate = birthDate;
        }

        public string Name { get; }
        public DateOnly BirthDate { get; }
        public IReadOnlyCollection<Person> Children => _children;

        public Person? FindEldestChild()
        {
            // If _children in empty - returns null
            return _children
                .OrderByDescending(p => p.BirthDate)
                .SingleOrDefault();
        }
    }
}