using System;
using System.Collections.Generic;

namespace Zoo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Zoo zoo = new Zoo();

            zoo.ConductExcursion();
        }
    }

    class Zoo
    {
        private List<Enclosure> _enclosures = new List<Enclosure>();
        private EnclosureCreator _enclosureCreator = new EnclosureCreator();

        public Zoo()
        {
            CreateEnclosures();
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

        private void CreateEnclosures()
        {
            _enclosures.Add(_enclosureCreator.CreateCatsEnclosure());
            _enclosures.Add(_enclosureCreator.CreateDogsEnclosure());
            _enclosures.Add(_enclosureCreator.CreateBirdsEnclosure());
            _enclosures.Add(_enclosureCreator.CreateFarmAnimalsEnclosure());
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

    class EnclosureCreator
    {
        private AnimalCreator _animalCreator = new AnimalCreator();

        public Enclosure CreateCatsEnclosure()
        {
            Enclosure catsEnclosure = new Enclosure("Это загон для зверей семейства кошачьих.");

            catsEnclosure.AddAnimal(_animalCreator.CreateLinx());
            catsEnclosure.AddAnimal(_animalCreator.CreateLion());
            catsEnclosure.AddAnimal(_animalCreator.CreateTiger());

            return catsEnclosure;
        }
        public Enclosure CreateDogsEnclosure()
        {
            Enclosure dogsEnclosure = new Enclosure("Это загон для зверей семейства собачьих.");

            dogsEnclosure.AddAnimal(_animalCreator.CreateHyena());
            dogsEnclosure.AddAnimal(_animalCreator.CreateWolf());
            dogsEnclosure.AddAnimal(_animalCreator.CreateDingoDog());

            return dogsEnclosure;
        }
        public Enclosure CreateBirdsEnclosure()
        {
            Enclosure birdsEnclosure = new Enclosure("Это загон для зверей семейства птичьих.");

            birdsEnclosure.AddAnimal(_animalCreator.CreateParrot());
            birdsEnclosure.AddAnimal(_animalCreator.CreateEagle());
            birdsEnclosure.AddAnimal(_animalCreator.CreateOwl());

            return birdsEnclosure;
        }

        public Enclosure CreateFarmAnimalsEnclosure()
        {
            Enclosure farmAnimalsEnclosure = new Enclosure("Это загон для сельскохозяйственных зверей.");

            farmAnimalsEnclosure.AddAnimal(_animalCreator.CreateHorse());
            farmAnimalsEnclosure.AddAnimal(_animalCreator.CreateCow());
            farmAnimalsEnclosure.AddAnimal(_animalCreator.CreateSheep());

            return farmAnimalsEnclosure;
        }
    }

    class AnimalCreator
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
