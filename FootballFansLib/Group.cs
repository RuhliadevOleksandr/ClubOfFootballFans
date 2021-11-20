using System;
using System.Collections.Generic;

namespace FootballFansLib
{
    public class Group<Member> where Member : Person
    {
        public int NumberOfMembers { get { return group.Count; } }
        public string NameOfGroup { get; private set; }
        public Member this[int index]
        {
            get
            {
                if(index >= 0 && index < NumberOfMembers)
                    return group[index];
                else
                    throw new ArgumentOutOfRangeException("\nIndex must be more than or equal 0 and less than number of members!");
            }
        }
        private List<Member> group;
        public Group(List<Member> people, string nameOfGroup)
        {
            if (people != null)
            {
                group = new List<Member>(people);
                try {AddNameOfGroup(nameOfGroup); }
	            catch (ArgumentException exception) { throw new ArgumentException(exception.Message); }
            }
            else
                throw new NullReferenceException("\nYou can't create a group of people from nothing!");
        }
        private void AddNameOfGroup(string nameOfGroup)
        {
                if (!string.IsNullOrEmpty(nameOfGroup))
                {
                    bool isCorrectName = true;
                    foreach(char symbol in nameOfGroup)
                            if (!Char.IsLetter(symbol) && !Char.IsSeparator(symbol))
                                isCorrectName = false;
                    if (isCorrectName)
                        NameOfGroup = nameOfGroup;
                    else
                        throw new ArgumentException("\nName of group must have only letters and separators!");
                }
                else
                    throw new ArgumentException("\nGroup must have name! And name of group can't be empty!");
        }
        public void AddMember(Member personToAdd)
        {
            if (group != null)
            {
                bool isMember = false;
                foreach(Person member in group)
                    if (member == personToAdd)
                        isMember = true;
                if (isMember)
                    throw new ArgumentException($"{personToAdd.Surname} is already a member! You can't add {personToAdd.Surname} to {NameOfGroup} again!");
                else
                    group.Add(personToAdd);
            }
            else
                throw new NullReferenceException($"You can't add {personToAdd.Surname} to a {NameOfGroup??"group"}! {NameOfGroup??"Group"} is empty!");
        }
        public void RemoveMember(Member personToRemove)
        {
            if (group != null)
            {
                bool wasMember = group.Remove(personToRemove);
                if(!wasMember)
                    throw new ArgumentException($" {personToRemove.Surname} isn't a member! You can't remove {personToRemove.Surname} from {NameOfGroup}!");
                if (NumberOfMembers <= 1)
                {
                    group = null;
                    throw new NullReferenceException($"Group: {NameOfGroup} is deleted. There are less then 2 person!");
                }
            }
            else
                throw new NullReferenceException($"You can't remove {personToRemove.Surname} from a {NameOfGroup??"group"}! {NameOfGroup??"Group"} is empty!");
        }
    }
}