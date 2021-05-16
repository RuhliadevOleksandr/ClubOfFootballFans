using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballFansLib
{
    public abstract class Person
    {
        private string _surname;
        public string Surname
        { 
            get 
            {
                return _surname;
            } 
        }
        public Person(string surname)
        {
            if (surname != null)
            {
                _surname = surname;
            }
            else
                throw new Exception("");
        }
    }
}