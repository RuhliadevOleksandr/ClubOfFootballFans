using System;

namespace FootballFansLib
{
    public abstract class Person
    {
        public string Surname { get; private set; }
        public Person(string surname)
        {
            if (surname != null)
            {
                Surname = surname;
            }
            else
                throw new NullReferenceException("Person must have surname!");
        }
    }
}