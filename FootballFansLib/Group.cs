using System;

namespace FootballFansLib
{
    public class Group
    {
        private int _numberOfMembers;
        private string _nameOfGroup;
        public int NumberOfMembers
        {
            get
            {
                return _numberOfMembers;
            }
        }
        public string NameOfGroup
        {
            get
            {
                return _nameOfGroup; ;
            }
        }
        private Person[] group;
        public Group(Person[] people, string nameOfGroup)
        {
            if (people != null)
            {
                _numberOfMembers = people.Length;
                group = new Person[_numberOfMembers];
                for (int i = 0; i < _numberOfMembers; i++)
                {
                    group[i] = people[i];
                }
                if (nameOfGroup != null)
                    _nameOfGroup = nameOfGroup;
                else
                    throw new NullReferenceException("Group must have name!");
            }
            else
                throw new NullReferenceException("You can't create a group of people from nothing!");
        }
        public string[] GetSurnamesOfMember()
        {
            string[] surnames = new string[_numberOfMembers];
            for (int i = 0; i < _numberOfMembers; i++)
            {
                surnames[i] = group[i].Surname;
            }
            return surnames;
        }
        public void AddMember(Person person)
        {
            if (group != null)
            {
                bool isMember = false;
                for (int i = _numberOfMembers - 1; i >= 0; i--)
                    if (group[i] == person)
                        isMember = true;
                if (isMember)
                    throw new NullReferenceException($" {person.Surname} is a member! You can't add {person.Surname} to {NameOfGroup} again!");
                else
                {
                    Person[] oldGroup = new Person[_numberOfMembers];
                    for (int i = 0; i < _numberOfMembers; i++)
                    {
                        oldGroup[i] = group[i];
                    }
                    _numberOfMembers++;
                    group = new Person[_numberOfMembers];
                    for (int i = 0; i < _numberOfMembers - 1; i++)
                    {
                        group[i] = oldGroup[i];
                    }
                    group[_numberOfMembers - 1] = person;
                }
            }
            else
                throw new NullReferenceException("You can't create a group of people from nothing!");
        }
        public void RemoveMember(Person person)
        {
            if (group != null)
            {
                bool isMember = false;
                int indexOfPerson = 0;
                for (int i = _numberOfMembers - 1; i >= 0; i--)
                    if (group[i] == person)
                    {
                        isMember = true;
                        indexOfPerson = i;
                    }
                if (isMember)
                {
                    Person[] oldGroup = new Person[_numberOfMembers];
                    for (int j = 0; j < _numberOfMembers; j++)
                    {
                        oldGroup[j] = group[j];
                    }
                    _numberOfMembers--;
                    group = new Person[_numberOfMembers];
                    for (int j = 0; j < _numberOfMembers + 1; j++)
                    {
                        if (indexOfPerson > j)
                            group[j] = oldGroup[j];
                        if (indexOfPerson < j)
                            group[j - 1] = oldGroup[j];
                    }
                }
                else
                    throw new NullReferenceException($" {person.Surname} isn't a member! You can't remove {person.Surname} from {NameOfGroup}!");
            }
            else
                throw new NullReferenceException("You can't create a group of people from nothing!");
        }
    }
}