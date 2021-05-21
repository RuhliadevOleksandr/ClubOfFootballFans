using System;

namespace FootballFansLib
{
    public class Group
    {
        public int NumberOfMembers { get; private set; }
        public string NameOfGroup { get; private set; }
        private Person[] group;
        public Group(Person[] people, string nameOfGroup)
        {
            if (people != null)
            {
                NumberOfMembers = people.Length;
                group = new Person[NumberOfMembers];
                for (int i = 0; i < NumberOfMembers; i++)
                {
                    group[i] = people[i];
                }
                if (nameOfGroup != null)
                    NameOfGroup = nameOfGroup;
                else
                    throw new NullReferenceException("Group must have name!");
            }
            else
                throw new NullReferenceException("You can't create a group of people from nothing!");
        }
        public string[] GetSurnamesOfMember()
        {
            string[] surnames = new string[NumberOfMembers];
            for (int i = 0; i < NumberOfMembers; i++)
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
                for (int i = NumberOfMembers - 1; i >= 0; i--)
                    if (group[i] == person)
                        isMember = true;
                if (isMember)
                    throw new NullReferenceException($" {person.Surname} is a member! You can't add {person.Surname} to {NameOfGroup} again!");
                else
                {
                    Person[] oldGroup = new Person[NumberOfMembers];
                    for (int i = 0; i < NumberOfMembers; i++)
                    {
                        oldGroup[i] = group[i];
                    }
                    NumberOfMembers++;
                    group = new Person[NumberOfMembers];
                    for (int i = 0; i < NumberOfMembers - 1; i++)
                    {
                        group[i] = oldGroup[i];
                    }
                    group[NumberOfMembers - 1] = person;
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
                for (int i = NumberOfMembers - 1; i >= 0; i--)
                    if (group[i] == person)
                    {
                        isMember = true;
                        indexOfPerson = i;
                    }
                if (isMember)
                {
                    Person[] oldGroup = new Person[NumberOfMembers];
                    for (int j = 0; j < NumberOfMembers; j++)
                    {
                        oldGroup[j] = group[j];
                    }
                    NumberOfMembers--;
                    if (NumberOfMembers == 1)
                    {
                        group = null;
                        throw new Exception($"Group: {NameOfGroup} is deleted. There are less then 2 person!");
                    }
                    else
                    {
                        group = new Person[NumberOfMembers];
                        for (int j = 0; j < NumberOfMembers + 1; j++)
                        {
                            if (indexOfPerson > j)
                                group[j] = oldGroup[j];
                            if (indexOfPerson < j)
                                group[j - 1] = oldGroup[j];
                        }
                    }
                }
                else
                    throw new NullReferenceException($" {person.Surname} isn't a member! You can't remove {person.Surname} from {NameOfGroup}!");
            }
            else
                throw new NullReferenceException("You can't create a group of people from nothing!");
            if (NumberOfMembers == 1)
            {
                group = null;
                throw new NullReferenceException($"Group: {NameOfGroup} is deleted. There are less then 2 person!");
            }
        }
    }
}