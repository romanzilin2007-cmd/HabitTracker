using System;
using System.Collections.Generic;

namespace HabitTracker
{
	internal class Program
	{
		static void Main(string[] args)
		{
			const string fileName = "habits.txt";
			HabitManager manager = new HabitManager(fileName);
			List<Habit> habits = manager.LoadHabits();

			while (true)
			{
				Console.WriteLine();
				Console.WriteLine("=== ТРЕКЕР ПРИВЫЧЕК ===");
				Console.WriteLine("1. Показать привычки");
				Console.WriteLine("2. Добавить привычку");
				Console.WriteLine("3. Отметить выполнение за сегодня");
				Console.WriteLine("4. Сохранить и выйти");
				Console.Write("Выберите пункт: ");

				string choice = Console.ReadLine();
				if (choice != null)
					choice = choice.Trim();

				switch (choice)
				{
					case "1":
						ShowHabits(habits);
						break;

					case "2":
						Console.Write("Введите название привычки: ");
						string rawName = Console.ReadLine() ?? string.Empty;
						string cleanName = rawName.Replace(";", " - ").Trim();

						if (string.IsNullOrWhiteSpace(cleanName))
						{
							Console.WriteLine("Название не может быть пустым.");
							break;
						}

						habits.Add(new Habit
						{
							Name = cleanName,
							Streak = 0,
							IsCompletedToday = false
						});
						Console.WriteLine("Привычка «" + cleanName + "» добавлена.");
						break;

					case "3":
						if (habits.Count == 0)
						{
							Console.WriteLine("Список привычек пуст.");
							break;
						}

						ShowHabits(habits);
						Console.Write("Введите номер привычки: ");
						int index;
						if (int.TryParse(Console.ReadLine(), out index) && index >= 1 && index <= habits.Count)
						{
							habits[index - 1].MarkCompletedToday();
						}
						else
						{
							Console.WriteLine("Некорректный номер.");
						}
						break;

					case "4":
						manager.SaveHabits(habits);
						Console.WriteLine("Данные сохранены в " + fileName + ".");
						return;

					default:
						Console.WriteLine("Введите число от 1 до 4.");
						break;
				}
			}
		}

		static void ShowHabits(List<Habit> list)
		{
			if (list.Count == 0)
			{
				Console.WriteLine("Привычек пока нет.");
				return;
			}

			Console.WriteLine("\nСписок привычек:");
			for (int i = 0; i < list.Count; i++)
			{
				string status = list[i].IsCompletedToday ? "[Выполнено]" : "[Не выполнено]";
				Console.WriteLine((i + 1) + ". " + list[i].Name + " — серия: " + list[i].Streak + " дн. " + status);
			}
		}
	}
}
