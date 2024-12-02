using System;
using System.Collections.Generic;

namespace Zoo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AnimalFactory animalFactory = new AnimalFactory();
            EnclosureFactory enclosureFactory = new EnclosureFactory(animalFactory);
            ZooFactory zooFactory = new ZooFactory(enclosureFactory);
            Zoo zoo = zooFactory.Create();

            zoo.ConductExcursion();
        }
    }

    class ZooFactory
    {
        private EnclosureFactory _enclosureFactory;

        public ZooFactory(EnclosureFactory enclosureFactory)
        {
            _enclosureFactory = enclosureFactory;
        }

        private List<Enclosure> CreateEnclosures()
        {
            List<Enclosure> enclosures = new List<Enclosure>();

            enclosures.Add(_enclosureFactory.CreateCatsEnclosure());
            enclosures.Add(_enclosureFactory.CreateDogsEnclosure());
            enclosures.Add(_enclosureFactory.CreateBirdsEnclosure());
            enclosures.Add(_enclosureFactory.CreateFarmAnimalsEnclosure());

            return enclosures;
        }

        public Zoo Create()
        {
            return new Zoo(CreateEnclosures());
        }
    }

    class Zoo
    {
        private List<Enclosure> _enclosures;
        
        public Zoo(List<Enclosure> enclosures)
        {
            _enclosures = enclosures;
        }

        public void ConductExcursion()
        {
            bool isWorking = true;
            string indent = "\n";
            ConsoleKey exitButton = ConsoleKey.Escape;
            ConsoleKey nextPressedKey;

            Console.WriteLine("Здравствуйте, добро пожаловать в наш зоопарк." + indent);

            while (isWorking)
            {
                int userInput;

                foreach (Enclosure enclosure in _enclosures)
                {
                    Console.WriteLine(enclosure.Description);
                }

                Console.WriteLine($"{indent}В нашем зоопарке есть {_enclosures.Count} вольера. Введите номер вольера, который желаете посмотреть.");

                do
                {
                    Console.Write("Ваш выбор: ");
                }
                while (int.TryParse(Console.ReadLine(), out userInput) == false || userInput <= 0 || userInput > _enclosures.Count);

                Console.Clear();
                _enclosures[userInput - 1].Show();

                Console.WriteLine(indent + "Нажмите любую клавишу для продолжения. Для выхода нажмите " + exitButton);

                nextPressedKey = Console.ReadKey().Key;

                if (nextPressedKey == exitButton)
                {
                    isWorking = false;
                }

                Console.Clear();
            }

            Console.WriteLine("До свидания, надеюсь вам понравилось в нашем зоопарке.");
        }
    }

    class Enclosure
    {
        private List<Animal> _animals = new List<Animal>();

        public Enclosure(string description)
        {
            Description = description;
        }

        public string Description { get; private set; }

        public void AddAnimal(Animal animal)
        {
            _animals.Add(animal);
        }

        public void Show()
        {
            Console.WriteLine(Description);
            Console.WriteLine($"Здесь живут и не тужат {_animals.Count} зверей.\n");

            foreach (Animal animal in _animals)
            {
                Console.WriteLine($"Такой вот звИрь - {animal.Name}. Его речь звучит, как - {animal.Sound}. Его пол - {animal.Gender}");
            }
        }
    }

    class EnclosureFactory
    {
        private AnimalFactory _animalFactory;

        public EnclosureFactory(AnimalFactory animalFactory)
        {
            _animalFactory = animalFactory;
        }

        public Enclosure CreateCatsEnclosure()
        {
            Enclosure catsEnclosure = new Enclosure("Это загон для зверей семейства кошачьих.");

            catsEnclosure.AddAnimal(_animalFactory.CreateLinx());
            catsEnclosure.AddAnimal(_animalFactory.CreateLion());
            catsEnclosure.AddAnimal(_animalFactory.CreateTiger());

            return catsEnclosure;
        }

        public Enclosure CreateDogsEnclosure()
        {
            Enclosure dogsEnclosure = new Enclosure("Это загон для зверей семейства собачьих.");

            dogsEnclosure.AddAnimal(_animalFactory.CreateHyena());
            dogsEnclosure.AddAnimal(_animalFactory.CreateWolf());
            dogsEnclosure.AddAnimal(_animalFactory.CreateDingoDog());

            return dogsEnclosure;
        }

        public Enclosure CreateBirdsEnclosure()
        {
            Enclosure birdsEnclosure = new Enclosure("Это загон для зверей семейства птичьих.");

            birdsEnclosure.AddAnimal(_animalFactory.CreateParrot());
            birdsEnclosure.AddAnimal(_animalFactory.CreateEagle());
            birdsEnclosure.AddAnimal(_animalFactory.CreateOwl());

            return birdsEnclosure;
        }

        public Enclosure CreateFarmAnimalsEnclosure()
        {
            Enclosure farmAnimalsEnclosure = new Enclosure("Это загон для сельскохозяйственных зверей.");

            farmAnimalsEnclosure.AddAnimal(_animalFactory.CreateHorse());
            farmAnimalsEnclosure.AddAnimal(_animalFactory.CreateCow());
            farmAnimalsEnclosure.AddAnimal(_animalFactory.CreateSheep());

            return farmAnimalsEnclosure;
        }
    }

    class AnimalFactory
    {
        public Animal CreateLinx()
        {
            return new Animal("Рысь", "Мияуауааауаааяяяя", GenerateGender());
        }

        public Animal CreateLion()
        {
            return new Animal("Лев", "РРРррааааауурр", GenerateGender());
        }

        public Animal CreateTiger()
        {
            return new Animal("Тигр", "Ррраааауу", GenerateGender());
        }

        public Animal CreateHyena()
        {
            return new Animal("Гиена", "Йи-хи-хи-хи-хи", GenerateGender());
        }

        public Animal CreateWolf()
        {
            return new Animal("Волк", "Бауууу", GenerateGender());
        }

        public Animal CreateDingoDog()
        {
            return new Animal("Дикая собака динго", "Буаф-бауф", GenerateGender());
        }

        public Animal CreateParrot()
        {
            return new Animal("Попугай", "Фиииить-чик-чик", GenerateGender());
        }

        public Animal CreateEagle()
        {
            return new Animal("Орёл", "Иии-иии", GenerateGender());
        }

        public Animal CreateOwl()
        {
            return new Animal("Сова", "Уу-уу-уу", GenerateGender());
        }

        public Animal CreateHorse()
        {
            return new Animal("Лошадь", "Иии-го-го", GenerateGender());
        }

        public Animal CreateCow()
        {
            return new Animal("Корова", "Муууу-у-у", GenerateGender());
        }

        public Animal CreateSheep()
        {
            return new Animal("Овца", "Меее-е-е", GenerateGender());
        }

        private string GenerateGender()
        {
            List<string> genders = new List<string>() { "Самец", "Самка" };

            return genders[UserUtils.GenerateRandomNumber(0, genders.Count)];
        }
    }

    class Animal
    {
        public Animal(string name, string sound, string gender)
        {
            Name = name;
            Sound = sound;
            Gender = gender;
        }

        public string Name { get; private set; }
        public string Sound { get; private set; }
        public string Gender { get; private set; }
    }

    static class UserUtils
    {
        private static Random s_random = new Random();

        public static int GenerateRandomNumber(int minValue, int maxValue)
        {
            return s_random.Next(minValue, maxValue);
        }
    }
}
