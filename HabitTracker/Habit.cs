using System;

namespace HabitTracker
{
	public class Habit
	{
		public string Name { get; set; }
		public int Streak { get; set; }
		public bool IsCompletedToday { get; set; }

		public void MarkCompletedToday()
		{
			if (IsCompletedToday)
			{
				Console.WriteLine("Эта привычка уже отмечена выполненной сегодня.");
				return;
			}

			IsCompletedToday = true;
			Streak++;
			Console.WriteLine("Отлично! Текущая серия для «" + Name + "»: " + Streak + " дн.");
		}

		public string ToCsvLine()
		{
			return Name + ";" + Streak + ";" + IsCompletedToday;
		}

		public static bool TryFromCsvLine(string line, out Habit habit)
		{
			habit = null;
			if (string.IsNullOrWhiteSpace(line))
				return false;

			string[] parts = line.Split(';');
			if (parts.Length != 3)
				return false;

			int streak;
			if (!int.TryParse(parts[1].Trim(), out streak) || streak < 0)
				return false;

			bool isCompleted;
			if (!bool.TryParse(parts[2].Trim(), out isCompleted))
				return false;

			string cleanName = parts[0].Trim();
			if (string.IsNullOrEmpty(cleanName))
				return false;

			habit = new Habit
			{
				Name = cleanName,
				Streak = streak,
				IsCompletedToday = isCompleted
			};

			return true;
		}
	}
}
