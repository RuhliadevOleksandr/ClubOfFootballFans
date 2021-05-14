using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballFansLib
{
    public class Group
    {
        private int _numberOfMembers;
        private string _nameOfGroup;
        Person[] group;
        public Group(Person[] people, string nameOfGroup)
        {
            if (people != null)
            {
                _numberOfMembers = people.Length;
                for (int i = 0; i < _numberOfMembers; i++)
                {
                    group[i] = people[i];
                }
                if (nameOfGroup != null)
                    _nameOfGroup = nameOfGroup;
                else
                    _nameOfGroup = "NoName";
            }
        }
    }
}