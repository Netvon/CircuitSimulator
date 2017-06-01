using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;

namespace CircuitSimulator.Core
{
	public class CircuitParser
	{
		/// <summary>
		/// Asynchronously reads a textfile and parses the content to a Dictionaty
		/// </summary>
		/// <param name="path">The path of the file</param>
		/// <returns></returns>
		public async Task<Dictionary<string, string>> LoadFile(string path)
		{
			if (!File.Exists(path))
				throw new FileNotFoundException("Input file not found", path);

			var lineBuffer = new Dictionary<string, string>();

			var filestream = File.OpenRead(path);
			using (var reader = new StreamReader(filestream))
			{
				var lines = await reader.ReadToEndAsync();

				return ParseToDictionary(lines);
			}
		}

		public IEnumerable<(string name, string value)> Parse(string lines)
		{
			var linesArr = lines.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
			var test = linesArr.TakeWhile(x => string.IsNullOrWhiteSpace(x)).ToArray();

			var sectionA = ParseSection(test);

			return sectionA;

			IEnumerable<(string name, string value)>ParseSection(string[] sectionLines)
			{
				foreach (var line in sectionLines)
				{
					if (line.StartsWith("#"))
						continue;

					if (string.IsNullOrWhiteSpace(line))
						break;

					var lineOut = ParseLine(line);

					if (lineOut.HasValue)
					{
						yield return (lineOut?.name, lineOut?.value);
					}
					else
					{
						int index = Array.IndexOf(linesArr, line) + 1;

						throw new CircuitParserException($"Circuit could not be Parsed. Error at line {index}")
						{
							Line = index
						};
					}
				}
			}
		}

		public Dictionary<string, string> ParseToDictionary(string lines)
			=> Parse(lines).ToDictionary(v => v.name, v => v.value);

		public (string name, string value)? ParseLine(string line)
		{
			var match = Regex.Match(line, @"([\w]+(?=\s*:))\s*:\s*([\w]+(?=\s*;))");

			if(match.Success && match.Groups.Count == 3)
			{
				return (name: match.Groups[1].Value, value: match.Groups[2].Value);
			}

			return null;
		}
	}
}
