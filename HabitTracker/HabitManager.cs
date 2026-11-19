using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HabitTracker
{
	public class HabitManager
	{
		private readonly string _filePath;

		public HabitManager(string filePath)
		{
			_filePath = filePath;
		}

		public List<Habit> LoadHabits()
		{
			List<Habit> habits = new List<Habit>();

			if (!File.Exists(_filePath))
			{
				Console.WriteLine("Файл " + _filePath + " не найден — начинаем с пустого списка.");
				return habits;
			}

			using (StreamReader reader = new StreamReader(_filePath, Encoding.UTF8))
			{
				string line;
				int lineNumber = 0;

				while ((line = reader.ReadLine()) != null)
				{
					lineNumber++;
					Habit habit;
					if (Habit.TryFromCsvLine(line, out habit))
					{
						habits.Add(habit);
					}
					else if (!string.IsNullOrWhiteSpace(line))
					{
						Console.WriteLine("Пропущена некорректная строка #" + lineNumber + ": " + line);
					}
				}
			}

			return habits;
		}

		public void SaveHabits(IEnumerable<Habit> habits)
		{
			using (StreamWriter writer = new StreamWriter(_filePath, false, Encoding.UTF8))
			{
				foreach (Habit habit in habits)
				{
					writer.WriteLine(habit.ToCsvLine());
				}
			}
		}
	}
}
