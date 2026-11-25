using System;
using System.ComponentModel;

namespace AppliedProgramming
{
    public enum Role
    {
        Client,
        Admin,
        Volunteer,
        Vet
    }

    public class User
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }

    public class Pet : INotifyPropertyChanged
    {
        private bool isAdopted;

        public int Number { get; set; }
        public string Name { get; set; }
        public string Kind { get; set; }
        public string Gender { get; set; }
        public string Breed { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public double Age { get; set; }
        public double Weight { get; set; }
        public string Vaccines { get; set; }
        public string PhotoPath { get; set; }

        public bool IsAdopted
        {
            get { return isAdopted; }
            set
            {
                isAdopted = value;
                Raise(nameof(IsAdopted));
                Raise(nameof(Status));
            }
        }

        public string Status
        {
            get { return IsAdopted ? "Передан в семью" : "В приюте"; }
        }

        public string Display
        {
            get { return "№" + Number + " " + Name + " (" + Kind + ")"; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Raise(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    // Заявка клиента на сдачу животного в приют / взятие в семью
    public class PetRequest
    {
        public string ClientName { get; set; }
        public string Phone { get; set; }
        public string RequestType { get; set; }
        public string PetInfo { get; set; }
        public DateTime Date { get; set; }
    }

    // Медицинская манипуляция
    public class VetRecord
    {
        public DateTime Date { get; set; }
        public string Procedure { get; set; }
        public int PetNumber { get; set; }
        public string PetName { get; set; }
        public string Notes { get; set; }
    }

    // Выдача питомца в семью
    public class Adoption
    {
        public int PetNumber { get; set; }
        public string PetName { get; set; }
        public string OwnerName { get; set; }
        public string OwnerPhone { get; set; }
        public DateTime Date { get; set; }
    }
}
