using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AppliedProgramming
{
    // Хранилище данных в памяти (списки заменяют базу данных до ПР 14-16)
    public static class Db
    {
        public const string ClientLogin = "Клиент";

        public static readonly string[] Kinds = { "Собака", "Кошка", "Кролик", "Грызун", "Птица", "Другое" };
        public static readonly string[] Sizes = { "Мелкая", "Средняя", "Крупная" };
        public static readonly string[] Genders = { "Самец", "Самка" };

        // Эталонные логины и пароли хранятся в списке
        public static readonly List<User> Users = new List<User>
        {
            new User { FullName = "Иванов Иван Иванович", Phone = "89001112233", Login = "admin", Password = "admin123", Role = Role.Admin },
            new User { FullName = "Петрова Анна Сергеевна", Phone = "89004445566", Login = "volunteer", Password = "vol12345", Role = Role.Volunteer },
            new User { FullName = "Сидоров Олег Петрович", Phone = "89007778899", Login = "doctor", Password = "doc12345", Role = Role.Vet }
        };

        public static readonly ObservableCollection<Pet> Pets = new ObservableCollection<Pet>
        {
            new Pet { Number = 1, Name = "Барсик", Kind = "Кошка", Gender = "Самец", Breed = "Дворовая", Color = "Рыжий", Size = "Средняя", Age = 2, Weight = 4.2, Vaccines = "Комплексная, бешенство" },
            new Pet { Number = 2, Name = "Найда", Kind = "Собака", Gender = "Самка", Breed = "Овчарка", Color = "Чёрно-рыжий", Size = "Крупная", Age = 3, Weight = 28, Vaccines = "Комплексная" },
            new Pet { Number = 3, Name = "Мурка", Kind = "Кошка", Gender = "Самка", Breed = "Британская", Color = "Серый", Size = "Мелкая", Age = 1, Weight = 3.5, Vaccines = "Нет данных" },
            new Pet { Number = 4, Name = "Рекс", Kind = "Собака", Gender = "Самец", Breed = "Лабрадор", Color = "Палевый", Size = "Крупная", Age = 4, Weight = 31, Vaccines = "Комплексная, бешенство" },
            new Pet { Number = 5, Name = "Снежок", Kind = "Кролик", Gender = "Самец", Breed = "Декоративный", Color = "Белый", Size = "Мелкая", Age = 1, Weight = 1.8, Vaccines = "Нет данных" },
            new Pet { Number = 6, Name = "Белка", Kind = "Собака", Gender = "Самка", Breed = "Дворняжка", Color = "Белый с пятнами", Size = "Средняя", Age = 2, Weight = 12, Vaccines = "Бешенство" }
        };

        public static readonly ObservableCollection<PetRequest> Requests = new ObservableCollection<PetRequest>();
        public static readonly ObservableCollection<VetRecord> VetRecords = new ObservableCollection<VetRecord>();
        public static readonly ObservableCollection<Adoption> Adoptions = new ObservableCollection<Adoption>();

        public static User FindUser(string login, string password)
        {
            return Users.FirstOrDefault(u => u.Login == login && u.Password == password);
        }

        public static bool LoginExists(string login)
        {
            return Users.Any(u => u.Login.ToLower() == login.ToLower());
        }

        public static Role RoleOf(string login)
        {
            User u = Users.FirstOrDefault(x => x.Login == login);
            return u == null ? Role.Client : u.Role;
        }

        public static string NameOf(string login)
        {
            User u = Users.FirstOrDefault(x => x.Login == login);
            return u == null ? "уважаемый клиент" : u.FullName;
        }

        public static int NextPetNumber()
        {
            return Pets.Count == 0 ? 1 : Pets.Max(p => p.Number) + 1;
        }
    }

    // Права доступа к функциям системы в зависимости от роли.
    // Используется как DataContext главного окна (триггеры данных, ПР №10)
    public class Access
    {
        private readonly Role role;

        public Access(Role role)
        {
            this.role = role;
        }

        public bool CanViewPets { get { return true; } }
        public bool CanRequest { get { return true; } }
        public bool CanRegisterPet { get { return role == Role.Admin || role == Role.Volunteer; } }
        public bool CanAdopt { get { return role == Role.Admin || role == Role.Volunteer; } }
        public bool CanVet { get { return role == Role.Admin || role == Role.Vet; } }
        public bool CanReport { get { return role == Role.Admin; } }

        public static string RoleTitle(Role role)
        {
            switch (role)
            {
                case Role.Admin: return "Администратор";
                case Role.Volunteer: return "Волонтёр";
                case Role.Vet: return "Ветеринарный врач";
                default: return "Клиент";
            }
        }
    }
}
