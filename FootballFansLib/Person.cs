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
                bool isCorrectSurname = true;
                if (surname == "")
                    throw new ArgumentException("\nSurname must have letters!");
                else
                {
                    for (int i = 0; i < surname.Length; i++)
                        if (!Char.IsLetter(surname[i]))
                            isCorrectSurname = false;
                }
                if (isCorrectSurname)
                    Surname = surname;
                else
                    throw new ArgumentException("\nSurname must have only letters!");
            }
            else
                throw new NullReferenceException("\nPerson must have surname!");
        }
    }
}