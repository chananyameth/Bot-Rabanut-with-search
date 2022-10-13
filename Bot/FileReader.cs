using System;
using LumenWorks.Framework.IO.Csv;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data;
using System.IO;

namespace Bot
{
    public class IOMap
	{
        public List<string> keywords;
        public string output;
	}
	public class FileReader
	{
        public List<IOMap> rows;
		private int Threshold = 250;
		private int VERY_HIGH = 50;
		private int HIGH = 40;
		private int MEDIUM = 10;

		public FileReader()
		{
            Read(); // read file into rows
		}

		private void Read()
		{
            rows = new List<IOMap>();
            var csvTable = new DataTable();
            string path = "data.csv";

            using (var csvReader = new CsvReader(new StreamReader(File.OpenRead(path)), true))
            {
                csvTable.Load(csvReader);
            }

            for (int i = 0; i < csvTable.Rows.Count; i++)
            {
                rows.Add(new IOMap {
                    keywords = csvTable.Rows[i][0].ToString().Split(", ").ToList(),
                    output = csvTable.Rows[i][1].ToString()
                });
            }
			for (int i = 0; i < rows.Count; i++)
			{
			    Console.WriteLine(rows[i]);
			}
        }

        public string SimpleSearch(string input)
		{
			foreach (var row in rows)
			{
				foreach (var kw in row.keywords)
				{
					if (kw.Contains(input))
					{
						return row.output;
					}
				}
			}

			return "Could not find any matching answer.\nTry different keywords, or use the menu.";
		}

		private int ScoreWord(IOMap row, string word)
		{
			int score = 0;
			int length = word.Length;
			foreach (var kw in row.keywords)
			{
				if (kw == word)
					score += VERY_HIGH * length;
				else if (kw.Contains(word))
					score += HIGH * length;
			}
			if(row.output.Contains(word))
				score += MEDIUM * length;

			return score;
		}
		public string Search_v1(string input)
		{        
			List<int> matching_score = new List<int>();

			for (int i = 0 ; i < rows.Count; i++)
			{
				IOMap row = rows[i];
				matching_score.Add(0);
				foreach (var word in input.Split(" "))
				{
					matching_score[i] += ScoreWord(row, word);
				}
			}

			int max = matching_score.Max();
			var final_outputs = Enumerable.Range(0, matching_score.Count)
								.Where(i => matching_score[i] == max)
								.ToList();
			if (max < Threshold)
				return "Could not find any matching answer.\nTry different keywords, or use the menu.";
			else 
				return string.Join("\n--------------\n", final_outputs.Select(i => rows[i].output));
		}
	}
}
