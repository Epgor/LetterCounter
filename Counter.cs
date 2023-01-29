using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetterCounter
{
    public class Counter
    {
        private string _file;
        private string _name;
        private string _text;
        private Dictionary<char, int> _tempLetters;
        private List<char> _possibleLetters;
        private Dictionary<char, double> _plFrequency;
        private Dictionary<char, double> _textFrequency;
        private int _totalLetters;

        public Counter(string name) 
        {
            _name = name;
            FillPossibleLettersList();
            FillLanguageFrequency();
            PrepareTextFrequencyDictionary();
        }

        private void FillLanguageFrequency()
        {
            _plFrequency = new Dictionary<char, double>()
            {
                { 'a', 8.965 }, { 'ą', 1.021 }, { 'b', 1.482 }, { 'c', 3.988 }, { 'ć', 0.448 }, { 'd', 3.293 }, { 'e', 7.921 },
                { 'ę', 1.131 }, { 'f', 0.312 }, { 'g', 1.377 }, { 'h', 1.072 }, { 'i', 8.286 }, { 'j', 2.343 }, { 'k', 3.411 },
                { 'l', 2.136 }, { 'ł', 1.746 }, { 'm', 2.911 }, { 'n', 5.600 }, { 'ń', 0.185 }, { 'o', 7.590 }, { 'ó', 0.823 },
                { 'p', 3.101 }, { 'q', 0.003 }, { 'r', 4.571 }, { 's', 4.263 }, { 'ś', 0.683 }, { 't', 3.966 }, { 'u', 2.347 },
                { 'v', 0.034 }, { 'w', 4.549 }, { 'x', 0.019 }, { 'y', 3.857 }, { 'z', 5.620 }, { 'ź', 0.061 }, { 'ż', 0.885 },
            };
        }

        private void PrepareTextFrequencyDictionary()
        {
            _textFrequency = new Dictionary<char, double>();
            foreach(var pair in _plFrequency)
            {
                _textFrequency.Add(pair.Key, 0);
            }
        }
        private void ClearDictionary()
        {
            _tempLetters = new Dictionary<char, int>() { };
        }

        private void FillFrequencyDict()
        {
            double frequency = 0;
            foreach (var pair in _tempLetters)
            {
                var tempFrequency = (double)pair.Value / (double)_totalLetters * 100;
                frequency = Math.Round(tempFrequency, 3);
                _textFrequency[pair.Key] = frequency;
            }
        }

        public string Count(bool v)
        {
            ClearDictionary();
            _totalLetters = 0;
            foreach(var letter in _text)
            {
                ProcessLetter(letter);
            }

            FillFrequencyDict();

            var countedLetters = 0;
            foreach(var pair in _tempLetters)
            {
                countedLetters += pair.Value;
            }

            return $"Counted = {countedLetters} // Total = {_totalLetters}";
        }

        public Dictionary<char, double> ReturnPlLanguageFrequerncy()
        {
            return _plFrequency;
        }

        public Dictionary<char, double> ReturnTextFrequerncy()
        {
            return _textFrequency;
        }

        private void FillPossibleLettersList()
        {
            _possibleLetters = new List<char>()
            {
                'a',   'ą',   'b',   'c',   'ć',   'd',   'e',
                'ę',   'f',   'g',   'h',   'i',   'j',   'k',
                'l',   'ł',   'm',   'n',   'ń',   'o',   'ó',
                'p',   'q',   'r',   's',   'ś',   't',   'u',
                'v',   'w',   'x',   'y',   'z',   'ź',   'ż',
            };
        }

        private void ProcessLetter(char letter)
        {
            if (IsInList(letter))
            {
                if (_tempLetters.ContainsKey(letter))
                {
                    _tempLetters[letter] += 1;
                }
                else
                {
                    _tempLetters.Add(letter, 1);
                }
                _totalLetters++;
            }
        }

        private bool IsInList(char letter)
        {
            if(_possibleLetters.Contains(letter))
            { 
                return true; 
            }
            return false;
        }

        internal void SetFile(string file) { _file = file; }

        internal void LoadFile()
        {
            try
            {
                _text = File.ReadAllText(_file);
            }
            catch (Exception e) { Console.WriteLine(e); }          
        }
    }
}
