using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleExplorer
{
    public class Explorer
    {
        private int _position;
        private DirectoryInfo _currentDir;
        private List<FileInfo> _items;

        public Explorer(string path)
        {
            var startDir = new DirectoryInfo(path);
            OpenDirectory(startDir);
            Print();
        }

        public void StepInto()
        {
            if (_position == 0)
            {
                StepBack();
                return;
            }
            var item = _items[_position - 1];
            if ((item.Attributes == FileAttributes.Directory))
            {
                var dir = new DirectoryInfo(item.FullName);
                OpenDirectory(dir);
            }
            Print();
        }

        public void StepBack()
        {
            var dir = _currentDir.Parent;
            if (dir != null)
            {
                OpenDirectory(dir);
                Print();
            }
        }

        public void NavigateDown()
        {
            if (_position < _items.Count)
            {
                _position++;
                Print();
            }
        }

        public void NavigateUp()
        {
            if (_position > 0)
            {
                _position--;
                Print();
            }
        }

        private List<FileInfo> GetDirectoryItems(string path)
        {
            var files = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly);
            var dirs = Directory.GetDirectories(path, "*.*", SearchOption.TopDirectoryOnly);
            var list = dirs.Select(item2 => new FileInfo(item2)).ToList();
            list.AddRange(files.Select(item => new FileInfo(item)));

            return list;
        }
        private void OpenDirectory(DirectoryInfo dir)
        {
            _currentDir = dir;
            _items = GetDirectoryItems(dir.FullName);
            _position = 0;
        }

        private void Print()
        {
            Console.Clear();
            Console.WriteLine("Current directory: {0}", _currentDir.Name);
            Console.WriteLine("{0} {1}", _position == 0 ? ">" : " ", "..");
            for (int i = 0; i < _items.Count; i++)
            {
                var item = _items[i];
                Console.WriteLine("{0} {1}", _position == i + 1 ? ">" : " ", item.Name);
            }
        }
    }
}
